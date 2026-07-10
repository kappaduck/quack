// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using Basic.Reference.Assemblies;
using KappaDuck.Quack.SourceGenerator.Events;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace SourceGenerator.Tests.Infrastructure;

internal static class EventGeneratorRunner
{
    private static readonly CSharpParseOptions _options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview);

    public static GeneratorResult Run(string source)
    {
        GeneratorDriver driver = CSharpGeneratorDriver.Create([new EventGenerator().AsSourceGenerator()], parseOptions: _options);
        driver = driver.RunGeneratorsAndUpdateCompilation(CreateCompilation(source), out Compilation output, out _);

        GeneratorDriverRunResult runResult = driver.GetRunResult();

        ImmutableArray<Diagnostic> generatorDiagnostics = [.. runResult.Results.SelectMany(static r => r.Diagnostics)];
        ImmutableArray<Diagnostic> compilationDiagnostics = [.. output.GetDiagnostics().Where(static d => d.Severity == DiagnosticSeverity.Error)];

        Dictionary<string, string> generatedFiles = runResult.Results
            .SelectMany(static r => r.GeneratedSources)
            .ToDictionary(static s => s.HintName, static s => s.SourceText.ToString());

        return new GeneratorResult(generatedFiles, generatorDiagnostics, compilationDiagnostics);
    }

    public static IReadOnlyDictionary<string, IReadOnlyList<IncrementalStepRunReason>> RunAndTrack(string source, params string[] trackingNames)
    {
        CSharpCompilation compilation = CreateCompilation(source);

        GeneratorDriver driver = CSharpGeneratorDriver.Create([new EventGenerator().AsSourceGenerator()], parseOptions: _options, driverOptions: new GeneratorDriverOptions(default, trackIncrementalGeneratorSteps: true));

        driver = driver.RunGenerators(compilation);

        Compilation modified = compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText("// forces a recompute", _options));
        driver = driver.RunGenerators(modified);

        GeneratorRunResult result = driver.GetRunResult().Results[0];

        Dictionary<string, IReadOnlyList<IncrementalStepRunReason>> reasons = [];

        foreach (string name in trackingNames)
        {
            reasons[name] = result.TrackedSteps.TryGetValue(name, out ImmutableArray<IncrementalGeneratorRunStep> steps)
                ? [.. steps.SelectMany(static s => s.Outputs).Select(static o => o.Reason)]
                : [];
        }

        return reasons;
    }

    public static Task Verify(string source)
    {
        GeneratorDriver driver = CSharpGeneratorDriver.Create([new EventGenerator().AsSourceGenerator()], parseOptions: _options);
        return Verifier.Verify(driver.RunGenerators(CreateCompilation(source)));
    }

    private static CSharpCompilation CreateCompilation(string source)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(source, _options);
        SyntaxTree stub = CSharpSyntaxTree.ParseText(EventStub.Source, _options);
        CSharpCompilationOptions options = new(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable);

        return CSharpCompilation.Create("SourceGenerator.Tests", [stub, tree], references: [.. Net100.References.All], options);
    }
}

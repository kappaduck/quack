// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace SourceGenerator.Tests.Infrastructure;

internal sealed record GeneratorResult(IReadOnlyDictionary<string, string> GeneratedFiles, ImmutableArray<Diagnostic> GeneratorDiagnostics, ImmutableArray<Diagnostic> CompilationDiagnostics)
{
    public string EventUnion => GeneratedFiles.TryGetValue("Event.g.cs", out string? value) ? value : string.Empty;

    public string EventTypeMap => GeneratedFiles.TryGetValue("EventMapper.g.cs", out string? value) ? value : string.Empty;

    public IReadOnlyList<string> GeneratorDiagnosticIds => [.. GeneratorDiagnostics.Select(static d => d.Id)];
}

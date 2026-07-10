// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Runtime.CompilerServices;

namespace SourceGenerator.Tests;

internal static class SourceGeneratorSetup
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
        UseSourceFileRelativeDirectory("snapshots");
    }
}

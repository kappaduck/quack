// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;

namespace Integration.Tests;

internal static class QuackSetup
{
    private static EngineScope _scope = default!;

    [Before(Assembly)]
    public static void Initialize(AssemblyHookContext _)
    {
        _scope = QuackEngine.Init(Subsystem.Events);
    }

    [After(Assembly)]
    public static void Cleanup(AssemblyHookContext _)
    {
        _scope.Dispose();
    }
}

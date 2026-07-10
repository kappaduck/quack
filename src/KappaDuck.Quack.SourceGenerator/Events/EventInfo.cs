// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.SourceGenerator.Events;

internal sealed record EventInfo(string Name, string NativeEventType)
{
    internal LocationInfo? Location { get; init; }

    internal string? NativeField { get; init; }

    internal bool ImplementsInterface { get; init; }

    internal bool Valid { get; init; }

    public bool HasConstructor { get; init; }
}

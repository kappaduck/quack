// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.SourceGenerator.Events;

internal sealed record Event(string Name, string NativeEventType, string? Field = null);

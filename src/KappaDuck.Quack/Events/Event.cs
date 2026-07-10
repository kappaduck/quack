// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a event polled from the event queue.
/// </summary>
[Union]
public readonly partial struct Event : IUnion;

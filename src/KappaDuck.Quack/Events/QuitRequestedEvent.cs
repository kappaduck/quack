// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Events;

/// <summary>
/// raised when the user closes the last window or
/// the operating system asks the application to terminate.
/// </summary>
public readonly struct QuitRequestedEvent : IEvent;

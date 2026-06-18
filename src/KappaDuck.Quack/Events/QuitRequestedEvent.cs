// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a request to quit the application, raised when the user closes the
/// last window or the operating system asks the application to terminate.
/// </summary>
public readonly record struct QuitRequestedEvent : IEvent;

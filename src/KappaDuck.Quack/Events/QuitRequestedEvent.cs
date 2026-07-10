// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// raised when the user closes the last window or
/// the operating system asks the application to terminate.
/// </summary>
[QuackEvent(SDL_EventType.Quit)]
public readonly struct QuitRequestedEvent : IEvent;

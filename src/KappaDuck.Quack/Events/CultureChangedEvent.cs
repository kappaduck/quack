// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the user's culture preferences has been changed.
/// </summary>
[QuackEvent(SDL_EventType.LocaleChanged)]
public readonly struct CultureChangedEvent : IEvent;

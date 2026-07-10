// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the user's theme has been changed.
/// </summary>
[QuackEvent(SDL_EventType.SystemThemeChanged)]
public readonly struct ThemeChangedEvent : IEvent;

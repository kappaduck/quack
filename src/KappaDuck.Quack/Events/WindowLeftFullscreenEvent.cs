// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windowing;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a window leaves fullscreen.
/// </summary>
[QuackEvent(SDL_EventType.WindowLeaveFullscreen, NativeField = nameof(SDL_Event.Window))]
public readonly struct WindowLeftFullscreenEvent : IEvent
{
    internal WindowLeftFullscreenEvent(SDL_WindowEvent e) => WindowId = e.WindowId;

    /// <summary>
    /// Gets the id of the window the event is for.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => Windows.FromId(WindowId);
}

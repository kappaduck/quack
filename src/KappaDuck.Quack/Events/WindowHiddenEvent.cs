// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a window is hidden.
/// </summary>
[QuackEvent(SDL_EventType.WindowHidden, NativeField = nameof(SDL_Event.Window))]
public readonly struct WindowHiddenEvent : IEvent
{
    internal WindowHiddenEvent(SDL_WindowEvent e) => WindowId = e.WindowId;

    /// <summary>
    /// Gets the id of the window the event is for.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => WindowManager.FromId(WindowId);
}

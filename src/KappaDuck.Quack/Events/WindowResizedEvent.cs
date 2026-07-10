// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a window is resized.
/// </summary>
[QuackEvent(SDL_EventType.WindowResized, NativeField = nameof(SDL_Event.Window))]
public readonly struct WindowResizedEvent : IEvent
{
    internal WindowResizedEvent(SDL_WindowEvent e)
    {
        WindowId = e.WindowId;
        Size = new Size(e.Data1, e.Data2);
    }

    /// <summary>
    /// Gets the id of the window the event is for.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the new size of the window in screen coordinates.
    /// </summary>
    public Size Size { get; }

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => WindowManager.FromId(WindowId);
}

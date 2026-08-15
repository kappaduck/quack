// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windowing;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a window is moved.
/// </summary>
[QuackEvent(SDL_EventType.WindowMoved, NativeField = nameof(SDL_Event.Window))]
public readonly struct WindowMovedEvent : IEvent
{
    internal WindowMovedEvent(SDL_WindowEvent e)
    {
        WindowId = e.WindowId;
        Position = new Point(e.Data1, e.Data2);
    }

    /// <summary>
    /// Gets the id of the window the event is for.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the new top-left position of the window in screen coordinates.
    /// </summary>
    public Point Position { get; }

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => Windows.FromId(WindowId);
}

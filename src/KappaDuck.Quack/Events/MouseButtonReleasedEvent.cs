// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Input;
using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windowing;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a mouse button is released.
/// </summary>
[QuackEvent(SDL_EventType.MouseButtonUp, NativeField = nameof(SDL_Event.Button))]
public readonly struct MouseButtonReleasedEvent : IEvent
{
    internal MouseButtonReleasedEvent(SDL_MouseButtonEvent e)
    {
        WindowId = e.WindowId;
        Which = e.Which;
        Button = e.Button;
        Position = new Point(e.X, e.Y);
    }

    /// <summary>
    /// Gets the window id on which the mouse button released.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse id which the mouse button released.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the button that was released.
    /// </summary>
    public MouseButton Button { get; }

    /// <summary>
    /// Gets the cursor position, relative to the top-left of the window, when the button was released.
    /// </summary>
    public Point Position { get; }

    /// <summary>
    /// Gets the mouse device which the button was released.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => Windows.FromId(WindowId);
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;
using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windows;
using System.Drawing;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a mouse button is pressed.
/// </summary>
public readonly struct MouseButtonPressedEvent : IEvent
{
    internal MouseButtonPressedEvent(SDL_MouseButtonEvent e)
    {
        WindowId = e.WindowId;
        Which = e.Which;
        Button = e.Button;
        Position = new PointF(e.X, e.Y);
        Clicks = e.Clicks;
    }

    /// <summary>
    /// Gets the window id on which the mouse button pressed.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse id which the mouse button pressed.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the button that was pressed.
    /// </summary>
    public MouseButton Button { get; }

    /// <summary>
    /// Gets the cursor position, relative to the top-left of the window, when the button was pressed.
    /// </summary>
    public PointF Position { get; }

    /// <summary>
    /// Gets the consecutive click count: 1 for a single click, 2 for a double click, and so on.
    /// </summary>
    public int Clicks { get; }

    /// <summary>
    /// Gets the mouse device which the button was pressed.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => WindowManager.FromId(WindowId);
}

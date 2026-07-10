// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;
using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windows;
using System.Drawing;
using System.Numerics;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the mouse moves.
/// </summary>
[QuackEvent(SDL_EventType.MouseMotion, NativeField = nameof(SDL_Event.Motion))]
public readonly struct MouseMovedEvent : IEvent
{
    internal MouseMovedEvent(SDL_MouseMotionEvent e)
    {
        WindowId = e.WindowId;
        Which = e.Which;
        Position = new PointF(e.X, e.Y);
        Delta = new Vector2(e.Xrel, e.Yrel);
        Buttons = e.State;
    }

    /// <summary>
    /// Gets the window id on which the mouse moved.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse id which moved.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the cursor position, relative to the top-left of the window.
    /// </summary>
    public PointF Position { get; }

    /// <summary>
    /// Gets the motion since the previous event.
    /// </summary>
    public Vector2 Delta { get; }

    /// <summary>
    /// Gets the buttons held while the mouse moved
    /// </summary>
    public MouseButtonState Buttons { get; }

    /// <summary>
    /// Gets the mouse device which moved.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => WindowManager.FromId(WindowId);
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windows;
using System.Drawing;
using System.Numerics;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the mouse wheel is scrolled.
/// </summary>
[QuackEvent(SDL_EventType.MouseWheel, NativeField = nameof(SDL_Event.Wheel))]
public readonly struct MouseWheelEvent : IEvent
{
    internal MouseWheelEvent(SDL_MouseWheelEvent e)
    {
        float sign = e.Direction is SDL_MouseWheelDirection.Flipped ? -1f : 1f;

        WindowId = e.WindowId;
        Which = e.Which;
        Delta = new Vector2(e.X * sign, e.Y * sign);
        Position = new PointF(e.MouseX, e.MouseY);
    }

    /// <summary>
    /// Gets the window id on which the mouse scrolled.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse id which scrolled.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the scroll amount. Positive <c>Y</c> scrolls away from the user and positive <c>X</c> to the
    /// right, regardless of the platform's natural-scrolling setting.
    /// </summary>
    public Vector2 Delta { get; }

    /// <summary>
    /// Gets the cursor position, relative to the top-left of the window, when the wheel was scrolled.
    /// </summary>
    public PointF Position { get; }

    /// <summary>
    /// Gets the mouse device which scrolled.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => WindowManager.FromId(WindowId);
}

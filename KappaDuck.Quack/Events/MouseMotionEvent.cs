// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Input.Mouse;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a mouse motion event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseMotionEvent
{
    /// <summary>
    /// Gets the mouse motion event type.
    /// </summary>
    /// <remarks>
    /// The event is <see cref="EventType.MouseMotion"/>
    /// </remarks>
    public EventType Type { get; }

    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the window id which has the mouse focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// gets the mouse id.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets The state of the mouse buttons.
    /// </summary>
    public MouseButtonState State { get; }

    private readonly float _x;
    private readonly float _y;
    private readonly float _xRel;
    private readonly float _yRel;

    /// <summary>
    /// Gets the mouse device which generated this event.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);

    /// <summary>
    /// Gets the position of the mouse, relative to window.
    /// </summary>
    public Vector2 Position => new(_x, _y);

    /// <summary>
    /// Gets the relative position of the mouse.
    /// </summary>
    public Vector2 RelativePosition => new(_xRel, _yRel);
}

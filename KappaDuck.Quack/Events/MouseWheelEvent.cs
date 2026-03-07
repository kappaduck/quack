// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Input.Mouse;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a mouse wheel event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseWheelEvent
{
    /// <summary>
    /// Gets the mouse wheel event type.
    /// </summary>
    /// <remarks>
    /// The event is <see cref="EventType.MouseWheel"/>
    /// </remarks>
    public EventType Type { get; }

    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the window id which has the mouse focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse id.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the horizontal scroll amount.
    /// </summary>
    /// <remarks>
    /// Positive to the right, negative to the left.
    /// </remarks>
    public float X { get; }

    /// <summary>
    /// Gets the vertical scroll amount.
    /// </summary>
    /// <remarks>
    /// Positive away from the user, negative towards the user.
    /// </remarks>
    public float Y { get; }

    /// <summary>
    /// Gets the direction of the scroll.
    /// </summary>
    /// <remarks>
    /// When <see cref="WheelDirection.Flipped"/> the values in X and Y will be opposite.
    /// Multiply by -1 to change them back.
    /// </remarks>
    public WheelDirection Direction { get; }

    private readonly float _mouseX;
    private readonly float _mouseY;

    /// <summary>
    /// Gets the mouse device which generated this event.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);

    /// <summary>
    /// Gets the position of the mouse, relative to the window which has the mouse focus.
    /// </summary>
    public Vector2 Position => new(_mouseX, _mouseY);
}

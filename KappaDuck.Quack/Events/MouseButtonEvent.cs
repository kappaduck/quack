// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Input.Mouse;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a mouse button event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseButtonEvent
{
    /// <summary>
    /// Gets the mouse button event type.
    /// </summary>
    /// <remarks>
    /// The event is one of the following types:
    /// <list type="bullet">
    /// <item><see cref="EventType.MouseButtonDown"/></item>
    /// <item><see cref="EventType.MouseButtonUp"/></item>
    /// </list>
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
    /// Gets the mouse button.
    /// </summary>
    public MouseButton Button { get; }

    private readonly byte _down;

    /// <summary>
    /// Gets the number of clicks. 1 for single-click, 2 for double-click, etc.
    /// </summary>
    public byte Clicks { get; }

    private readonly byte _padding;
    private readonly float _x;
    private readonly float _y;

    /// <summary>
    /// Gets the mouse device which generated this event.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);

    /// <summary>
    /// Gets a value indicating whether the button is pressed.
    /// </summary>
    public bool Down => _down != 0;

    /// <summary>
    /// Gets the position of the mouse, relative to window.
    /// </summary>
    public Vector2 Position => new(_x, _y);
}

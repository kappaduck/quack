// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Inputs;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a mouse wheel event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseWheelEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// The window with mouse focus.
    /// </summary>
    public readonly uint WindowId;

    /// <summary>
    /// The mouse instance id in relative mode or 0.
    /// </summary>
    public readonly uint Which;

    /// <summary>
    /// The horizontal scroll amount.
    /// </summary>
    /// <remarks>
    /// Positive to the right, negative to the left.
    /// </remarks>
    public readonly float X;

    /// <summary>
    /// The vertical scroll amount.
    /// </summary>
    /// <remarks>
    /// Positive away from the user, negative towards the user.
    /// </remarks>
    public readonly float Y;

    /// <summary>
    /// The direction of the scroll.
    /// </summary>
    /// <remarks>
    /// When <see cref="Mouse.WheelDirection.Flipped"/> the values in X and Y will be opposite.
    /// Multiply by -1 to change them back.
    /// </remarks>
    public readonly Mouse.WheelDirection Direction;

    private readonly float _mouseX;
    private readonly float _mouseY;

    /// <summary>
    /// Gets the position of the mouse, relative to window.
    /// </summary>
    public readonly Vector2 MousePosition => new(_mouseX, _mouseY);
}

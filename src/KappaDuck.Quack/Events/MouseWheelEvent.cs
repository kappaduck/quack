// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Inputs;
using System.Diagnostics;

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
    /// Gets the window unique identifier which has the mouse focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse unique identifier.
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
    /// When <see cref="Mouse.WheelDirection.Flipped"/> the values in X and Y will be opposite.
    /// Multiply by -1 to change them back.
    /// </remarks>
    public Mouse.WheelDirection Direction { get; }

    private readonly float _mouseX;
    private readonly float _mouseY;

    /// <summary>
    /// Gets the mouse device associated with <see cref="EventType.MouseWheel"/>.
    /// </summary>
    public Mouse Mouse
    {
        get
        {
            Debug.Assert(_type is EventType.MouseWheel);
            return Mouse.Get(Which);
        }
    }

    /// <summary>
    /// Gets the position of the mouse, relative to window.
    /// </summary>
    public Vector2 MousePosition => new(_mouseX, _mouseY);
}

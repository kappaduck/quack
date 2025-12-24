// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Inputs;
using System.Diagnostics;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a mouse button event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseButtonEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the window id with mouse focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the mouse id.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the mouse button.
    /// </summary>
    public Mouse.Button Button { get; }

    private readonly byte _down;

    /// <summary>
    /// Gets the number of clicks. 1 for single-click, 2 for double-click, etc.
    /// </summary>
    public byte Clicks { get; }

    private readonly byte _padding;
    private readonly float _x;
    private readonly float _y;

    /// <summary>
    /// Gets the mouse device associated with <see cref="EventType.MouseButtonDown"/> or <see cref="EventType.MouseButtonUp"/>.
    /// </summary>
    public Mouse Mouse
    {
        get
        {
            Debug.Assert(_type is EventType.MouseButtonDown or EventType.MouseButtonUp);
            return Mouse.Get(Which);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the button is pressed.
    /// </summary>
    public bool Down => _down != 0;

    /// <summary>
    /// Gets the position of the mouse, relative to window.
    /// </summary>
    public Vector2 Position => new(_x, _y);
}

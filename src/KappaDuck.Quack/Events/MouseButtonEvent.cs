// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Inputs;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a mouse button event.
/// </summary>
[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseButtonEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// The window with mouse focus.
    /// </summary>
    public readonly uint WindowId;

    /// <summary>
    /// The mouse instance id in relative mode.
    /// </summary>
    public readonly uint Which;

    /// <summary>
    /// The mouse button index.
    /// </summary>
    public readonly Mouse.Button Button;

    private readonly byte _down;

    /// <summary>
    /// 1 for single-click, 2 for double-click, etc.
    /// </summary>
    public readonly byte Clicks;

    private readonly byte _padding;

    private readonly float _x;

    private readonly float _y;

    /// <summary>
    /// Gets a value indicating whether the button is pressed.
    /// </summary>
    public readonly bool Down => _down != 0;

    /// <summary>
    /// Gets the position of the mouse, relative to window.
    /// </summary>
    public readonly Vector2 Position => new(_x, _y);
}

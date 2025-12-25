// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Video.Displays;
using System.Diagnostics;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a window event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct WindowEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the associated window id.
    /// </summary>
    public uint Id { get; }

    private readonly int _data1;
    private readonly int _data2;

    /// <summary>
    /// Gets the display associated with the current window from <see cref="EventType.WindowDisplayChanged"/>.
    /// </summary>
    public Display CurrentDisplay
    {
        get
        {
            Debug.Assert(_type == EventType.WindowDisplayChanged);
            return Display.GetDisplay((uint)_data1);
        }
    }

    /// <summary>
    /// Gets a value indicating whether high dynamic range (HDR) is currently enabled or not from <see cref="EventType.HdrStateChanged"/>.
    /// </summary>
    public bool IsHdrEnabled
    {
        get
        {
            Debug.Assert(_type == EventType.HdrStateChanged);
            return _data1 != 0;
        }
    }

    /// <summary>
    /// Gets the new position of the window from <see cref="EventType.WindowMoved"/>.
    /// </summary>
    public Vector2Int Position
    {
        get
        {
            Debug.Assert(_type == EventType.WindowMoved);
            return new(_data1, _data2);
        }
    }

    /// <summary>
    /// Gets the resized window in pixel size from <see cref="EventType.WindowPixelSizeChanged"/>.
    /// </summary>
    public SizeInt SizeInPixels
    {
        get
        {
            Debug.Assert(_type == EventType.WindowPixelSizeChanged);
            return new SizeInt(_data1, _data2);
        }
    }

    /// <summary>
    /// Gets the resized window from <see cref="EventType.WindowResized"/>.
    /// </summary>
    public SizeInt Size
    {
        get
        {
            Debug.Assert(_type == EventType.WindowResized);
            return new SizeInt(_data1, _data2);
        }
    }
}

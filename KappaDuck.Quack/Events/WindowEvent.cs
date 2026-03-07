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
    /// <summary>
    /// Gets the window event type.
    /// </summary>
    /// <remarks>
    /// The event is one of the following types:
    /// <list type="bullet">
    /// <item><see cref="EventType.EnterFullScreen"/></item>
    /// <item><see cref="EventType.FocusGained"/></item>
    /// <item><see cref="EventType.FocusLost"/></item>
    /// <item><see cref="EventType.HdrStateChanged"/></item>
    /// <item><see cref="EventType.IccProfileChanged"/></item>
    /// <item><see cref="EventType.LeaveFullScreen"/></item>
    /// <item><see cref="EventType.MouseEnter"/></item>
    /// <item><see cref="EventType.MouseLeave"/></item>
    /// <item><see cref="EventType.WindowCloseRequested"/></item>
    /// <item><see cref="EventType.WindowDestroyed"/></item>
    /// <item><see cref="EventType.WindowDisplayChanged"/></item>
    /// <item><see cref="EventType.WindowDisplayScaleChanged"/></item>
    /// <item><see cref="EventType.WindowExposed"/></item>
    /// <item><see cref="EventType.WindowHidden"/></item>
    /// <item><see cref="EventType.WindowHitTest"/></item>
    /// <item><see cref="EventType.WindowMaximized"/></item>
    /// <item><see cref="EventType.WindowMinimized"/></item>
    /// <item><see cref="EventType.WindowMoved"/></item>
    /// <item><see cref="EventType.WindowOccluded"/></item>
    /// <item><see cref="EventType.WindowPixelSizeChanged"/></item>
    /// <item><see cref="EventType.WindowResized"/></item>
    /// <item><see cref="EventType.WindowRestored"/></item>
    /// <item><see cref="EventType.WindowSafeAreaChanged"/></item>
    /// <item><see cref="EventType.WindowShown"/></item>
    /// </list>
    /// </remarks>
    public EventType Type { get; }

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
            Debug.Assert(Type == EventType.WindowDisplayChanged);
            return Display.FromId((uint)_data1);
        }
    }

    /// <summary>
    /// Gets a value indicating whether high dynamic range (HDR) is currently enabled or not from <see cref="EventType.HdrStateChanged"/>.
    /// </summary>
    public bool IsHdrEnabled
    {
        get
        {
            Debug.Assert(Type == EventType.HdrStateChanged);
            return _data1 != 0;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window is currently being resized interactively by the user from <see cref="EventType.WindowExposed"/>.
    /// </summary>
    public bool IsLiveResize
    {
        get
        {
            Debug.Assert(Type == EventType.WindowExposed);
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
            Debug.Assert(Type == EventType.WindowMoved);
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
            Debug.Assert(Type == EventType.WindowPixelSizeChanged);
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
            Debug.Assert(Type == EventType.WindowResized);
            return new SizeInt(_data1, _data2);
        }
    }
}

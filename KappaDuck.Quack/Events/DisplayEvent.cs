// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Video.Displays;
using System.Diagnostics;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a display event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct DisplayEvent
{
    /// <summary>
    /// Gets the display event type.
    /// </summary>
    /// <remarks>
    /// The event is one of the following types:
    /// <list type="bullet">
    /// <item><see cref="EventType.ContentScaleChanged"/></item>
    /// <item><see cref="EventType.CurrentModeChanged"/></item>
    /// <item><see cref="EventType.DesktopModeChanged"/></item>
    /// <item><see cref="EventType.DisplayAdded"/></item>
    /// <item><see cref="EventType.DisplayMoved"/></item>
    /// <item><see cref="EventType.DisplayOrientationChanged"/></item>
    /// <item><see cref="EventType.DisplayRemoved"/></item>
    /// <item><see cref="EventType.UsableBoundsChanged"/></item>
    /// </list>
    /// </remarks>
    public EventType Type { get; }

    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the associated display id.
    /// </summary>
    public uint Id { get; }

    private readonly int _data1;
    private readonly int _data2;

    /// <summary>
    /// Gets the display which generated this event.
    /// </summary>
    public Display Current => Display.FromId(Id);

    /// <summary>
    /// Gets the new display size from <see cref="EventType.CurrentModeChanged"/>.
    /// </summary>
    public SizeInt DisplaySize
    {
        get
        {
            Debug.Assert(Type is EventType.CurrentModeChanged);
            return new(_data1, _data2);
        }
    }

    /// <summary>
    /// Gets the new desktop size from <see cref="EventType.DesktopModeChanged"/>.
    /// </summary>
    public SizeInt DesktopSize
    {
        get
        {
            Debug.Assert(Type is EventType.DesktopModeChanged);
            return new(_data1, _data2);
        }
    }

    /// <summary>
    /// Gets the new orientation from <see cref="EventType.DisplayOrientationChanged"/>.
    /// </summary>
    public DisplayOrientation Orientation
    {
        get
        {
            Debug.Assert(Type is EventType.DisplayOrientationChanged);
            return (DisplayOrientation)_data1;
        }
    }
}

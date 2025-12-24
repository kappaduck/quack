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
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the associated display id.
    /// </summary>
    public uint Id { get; }

    private readonly int _data1;
    private readonly int _data2;

    /// <summary>
    /// Gets the display associated with the current display-related event.
    /// </summary>
    public Display Current
    {
        get
        {
            Debug.Assert(IsDisplayEvent(_type));
            return Display.GetDisplay(Id);

            static bool IsDisplayEvent(EventType type)
            {
                return type is EventType.ContentScaleChanged
                    or EventType.CurrentModeChanged
                    or EventType.DesktopModeChanged
                    or EventType.DisplayAdded
                    or EventType.DisplayMoved
                    or EventType.DisplayOrientationChanged
                    or EventType.DisplayRemoved
                    or EventType.UsableBoundsChanged;
            }
        }
    }

    /// <summary>
    /// Gets the new display size from <see cref="EventType.CurrentModeChanged"/>.
    /// </summary>
    public SizeInt DisplaySize
    {
        get
        {
            Debug.Assert(_type is EventType.CurrentModeChanged);
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
            Debug.Assert(_type is EventType.DesktopModeChanged);
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
            Debug.Assert(_type is EventType.DisplayOrientationChanged);
            return (DisplayOrientation)_data1;
        }
    }
}

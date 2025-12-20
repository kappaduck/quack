// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which is processed by the event loop.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct Event
{
    [FieldOffset(0)]
    private unsafe fixed byte _padding[128];

    /// <summary>
    /// The type of the event.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly EventType Type { get; }

    /// <summary>
    /// The display event data.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly DisplayEvent Display { get; }

    /// <summary>
    /// The keyboard device event data.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly KeyboardDeviceEvent KeyboardDevice { get; }

    /// <summary>
    /// The keyboard event data.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly KeyboardEvent Keyboard { get; }

    /// <summary>
    /// The mouse button event data.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly MouseButtonEvent Mouse { get; }

    /// <summary>
    /// The mouse device event data.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly MouseDeviceEvent MouseDevice { get; }

    /// <summary>
    /// The mouse motion event data.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly MouseMotionEvent Motion { get; }

    /// <summary>
    /// The mouse wheel event data.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly MouseWheelEvent Wheel { get; }

    /// <summary>
    /// The window event data.
    /// </summary>
    [field: FieldOffset(0)]
    public readonly WindowEvent Window { get; }
}

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

    [field: FieldOffset(0)]
    internal EventType Type { get; }

    [field: FieldOffset(0)]
    internal DisplayEvent Display { get; }

    [field: FieldOffset(0)]
    internal KeyboardDeviceEvent KeyboardDevice { get; }

    [field: FieldOffset(0)]
    internal KeyboardEvent Keyboard { get; }

    [field: FieldOffset(0)]
    internal MouseButtonEvent Mouse { get; }

    [field: FieldOffset(0)]
    internal MouseDeviceEvent MouseDevice { get; }

    [field: FieldOffset(0)]
    internal MouseMotionEvent Motion { get; }

    [field: FieldOffset(0)]
    internal RendererEvent Renderer { get; }

    [field: FieldOffset(0)]
    internal MouseWheelEvent Wheel { get; }

    [field: FieldOffset(0)]
    internal WindowEvent Window { get; }
}

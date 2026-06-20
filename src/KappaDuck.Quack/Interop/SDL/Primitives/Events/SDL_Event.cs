// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Explicit, Size = 128)]
internal readonly struct SDL_Event
{
    [field: FieldOffset(0)]
    internal SDL_EventType Type { get; }

    [field: FieldOffset(0)]
    internal SDL_KeyboardDeviceEvent KeyboardDevice { get; }

    [field: FieldOffset(0)]
    internal SDL_MouseDeviceEvent MouseDevice { get; }

    [field: FieldOffset(0)]
    internal SDL_KeyboardEvent Keyboard { get; }

    [field: FieldOffset(0)]
    internal SDL_MouseMotionEvent Motion { get; }

    [field: FieldOffset(0)]
    internal SDL_MouseButtonEvent Button { get; }

    [field: FieldOffset(0)]
    internal SDL_MouseWheelEvent Wheel { get; }
}

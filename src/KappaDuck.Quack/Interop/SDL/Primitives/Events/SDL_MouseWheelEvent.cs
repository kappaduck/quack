// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MouseWheelEvent
{
    private readonly SDL_EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    internal uint WindowId { get; }

    internal uint Which { get; }

    internal float X { get; }

    internal float Y { get; }

    internal SDL_MouseWheelDirection Direction { get; }

    internal float MouseX { get; }

    internal float MouseY { get; }

    private readonly int _integerX;
    private readonly int _integerY;
}

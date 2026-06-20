// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MouseMotionEvent
{
    private readonly SDL_EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    internal uint WindowId { get; }

    internal uint Which { get; }

    internal MouseButtonState State { get; }

    internal float X { get; }

    internal float Y { get; }

    internal float Xrel { get; }

    internal float Yrel { get; }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MouseButtonEvent
{
    private readonly SDL_EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    internal uint WindowId { get; }

    internal uint Which { get; }

    internal MouseButton Button { get; }

    private readonly byte _down;

    internal byte Clicks { get; }

    private readonly byte _padding;

    internal float X { get; }

    internal float Y { get; }
}

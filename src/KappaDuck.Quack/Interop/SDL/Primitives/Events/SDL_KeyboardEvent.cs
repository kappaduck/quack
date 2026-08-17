// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_KeyboardEvent
{
    private readonly SDL_EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    internal uint WindowId { get; }

    internal uint Which { get; init; }

    internal Scancode Scancode { get; init; }

    internal Key Key { get; init; }

    internal KeyModifiers Mod { get; init; }

    private readonly ushort _raw;
    private readonly byte _down;

    internal byte Repeat { get; init; }
}

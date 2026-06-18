// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_KeyboardEvent
{
    internal SDL_EventType Type { get; init; }

    private readonly uint _reserved;
    private readonly ulong _timestamp;
    private readonly uint _windowId;

    internal uint Which { get; init; }

    internal Scancode Scancode { get; init; }

    internal Key Key { get; init; }

    internal Keymod Keymod { get; init; }

    private readonly ushort _raw;
    private readonly byte _down;

    internal byte Repeat { get; init; }
}

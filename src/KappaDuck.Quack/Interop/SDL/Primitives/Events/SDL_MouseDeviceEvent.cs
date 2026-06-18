// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MouseDeviceEvent(uint which, SDL_EventType type)
{
    private readonly SDL_EventType _type = type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    internal readonly uint Which { get; } = which;
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MouseDeviceEvent
{
    private readonly SDL_EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    internal readonly uint Which { get; }
}

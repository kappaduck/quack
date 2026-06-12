// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_QuitEvent(SDL_EventType type)
{
    private readonly SDL_EventType _type = type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_QuitEvent()
{
    private readonly SDL_EventType _type = SDL_EventType.Quit;
    private readonly uint _reserved;
    private readonly ulong _timestamp;
}

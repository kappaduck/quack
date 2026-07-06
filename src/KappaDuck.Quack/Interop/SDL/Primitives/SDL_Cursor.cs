// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal readonly struct SDL_Cursor;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_CursorFrameInfo
{
    private readonly SDL_Surface* _surface;
    private readonly uint _duration;

    internal SDL_CursorFrameInfo(SDL_Surface* surface, uint duration)
    {
        _surface = surface;
        _duration = duration;
    }
}

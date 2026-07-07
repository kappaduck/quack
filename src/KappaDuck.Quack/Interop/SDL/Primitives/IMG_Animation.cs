// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct IMG_Animation
{
    internal IMG_Animation(int width, int height, int count, SDL_Surface** frames, int* delays)
    {
        Width = width;
        Height = height;
        Count = count;
        Frames = frames;
        Delays = delays;
    }

    internal readonly int Width { get; }

    internal readonly int Height { get; }

    internal readonly int Count { get; }

    internal readonly SDL_Surface** Frames { get; }

    internal readonly int* Delays { get; }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_Surface
{
    internal SDL_SurfaceState State { get; }

    internal PixelFormat Format { get; }

    internal int Width { get; }

    internal int Height { get; }

    internal int Pitch { get; }

    internal void* Pixels { get; }

    private readonly int _refCount;
    private readonly nint _reserved;
}

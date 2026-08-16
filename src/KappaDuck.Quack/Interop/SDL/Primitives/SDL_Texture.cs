// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_Texture
{
    internal PixelFormat Format { get; }

    internal int Width { get; }

    internal int Height { get; }

    private readonly int _refCount;
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Pixels;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct SDL_Surface
{
    internal SurfaceState State { get; }

    internal PixelFormat Format { get; }

    internal int Width { get; }

    internal int Height { get; }

    internal int Pitch { get; }

    internal void* Pixels { get; }

    private readonly int _refCount;
    private readonly nint _reserved;
}

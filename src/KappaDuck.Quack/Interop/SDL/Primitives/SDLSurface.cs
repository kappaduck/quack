// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Pixels;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDLSurface
{
    internal readonly SurfaceState State;

    internal readonly PixelFormat Format;

    internal readonly int Width;

    internal readonly int Height;

    internal readonly int Pitch;

    internal void* Pixels;

    private readonly int _refCount;
    private readonly nint _reserved;
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Pixels;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Native;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDL_Surface
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

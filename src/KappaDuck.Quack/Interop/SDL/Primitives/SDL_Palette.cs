// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_Palette
{
    public int Count { get; }

    public Color* Colors { get; }

    private readonly uint _version;
    private readonly int _refCount;
}

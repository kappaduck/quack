// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly unsafe struct SDL_Palette
{
    internal int Length { get; }

    internal SDL_Color* Colors { get; }

    private readonly uint _version;
    private readonly int _refCount;
}

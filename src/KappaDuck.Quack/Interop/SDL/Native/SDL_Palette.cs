// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Native;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDL_Palette
{
    internal readonly int Length;

    internal SDL_Color* Colors;

    private readonly uint _version;
    private readonly int _refCount;
}

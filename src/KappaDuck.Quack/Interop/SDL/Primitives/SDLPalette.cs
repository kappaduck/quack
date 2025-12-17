// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDLPalette
{
    internal readonly int Length;

    internal SDLColor* Colors;

    private readonly uint _version;
    private readonly int _refCount;
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Drawing;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Native;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_Color
{
    internal SDL_Color(byte r, byte g, byte b, byte a)
    {
        _r = r;
        _g = g;
        _b = b;
        _a = a;
    }

    private readonly byte _r;
    private readonly byte _g;
    private readonly byte _b;
    private readonly byte _a;

    /// <summary>
    /// Gets the color.
    /// </summary>
    internal Color Color => Color.FromArgb(_a, _r, _g, _b);
}

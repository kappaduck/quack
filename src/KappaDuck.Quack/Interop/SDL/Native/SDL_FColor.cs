// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Drawing;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Native;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_FColor
{
    internal SDL_FColor(byte r, byte g, byte b, byte a)
    {
        _r = r / 255f;
        _g = g / 255f;
        _b = b / 255f;
        _a = a / 255f;
    }

    private readonly float _r;
    private readonly float _g;
    private readonly float _b;
    private readonly float _a;

    /// <summary>
    /// Gets the color.
    /// </summary>
    internal Color Color => Color.FromArgb((int)(_a * 255), (int)(_r * 255), (int)(_g * 255), (int)(_b * 255));
}

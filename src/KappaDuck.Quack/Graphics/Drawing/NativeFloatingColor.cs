// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Drawing;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// This struct represents a color with floating-point components in a format suitable for interoperability with native code.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal readonly struct NativeFloatingColor
{
    internal NativeFloatingColor(byte r, byte g, byte b, byte a)
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

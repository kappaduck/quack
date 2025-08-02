// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Pixels;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Represents a display mode, which includes properties such as resolution, refresh rate, and pixel format.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct DisplayMode
{
    /// <summary>
    /// The display this mode is associated with.
    /// </summary>
    public readonly uint DisplayId;

    /// <summary>
    /// The pixel format.
    /// </summary>
    public readonly PixelFormat Format;

    /// <summary>
    /// The width of the display mode.
    /// </summary>
    public readonly int Width;

    /// <summary>
    /// The height of the display mode.
    /// </summary>
    public readonly int Height;

    /// <summary>
    /// Scale converting size to pixels (e.g. a 1920x1080 mode with 2.0 scale would have 3840x2160 pixels).
    /// </summary>
    public readonly float PixelDensity;

    /// <summary>
    /// The refresh rate of the display mode or 0.0f for unspecified.
    /// </summary>
    public readonly float RefreshRate;

    /// <summary>
    /// Precise refresh rate numerator or 0 for unspecified.
    /// </summary>
    public readonly int RefreshRateNumerator;

    /// <summary>
    /// Precise refresh rate denominator or 0 for unspecified.
    /// </summary>
    public readonly int RefreshRateDenominator;

    private readonly nint _internal;
}

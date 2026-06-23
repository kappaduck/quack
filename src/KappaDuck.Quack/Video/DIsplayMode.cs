// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Video;

/// <summary>
/// Describes a single mode (resolution, pixel format and refresh rate) supported by a <see cref="Video.Display"/>.
/// </summary>
public readonly struct DisplayMode
{
    internal DisplayMode(SDL_DisplayMode mode)
    {
        Display = new Display(mode.DisplayId);
        Format = mode.Format;
        Size = new Size(mode.Width, mode.Height);
        PixelDensity = mode.PixelDensity;
        RefreshRate = mode.RefreshRate;
        RefreshRateNumerator = mode.RefreshRateNumerator;
        RefreshRateDenominator = mode.RefreshRateDenominator;
    }

    /// <summary>
    /// Gets the display this mode belongs to.
    /// </summary>
    public Display Display { get; }

    /// <summary>
    /// Gets the pixel format of the mode.
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// Gets the size of the mode in screen coordinates.
    /// </summary>
    /// <remarks>
    /// Multiply by <see cref="PixelDensity"/> to obtain the size in physical pixels.
    /// </remarks>
    public Size Size { get; }

    /// <summary>
    /// Gets the width of the mode in screen coordinates.
    /// </summary>
    public int Width => Size.Width;

    /// <summary>
    /// Gets the height of the mode in screen coordinates.
    /// </summary>
    public int Height => Size.Height;

    /// <summary>
    /// Gets the scale converting screen coordinates to physical pixels.
    /// </summary>
    /// <remarks>
    /// For example, a 1920x1080 mode with a pixel density of 2.0 has 3840x2160 physical pixels.
    /// </remarks>
    public float PixelDensity { get; }

    /// <summary>
    /// Gets the refresh rate in hertz, or 0 if unspecified.
    /// </summary>
    public float RefreshRate { get; }

    /// <summary>
    /// Gets the numerator of the precise refresh rate, or 0 if unspecified.
    /// </summary>
    /// <remarks>
    /// Use this together with <see cref="RefreshRateDenominator"/> when an exact fractional refresh
    /// rate is required, such as 59.94 Hz expressed as 60000 / 1001.
    /// </remarks>
    public int RefreshRateNumerator { get; }

    /// <summary>
    /// Gets the denominator of the precise refresh rate, or 0 if unspecified.
    /// </summary>
    public int RefreshRateDenominator { get; }

    /// <inheritdoc/>
    public override string ToString() => $"{Width}x{Height} {Format} @ {RefreshRate}Hz";
}

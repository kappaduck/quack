// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// Describes the bit layout of a <see cref="PixelFormat"/> as a bit depth and per-channel masks.
/// </summary>
public readonly record struct PixelFormatMask
{
    /// <summary>
    /// Initializes a mask for a pixel format.
    /// </summary>
    /// <param name="bitsPerPixel">The number of bits used to store a single pixel.</param>
    /// <param name="red">The bitmask that selects the red channel.</param>
    /// <param name="green">The bitmask that selects the green channel.</param>
    /// <param name="blue">The bitmask that selects the blue channel.</param>
    /// <param name="alpha">The bitmask that selects the alpha channel, or zero when the format has no alpha.</param>
    public PixelFormatMask(int bitsPerPixel, uint red, uint green, uint blue, uint alpha)
    {
        BitsPerPixel = bitsPerPixel;
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    /// <summary>
    /// Gets the number of bits used to store a single pixel.
    /// </summary>
    public int BitsPerPixel { get; init; }

    /// <summary>
    /// Gets the red channel bitmask.
    /// </summary>
    public uint Red { get; init; }

    /// <summary>
    /// Gets the green channel bitmask.
    /// </summary>
    public uint Green { get; init; }

    /// <summary>
    /// Gets the blue channel bitmask.
    /// </summary>
    public uint Blue { get; init; }

    /// <summary>
    /// Gets the alpha channel bitmask, or zero when the format has no alpha.
    /// </summary>
    public uint Alpha { get; init; }
}

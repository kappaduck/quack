// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Details about the format of a pixel.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct PixelFormatDetails
{
    /// <summary>
    /// Gets the pixel format.
    /// </summary>
    public readonly PixelFormat Format;

    /// <summary>
    /// Gets the number of bits per pixel.
    /// </summary>
    public readonly byte BitsPerPixel;

    /// <summary>
    /// Gets the number of bytes per pixel.
    /// </summary>
    public readonly byte BytesPerPixel;

    private unsafe fixed byte _padding[2];

    /// <summary>
    /// Gets the red mask.
    /// </summary>
    public readonly uint RedMask;

    /// <summary>
    /// Gets the green mask.
    /// </summary>
    public uint GreenMask;

    /// <summary>
    /// Gets the blue mask.
    /// </summary>
    public uint BlueMask;

    /// <summary>
    /// Gets the alpha mask.
    /// </summary>
    public readonly uint AlphaMask;

    /// <summary>
    /// Gets the red bits.
    /// </summary>
    public readonly byte RedBits;

    /// <summary>
    /// Gets the green bits.
    /// </summary>
    public readonly byte GreenBits;

    /// <summary>
    /// Gets the blue bits.
    /// </summary>
    public readonly byte BlueBits;

    /// <summary>
    /// Gets the alpha bits.
    /// </summary>
    public readonly byte AlphaBits;

    /// <summary>
    /// Gets the red shift.
    /// </summary>
    public readonly byte RedShift;

    /// <summary>
    /// Gets the green shift.
    /// </summary>
    public readonly byte GreenShift;

    /// <summary>
    /// Gets the blue shift.
    /// </summary>
    public readonly byte BlueShift;

    /// <summary>
    /// Gets the alpha shift.
    /// </summary>
    public readonly byte AlphaShift;
}

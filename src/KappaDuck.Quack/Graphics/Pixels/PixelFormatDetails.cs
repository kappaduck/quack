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
    public readonly PixelFormat Format { get; }

    /// <summary>
    /// Gets the number of bits per pixel.
    /// </summary>
    public readonly byte BitsPerPixel { get; }

    /// <summary>
    /// Gets the number of bytes per pixel.
    /// </summary>
    public readonly byte BytesPerPixel { get; }

    private unsafe fixed byte _padding[2];

    /// <summary>
    /// Gets the red mask.
    /// </summary>
    public readonly uint RedMask { get; }

    /// <summary>
    /// Gets the green mask.
    /// </summary>
    public readonly uint GreenMask { get; }

    /// <summary>
    /// Gets the blue mask.
    /// </summary>
    public readonly uint BlueMask { get; }

    /// <summary>
    /// Gets the alpha mask.
    /// </summary>
    public readonly uint AlphaMask { get; }

    /// <summary>
    /// Gets the red bits.
    /// </summary>
    public readonly byte RedBits { get; }

    /// <summary>
    /// Gets the green bits.
    /// </summary>
    public readonly byte GreenBits { get; }

    /// <summary>
    /// Gets the blue bits.
    /// </summary>
    public readonly byte BlueBits { get; }

    /// <summary>
    /// Gets the alpha bits.
    /// </summary>
    public readonly byte AlphaBits { get; }

    /// <summary>
    /// Gets the red shift.
    /// </summary>
    public readonly byte RedShift { get; }

    /// <summary>
    /// Gets the green shift.
    /// </summary>
    public readonly byte GreenShift { get; }

    /// <summary>
    /// Gets the blue shift.
    /// </summary>
    public readonly byte BlueShift { get; }

    /// <summary>
    /// Gets the alpha shift.
    /// </summary>
    public readonly byte AlphaShift { get; }
}

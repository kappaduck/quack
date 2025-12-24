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
    public PixelFormat Format { get; }

    /// <summary>
    /// Gets the number of bits per pixel.
    /// </summary>
    public byte BitsPerPixel { get; }

    /// <summary>
    /// Gets the number of bytes per pixel.
    /// </summary>
    public byte BytesPerPixel { get; }

    private unsafe fixed byte _padding[2];

    /// <summary>
    /// Gets the red mask.
    /// </summary>
    public uint RedMask { get; }

    /// <summary>
    /// Gets the green mask.
    /// </summary>
    public uint GreenMask { get; }

    /// <summary>
    /// Gets the blue mask.
    /// </summary>
    public uint BlueMask { get; }

    /// <summary>
    /// Gets the alpha mask.
    /// </summary>
    public uint AlphaMask { get; }

    /// <summary>
    /// Gets the red bits.
    /// </summary>
    public byte RedBits { get; }

    /// <summary>
    /// Gets the green bits.
    /// </summary>
    public byte GreenBits { get; }

    /// <summary>
    /// Gets the blue bits.
    /// </summary>
    public byte BlueBits { get; }

    /// <summary>
    /// Gets the alpha bits.
    /// </summary>
    public byte AlphaBits { get; }

    /// <summary>
    /// Gets the red shift.
    /// </summary>
    public byte RedShift { get; }

    /// <summary>
    /// Gets the green shift.
    /// </summary>
    public byte GreenShift { get; }

    /// <summary>
    /// Gets the blue shift.
    /// </summary>
    public byte BlueShift { get; }

    /// <summary>
    /// Gets the alpha shift.
    /// </summary>
    public byte AlphaShift { get; }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using System.Drawing;

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// Low-level operations on raw pixel buffers.
/// </summary>
public static class Pixels
{
    /// <summary>
    /// Converts a block of pixels from one format to another, copying the result into a destination buffer.
    /// </summary>
    /// <param name="dimension">The dimension of the block in pixels.</param>
    /// <param name="source">The source pixel data.</param>
    /// <param name="sourceFormat">The format of the source data.</param>
    /// <param name="sourcePitch">The number of bytes per row in <paramref name="source"/>.</param>
    /// <param name="destination">The destination buffer to write into.</param>
    /// <param name="destinationFormat">The format to convert to.</param>
    /// <param name="destinationPitch">The number of bytes per row in <paramref name="destination"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">A dimension is negative, or a buffer is too small for its pitch and height.</exception>
    public static void Convert(Size dimension, ReadOnlySpan<byte> source, PixelFormat sourceFormat, int sourcePitch, Span<byte> destination, PixelFormat destinationFormat, int destinationPitch)
        => Convert(dimension.Width, dimension.Height, source, sourceFormat, sourcePitch, destination, destinationFormat, destinationPitch);

    /// <summary>
    /// Converts a block of pixels from one format to another, copying the result into a destination buffer.
    /// </summary>
    /// <param name="width">The width of the block in pixels.</param>
    /// <param name="height">The height of the block in pixels.</param>
    /// <param name="source">The source pixel data.</param>
    /// <param name="sourceFormat">The format of the source data.</param>
    /// <param name="sourcePitch">The number of bytes per row in <paramref name="source"/>.</param>
    /// <param name="destination">The destination buffer to write into.</param>
    /// <param name="destinationFormat">The format to convert to.</param>
    /// <param name="destinationPitch">The number of bytes per row in <paramref name="destination"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">A dimension is negative, or a buffer is too small for its pitch and height.</exception>
    public static void Convert(int width, int height, ReadOnlySpan<byte> source, PixelFormat sourceFormat, int sourcePitch, Span<byte> destination, PixelFormat destinationFormat, int destinationPitch)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(width);
        ArgumentOutOfRangeException.ThrowIfNegative(height);
        ArgumentOutOfRangeException.ThrowIfLessThan(source.Length, sourcePitch * height);
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, destinationPitch * height);

        SDLThrowHelper.ThrowIfFailed(SDL3.ConvertPixels(width, height, sourceFormat, source, sourcePitch, destinationFormat, destination, destinationPitch));
    }

    /// <summary>
    /// Converts a block of pixels from one format and colorspace to another, copying the result into a destination buffer.
    /// </summary>
    /// <param name="dimension">The dimension of the block in pixels.</param>
    /// <param name="source">The source pixel data.</param>
    /// <param name="sourceFormat">The format of the source data.</param>
    /// <param name="sourceColorspace">The colorspace of the source data</param>
    /// <param name="sourcePitch">The number of bytes per row in <paramref name="source"/>.</param>
    /// <param name="destination">The destination buffer to write into.</param>
    /// <param name="destinationFormat">The format to convert to.</param>
    /// <param name="destinationColorspace">The colorspace of the destination data</param>
    /// <param name="destinationPitch">The number of bytes per row in <paramref name="destination"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">A dimension is negative, or a buffer is too small for its pitch and height.</exception>
    public static void Convert(Size dimension, ReadOnlySpan<byte> source, PixelFormat sourceFormat, Colorspace sourceColorspace, int sourcePitch, Span<byte> destination, PixelFormat destinationFormat, Colorspace destinationColorspace, int destinationPitch)
        => Convert(dimension.Width, dimension.Height, source, sourceFormat, sourceColorspace, sourcePitch, destination, destinationFormat, destinationColorspace, destinationPitch);

    /// <summary>
    /// Converts a block of pixels from one format and colorspace to another, copying the result into a destination buffer.
    /// </summary>
    /// <param name="width">The width of the block in pixels.</param>
    /// <param name="height">The height of the block in pixels.</param>
    /// <param name="source">The source pixel data.</param>
    /// <param name="sourceFormat">The format of the source data.</param>
    /// <param name="sourceColorspace">The colorspace of the source data</param>
    /// <param name="sourcePitch">The number of bytes per row in <paramref name="source"/>.</param>
    /// <param name="destination">The destination buffer to write into.</param>
    /// <param name="destinationFormat">The format to convert to.</param>
    /// <param name="destinationColorspace">The colorspace of the destination data</param>
    /// <param name="destinationPitch">The number of bytes per row in <paramref name="destination"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">A dimension is negative, or a buffer is too small for its pitch and height.</exception>
    public static void Convert(int width, int height, ReadOnlySpan<byte> source, PixelFormat sourceFormat, Colorspace sourceColorspace, int sourcePitch, Span<byte> destination, PixelFormat destinationFormat, Colorspace destinationColorspace, int destinationPitch)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(width);
        ArgumentOutOfRangeException.ThrowIfNegative(height);
        ArgumentOutOfRangeException.ThrowIfLessThan(source.Length, sourcePitch * height);
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, destinationPitch * height);

        SDLThrowHelper.ThrowIfFailed(SDL3.ConvertPixelsAndColorspace(width, height, sourceFormat, sourceColorspace, 0, source, sourcePitch, destinationFormat, destinationColorspace, 0, destination, destinationPitch));
    }

    /// <summary>
    /// Premultiplies the color channels of a block of pixels by their alpha channel, copying the result into a destination buffer.
    /// </summary>
    /// <param name="dimension">The dimension of the block in pixels.</param>
    /// <param name="source">The source pixel data.</param>
    /// <param name="sourceFormat">The format of the source data.</param>
    /// <param name="sourcePitch">The number of bytes per row in <paramref name="source"/>.</param>
    /// <param name="destination">The destination buffer to write into.</param>
    /// <param name="destinationFormat">The format to write.</param>
    /// <param name="destinationPitch">The number of bytes per row in <paramref name="destination"/>.</param>
    /// <param name="linear">
    /// <see langword="true"/> to convert from sRGB to linear space for the multiplication; <see langword="false"/> to multiply in sRGB space.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">A dimension is negative, or a buffer is too small for its pitch and height.</exception>
    /// <exception cref="QuackInteropException">Failed to premultiplies the pixels</exception>
    public static void PremultiplyAlpha(Size dimension, ReadOnlySpan<byte> source, PixelFormat sourceFormat, int sourcePitch, Span<byte> destination, PixelFormat destinationFormat, int destinationPitch, bool linear = false)
        => PremultiplyAlpha(dimension.Width, dimension.Height, source, sourceFormat, sourcePitch, destination, destinationFormat, destinationPitch, linear);

    /// <summary>
    /// Premultiplies the color channels of a block of pixels by their alpha channel, copying the result into a destination buffer.
    /// </summary>
    /// <param name="width">The width of the block in pixels.</param>
    /// <param name="height">The height of the block in pixels.</param>
    /// <param name="source">The source pixel data.</param>
    /// <param name="sourceFormat">The format of the source data.</param>
    /// <param name="sourcePitch">The number of bytes per row in <paramref name="source"/>.</param>
    /// <param name="destination">The destination buffer to write into.</param>
    /// <param name="destinationFormat">The format to write.</param>
    /// <param name="destinationPitch">The number of bytes per row in <paramref name="destination"/>.</param>
    /// <param name="linear">
    /// <see langword="true"/> to convert from sRGB to linear space for the multiplication; <see langword="false"/> to multiply in sRGB space.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">A dimension is negative, or a buffer is too small for its pitch and height.</exception>
    /// <exception cref="QuackInteropException">Failed to premultiplies the pixels</exception>
    public static void PremultiplyAlpha(int width, int height, ReadOnlySpan<byte> source, PixelFormat sourceFormat, int sourcePitch, Span<byte> destination, PixelFormat destinationFormat, int destinationPitch, bool linear = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(width);
        ArgumentOutOfRangeException.ThrowIfNegative(height);
        ArgumentOutOfRangeException.ThrowIfLessThan(source.Length, sourcePitch * height);
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, destinationPitch * height);

        SDLThrowHelper.ThrowIfFailed(SDL3.PremultiplyAlpha(width, height, sourceFormat, source, sourcePitch, destinationFormat, destination, destinationPitch, linear));
    }
}

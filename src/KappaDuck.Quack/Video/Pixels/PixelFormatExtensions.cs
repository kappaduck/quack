// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// Provides additional <see cref="PixelFormat"/> members.
/// </summary>
public static class PixelFormatExtensions
{
    extension(PixelFormat format)
    {
        /// <summary>
        /// Gets the number of bits used to store a single pixel.
        /// </summary>
        /// <remarks>
        /// This is the number of significant bits and can be smaller than BytesPerPixel times eight for
        /// formats with unused bits. It is zero for formats that are not laid out as discrete bits, such as the YUV formats.
        /// </remarks>
        public int BitsPerPixel => format.IsFourCharacterCode ? 0 : (int)(((uint)format >> 8) & 0xFF);

        /// <summary>
        /// Gets the number of bytes used to store a single pixel.
        /// </summary>
        /// <remarks>
        /// It is zero for formats that pack several pixels into one byte, such as the 1-, 2- and 4-bit indexed formats.
        /// </remarks>
        public int BytesPerPixel
        {
            get
            {
                if (!format.IsFourCharacterCode)
                    return (int)((uint)format & 0xFF);

                return format is PixelFormat.Yuy2 or PixelFormat.Uyvy or PixelFormat.Yvyu or PixelFormat.P010 ? 2 : 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the pixel format is an indexed format.
        /// </summary>
        public bool IsIndexed
        {
            get
            {
                return format is PixelFormat.Index1Lsb
                    or PixelFormat.Index1Msb
                    or PixelFormat.Index2Lsb
                    or PixelFormat.Index2Msb
                    or PixelFormat.Index4Lsb
                    or PixelFormat.Index4Msb
                    or PixelFormat.Index8;
            }
        }

        /// <summary>
        /// Gets the bit depth and channel masks that describe the format.
        /// </summary>
        /// <remarks>
        /// Returns a zeroed <see cref="PixelFormatMask"/> for formats that have no channel masks, such as indexed
        /// and YUV formats.
        /// </remarks>
        public PixelFormatMask Mask
        {
            get
            {
                int bpp;
                uint red, green, blue, alpha;

                SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetMasksForPixelFormat(format, &bpp, &red, &green, &blue, &alpha)));
                return new PixelFormatMask(bpp, red, green, blue, alpha);
            }
        }

        /// <summary>
        /// Extracts the opaque color from a pixel value.
        /// </summary>
        /// <remarks>The returned color is always fully opaque. Use <see cref="GetRGBA"/> to read the alpha channel.</remarks>
        /// <param name="pixel">The pixel value to unpack.</param>
        /// <param name="palette">An optional palette for indexed formats.</param>
        /// <returns>The unpacked color.</returns>
        public Color GetRGB(uint pixel, Palette? palette = null)
        {
            byte r, g, b;
            unsafe
            {
                SDL3.GetRGB(pixel, format.Details, palette?.Handle, &r, &g, &b);
            }

            return new Color(r, g, b);
        }

        /// <summary>
        /// Extracts the color, including its alpha channel, from a pixel value.
        /// </summary>
        /// <remarks>The alpha channel is returned as fully opaque when the format has no alpha channel.</remarks>
        /// <param name="pixel">The pixel value to unpack.</param>
        /// <param name="palette">An optional palette for indexed formats.</param>
        /// <returns>The unpacked color.</returns>
        public Color GetRGBA(uint pixel, Palette? palette = null)
        {
            byte r, g, b, a;
            unsafe
            {
                SDL3.GetRGBA(pixel, format.Details, palette?.Handle, &r, &g, &b, &a);
            }

            return new Color(r, g, b, a);
        }

        /// <summary>
        /// Maps a color to an opaque pixel value for the format.
        /// </summary>
        /// <remarks>
        /// The alpha channel of <paramref name="color"/> is ignored; if the format has an alpha channel it is set to
        /// fully opaque. Use <see cref="MapRGBA(PixelFormat, Color, Palette?)"/> to preserve alpha.
        /// </remarks>
        /// <param name="color">The color to map.</param>
        /// <param name="palette">An optional palette for indexed formats.</param>
        /// <returns>The pixel value packed for the format.</returns>
        public uint MapRGB(Color color, Palette? palette = null)
            => unsafe (SDL3.MapRGB(format.Details, palette?.Handle, color.R, color.G, color.B));

        /// <summary>
        /// Maps a color to an opaque pixel value for the format.
        /// </summary>
        /// <remarks>
        /// The color is first quantized to 8 bits per channel. The alpha channel is ignored; if the format has an alpha
        /// channel it is set to fully opaque. Use <see cref="MapRGBA(PixelFormat, ColorF, Palette?)"/> to preserve alpha.
        /// </remarks>
        /// <param name="color">The color to map.</param>
        /// <param name="palette">An optional palette for indexed formats.</param>
        /// <returns>The pixel value packed for the format.</returns>
        public uint MapRGB(ColorF color, Palette? palette = null)
            => MapRGB(format, color.ToColor(), palette);

        /// <summary>
        /// Maps a color, including its alpha channel, to a pixel value for the format.
        /// </summary>
        /// <param name="color">The color to map.</param>
        /// <param name="palette">An optional palette for indexed formats.</param>
        /// <returns>The pixel value packed for the format.</returns>
        public uint MapRGBA(Color color, Palette? palette = null)
            => unsafe (SDL3.MapRGBA(format.Details, palette?.Handle, color.R, color.G, color.B, color.A));

        /// <summary>
        /// Maps a color, including its alpha channel, to a pixel value for the format.
        /// </summary>
        /// <remarks>The color is first quantized to 8 bits per channel.</remarks>
        /// <param name="color">The color to map.</param>
        /// <param name="palette">An optional palette for indexed formats.</param>
        /// <returns>The pixel value packed for the format.</returns>
        public uint MapRGBA(ColorF color, Palette? palette = null)
            => MapRGBA(format, color.ToColor(), palette);

        internal unsafe SDL_PixelFormatDetails* Details => SDL3.GetPixelFormatDetails(format);

        private bool IsFourCharacterCode => format != PixelFormat.Unknown && (((uint)format >> 28) & 0x0F) != 1;
    }

    extension(PixelFormat)
    {
        /// <summary>
        /// Finds the format that matches the given bit depth and channel masks.
        /// </summary>
        /// <param name="mask">The bit depth and channel masks to match.</param>
        /// <returns>The matching format, or <see cref="PixelFormat.Unknown"/> if no format matches.</returns>
        public static PixelFormat FromMask(PixelFormatMask mask)
            => SDL3.GetPixelFormatForMasks(mask.BitsPerPixel, mask.Red, mask.Green, mask.Blue, mask.Alpha);

        /// <summary>
        /// Gets the 32-bit format whose four bytes in memory are red, green, blue then alpha, on the current platform.
        /// </summary>
        /// <remarks>
        /// The byte order in memory is the same on every platform. It resolves to <see cref="PixelFormat.Rgba8888"/>
        /// on big-endian platforms and <see cref="PixelFormat.Abgr8888"/> on little-endian platforms.
        /// </remarks>
        public static PixelFormat Rgba32 => BitConverter.IsLittleEndian ? PixelFormat.Abgr8888 : PixelFormat.Rgba8888;

        /// <summary>
        /// Gets the 32-bit format whose four bytes in memory are alpha, red, green then blue, on the current platform.
        /// </summary>
        /// <remarks>
        /// The byte order in memory is the same on every platform. It resolves to <see cref="PixelFormat.Argb8888"/>
        /// on big-endian platforms and <see cref="PixelFormat.Bgra8888"/> on little-endian platforms.
        /// </remarks>
        public static PixelFormat Argb32 => BitConverter.IsLittleEndian ? PixelFormat.Bgra8888 : PixelFormat.Argb8888;

        /// <summary>
        /// Gets the 32-bit format whose four bytes in memory are blue, green, red then alpha, on the current platform.
        /// </summary>
        /// <remarks>
        /// The byte order in memory is the same on every platform. It resolves to <see cref="PixelFormat.Bgra8888"/>
        /// on big-endian platforms and <see cref="PixelFormat.Argb8888"/> on little-endian platforms.
        /// </remarks>
        public static PixelFormat Bgra32 => BitConverter.IsLittleEndian ? PixelFormat.Argb8888 : PixelFormat.Bgra8888;

        /// <summary>
        /// Gets the 32-bit format whose four bytes in memory are alpha, blue, green then red, on the current platform.
        /// </summary>
        /// <remarks>
        /// The byte order in memory is the same on every platform. It resolves to <see cref="PixelFormat.Abgr8888"/>
        /// on big-endian platforms and <see cref="PixelFormat.Rgba8888"/> on little-endian platforms.
        /// </remarks>
        public static PixelFormat Abgr32 => BitConverter.IsLittleEndian ? PixelFormat.Rgba8888 : PixelFormat.Abgr8888;

        /// <summary>
        /// Gets the 32-bit format whose four bytes in memory are red, green, blue then an unused byte, on the current platform.
        /// </summary>
        /// <remarks>
        /// The byte order in memory is the same on every platform. It resolves to <see cref="PixelFormat.Rgbx8888"/>
        /// on big-endian platforms and <see cref="PixelFormat.Xbgr8888"/> on little-endian platforms.
        /// </remarks>
        public static PixelFormat Rgbx32 => BitConverter.IsLittleEndian ? PixelFormat.Xbgr8888 : PixelFormat.Rgbx8888;

        /// <summary>
        /// Gets the 32-bit format whose four bytes in memory are an unused byte, red, green then blue, on the current platform.
        /// </summary>
        /// <remarks>
        /// The byte order in memory is the same on every platform. It resolves to <see cref="PixelFormat.Xrgb8888"/>
        /// on big-endian platforms and <see cref="PixelFormat.Bgrx8888"/> on little-endian platforms.
        /// </remarks>
        public static PixelFormat Xrgb32 => BitConverter.IsLittleEndian ? PixelFormat.Bgrx8888 : PixelFormat.Xrgb8888;

        /// <summary>
        /// Gets the 32-bit format whose four bytes in memory are blue, green, red then an unused byte, on the current platform.
        /// </summary>
        /// <remarks>
        /// The byte order in memory is the same on every platform. It resolves to <see cref="PixelFormat.Bgrx8888"/>
        /// on big-endian platforms and <see cref="PixelFormat.Xrgb8888"/> on little-endian platforms.
        /// </remarks>
        public static PixelFormat Bgrx32 => BitConverter.IsLittleEndian ? PixelFormat.Xrgb8888 : PixelFormat.Bgrx8888;

        /// <summary>
        /// Gets the 32-bit format whose four bytes in memory are an unused byte, blue, green then red, on the current platform.
        /// </summary>
        /// <remarks>
        /// The byte order in memory is the same on every platform. It resolves to <see cref="PixelFormat.Xbgr8888"/>
        /// on big-endian platforms and <see cref="PixelFormat.Rgbx8888"/> on little-endian platforms.
        /// </remarks>
        public static PixelFormat Xbgr32 => BitConverter.IsLittleEndian ? PixelFormat.Rgbx8888 : PixelFormat.Xbgr8888;
    }
}

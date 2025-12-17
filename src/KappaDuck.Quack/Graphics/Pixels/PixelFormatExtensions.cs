// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Provides extension methods for <see cref="PixelFormat"/> and <see cref="PixelFormatDetails"/>.
/// </summary>
public static class PixelFormatExtensions
{
    extension(PixelFormat format)
    {
        /// <summary>
        /// Gets the details of the pixel format.
        /// </summary>
        /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
        public unsafe PixelFormatDetails Details
        {
            get
            {
                PixelFormatDetails* details = Native.SDL_GetPixelFormatDetails(format);
                QuackNativeException.ThrowIfNull(details);

                return *details;
            }
        }

        /// <summary>
        /// Gets the name of the pixel format.
        /// </summary>
        /// <remarks>
        /// It will return <see cref="PixelFormat.Unknown"/> if the format isn't recognized.
        /// </remarks>
        public string Name => Native.SDL_GetPixelFormatName(format);

        /// <summary>
        /// Gets the bits per pixel and masks for the pixel format.
        /// </summary>
        /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
        public (int bitsPerPixel, uint RedMask, uint GreenMask, uint BlueMask, uint AlphaMask) Masks
        {
            get
            {
                QuackNativeException.ThrowIfFailed(Native.SDL_GetMasksForPixelFormat(format, out int bitsPerPixel, out uint redMask, out uint greenMask, out uint blueMask, out uint alphaMask));
                return (bitsPerPixel, redMask, greenMask, blueMask, alphaMask);
            }
        }

        /// <summary>
        /// Convert a bits per pixel and RGBA masks to a pixel format.
        /// </summary>
        /// <param name="bitsPerPixel">Bits per pixel value; usually 15, 16, 32</param>
        /// <param name="redMask">Red mask.</param>
        /// <param name="greenMask">Green mask.</param>
        /// <param name="blueMask">Blue mask.</param>
        /// <param name="alphaMask">Alpha mask.</param>
        /// <returns>The corresponding pixel format or <see cref="PixelFormat.Unknown"/>.</returns>
        public static PixelFormat FromMasks(int bitsPerPixel, uint redMask, uint greenMask, uint blueMask, uint alphaMask)
            => Native.SDL_GetPixelFormatForMasks(bitsPerPixel, redMask, greenMask, blueMask, alphaMask);

        /// <summary>
        /// Gets the color from a pixel value.
        /// </summary>
        /// <param name="pixel">The pixel value.</param>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="palette">The palette to use for indexed color formats, or <see langword="null" /> for non-indexed formats.</param>
        /// <returns>The color represented by the pixel value.</returns>
        public static Color GetColor(uint pixel, PixelFormatDetails details, Palette? palette = null)
        {
            (byte r, byte g, byte b, byte a) = GetRGBA(pixel, details, palette);
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// Gets the RGB components from a pixel value.
        /// </summary>
        /// <remarks>
        /// It uses the entire 8-bit [0..255] range when converting color components from pixel formats
        /// with less than 8-bits per RGB component (e.g., a completely white pixel in 16-bit RGB565 format would return [0xff, 0xff, 0xff] not [0xf8, 0xfc, 0xf8]).
        /// </remarks>
        /// <param name="pixel">The pixel value.</param>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="palette">The palette to use for indexed color formats, or <see langword="null" /> for non-indexed formats.</param>
        /// <returns>The RGB components of the pixel.</returns>
        public static unsafe (byte Red, byte Green, byte Blue) GetRGB(uint pixel, PixelFormatDetails details, Palette? palette = null)
        {
            byte r;
            byte g;
            byte b;

            Native.SDL_GetRGB(pixel, &details, GetPaletteHandle(palette), &r, &g, &b);
            return (r, g, b);
        }

        /// <summary>
        /// Gets the RGBA components from a pixel value.
        /// </summary>
        /// <remarks>
        /// It uses the entire 8-bit [0..255] range when converting color components from pixel formats
        /// with less than 8-bits per RGB component (e.g., a completely white pixel in 16-bit RGB565 format would return [0xff, 0xff, 0xff] not [0xf8, 0xfc, 0xf8]).
        /// If the surface has no alpha component, the alpha will be returned as 0xff (100% opaque).
        /// </remarks>
        /// <param name="pixel">The pixel value.</param>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="palette">The palette to use for indexed color formats, or <see langword="null" /> for non-indexed formats.</param>
        /// <returns>The RGBA components of the pixel.</returns>
        public static unsafe (byte Red, byte Green, byte Blue, byte Alpha) GetRGBA(uint pixel, PixelFormatDetails details, Palette? palette = null)
        {
            byte r;
            byte g;
            byte b;
            byte a;

            Native.SDL_GetRGBA(pixel, &details, GetPaletteHandle(palette), &r, &g, &b, &a);
            return (r, g, b, a);
        }

        /// <summary>
        /// Maps the RGB components to a pixel value.
        /// </summary>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="palette">The palette to use for indexed color formats, or <see langword="null" /> for non-indexed formats.</param>
        /// <returns>The pixel value.</returns>
        public static unsafe uint MapRGB(PixelFormatDetails details, byte r, byte g, byte b, Palette? palette = null)
            => Native.SDL_MapRGB(&details, GetPaletteHandle(palette), r, g, b);

        /// <summary>
        /// Maps the RGBA components to a pixel value.
        /// </summary>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <param name="palette">The palette to use for indexed color formats, or <see langword="null" /> for non-indexed formats.</param>
        /// <returns>The pixel value.</returns>
        public static unsafe uint MapRGBA(PixelFormatDetails details, byte r, byte g, byte b, byte a, Palette? palette = null)
            => Native.SDL_MapRGBA(&details, GetPaletteHandle(palette), r, g, b, a);

        /// <summary>
        /// Maps the color components to a pixel value.
        /// </summary>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="color">The color components.</param>
        /// <param name="palette">The palette to use for indexed color formats, or <see langword="null" /> for non-indexed formats.</param>
        /// <returns>The pixel value.</returns>
        public static uint MapColor(PixelFormatDetails details, Color color, Palette? palette = null) => MapRGBA(details, color.R, color.G, color.B, color.A, palette);
    }

    [SuppressMessage("Minor Code Smell", "S3398:\"private\" methods called only by inner classes should be moved to those classes", Justification = "Sonar seems to be confused by the 'extension' syntax.")]
    private static unsafe SDLPalette* GetPaletteHandle(Palette? palette) => palette is not null ? palette.Handle : null;
}

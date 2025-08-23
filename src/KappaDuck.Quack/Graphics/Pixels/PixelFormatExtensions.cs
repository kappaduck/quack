// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Marshallers;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Provides extension methods for <see cref="PixelFormat"/> and <see cref="PixelFormatDetails"/>.
/// </summary>
public static partial class PixelFormatExtensions
{
    extension(PixelFormat format)
    {
        /// <summary>
        /// Gets the details of the pixel format.
        /// </summary>
        /// <exception cref="QuackNativeException">Thrown if failed to retrieve the details.</exception>
        public PixelFormatDetails Details
        {
            get
            {
                unsafe
                {
                    PixelFormatDetails* details = SDL_GetPixelFormatDetails(format);
                    QuackNativeException.ThrowIfNull(details);

                    return *details;
                }
            }
        }

        /// <summary>
        /// Gets the name of the pixel format.
        /// </summary>
        /// <remarks>
        /// It will return "SDL_PIXELFORMAT_UNKNOWN" if the format isn't recognized.
        /// </remarks>
        public string Name => SDL_GetPixelFormatName(format);

        /// <summary>
        /// Gets the bits per pixel and masks for the pixel format.
        /// </summary>
        /// <exception cref="QuackNativeException">Thrown if failed to retrieve the mask.</exception>
        public (int bitsPerPixel, uint RedMask, uint GreenMask, uint BlueMask, uint AlphaMask) Masks
        {
            get
            {
                QuackNativeException.ThrowIfFailed(SDL_GetMasksForPixelFormat(format, out int bitsPerPixel, out uint redMask, out uint greenMask, out uint blueMask, out uint alphaMask));
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
            => SDL_GetPixelFormatForMasks(bitsPerPixel, redMask, greenMask, blueMask, alphaMask);

        /// <summary>
        /// Gets the color from a pixel value.
        /// </summary>
        /// <param name="pixel">The pixel value.</param>
        /// <param name="details">The description of the pixel format.</param>
        /// <returns>The color represented by the pixel value.</returns>
        public static Color GetColor(uint pixel, PixelFormatDetails details)
        {
            (byte r, byte g, byte b, byte a) = GetRGBA(pixel, details);
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
        /// <returns>The RGB components of the pixel.</returns>
        public static (byte Red, byte Green, byte Blue) GetRGB(uint pixel, PixelFormatDetails details)
        {
            unsafe
            {
                byte r;
                byte g;
                byte b;

                SDL_GetRGB(pixel, &details, IntPtr.Zero, &r, &g, &b);
                return (r, g, b);
            }
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
        /// <returns>The RGBA components of the pixel.</returns>
        public static (byte Red, byte Green, byte Blue, byte Alpha) GetRGBA(uint pixel, PixelFormatDetails details)
        {
            unsafe
            {
                byte r;
                byte g;
                byte b;
                byte a;

                SDL_GetRGBA(pixel, &details, IntPtr.Zero, &r, &g, &b, &a);
                return (r, g, b, a);
            }
        }

        /// <summary>
        /// Maps the RGB components to a pixel value.
        /// </summary>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <returns>The pixel value.</returns>
        public static uint MapRGB(PixelFormatDetails details, byte r, byte g, byte b)
        {
            unsafe
            {
                return SDL_MapRGB(&details, IntPtr.Zero, r, g, b);
            }
        }

        /// <summary>
        /// Maps the RGBA components to a pixel value.
        /// </summary>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        /// <returns>The pixel value.</returns>
        public static uint MapRGBA(PixelFormatDetails details, byte r, byte g, byte b, byte a)
        {
            unsafe
            {
                return SDL_MapRGBA(&details, IntPtr.Zero, r, g, b, a);
            }
        }

        /// <summary>
        /// Maps the color components to a pixel value.
        /// </summary>
        /// <param name="details">The description of the pixel format.</param>
        /// <param name="color">The color components.</param>
        /// <returns>The pixel value.</returns>
        public static uint MapColor(PixelFormatDetails details, Color color) => MapRGBA(details, color.R, color.G, color.B, color.A);
    }

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_GetMasksForPixelFormat(PixelFormat format, out int bitsPerPixel, out uint redMask, out uint greenMask, out uint blueMask, out uint alphaMask);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial PixelFormatDetails* SDL_GetPixelFormatDetails(PixelFormat format);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    private static partial string SDL_GetPixelFormatName(PixelFormat format);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat SDL_GetPixelFormatForMasks(int bitsPerPixel, uint redMask, uint greenMask, uint blueMask, uint alphaMask);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void SDL_GetRGB(uint pixel, PixelFormatDetails* details, IntPtr palette, byte* r, byte* g, byte* b);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void SDL_GetRGBA(uint pixel, PixelFormatDetails* details, IntPtr palette, byte* r, byte* g, byte* b, byte* a);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial uint SDL_MapRGB(PixelFormatDetails* details, IntPtr palette, byte r, byte g, byte b);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial uint SDL_MapRGBA(PixelFormatDetails* details, IntPtr palette, byte r, byte g, byte b, byte a);
}

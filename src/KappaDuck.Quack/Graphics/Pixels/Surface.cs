// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

// ReSharper disable InconsistentNaming

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Primitives;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// A collection of pixels used in software blitting.
/// </summary>
/// <remarks>
/// <para>
/// Pixels are arranged in memory in rows, with the top row first. Each row occupies an amount of memory given by the pitch/row stride.
/// </para>
/// <para>
/// Within each row, pixels are arranged from left to right until the width is reached.
/// Each pixel occupies a number of bits appropriate for its format,
/// with most formats representing each pixel as one or more whole bytes (in some indexed formats, instead multiple pixels are packed into each byte), and a byte order given by the format.
/// After encoding all pixels, any remaining bytes to reach the pitch are used as padding to reach a desired alignment, and have undefined contents.
/// </para>
/// <para>
/// When a surface holds YUV format data, the planes are assumed to be contiguous without padding between them, e.g. a 32x32 surface in NV12 format with a pitch of 32
/// would consist of 32x32 bytes of Y plane followed by 32x16 bytes of UV plane.
/// </para>
/// <para>
/// When a surface holds MJPG format data, pixels points at the compressed JPEG image and pitch is the length of that data.
/// </para>
/// </remarks>
[PublicAPI]
public sealed unsafe class Surface : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Surface"/> class with the specified width, height, and pixel format.
    /// </summary>
    /// <param name="width">The width of the surface in pixels.</param>
    /// <param name="height">The height of the surface in pixels.</param>
    /// <param name="format">The pixel format of the surface.</param>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public Surface(int width, int height, PixelFormat format)
    {
        Format = format;
        Details = format.Details;
        Width = width;
        Height = height;

        Handle = Native.SDL_CreateSurface(width, height, format);

        QuackNativeException.ThrowIfNull(Handle);
    }

    internal Surface(SDLSurface* handle)
    {
        QuackNativeException.ThrowIfNull(handle);

        Format = handle->Format;
        Details = Format.Details;
        Width = handle->Width;
        Height = handle->Height;

        Handle = handle;
    }

    /// <summary>
    /// Gets the pixel format of the surface.
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// Gets the pixel format details of the surface.
    /// </summary>
    public PixelFormatDetails Details { get; }

    /// <summary>
    /// Gets a value indicating whether the surface has a color key.
    /// </summary>
    public bool HasColorKey => Native.SDL_SurfaceHasColorKey(Handle);

    /// <summary>
    /// Gets a value indicating whether the surface uses RLE acceleration.
    /// </summary>
    public bool HasRLE => Native.SDL_SurfaceHasRLE(Handle);

    /// <summary>
    /// Gets the height of the surface in pixels.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets or sets the palette associated with the surface.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public Palette? Palette
    {
        get;
        set
        {
            ThrowIfDisposed();

            SDLPalette* palette = value is null ? null : value.Handle;
            QuackNativeException.ThrowIfFailed(Native.SDL_SetSurfacePalette(Handle, palette));

            field = value;
        }
    }

    /// <summary>
    /// Gets the pitch (length of a row of pixels) of the surface.
    /// </summary>
    public int Pitch { get; }

    /// <summary>
    /// Gets the width of the surface in pixels.
    /// </summary>
    public int Width { get; }

    internal SDLSurface* Handle { get; private set; }

    /// <summary>
    /// Clones a surface based on the current surface.
    /// </summary>
    /// <remarks>
    /// If the original surface has alternate images, the cloned surface will have a reference to them as well.
    /// </remarks>
    /// <returns>The cloned surface.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public Surface Clone()
    {
        ThrowIfDisposed();

        SDLSurface* handle = Native.SDL_DuplicateSurface(Handle);
        QuackNativeException.ThrowIfNull(handle);

        return new Surface(handle);
    }

    /// <summary>
    /// Converts the surface to a new pixel format.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is used to optimize images for faster repeat blitting. This is accomplished by converting the original and storing
    /// the result as a new surface. The new, optimized surface can then be used as the source for future blits, making them faster.
    /// </para>
    /// <para>
    /// If the original surface has alternate images, the new surface will have a reference to them as well.
    /// </para>
    /// </remarks>
    /// <param name="format">The desired pixel format for the new surface.</param>
    /// <returns>The converted surface in the new pixel format.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public Surface Convert(PixelFormat format)
    {
        ThrowIfDisposed();

        SDLSurface* handle = Native.SDL_ConvertSurface(Handle, format);
        QuackNativeException.ThrowIfNull(handle);

        return new Surface(handle);
    }

    /// <summary>
    /// Creates and associates a new palette with the surface.
    /// </summary>
    /// <remarks>
    /// <para>The surface must be in an indexed pixel format (1, 4, or 8 bits per pixel) to have a palette.</para>
    /// <para>
    /// You do not need to dispose of the palette separately; it will be automatically disposed when the surface is disposed.
    /// </para>
    /// <para>
    /// Bitmap surfaces (with format <see cref="PixelFormat.Index1LSB"/> or <see cref="PixelFormat.Index1MSB"/>)
    /// will have the palette initialized with 0 as white and 1 as black.
    /// Other surfaces will get a palette initialized with white in every entry.
    /// </para>
    /// <para>
    /// If this function is called for a surface that already has a palette, a new palette will be created to replace it.
    /// </para>
    /// </remarks>
    /// <returns>The created palette associated with the surface.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    /// <exception cref="QuackException">Thrown when the surface is not in an indexed pixel format.</exception>
    public Palette CreatePalette()
    {
        ThrowIfDisposed();
        QuackException.ThrowIf(!Format.IsIndexed, $"The format {Format} is not indexed and cannot have a palette.");

        return new Palette(Handle);
    }

    /// <summary>
    /// Disposes the surface and releases its unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        if (Handle is null)
            return;

        Native.SDL_DestroySurface(Handle);
        Handle = null;
    }

    /// <summary>
    /// Fills the entire surface with the specified RGBA color.
    /// </summary>
    /// <remarks>
    /// <para>This function handles all surface formats, and ignores any clip rectangle.</para>
    /// <para>
    /// If the surface is YUV, the color is assumed to be in the sRGB colorspace, otherwise the color is assumed to be in the colorspace of the surface.
    /// </para>
    /// </remarks>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <param name="a">The alpha component of the color.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(byte r, byte g, byte b, byte a)
    {
        ThrowIfDisposed();
        QuackNativeException.ThrowIfFailed(Native.SDL_ClearSurface(Handle, r / 255f, g / 255f, b / 255f, a / 255f));
    }

    /// <summary>
    /// Fills the entire surface with the specified RGBA color.
    /// </summary>
    /// <remarks>
    /// <para>The color components are expected to be in the range 0.0 to 1.0.</para>
    /// <para>This function handles all surface formats, and ignores any clip rectangle.</para>
    /// <para>
    /// If the surface is YUV, the color is assumed to be in the sRGB colorspace, otherwise the color is assumed to be in the colorspace of the surface.
    /// </para>
    /// </remarks>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <param name="a">The alpha component of the color.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(float r, float g, float b, float a)
    {
        ThrowIfDisposed();
        QuackNativeException.ThrowIfFailed(Native.SDL_ClearSurface(Handle, r, g, b, a));
    }

    /// <summary>
    /// Fills the entire surface with the specified RGB color and full opacity.
    /// </summary>
    /// <remarks>
    /// <para>This function handles all surface formats, and ignores any clip rectangle.</para>
    /// <para>
    /// If the surface is YUV, the color is assumed to be in the sRGB colorspace, otherwise the color is assumed to be in the colorspace of the surface.
    /// </para>
    /// </remarks>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(byte r, byte g, byte b) => Fill(r, g, b, 255);

    /// <summary>
    /// Fills the entire surface with the specified RGB color and full opacity.
    /// </summary>
    /// <remarks>
    /// <para>The color components are expected to be in the range 0.0 to 1.0.</para>
    /// <para>This function handles all surface formats, and ignores any clip rectangle.</para>
    /// <para>
    /// If the surface is YUV, the color is assumed to be in the sRGB colorspace, otherwise the color is assumed to be in the colorspace of the surface.
    /// </para>
    /// </remarks>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(float r, float g, float b) => Fill(r, g, b, 1.0f);

    /// <summary>
    /// Fills the entire surface with the specified color.
    /// </summary>
    /// <remarks>
    /// <para>This function handles all surface formats, and ignores any clip rectangle.</para>
    /// <para>
    /// If the surface is YUV, the color is assumed to be in the sRGB colorspace, otherwise the color is assumed to be in the colorspace of the surface.
    /// </para>
    /// </remarks>
    /// <param name="color">The color to fill the surface with.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(Color color) => Fill(color.R, color.G, color.B, color.A);

    /// <summary>
    /// Fills the specified rectangle on the surface with the given color.
    /// </summary>
    /// <remarks>
    /// If there is a clip rectangle set on the surface, it will fill based on the intersection of the clip rectangle and rect.
    /// </remarks>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <param name="a">The alpha component of the color.</param>
    /// <param name="rect">The rectangle to fill.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(byte r, byte g, byte b, byte a, RectInt rect)
    {
        ThrowIfDisposed();

        uint rgba = PixelFormat.MapRGBA(Details, r, g, b, a);
        QuackNativeException.ThrowIfFailed(Native.SDL_FillSurfaceRect(Handle, &rect, rgba));
    }

    /// <summary>
    /// Fills the specified rectangle on the surface with the given color.
    /// </summary>
    /// <remarks>
    /// If there is a clip rectangle set on the surface, it will fill based on the intersection of the clip rectangle and rect.
    /// </remarks>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <param name="rect">The rectangle to fill.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(byte r, byte g, byte b, RectInt rect)
    {
        ThrowIfDisposed();

        uint rgb = PixelFormat.MapRGB(Details, r, g, b);
        QuackNativeException.ThrowIfFailed(Native.SDL_FillSurfaceRect(Handle, &rect, rgb));
    }

    /// <summary>
    /// Fills the specified rectangle on the surface with the given color.
    /// </summary>
    /// <remarks>
    /// If there is a clip rectangle set on the surface, it will fill based on the intersection of the clip rectangle and rect.
    /// </remarks>
    /// <param name="color">The color to fill the rectangle.</param>
    /// <param name="rect">The rectangle to fill.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(Color color, RectInt rect) => Fill(color.R, color.G, color.B, color.A, rect);

    /// <summary>
    /// Fills a set of rectangles on the surface with the given color.
    /// </summary>
    /// <remarks>
    /// If there is a clip rectangle set on the surface, it will fill based on the intersection of the clip rectangle and rect.
    /// </remarks>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <param name="a">The alpha component of the color.</param>
    /// <param name="rects">The rectangles to fill.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(byte r, byte g, byte b, byte a, params ReadOnlySpan<RectInt> rects)
    {
        ThrowIfDisposed();

        uint rgba = PixelFormat.MapRGBA(Details, r, g, b, a);
        QuackNativeException.ThrowIfFailed(Native.SDL_FillSurfaceRects(Handle, rects, rects.Length, rgba));
    }

    /// <summary>
    /// Fills a set of rectangles on the surface with the given color.
    /// </summary>
    /// <remarks>
    /// If there is a clip rectangle set on the surface, it will fill based on the intersection of the clip rectangle and rect.
    /// </remarks>
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <param name="rects">The rectangles to fill.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(byte r, byte g, byte b, params ReadOnlySpan<RectInt> rects)
    {
        ThrowIfDisposed();

        uint rgb = PixelFormat.MapRGB(Details, r, g, b);
        QuackNativeException.ThrowIfFailed(Native.SDL_FillSurfaceRects(Handle, rects, rects.Length, rgb));
    }

    /// <summary>
    /// Fills a set of rectangles on the surface with the given color.
    /// </summary>
    /// <remarks>
    /// If there is a clip rectangle set on the surface, it will fill based on the intersection of the clip rectangle and rect.
    /// </remarks>
    /// <param name="color">The color to fill the rectangle.</param>
    /// <param name="rects">The rectangles to fill.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Fill(Color color, params ReadOnlySpan<RectInt> rects) => Fill(color.R, color.G, color.B, color.A, rects);

    /// <summary>
    /// Flips the surface according to the specified mode.
    /// </summary>
    /// <param name="mode">The flip mode.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    public void Flip(FlipMode mode)
    {
        ThrowIfDisposed();
        QuackNativeException.ThrowIfFailed(Native.SDL_FlipSurface(Handle, mode));
    }

    /// <summary>
    /// Scales the surface to the specified width and height using the given scale mode.
    /// </summary>
    /// <param name="width">The target width.</param>
    /// <param name="height">The target height.</param>
    /// <param name="mode">The scale mode.</param>
    /// <returns>>The scaled surface.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the surface has been disposed.</exception>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public Surface Scale(int width, int height, ScaleMode mode)
    {
        ThrowIfDisposed();

        SDLSurface* handle = Native.SDL_ScaleSurface(Handle, width, height, mode);
        QuackNativeException.ThrowIfNull(handle);

        return new Surface(handle);
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(Handle is null, typeof(Surface));
}

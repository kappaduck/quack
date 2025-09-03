// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
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
/// <remarks>
/// Initializes a new instance of the <see cref="Surface"/>.
/// </remarks>
public sealed unsafe partial class Surface : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Surface"/>.
    /// </summary>
    /// <param name="width">The width of the surface.</param>
    /// <param name="height">The height of the surface.</param>
    /// <param name="format">The pixel format of the surface.</param>
    public Surface(int width, int height, PixelFormat format)
    {
        FormatDetails = format.Details;
        Handle = SDL_CreateSurface(width, height, format);

        QuackNativeException.ThrowIfNull(Handle);
    }

    internal Surface(SurfaceHandle* handle)
    {
        QuackNativeException.ThrowIfNull(handle);

        FormatDetails = handle->Format.Details;
        Handle = handle;
    }

    /// <summary>
    /// Gets the pixel format of the surface.
    /// </summary>
    public PixelFormat Format
    {
        get
        {
            ThrowIfDisposed();
            return Handle->Format;
        }
    }

    /// <summary>
    /// Gets the pixel format details of the surface.
    /// </summary>
    public PixelFormatDetails FormatDetails { get; private set; }

    /// <summary>
    /// Gets the native surface handle.
    /// </summary>
    internal SurfaceHandle* Handle { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the surface has a color key.
    /// </summary>
    public bool HasColorKey => SDL_SurfaceHasColorKey(Handle);

    /// <summary>
    /// Gets a value indicating whether the surface has RLE (Run-Length Encoding) enabled.
    /// </summary>
    public bool HasRLE => SDL_SurfaceHasRLE(Handle);

    /// <summary>
    /// Gets the height of the surface.
    /// </summary>
    public int Height
    {
        get
        {
            ThrowIfDisposed();
            return Handle->Height;
        }
    }

    /// <summary>
    /// Gets or sets the palette associated with the surface.
    /// </summary>
    public Palette? Palette
    {
        get
        {
            ThrowIfDisposed();

            Palette.PaletteHandle* palette = SDL_GetSurfacePalette(Handle);
            return palette is not null ? new Palette(palette) : null;
        }
        set
        {
            ThrowIfDisposed();

            Palette.PaletteHandle* palette = value is not null ? value.Handle : null;
            QuackNativeException.ThrowIfFailed(SDL_SetSurfacePalette(Handle, palette));
        }
    }

    /// <summary>
    /// Gets the pitch (row stride) of the surface.
    /// </summary>
    public int Pitch
    {
        get
        {
            ThrowIfDisposed();
            return Handle->Pitch;
        }
    }

    /// <summary>
    /// Gets the width of the surface.
    /// </summary>
    public int Width
    {
        get
        {
            ThrowIfDisposed();
            return Handle->Width;
        }
    }

    /// <summary>
    /// Creates a new surface identical to the current surface.
    /// </summary>
    /// <remarks>
    /// If the original surface has alternate images, the new surface will have a reference to them as well.
    /// </remarks>
    /// <returns>A new surface that is a duplicate of the current surface.</returns>
    public Surface Clone()
    {
        ThrowIfDisposed();

        SurfaceHandle* newHandle = SDL_DuplicateSurface(Handle);
        return new Surface(newHandle);
    }

    /// <summary>
    /// Copies the surface to a new surface with the specified pixel format.
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
    /// <param name="format">The pixel format to convert the surface to.</param>
    /// <returns>A new surface that is a copy of the original surface with the specified pixel format.</returns>
    public Surface Convert(PixelFormat format)
    {
        ThrowIfDisposed();

        SurfaceHandle* newHandle = SDL_ConvertSurface(Handle, format);
        return new Surface(newHandle);
    }

    /// <summary>
    /// Creates a palette and associates it with the surface.
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
    /// <returns>A new palette associated with the surface.</returns>
    public Palette CreatePalette()
    {
        ThrowIfDisposed();
        QuackException.ThrowIf(!IsIndexedFormat(Format), $"The format {Format} is not an indexed format.");

        return new Palette(Handle);

        static bool IsIndexedFormat(PixelFormat format)
        {
            return format is PixelFormat.Index1LSB
                   or PixelFormat.Index1MSB
                   or PixelFormat.Index4LSB
                   or PixelFormat.Index4MSB
                   or PixelFormat.Index8;
        }
    }

    /// <summary>
    /// Disposes the surface and releases any unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        if (Handle is not null)
        {
            SDL_DestroySurface(Handle);
            Handle = null;
        }
    }

    /// <summary>
    /// Fills the entire surface with the specified color.
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
    public void Fill(byte r, byte g, byte b, byte a)
    {
        ThrowIfDisposed();
        QuackNativeException.ThrowIfFailed(SDL_ClearSurface(Handle, r / 255f, g / 255f, b / 255f, a / 255f));
    }

    /// <summary>
    /// Fills the entire surface with the specified color.
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
    public void Fill(float r, float g, float b, float a)
    {
        ThrowIfDisposed();
        QuackNativeException.ThrowIfFailed(SDL_ClearSurface(Handle, r, g, b, a));
    }

    /// <summary>
    /// Fills the entire surface with the specified color.
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
    public void Fill(byte r, byte g, byte b) => Fill(r, g, b, 255);

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
    public void Fill(byte r, byte g, byte b, byte a, RectInt rect)
    {
        ThrowIfDisposed();

        uint rgba = PixelFormat.MapRGBA(FormatDetails, r, g, b, a);
        QuackNativeException.ThrowIfFailed(SDL_FillSurfaceRect(Handle, &rect, rgba));
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
    public void Fill(byte r, byte g, byte b, RectInt rect)
    {
        ThrowIfDisposed();

        uint rgb = PixelFormat.MapRGB(FormatDetails, r, g, b);
        QuackNativeException.ThrowIfFailed(SDL_FillSurfaceRect(Handle, &rect, rgb));
    }

    /// <summary>
    /// Fills the specified rectangle on the surface with the given color.
    /// </summary>
    /// <remarks>
    /// If there is a clip rectangle set on the surface, it will fill based on the intersection of the clip rectangle and rect.
    /// </remarks>
    /// <param name="color">The color to fill the rectangle with.</param>
    /// <param name="rect">The rectangle to fill.</param>
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
    public void Fill(byte r, byte g, byte b, byte a, params ReadOnlySpan<RectInt> rects)
    {
        ThrowIfDisposed();

        uint rgba = PixelFormat.MapRGBA(FormatDetails, r, g, b, a);
        QuackNativeException.ThrowIfFailed(SDL_FillSurfaceRects(Handle, rects, rects.Length, rgba));
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
    public void Fill(byte r, byte g, byte b, params ReadOnlySpan<RectInt> rects)
    {
        ThrowIfDisposed();

        uint rgb = PixelFormat.MapRGB(FormatDetails, r, g, b);
        QuackNativeException.ThrowIfFailed(SDL_FillSurfaceRects(Handle, rects, rects.Length, rgb));
    }

    /// <summary>
    /// Fills a set of rectangles on the surface with the given color.
    /// </summary>
    /// <remarks>
    /// If there is a clip rectangle set on the surface, it will fill based on the intersection of the clip rectangle and rect.
    /// </remarks>
    /// <param name="color">The color to fill the rectangle with.</param>
    /// <param name="rects">The rectangles to fill.</param>
    public void Fill(Color color, params ReadOnlySpan<RectInt> rects) => Fill(color.R, color.G, color.B, color.A, rects);

    /// <summary>
    /// Flip a surface vertically or horizontally.
    /// </summary>
    /// <param name="mode">The flip mode to apply.</param>
    public void Flip(FlipMode mode)
    {
        ThrowIfDisposed();
        QuackNativeException.ThrowIfFailed(SDL_FlipSurface(Handle, mode));
    }

    /// <summary>
    /// Scales the surface to the specified dimensions using the given scaling mode.
    /// </summary>
    /// <param name="width">The target width of the scaled surface.</param>
    /// <param name="height">The target height of the scaled surface.</param>
    /// <param name="mode">The scaling mode to use.</param>
    /// <returns>A new surface representing the scaled version of the original surface.</returns>
    public Surface Scale(int width, int height, ScaleMode mode)
    {
        ThrowIfDisposed();

        SurfaceHandle* scaledSurface = SDL_ScaleSurface(Handle, width, height, mode);
        QuackNativeException.ThrowIfNull(scaledSurface);

        return new Surface(scaledSurface);
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(Handle is null, typeof(Surface));
}

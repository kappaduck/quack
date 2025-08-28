// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

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
/// <param name="width">The width of the surface.</param>
/// <param name="height">The height of the surface.</param>
/// <param name="format">The pixel format of the surface.</param>
public sealed unsafe partial class Surface(int width, int height, PixelFormat format) : IDisposable
{
    /// <summary>
    /// Gets the pixel format of the surface.
    /// </summary>
    public PixelFormat Format => Handle->Format;

    /// <summary>
    /// Gets the pixel format details of the surface.
    /// </summary>
    public PixelFormatDetails FormatDetails { get; private set; } = format.Details;

    /// <summary>
    /// Gets the native surface handle.
    /// </summary>
    internal SurfaceHandle* Handle { get; } = SDL_CreateSurface(width, height, format);

    /// <summary>
    /// Gets the height of the surface.
    /// </summary>
    public int Height => Handle->Height;

    /// <summary>
    /// Gets the pitch (row stride) of the surface.
    /// </summary>
    public int Pitch => Handle->Pitch;

    /// <summary>
    /// Gets the width of the surface.
    /// </summary>
    public int Width => Handle->Width;

    /// <summary>
    /// Disposes the surface and releases any unmanaged resources.
    /// </summary>
    public void Dispose() => SDL_DestroySurface(Handle);

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
    public void Fill(byte r, byte g, byte b, byte a) => SDL_ClearSurface(Handle, r / 255f, g / 255f, b / 255f, a / 255f);

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
    public void Fill(float r, float g, float b, float a) => SDL_ClearSurface(Handle, r, g, b, a);

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
        uint rgba = PixelFormat.MapRGBA(FormatDetails, r, g, b, a);
        SDL_FillSurfaceRect(Handle, &rect, rgba);
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
        uint rgb = PixelFormat.MapRGB(FormatDetails, r, g, b);
        SDL_FillSurfaceRect(Handle, &rect, rgb);
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
        uint rgba = PixelFormat.MapRGBA(FormatDetails, r, g, b, a);
        SDL_FillSurfaceRects(Handle, rects, rects.Length, rgba);
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
        uint rgb = PixelFormat.MapRGB(FormatDetails, r, g, b);
        SDL_FillSurfaceRects(Handle, rects, rects.Length, rgb);
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
}

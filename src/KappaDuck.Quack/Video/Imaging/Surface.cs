// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Interop.SDL.Primitives;
using System.Buffers;
using System.ComponentModel;

namespace KappaDuck.Quack.Video.Imaging;

/// <summary>
/// A collection of pixels held in regular memory, used for software image manipulation and blitting.
/// </summary>
public sealed class Surface : IDisposable
{
    private readonly uint _properties;
    private readonly bool _owned;
    private readonly MemoryHandle _pixels;

    /// <summary>
    /// Creates a surface of the given size and pixel format with its pixels zero-initialized.
    /// </summary>
    /// <param name="size">The size in pixels</param>
    /// <param name="format">The pixel format</param>
    /// <exception cref="ArgumentOutOfRangeException"><see cref="Size.Width"/> or <see cref="Size.Height"/> is negative or zero.</exception>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    public Surface(Size size, PixelFormat format) : this(size.Width, size.Height, format)
    {
    }

    /// <summary>
    /// Creates a surface of the given size and pixel format with its pixels zero-initialized.
    /// </summary>
    /// <param name="width">The width in pixels.</param>
    /// <param name="height">The height in pixels.</param>
    /// <param name="format">The pixel format</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> is negative.</exception>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    public Surface(int width, int height, PixelFormat format)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

        unsafe
        {
            Handle = SDL3.CreateSurface(width, height, format);
            SDLThrowHelper.ThrowIfNull(Handle);

            _properties = SDL3.GetSurfaceProperties(Handle);

            Pitch = Handle->Pitch;
            MustLock = (Handle->State & SDL_SurfaceState.LockNeeded) != SDL_SurfaceState.None;
        }

        Width = width;
        Height = height;
        Format = format;

        _owned = true;
    }

    internal unsafe Surface(SDL_Surface* surface, bool owned = true)
    {
        SDLThrowHelper.ThrowIfNull(surface);

        Handle = surface;
        _owned = owned;

        _properties = SDL3.GetSurfaceProperties(Handle);
    }

    private unsafe Surface(SDL_Surface* surface, MemoryHandle handle)
    {
        Handle = surface;
        _owned = true;
        _pixels = handle;

        _properties = SDL3.GetSurfaceProperties(Handle);
    }

    /// <summary>
    /// Gets or sets the alpha modulation applied when this surface is blitted, where 255 is fully opaque.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get or set the alpha modulation.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public byte AlphaModulation
    {
        get
        {
            ThrowIfDisposed();

            byte alpha;
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetSurfaceAlphaMod(Handle, &alpha)));

            return alpha;
        }

        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetSurfaceAlphaMod(Handle, value)));
        }
    }

    /// <summary>
    /// Gets or sets the blend mode used when this surface is blitted onto another.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get or set the blend mode.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public BlendMode BlendMode
    {
        get
        {
            ThrowIfDisposed();

            BlendMode mode;
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetSurfaceBlendMode(Handle, &mode)));

            return mode;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetSurfaceBlendMode(Handle, value)));
        }
    }

    /// <summary>
    /// Gets or sets the clipping rectangle that limits where blits and fills can draw on the surface.
    /// </summary>
    /// <remarks>
    /// The clip rectangle is intersected with the surface bounds. Setting a rectangle that does not overlap the
    /// surface clips drawing entirely. To remove the clip pass <see langword="null"/> to clear it back to the whole surface.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to set the clipping rectangle.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public RectI? Clip
    {
        get;
        set
        {
            ThrowIfDisposed();

            RectI clip = value.GetValueOrDefault();
            RectI* rect = null;

            if (value.HasValue)
                rect = &clip;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetSurfaceClipRect(Handle, rect)));
            field = value;
        }
    }

    /// <summary>
    /// Gets or sets the color key, the color treated as transparent when this surface is blitted.
    /// </summary>
    /// <remarks>
    /// Passing <see langword="null"/> will disable the color key.
    /// </remarks>
    /// <remarks>Matching is done on the red, green and blue channels; the alpha channel is ignored.</remarks>
    /// <exception cref="QuackInteropException">Failed to set the color key.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Color? ColorKey
    {
        get;
        set
        {
            ThrowIfDisposed();

            unsafe
            {
                if (value.HasValue)
                {
                    uint key = SDL3.MapSurfaceRGB(Handle, value.Value.R, value.Value.G, value.Value.B);
                    SDLThrowHelper.ThrowIfFailed(SDL3.SetSurfaceColorKey(Handle, true, key));
                }
                else
                {
                    SDLThrowHelper.ThrowIfFailed(SDL3.SetSurfaceColorKey(Handle, false, 0));
                }
            }

            field = value;
        }
    }

    /// <summary>
    /// Gets or sets the color modulation applied when this surface is blitted.
    /// </summary>
    /// <remarks>
    /// Only the red, green and blue channels are used. The alpha channel is ignored; use <see cref="AlphaModulation"/> for alpha.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the color modulation.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Color ColorModulation
    {
        get
        {
            ThrowIfDisposed();

            byte r, g, b;
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetSurfaceColorMod(Handle, &r, &g, &b)));

            return new Color(r, g, b);
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetSurfaceColorMod(Handle, value.R, value.G, value.B)));
        }
    }

    /// <summary>
    /// Gets or sets the colorspace used to interpret the surface's pixels.
    /// </summary>
    /// <remarks>Setting this does not convert the pixels; use <see cref="Convert(PixelFormat, Colorspace, Palette?)"/> to convert.</remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the colorspace.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Colorspace Colorspace
    {
        get
        {
            ThrowIfDisposed();
            return unsafe (SDL3.GetSurfaceColorspace(Handle));
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetSurfaceColorspace(Handle, value)));
        }
    }

    /// <summary>
    /// Gets the pixel format of the surface.
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// Gets or sets the maximum dynamic range used by the content, in terms of the SDR white point.
    /// </summary>
    /// <remarks>Defaults to 0, which disables tone mapping.</remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the HDR.</exception>
    public float HDR
    {
        get => Properties.Get(_properties, "SDL.surface.HDR_headroom", 0.0f);
        set => Properties.Set(_properties, "SDL.surface.HDR_headroom", value);
    }

    /// <summary>
    /// Gets the height of the surface in pixels.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets or sets the hotspot offset to use when the surface is used as a cursor.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get or set the hotspot.</exception>
    public Point Hotspot
    {
        get
        {
            int x = Properties.Get(_properties, "SDL.surface.hotspot.x", 0);
            int y = Properties.Get(_properties, "SDL.surface.hotspot.y", 0);

            return new Point(x, y);
        }
        set
        {
            Properties.Set(_properties, "SDL.surface.hotspot.x", value.X);
            Properties.Set(_properties, "SDL.surface.hotspot.y", value.Y);
        }
    }

    /// <summary>
    /// Gets every version of this surface, with this surface as the first element followed by its alternate images.
    /// </summary>
    /// <remarks>
    /// The returned surfaces are owned by this surface; they are views and disposing them does nothing. They become
    /// invalid once the alternate images are removed or this surface is disposed.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get the images.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public IReadOnlyList<Surface> Images
    {
        get
        {
            ThrowIfDisposed();

            unsafe
            {
                int count;
                SDL_Surface** images = SDL3.GetSurfaceImages(Handle, &count);
                SDLThrowHelper.ThrowIfNull(images);

                try
                {
                    Surface[] result = new Surface[count];

                    for (int i = 0; i < count; i++)
                        result[i] = new Surface(images[i], owned: false);

                    return result;
                }
                finally
                {
                    SDL3.Free(images);
                }
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether the surface must be locked before its pixels can be accessed.
    /// </summary>
    public bool MustLock { get; }

    /// <summary>
    /// Gets or sets the palette associated with the surface.
    /// </summary>
    /// <remarks>
    /// The returned palette is owned by the surface; it is a view and disposing it does nothing. It becomes invalid
    /// once the palette is replaced or the surface is disposed.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the palette.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Palette? Palette
    {
        get
        {
            ThrowIfDisposed();

            unsafe
            {
                SDL_Palette* palette = SDL3.GetSurfacePalette(Handle);
                return palette is null ? null : new Palette(palette);
            }
        }
        set
        {
            ThrowIfDisposed();

            unsafe
            {
                SDL_Palette* palette = value is null ? null : value.Handle;
                SDLThrowHelper.ThrowIfFailed(SDL3.SetSurfacePalette(Handle, palette));
            }
        }
    }

    /// <summary>
    /// Gets the distance in bytes between the start of one row of pixels and the next.
    /// </summary>
    public int Pitch { get; }

    /// <summary>
    /// Gets or sets the surface is meant to be rotated clockwise to make the image right-side up.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get or set the rotation.</exception>
    public Angle Rotation
    {
        get => Angle.FromDegrees(Properties.Get(_properties, "SDL.surface.rotation", 0.0f));
        set => Properties.Set(_properties, "SDL.surface.rotation", value.Degrees);
    }

    /// <summary>
    /// Gets or sets the value of 100% diffuse white for HDR10 and floating-point surfaces.
    /// </summary>
    /// <remarks>Higher values are displayed in the HDR headroom. Defaults to 203 for HDR10 surfaces and 1 for floating-point surfaces.</remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the SDR white point.</exception>
    public float SDR
    {
        get => Properties.Get(_properties, "SDL.surface.SDR_white_point", 1.0f);
        set => Properties.Set(_properties, "SDL.surface.SDR_white_point", value);
    }

    /// <summary>
    /// Gets or sets the tone mapping operator used when compressing from high dynamic range to a lower range.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="ToneMap.Chrome"/> (the default),
    /// <see cref="ToneMap.LinearSpaceScaleFactor(float)"/> (<c>"*=N"</c>) where N is a linear-space scale factor,
    /// and <see cref="ToneMap.None"/> to disable tone mapping.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the tone mapping.</exception>
    public string ToneMapOperator
    {
        get => Properties.Get(_properties, "SDL.surface.tonemap", "chrome");
        set => Properties.Set(_properties, "SDL.surface.tonemap", value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether run-length encoding acceleration is enabled for color-key and alpha blits.
    /// </summary>
    /// <remarks>
    /// While enabled the surface is treated as encoded and its pixels should be accessed through <see cref="Lock"/>.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the RLE.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public bool UseRLE
    {
        get
        {
            ThrowIfDisposed();
            return unsafe (SDL3.SurfaceHasRLE(Handle));
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetSurfaceRLE(Handle, value)));
        }
    }

    /// <summary>
    /// Gets the width of the surface in pixels.
    /// </summary>
    public int Width { get; }

    internal SDL_Surface* Handle { get; private set; }

    /// <summary>
    /// Attaches an alternate version of this image, typically a higher-resolution variant used on high-DPI displays.
    /// </summary>
    /// <remarks>The surface takes a reference to <paramref name="image"/>, so it remains valid even if the original is disposed.</remarks>
    /// <param name="image">The alternate image to attach.</param>
    /// <exception cref="ObjectDisposedException">The surface or the image is disposed.</exception>
    public void AddAlternateImage(Surface image)
    {
        ThrowIfDisposed();
        image.ThrowIfDisposed();

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.AddSurfaceAlternateImage(Handle, image.Handle)));
    }

    /// <summary>
    /// Copies pixels from a source surface onto this surface.
    /// </summary>
    /// <remarks>
    /// The blit is one-to-one; only the position of <paramref name="destination"/> is used. Use
    /// <see cref="BlitScaled"/> to resize while copying. Neither surface should be locked during a blit.
    /// </remarks>
    /// <param name="source">The surface to copy from.</param>
    /// <param name="region">The region of <paramref name="source"/> to copy, or <see langword="null"/> for all of it.</param>
    /// <param name="destination">The position in this surface to copy to, or <see langword="null"/> for the origin.</param>
    /// <exception cref="QuackInteropException">Failed to blit the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface or source is disposed.</exception>
    public void Blit(Surface source, RectI? region = null, RectI? destination = null)
    {
        ThrowIfDisposed();
        source.ThrowIfDisposed();

        RectI src = region.GetValueOrDefault();
        RectI dst = destination.GetValueOrDefault();

        RectI* sourceRect = null;
        RectI* destinationRect = null;

        if (region.HasValue)
            sourceRect = &src;

        if (destination.HasValue)
            destinationRect = &dst;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.BlitSurface(source.Handle, sourceRect, Handle, destinationRect)));
    }

    /// <summary>
    /// Copies pixels from a source surface onto this surface, scaling to fit the destination region.
    /// </summary>
    /// <param name="source">The surface to copy from.</param>
    /// <param name="scaleMode">The filtering to use while scaling.</param>
    /// <param name="region">The region of <paramref name="source"/> to copy, or <see langword="null"/> for all of it.</param>
    /// <param name="destination">The region of this surface to copy to, or <see langword="null"/> for the whole surface.</param>
    /// <exception cref="QuackInteropException">Failed to blit the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface or source is disposed.</exception>
    public void BlitScaled(Surface source, ScaleMode scaleMode, RectI? region = null, RectI? destination = null)
    {
        ThrowIfDisposed();
        source.ThrowIfDisposed();

        RectI src = region.GetValueOrDefault();
        RectI dst = destination.GetValueOrDefault();

        RectI* sourceRect = null;
        RectI* destinationRect = null;

        if (region.HasValue)
            sourceRect = &src;

        if (destination.HasValue)
            destinationRect = &dst;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.BlitSurfaceScaled(source.Handle, sourceRect, Handle, destinationRect, scaleMode)));
    }

    /// <summary>
    /// Copies pixels from a source surface onto this surface, repeating them to fill the destination region.
    /// </summary>
    /// <param name="source">The surface to copy from.</param>
    /// <param name="region">The region of <paramref name="source"/> to tile, or <see langword="null"/> for all of it.</param>
    /// <param name="destination">The region of this surface to fill, or <see langword="null"/> for the whole surface.</param>
    /// <exception cref="QuackInteropException">Failed to blit the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface or source is disposed.</exception>
    public void BlitTiled(Surface source, RectI? region = null, RectI? destination = null)
    {
        ThrowIfDisposed();
        source.ThrowIfDisposed();

        RectI src = region.GetValueOrDefault();
        RectI dst = destination.GetValueOrDefault();

        RectI* sourceRect = null;
        RectI* destinationRect = null;

        if (region.HasValue)
            sourceRect = &src;

        if (destination.HasValue)
            destinationRect = &dst;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.BlitSurfaceTiled(source.Handle, sourceRect, Handle, destinationRect)));
    }

    /// <summary>
    /// Copies pixels from a source surface onto this surface, scaling each tile then repeating it to fill the destination region.
    /// </summary>
    /// <param name="source">The surface to copy from.</param>
    /// <param name="scale">The scale applied to each tile; for example 2 turns a 32x32 region into 64x64 tiles.</param>
    /// <param name="scaleMode">The filtering to use while scaling.</param>
    /// <param name="region">The region of <paramref name="source"/> to tile, or <see langword="null"/> for all of it.</param>
    /// <param name="destination">The region of this surface to fill, or <see langword="null"/> for the whole surface.</param>
    /// <exception cref="QuackInteropException">Failed to blit the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface or source is disposed.</exception>
    public void BlitTiledScaled(Surface source, float scale, ScaleMode scaleMode, RectI? region = null, RectI? destination = null)
    {
        ThrowIfDisposed();
        source.ThrowIfDisposed();

        RectI src = region.GetValueOrDefault();
        RectI dst = destination.GetValueOrDefault();

        RectI* sourceRect = null;
        RectI* destinationRect = null;

        if (region.HasValue)
            sourceRect = &src;

        if (destination.HasValue)
            destinationRect = &dst;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.BlitSurfaceTiledWithScale(source.Handle, sourceRect, scale, scaleMode, Handle, destinationRect)));
    }

    /// <summary>
    /// Copies pixels from a source surface onto this surface using a 9-grid (nine-patch) layout.
    /// </summary>
    /// <remarks>
    /// The source is divided into a 3x3 grid: the four corners are scaled and placed into the corners of the
    /// destination, while the edges and center stretch to cover the remaining space.
    /// </remarks>
    /// <param name="source">The surface to copy from.</param>
    /// <param name="leftWidth">The width, in pixels, of the left corners.</param>
    /// <param name="rightWidth">The width, in pixels, of the right corners.</param>
    /// <param name="topHeight">The height, in pixels, of the top corners.</param>
    /// <param name="bottomHeight">The height, in pixels, of the bottom corners.</param>
    /// <param name="scale">The scale applied to the corners, or 0 for an unscaled blit.</param>
    /// <param name="scaleMode">The filtering to use while scaling.</param>
    /// <param name="region">The region of <paramref name="source"/> to use, or <see langword="null"/> for all of it.</param>
    /// <param name="destination">The region of this surface to fill, or <see langword="null"/> for the whole surface.</param>
    /// <exception cref="QuackInteropException">Failed to blit the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface or source is disposed.</exception>
    public void Blit9Grid(Surface source, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode scaleMode, RectI? region = null, RectI? destination = null)
    {
        ThrowIfDisposed();
        source.ThrowIfDisposed();

        RectI src = region.GetValueOrDefault();
        RectI dst = destination.GetValueOrDefault();

        RectI* sourceRect = null;
        RectI* destinationRect = null;

        if (region.HasValue)
            sourceRect = &src;

        if (destination.HasValue)
            destinationRect = &dst;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.BlitSurface9Grid(source.Handle, sourceRect, leftWidth, rightWidth, topHeight, bottomHeight, scale, scaleMode, Handle, destinationRect)));
    }

    /// <summary>
    /// Copies pixels from a source surface onto this surface without clipping or validation.
    /// </summary>
    /// <remarks>
    /// This skips the clipping and bounds checks that <see cref="Blit"/> performs. Both regions must be valid and
    /// the same size; passing regions outside either surface results in undefined behavior. Prefer <see cref="Blit"/>.
    /// </remarks>
    /// <param name="source">The surface to copy from.</param>
    /// <param name="region">The region of <paramref name="source"/> to copy.</param>
    /// <param name="destination">The region of this surface to copy to.</param>
    /// <exception cref="QuackInteropException">Failed to blit the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface or source is disposed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void BlitUnchecked(Surface source, RectI region, RectI destination)
    {
        ThrowIfDisposed();
        source.ThrowIfDisposed();

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.BlitSurfaceUnchecked(source.Handle, &region, Handle, &destination)));
    }

    /// <summary>
    /// Copies and scales pixels from a source surface onto this surface without clipping or validation.
    /// </summary>
    /// <remarks>
    /// This skips the clipping and bounds checks that <see cref="BlitScaled"/> performs. Both regions must be valid;
    /// passing regions outside either surface results in undefined behavior. Prefer <see cref="BlitScaled"/>.
    /// </remarks>
    /// <param name="source">The surface to copy from.</param>
    /// <param name="region">The region of <paramref name="source"/> to copy.</param>
    /// <param name="destination">The region of this surface to copy to.</param>
    /// <param name="scaleMode">The filtering to use while scaling.</param>
    /// <exception cref="QuackInteropException">Failed to blit the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface or source is disposed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void BlitUncheckedScaled(Surface source, RectI region, RectI destination, ScaleMode scaleMode)
    {
        ThrowIfDisposed();
        source.ThrowIfDisposed();

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.BlitSurfaceUncheckedScaled(source.Handle, &region, Handle, &destination, scaleMode)));
    }

    /// <summary>
    /// Clears the entire surface to the given color.
    /// </summary>
    /// <param name="color">The color to fill the surface with.</param>
    /// <exception cref="QuackInteropException">Failed to clear the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void Clear(ColorF color)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.ClearSurface(Handle, color.R, color.G, color.B, color.A)));
    }

    /// <summary>
    /// Creates a new surface with the same pixels converted to a different format.
    /// </summary>
    /// <param name="format">The pixel format of the new surface.</param>
    /// <returns>A new surface in the requested format. The caller owns it and must dispose it.</returns>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Surface Convert(PixelFormat format)
    {
        ThrowIfDisposed();

        unsafe
        {
            return new(SDL3.ConvertSurface(Handle, format));
        }
    }

    /// <summary>
    /// Creates a new surface with the same pixels converted to a different format and colorspace.
    /// </summary>
    /// <param name="format">The pixel format of the new surface.</param>
    /// <param name="colorspace">The colorspace of the new surface.</param>
    /// <param name="palette">An optional palette to use for indexed formats.</param>
    /// <returns>A new surface in the requested format and colorspace. The caller owns it and must dispose it.</returns>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Surface Convert(PixelFormat format, Colorspace colorspace, Palette? palette = null)
    {
        ThrowIfDisposed();

        unsafe
        {
            SDL_Surface* handle = SDL3.ConvertSurfaceAndColorspace(Handle, format, palette?.Handle, colorspace, _properties);
            return new(handle);
        }
    }

    /// <summary>
    /// Creates a surface of an existing block of pixel memory without copying it.
    /// </summary>
    /// <remarks>
    /// The surface aliases <paramref name="pixels"/>: writes to either are visible to the other. The memory is pinned
    /// for the lifetime of the surface and released when it is disposed, so the surface must be disposed before the
    /// memory is reused. Rows are <paramref name="pitch"/> bytes apart; pass 0 for tightly packed rows.
    /// </remarks>
    /// <param name="pixels">The pixel memory to wrap.</param>
    /// <param name="dimension">The dimension in pixels.</param>
    /// <param name="format">The pixel format of the data.</param>
    /// <param name="pitch">The number of bytes per row in <paramref name="pixels"/>, or 0 for tightly packed rows.</param>
    /// <returns>A new surface that aliases the supplied memory.</returns>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    /// <exception cref="ArgumentOutOfRangeException">A dimension or pitch is invalid, or <paramref name="pixels"/> is too small.</exception>
    public static Surface Create(Memory<byte> pixels, Size dimension, PixelFormat format, int pitch = 0) => Create(pixels, dimension.Width, dimension.Height, format, pitch);

    /// <summary>
    /// Creates a surface of an existing block of pixel memory without copying it.
    /// </summary>
    /// <remarks>
    /// The surface aliases <paramref name="pixels"/>: writes to either are visible to the other. The memory is pinned
    /// for the lifetime of the surface and released when it is disposed, so the surface must be disposed before the
    /// memory is reused. Rows are <paramref name="pitch"/> bytes apart; pass 0 for tightly packed rows.
    /// </remarks>
    /// <param name="pixels">The pixel memory to wrap.</param>
    /// <param name="width">The width in pixels.</param>
    /// <param name="height">The height in pixels.</param>
    /// <param name="format">The pixel format of the data.</param>
    /// <param name="pitch">The number of bytes per row in <paramref name="pixels"/>, or 0 for tightly packed rows.</param>
    /// <returns>A new surface that aliases the supplied memory.</returns>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    /// <exception cref="ArgumentOutOfRangeException">A dimension or pitch is invalid, or <paramref name="pixels"/> is too small.</exception>
    public static Surface Create(Memory<byte> pixels, int width, int height, PixelFormat format, int pitch = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(width);
        ArgumentOutOfRangeException.ThrowIfNegative(height);

        int rowBytes = width * format.BytesPerPixel;
        int actualPitch = pitch == 0 ? rowBytes : pitch;

        ArgumentOutOfRangeException.ThrowIfLessThan(actualPitch, rowBytes);
        ArgumentOutOfRangeException.ThrowIfLessThan(pixels.Length, actualPitch * height);

        MemoryHandle handle = pixels.Pin();

        unsafe
        {
            SDL_Surface* surface;
            try
            {
                surface = SDL3.CreateSurfaceFrom(width, height, format, handle.Pointer, actualPitch);
                SDLThrowHelper.ThrowIfNull(surface);
            }
            catch
            {
                handle.Dispose();
                throw;
            }

            return new Surface(surface, handle);
        }
    }

    /// <summary>
    /// Creates a surface of the given size and format, copying the supplied pixel data into it.
    /// </summary>
    /// <remarks>
    /// The pixel data is copied, so the source buffer does not need to remain valid after this call. Rows in
    /// <paramref name="pixels"/> are <paramref name="pitch"/> bytes apart; pass 0 for tightly packed rows.
    /// </remarks>
    /// <param name="pixels">The source pixel data to copy.</param>
    /// <param name="dimension">The dimension in pixels.</param>
    /// <param name="format">The pixel format of the data.</param>
    /// <param name="pitch">The number of bytes per row in <paramref name="pixels"/>, or 0 for tightly packed rows.</param>
    /// <returns>A new surface containing a copy of the pixel data.</returns>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    /// <exception cref="ArgumentOutOfRangeException">A dimension or pitch is invalid, or <paramref name="pixels"/> is too small.</exception>
    public static Surface Create(ReadOnlySpan<byte> pixels, Size dimension, PixelFormat format, int pitch = 0) => Create(pixels, dimension.Width, dimension.Height, format, pitch);

    /// <summary>
    /// Creates a surface of the given size and format, copying the supplied pixel data into it.
    /// </summary>
    /// <remarks>
    /// The pixel data is copied, so the source buffer does not need to remain valid after this call. Rows in
    /// <paramref name="pixels"/> are <paramref name="pitch"/> bytes apart; pass 0 for tightly packed rows.
    /// </remarks>
    /// <param name="pixels">The source pixel data to copy.</param>
    /// <param name="width">The width in pixels.</param>
    /// <param name="height">The height in pixels.</param>
    /// <param name="format">The pixel format of the data.</param>
    /// <param name="pitch">The number of bytes per row in <paramref name="pixels"/>, or 0 for tightly packed rows.</param>
    /// <returns>A new surface containing a copy of the pixel data.</returns>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    /// <exception cref="ArgumentOutOfRangeException">A dimension or pitch is invalid, or <paramref name="pixels"/> is too small.</exception>
    public static Surface Create(ReadOnlySpan<byte> pixels, int width, int height, PixelFormat format, int pitch = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(width);
        ArgumentOutOfRangeException.ThrowIfNegative(height);

        int rowBytes = width * format.BytesPerPixel;
        int sourcePitch = pitch == 0 ? rowBytes : pitch;

        ArgumentOutOfRangeException.ThrowIfLessThan(sourcePitch, rowBytes);
        ArgumentOutOfRangeException.ThrowIfLessThan(pixels.Length, sourcePitch * height);

        Surface surface = new(width, height, format);

        using (SurfaceLock locked = surface.Lock())
        {
            Span<byte> destination = locked.Pixels;
            int destinationPitch = surface.Pitch;

            for (int y = 0; y < height; y++)
            {
                ReadOnlySpan<byte> sourceRow = pixels.Slice(y * sourcePitch, rowBytes);
                sourceRow.CopyTo(destination.Slice(y * destinationPitch, rowBytes));
            }
        }

        return surface;
    }

    /// <summary>
    /// Creates a new palette, sized for the surface's format, and associates it with the surface.
    /// </summary>
    /// <remarks>
    /// The returned palette is owned by the surface; it is a view and disposing it does nothing. It becomes invalid
    /// once the palette is replaced or the surface is disposed.
    /// </remarks>
    /// <returns>The newly created palette.</returns>
    /// <exception cref="QuackInteropException">Failed to create the palette.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Palette CreatePalette()
    {
        ThrowIfDisposed();

        unsafe
        {
            SDL_Palette* palette = SDL3.CreateSurfacePalette(Handle);
            SDLThrowHelper.ThrowIfNull(palette);

            return new Palette(palette);
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        unsafe
        {
            if (Handle is null)
                return;

            if (_owned)
                SDL3.DestroySurface(Handle);

            Handle = null;
        }

        _pixels.Dispose();
    }

    /// <summary>
    /// Creates a copy of this surface with the same format and pixels.
    /// </summary>
    /// <returns>A new surface identical to this one. The caller owns it and must dispose it.</returns>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Surface Duplicate()
    {
        ThrowIfDisposed();

        unsafe
        {
            return new Surface(SDL3.DuplicateSurface(Handle));
        }
    }

    /// <summary>
    /// Fills the entire surface with a single color.
    /// </summary>
    /// <param name="color">The color to fill with.</param>
    /// <exception cref="QuackInteropException">Failed to fill the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void Fill(Color color)
    {
        ThrowIfDisposed();

        unsafe
        {
            uint pixel = SDL3.MapSurfaceRGBA(Handle, color.R, color.G, color.B, color.A);
            SDLThrowHelper.ThrowIfFailed(SDL3.FillSurfaceRect(Handle, null, pixel));
        }
    }

    /// <summary>
    /// Fills a rectangular region of the surface with a single color.
    /// </summary>
    /// <param name="area">The region to fill.</param>
    /// <param name="color">The color to fill with.</param>
    /// <exception cref="QuackInteropException">Failed to fill the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void Fill(RectI area, Color color)
    {
        ThrowIfDisposed();

        unsafe
        {
            uint pixel = SDL3.MapSurfaceRGBA(Handle, color.R, color.G, color.B, color.A);
            SDLThrowHelper.ThrowIfFailed(SDL3.FillSurfaceRect(Handle, &area, pixel));
        }
    }

    /// <summary>
    /// Fills several rectangular regions of the surface with a single color.
    /// </summary>
    /// <param name="areas">The regions to fill.</param>
    /// <param name="color">The color to fill with.</param>
    /// <exception cref="QuackInteropException">Failed to fill the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void Fill(ReadOnlySpan<RectI> areas, Color color)
    {
        ThrowIfDisposed();

        unsafe
        {
            uint pixel = SDL3.MapSurfaceRGBA(Handle, color.R, color.G, color.B, color.A);
            SDLThrowHelper.ThrowIfFailed(SDL3.FillSurfaceRects(Handle, areas, areas.Length, pixel));
        }
    }

    /// <summary>
    /// Flips the surface in place along the given axes.
    /// </summary>
    /// <param name="flip">The axes to flip along.</param>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void Flip(FlipMode flip)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.FlipSurface(Handle, flip)));
    }

    /// <summary>
    /// Loads an image from a file.
    /// </summary>
    /// <param name="path">The path to the image file.</param>
    /// <returns>The loaded image.</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="QuackInteropException">Thrown when failed to load the image.</exception>
    public static Surface FromFile(string path)
    {
        if (!File.Exists(path))
            ThrowHelper.ThrowFileNotFound("The file path does not exist.", path);

        unsafe
        {
            SDL_Surface* handle = SDL3_image.FromFile(path);
            return new Surface(handle, true);
        }
    }

    /// <summary>
    /// Loads an image from a stream.
    /// </summary>
    /// <param name="stream">The stream to read the image from.</param>
    /// <returns>The loaded image.</returns>
    /// <exception cref="QuackInteropException">Thrown when failed to load the image.</exception>
    public static Surface FromStream(Stream stream)
    {
        using IOStream source = IOStream.FromStream(stream);

        unsafe
        {
            SDL_Surface* handle = SDL3_image.FromStream(source.Handle, false);
            return new Surface(handle, true);
        }
    }

    /// <summary>
    /// Locks the surface and returns a scope that exposes its pixel buffer for direct access.
    /// </summary>
    /// <remarks>
    /// Dispose the returned scope to unlock the surface. The exposed buffer is only valid for the lifetime of the
    /// scope. Not every surface requires locking; see <see cref="MustLock"/>.
    /// </remarks>
    /// <returns>A scope that exposes the pixel buffer and unlocks the surface when disposed.</returns>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public SurfaceLock Lock()
    {
        ThrowIfDisposed();
        return new SurfaceLock(this);
    }

    /// <summary>
    /// Maps an opaque color to a pixel value for this surface's format and palette.
    /// </summary>
    /// <remarks>The alpha channel is ignored; if the format has an alpha channel it is set to fully opaque.</remarks>
    /// <param name="color">The color to map.</param>
    /// <returns>The pixel value packed for the surface format.</returns>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public uint MapRGB(Color color)
    {
        ThrowIfDisposed();

        unsafe
        {
            return SDL3.MapSurfaceRGB(Handle, color.R, color.G, color.B);
        }
    }

    /// <summary>
    /// Maps a color, including its alpha channel, to a pixel value for this surface's format and palette.
    /// </summary>
    /// <param name="color">The color to map.</param>
    /// <returns>The pixel value packed for the surface format.</returns>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public uint MapRGBA(Color color)
    {
        ThrowIfDisposed();

        unsafe
        {
            return SDL3.MapSurfaceRGBA(Handle, color.R, color.G, color.B, color.A);
        }
    }

    /// <summary>
    /// Premultiplies the color channels of the surface by its alpha channel, in place.
    /// </summary>
    /// <param name="linear">
    /// <see langword="true"/> to convert from sRGB to linear space for the multiplication; <see langword="false"/> to multiply in sRGB space.
    /// </param>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void PremultiplyAlpha(bool linear = false)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.PremultiplySurfaceAlpha(Handle, linear)));
    }

    /// <summary>
    /// Reads the color of a single pixel.
    /// </summary>
    /// <remarks>This prioritizes correctness over speed and is not intended for per-frame use.</remarks>
    /// <param name="x">The horizontal coordinate, from 0 to <see cref="Width"/> minus one.</param>
    /// <param name="y">The vertical coordinate, from 0 to <see cref="Height"/> minus one.</param>
    /// <returns>The color at the given coordinate.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="x"/> or <paramref name="y"/> is outside the surface.</exception>
    /// <exception cref="QuackInteropException">Failed to reads the pixel from the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Color ReadPixel(int x, int y)
    {
        ThrowIfDisposed();
        ThrowIfInvalid(x, y);

        byte r, g, b, a;
        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.ReadSurfacePixel(Handle, x, y, &r, &g, &b, &a)));

        return new Color(r, g, b, a);
    }

    /// <summary>
    /// Reads the color of a single pixel with floating-point precision.
    /// </summary>
    /// <remarks>This prioritizes correctness over speed and is not intended for per-frame use.</remarks>
    /// <param name="x">The horizontal coordinate, from 0 to <see cref="Width"/> minus one.</param>
    /// <param name="y">The vertical coordinate, from 0 to <see cref="Height"/> minus one.</param>
    /// <returns>The color at the given coordinate.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="x"/> or <paramref name="y"/> is outside the surface.</exception>
    /// <exception cref="QuackInteropException">Failed to reads the pixel from the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public ColorF ReadPixelFloat(int x, int y)
    {
        ThrowIfDisposed();
        ThrowIfInvalid(x, y);

        float r, g, b, a;
        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.ReadSurfacePixelFloat(Handle, x, y, &r, &g, &b, &a)));

        return new ColorF(r, g, b, a);
    }

    /// <summary>
    /// Removes all alternate images attached to this surface.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void RemoveAlternateImages()
    {
        ThrowIfDisposed();

        unsafe
        {
            SDL3.RemoveSurfaceAlternateImages(Handle);
        }
    }

    /// <summary>
    /// Creates a copy of this surface rotated clockwise by the given angle.
    /// </summary>
    /// <remarks>
    /// A negative angle rotates counter-clockwise. When the angle is not a multiple of 90 degrees the result is
    /// larger than the original, with the padding filled by the color key if set, or transparent white otherwise.
    /// </remarks>
    /// <param name="angle">The rotation angle.</param>
    /// <returns>A new rotated surface. The caller owns it and must dispose it.</returns>
    /// <exception cref="QuackInteropException">Failed to create the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Surface Rotate(Angle angle)
    {
        ThrowIfDisposed();

        unsafe
        {
            return new Surface(SDL3.RotateSurface(Handle, angle.Degrees));
        }
    }

    /// <summary>
    /// Save the current surface into an image file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the file already exists, it will be overwritten.
    /// </para>
    /// <para>
    /// For formats (avif, jpg, webp) that accept a quality, a default quality of 90 will be used.
    /// </para>
    /// </remarks>
    /// <param name="path">Path on the filesystem to write new file to.</param>
    /// <param name="quality">The desired quality ranging between 0 and 100.</param>
    public void Save(string path, int quality = 90) => Save(this, path, quality);

    /// <summary>
    /// Save the current surface into an image via a stream.
    /// </summary>
    /// <remarks>
    /// <para>
    /// For formats (avif, jpg, webp) that accept a quality, a default quality of 90 will be used.
    /// </para>
    /// </remarks>
    /// <param name="stream">The stream to save the image to.</param>
    /// <param name="format">The image format to save as.</param>
    /// <param name="quality">The desired quality ranging between 0 and 100.</param>
    public void Save(Stream stream, ImageFormat format, int quality = 90) => Save(this, stream, format, quality);

    /// <summary>
    /// Save a surface into an image file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the file already exists, it will be overwritten.
    /// </para>
    /// <para>
    /// For formats (avif, jpg, webp) that accept a quality, a default quality of 90 will be used.
    /// </para>
    /// </remarks>
    /// <param name="surface">The surface to save.</param>
    /// <param name="path">Path on the filesystem to write new file to.</param>
    /// <param name="quality">The desired quality ranging between 0 and 100.</param>
    public static void Save(Surface surface, string path, int quality = 90)
    {
        surface.ThrowIfDisposed();

        ReadOnlySpan<char> ext = Path.GetExtension(path);

        if (ext.Contains("avif", StringComparison.OrdinalIgnoreCase))
        {
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.SaveAVIF(surface.Handle, path, quality)));
            return;
        }

        if (ext.Contains("jpg", StringComparison.OrdinalIgnoreCase) || ext.Contains("jpeg", StringComparison.OrdinalIgnoreCase))
        {
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.SaveAVIF(surface.Handle, path, quality)));
            return;
        }

        if (ext.Contains("webp", StringComparison.OrdinalIgnoreCase))
        {
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.SaveWEBP(surface.Handle, path, quality)));
            return;
        }

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.Save(surface.Handle, path)));
    }

    /// <summary>
    /// Save a surface into an image via a stream.
    /// </summary>
    /// <remarks>
    /// <para>
    /// For formats (avif, jpg, webp) that accept a quality, a default quality of 90 will be used.
    /// </para>
    /// </remarks>
    /// <param name="surface">The surface to save.</param>
    /// <param name="stream">The stream to save the image to.</param>
    /// <param name="format">The image format to save as.</param>
    /// <param name="quality">The desired quality ranging between 0 and 100.</param>
    /// <exception cref="NotSupportedException">The image format is not supported.</exception>
    public static void Save(Surface surface, Stream stream, ImageFormat format, int quality = 90)
    {
        surface.ThrowIfDisposed();

        using IOStream source = IOStream.FromStream(stream);

        if (format is ImageFormat.AVIF)
        {
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.SaveAVIF(surface.Handle, source.Handle, quality)));
            return;
        }

        if (format is ImageFormat.JPG)
        {
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.SaveJPG(surface.Handle, source.Handle, quality)));
            return;
        }

        if (format is ImageFormat.WEBP)
        {
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.SaveWEBP(surface.Handle, source.Handle, quality)));
            return;
        }

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.Save(surface.Handle, source.Handle, GetFormatName(format))));

        static string GetFormatName(ImageFormat format) => format switch
        {
            ImageFormat.AVIF => nameof(ImageFormat.AVIF),
            ImageFormat.BMP => nameof(ImageFormat.BMP),
            ImageFormat.CUR => nameof(ImageFormat.CUR),
            ImageFormat.GIF => nameof(ImageFormat.GIF),
            ImageFormat.ICO => nameof(ImageFormat.ICO),
            ImageFormat.JPG => nameof(ImageFormat.JPG),
            ImageFormat.PNG => nameof(ImageFormat.PNG),
            ImageFormat.TGA => nameof(ImageFormat.TGA),
            ImageFormat.WEBP => nameof(ImageFormat.WEBP),
            _ => throw new NotSupportedException()
        };
    }

    /// <summary>
    /// Creates a new surface identical to this one, scaled to the given size.
    /// </summary>
    /// <param name="width">The width of the new surface.</param>
    /// <param name="height">The height of the new surface.</param>
    /// <param name="scaleMode">The filtering to use while scaling.</param>
    /// <returns>A new scaled surface. The caller owns it and must dispose it.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> is negative.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Surface Scale(int width, int height, ScaleMode scaleMode)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(width);
        ArgumentOutOfRangeException.ThrowIfNegative(height);

        unsafe
        {
            return new Surface(SDL3.ScaleSurface(Handle, width, height, scaleMode));
        }
    }

    /// <summary>
    /// Performs a stretched pixel copy from a source surface onto this surface.
    /// </summary>
    /// <param name="source">The surface to copy from.</param>
    /// <param name="scaleMode">The filtering to use while stretching.</param>
    /// <param name="region">The region of <paramref name="source"/> to copy, or <see langword="null"/> for all of it.</param>
    /// <param name="destination">The region of this surface to fill, or <see langword="null"/> for the whole surface.</param>
    /// <exception cref="ObjectDisposedException">The surface or the source is disposed.</exception>
    public void Stretch(Surface source, ScaleMode scaleMode, RectI? region = null, RectI? destination = null)
    {
        ThrowIfDisposed();
        source.ThrowIfDisposed();

        RectI src = region.GetValueOrDefault();
        RectI dst = destination.GetValueOrDefault();

        RectI* sourceRect = null;
        RectI* destinationRect = null;

        if (region.HasValue)
            sourceRect = &src;

        if (destination.HasValue)
            destinationRect = &dst;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.StretchSurface(source.Handle, sourceRect, Handle, destinationRect, scaleMode)));
    }

    /// <summary>
    /// Writes a single pixel.
    /// </summary>
    /// <remarks>This prioritizes correctness over speed and is not intended for per-frame use.</remarks>
    /// <param name="x">The horizontal coordinate, from 0 to <see cref="Width"/> minus one.</param>
    /// <param name="y">The vertical coordinate, from 0 to <see cref="Height"/> minus one.</param>
    /// <param name="color">The color to write.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="x"/> or <paramref name="y"/> is outside the surface.</exception>
    /// <exception cref="QuackInteropException">Failed to write the pixel from the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void WritePixel(int x, int y, Color color)
    {
        ThrowIfDisposed();
        ThrowIfInvalid(x, y);

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.WriteSurfacePixel(Handle, x, y, color.R, color.G, color.B, color.A)));
    }

    /// <summary>
    /// Writes a single pixel with floating-point precision.
    /// </summary>
    /// <remarks>This prioritizes correctness over speed and is not intended for per-frame use.</remarks>
    /// <param name="x">The horizontal coordinate, from 0 to <see cref="Width"/> minus one.</param>
    /// <param name="y">The vertical coordinate, from 0 to <see cref="Height"/> minus one.</param>
    /// <param name="color">The color to write.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="x"/> or <paramref name="y"/> is outside the surface.</exception>
    /// <exception cref="QuackInteropException">Failed to write the pixel from the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void WritePixel(int x, int y, ColorF color)
    {
        ThrowIfDisposed();
        ThrowIfInvalid(x, y);

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.WriteSurfacePixelFloat(Handle, x, y, color.R, color.G, color.B, color.A)));
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(unsafe (Handle is null), typeof(Surface));

    private void ThrowIfInvalid(int x, int y)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(x);
        ArgumentOutOfRangeException.ThrowIfNegative(y);

        unsafe
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(x, Handle->Width);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(y, Handle->Height);
        }
    }
}

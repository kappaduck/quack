// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using CommunityToolkit.HighPerformance;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// An image stored in GPU memory that a <see cref="Renderer"/> can draw.
/// </summary>
public sealed class Texture : IDisposable
{
    internal unsafe Texture(SDL_Texture* handle)
    {
        SDLThrowHelper.ThrowIfNull(handle);

        Handle = handle;

        uint properties = SDL3.GetTextureProperties(Handle);
        Format = Properties.Get(properties, "SDL.texture.format", PixelFormat.Unknown);

        Height = Handle->Height;
        Width = Handle->Width;
    }

    /// <summary>
    /// Gets or sets the alpha applied when the texture is drawn, where 255 is fully opaque.
    /// </summary>
    /// <remarks>
    /// When this texture is rendered, during the copy operation the source alpha value is
    /// modulated by this alpha value according to the following formula:
    /// <c>srcA = srcA * (AlphaModulation / 255)</c>
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the alpha modulation.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public byte AlphaModulation
    {
        get
        {
            ThrowIfDisposed();

            byte alpha;
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetTextureAlphaMod(Handle, &alpha)));

            return alpha;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetTextureAlphaMod(Handle, value)));
        }
    }

    /// <summary>
    /// Gets or sets the blend mode used when the texture is drawn.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get or set the blend mode.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public BlendMode BlendMode
    {
        get
        {
            ThrowIfDisposed();

            BlendMode mode;
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetTextureBlendMode(Handle, &mode)));

            return mode;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetTextureBlendMode(Handle, value)));
        }
    }

    /// <summary>
    /// Gets or sets a color multiplied into the texture when it is drawn, tinting it.
    /// </summary>
    /// <remarks>
    /// When this texture is rendered, during the copy operation each source color channel is
    /// modulated by the appropriate color value according to the following formula:
    /// <c>srcC = srcC * (color / 255)</c>
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the color modulation.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public Color ColorModulation
    {
        get
        {
            ThrowIfDisposed();

            byte r, g, b;
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetTextureColorMod(Handle, &r, &g, &b)));

            return new Color(r, g, b);
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetTextureColorMod(Handle, value.R, value.G, value.B)));
        }
    }

    /// <summary>
    /// Gets the pixel format of the texture.
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// Gets the height of the texture in pixels.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets or sets the palette used by the texture, or <see langword="null"/> if it has no palette.
    /// </summary>
    /// <remarks>
    /// The returned palette is a view owned by the texture; disposing it does nothing, and it becomes invalid once the
    /// palette is replaced or the texture is disposed. When setting, the texture keeps its own reference to the palette,
    /// so the assigned palette may be safely disposed afterward, and a single palette may be shared by several textures.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the palette.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public Palette? Palette
    {
        get
        {
            ThrowIfDisposed();

            unsafe
            {
                SDL_Palette* palette = SDL3.GetTexturePalette(Handle);
                return palette is null ? null : new Palette(palette);
            }
        }
        set
        {
            ThrowIfDisposed();

            unsafe
            {
                SDL_Palette* palette = value is null ? null : value.Handle;
                SDLThrowHelper.ThrowIfFailed(SDL3.SetTexturePalette(Handle, palette));
            }
        }
    }

    /// <summary>
    /// Gets or sets the scaling filter used when the texture is drawn at a size other than its own.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get or set the scale mode.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public ScaleMode ScaleMode
    {
        get
        {
            ThrowIfDisposed();

            ScaleMode mode;
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetTextureScaleMode(Handle, &mode)));

            return mode;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetTextureScaleMode(Handle, value)));
        }
    }

    /// <summary>
    /// Gets the width of the texture in pixels.
    /// </summary>
    public int Width { get; }

    internal SDL_Texture* Handle { get; private set; }

    /// <inheritdoc/>
    public void Dispose()
    {
        unsafe
        {
            if (Handle is null)
                return;

            SDL3.DestroyTexture(Handle);
            Handle = null;
        }
    }

    /// <summary>
    /// Locks the texture and returns a scope that exposes its pixel buffer as <see cref="Surface"/> for direct writing.
    /// </summary>
    /// <remarks>
    /// The texture must have been created with <see cref="TextureAccess.Streaming"/>. Dispose the returned scope to
    /// upload the changes and unlock the texture. The exposed buffer is write-only and only valid for the lifetime of
    /// the scope. For textures that change rarely, prefer <see cref="Update"/>.
    /// </remarks>
    /// <param name="region">The region to lock, or <see langword="null"/> to lock the whole texture.</param>
    /// <returns>A scope that exposes the pixel buffer and unlocks the texture when disposed.</returns>
    /// <exception cref="QuackInteropException">The texture could not be locked, for example if it is not streaming.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public TextureLock Lock(RectI? region = null)
    {
        ThrowIfDisposed();

        unsafe
        {
            return new TextureLock(Handle, region);
        }
    }

    /// <summary>
    /// Replaces a region of the texture's pixels with new data.
    /// </summary>
    /// <remarks>
    /// The pixel data is copied. Rows in <paramref name="pixels"/> are <paramref name="pitch"/> bytes apart. Best for
    /// textures that change rarely; for frequent updates create a <see cref="TextureAccess.Streaming"/> texture and use
    /// <see cref="Lock"/>.
    /// </remarks>
    /// <param name="pixels">The source pixel data to copy, in the texture's format.</param>
    /// <param name="pitch">The number of bytes in a row of <paramref name="pixels"/>.</param>
    /// <param name="region">The region to update, or <see langword="null"/> to update the whole texture.</param>
    /// <exception cref="QuackInteropException">The texture could not be updated.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public void Update(ReadOnlySpan<byte> pixels, int pitch, RectI? region = null)
    {
        ThrowIfDisposed();

        if (region is { } area)
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.UpdateTexture(Handle, &area, pixels, pitch)));
        else
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.UpdateTexture(Handle, null, pixels, pitch)));
    }

    /// <summary>
    /// Replaces a region of a planar YUV texture (such as IYUV or YV12) with new data from separate Y, U and V planes.
    /// </summary>
    /// <remarks>
    /// The texture must have been created with a planar YUV format. Use this when the planes are stored separately; if
    /// the Y and U/V data is a single contiguous block in the correct order, <see cref="Update"/> works too. The pixel
    /// data is copied.
    /// </remarks>
    /// <param name="y">The Y (luma) plane.</param>
    /// <param name="yPitch">The number of bytes in a row of the <paramref name="y"/> plane.</param>
    /// <param name="u">The U (chroma) plane.</param>
    /// <param name="uPitch">The number of bytes in a row of the <paramref name="u"/> plane.</param>
    /// <param name="v">The V (chroma) plane.</param>
    /// <param name="vPitch">The number of bytes in a row of the <paramref name="v"/> plane.</param>
    /// <param name="region">The region to update, or <see langword="null"/> to update the whole texture.</param>
    /// <exception cref="QuackInteropException">The texture could not be updated.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public void UpdateYUV(ReadOnlySpan<byte> y, int yPitch, ReadOnlySpan<byte> u, int uPitch, ReadOnlySpan<byte> v, int vPitch, RectI? region = null)
    {
        ThrowIfDisposed();

        if (region is RectI area)
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.UpdateYUVTexture(Handle, &area, y, yPitch, u, uPitch, v, vPitch)));
        else
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.UpdateYUVTexture(Handle, null, y, yPitch, u, uPitch, v, vPitch)));
    }

    /// <summary>
    /// Replaces a region of a semi-planar texture (NV12 or NV21) with new data from a Y plane and an interleaved UV plane.
    /// </summary>
    /// <remarks>
    /// The texture must have been created with an NV12 or NV21 format. Use this when the planes are stored separately; if
    /// the Y and UV data is a single contiguous block in the correct order, <see cref="Update"/> works too. The pixel
    /// data is copied.
    /// </remarks>
    /// <param name="y">The Y (luma) plane.</param>
    /// <param name="yPitch">The number of bytes in a row of the <paramref name="y"/> plane.</param>
    /// <param name="uv">The interleaved UV (chroma) plane.</param>
    /// <param name="uvPitch">The number of bytes in a row of the <paramref name="uv"/> plane.</param>
    /// <param name="region">The region to update, or <see langword="null"/> to update the whole texture.</param>
    /// <exception cref="QuackInteropException">The texture could not be updated.</exception>
    /// <exception cref="ObjectDisposedException">The texture is disposed.</exception>
    public void UpdateNV(ReadOnlySpan<byte> y, int yPitch, ReadOnlySpan<byte> uv, int uvPitch, RectI? region = null)
    {
        ThrowIfDisposed();

        if (region is RectI area)
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.UpdateNVTexture(Handle, &area, y, yPitch, uv, uvPitch)));
        else
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.UpdateNVTexture(Handle, null, y, yPitch, uv, uvPitch)));
    }

    /// <summary>
    /// Creates an empty texture of the given size.
    /// </summary>
    /// <param name="renderer">The renderer to use to create the texture</param>
    /// <param name="size">The size of the texture in pixels.</param>
    /// <param name="format">The pixel format of the texture.</param>
    /// <param name="access">How the texture's pixels may be accessed after creation.</param>
    /// <exception cref="ArgumentOutOfRangeException"><see cref="SizeI.Width"/> or <see cref="SizeI.Height"/> is negative or zero.</exception>
    /// <exception cref="QuackInteropException">The texture could not be created.</exception>
    public static Texture Create(Renderer renderer, SizeI size, PixelFormat format, TextureAccess access = TextureAccess.Static)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size.Width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size.Height);

        unsafe
        {
            SDL_Texture* texture = SDL3.CreateTexture(renderer.Handle, format, access, size.Width, size.Height);
            return new(texture);
        }
    }

    /// <summary>
    /// Loads an image from a raw file bytes into a texture.
    /// </summary>
    /// <param name="renderer">The renderer to use to create the texture</param>
    /// <param name="bytes">The raw bytes of the image file.</param>
    /// <returns>The loaded texture.</returns>
    /// <exception cref="QuackInteropException">Thrown when failed to load the image.</exception>
    public static Texture FromBytes(Renderer renderer, ReadOnlyMemory<byte> bytes)
    {
        using Stream stream = bytes.AsStream();
        return FromStream(renderer, stream);
    }

    /// <summary>
    /// Loads an image from a file into a texture.
    /// </summary>
    /// <param name="renderer">The renderer to use to create the texture</param>
    /// <param name="path">The path to the image file.</param>
    /// <returns>The loaded texture.</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="QuackInteropException">Thrown when failed to load the image.</exception>
    public static Texture FromFile(Renderer renderer, string path)
    {
        if (!File.Exists(path))
            ThrowHelper.ThrowFileNotFound("The file path does not exist.", path);

        unsafe
        {
            SDL_Texture* handle = SDL3_image.FromFile(renderer.Handle, path);
            return new Texture(handle);
        }
    }

    /// <summary>
    /// Loads an image from a stream into a texture.
    /// </summary>
    /// <param name="renderer">The renderer to use to create the texture</param>
    /// <param name="stream">The stream to read the image from.</param>
    /// <returns>The loaded texture.</returns>
    /// <exception cref="QuackInteropException">The image could not be loaded.</exception>
    public static Texture FromStream(Renderer renderer, Stream stream)
    {
        using IOStream source = IOStream.FromStream(stream);

        unsafe
        {
            SDL_Texture* texture = SDL3_image.FromStream(renderer.Handle, source.Handle, false);
            return new Texture(texture);
        }
    }

    /// <summary>
    /// Creates a texture from an existing surface.
    /// </summary>
    /// <param name="renderer">The renderer to use to create the texture</param>
    /// <param name="surface">The source pixels. The surface is copied and may be disposed afterward.</param>
    /// <returns>The created texture.</returns>
    /// <exception cref="QuackInteropException">The texture could not be created.</exception>
    public static Texture FromSurface(Renderer renderer, Surface surface)
    {
        unsafe
        {
            SDL_Texture* texture = SDL3.CreateTextureFromSurface(renderer.Handle, surface.Handle);
            return new(texture);
        }
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(unsafe (Handle is null), typeof(Texture));
}

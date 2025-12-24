// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Pixels;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// Represents a graphical texture resource used for rendering images or patterns.
/// </summary>
public sealed class Texture : IDisposable
{
    private readonly SDL_Texture _handle;
    private readonly SDL_Renderer _renderer;

    internal Texture(SDL_Renderer renderer, PixelFormat format, TextureAccess access, int width, int height)
    {
        _renderer = renderer;

        _handle = Native.SDL_CreateTexture(_renderer, format, access, width, height);
        QuackNativeException.ThrowIfHandleInvalid(_handle);

        Format = format;
        Width = width;
        Height = height;
    }

    internal unsafe Texture(SDL_Renderer renderer, Surface surface)
    {
        _renderer = renderer;

        _handle = Native.SDL_CreateTextureFromSurface(_renderer, surface.Handle);
        QuackNativeException.ThrowIfHandleInvalid(_handle);

        uint properties = Native.SDL_GetTextureProperties(_handle);
        Format = Native.GetEnumProperty(properties, "SDL.texture.format", PixelFormat.Unknown);

        Width = surface.Width;
        Height = surface.Height;
    }

    internal Texture(SDL_Renderer renderer, string file)
    {
        _renderer = renderer;

        _handle = Native.IMG_LoadTexture(renderer, file);
        QuackNativeException.ThrowIfHandleInvalid(_handle);

        uint properties = Native.SDL_GetTextureProperties(_handle);

        Format = Native.GetEnumProperty(properties, "SDL.texture.format", PixelFormat.Unknown);
        Width = Native.GetNumberProperty(properties, "SDL.texture.width", 0);
        Height = Native.GetNumberProperty(properties, "SDL.texture.height", 0);
    }

    /// <summary>
    /// Gets or sets the additional alpha value multiplied into render copy operations.
    /// </summary>
    /// <remarks>
    /// <para>If the texture is not valid, the default value of 255 is returned.</para>
    /// <para>
    /// When this texture is rendered, during the copy operation the source alpha value is
    /// modulated by this alpha value according to the following formula:
    /// <c>srcA = srcA * (AlphaModulation / 255)</c>
    /// </para>
    /// <para>
    /// Alpha modulation is not always supported by the renderer.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get or set the alpha modulation.</exception>
    public byte AlphaModulation
    {
        get
        {
            if (_handle.IsInvalid)
                return 255;

            QuackNativeException.ThrowIfFailed(Native.SDL_GetTextureAlphaMod(_handle, out byte alpha));
            return alpha;
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetTextureAlphaMod(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the blend mode used for the texture copy operations.
    /// </summary>
    /// <remarks>
    /// <para>If the texture is not valid, the default value of BlendMode.None is returned.</para>
    /// <para>If the blend mode is not supported, the closest supported mode is chosen.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get or set the blend mode.</exception>
    public BlendMode BlendMode
    {
        get
        {
            if (_handle.IsInvalid)
                return BlendMode.None;

            QuackNativeException.ThrowIfFailed(Native.SDL_GetTextureBlendMode(_handle, out BlendMode mode));
            return mode;
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetTextureBlendMode(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the additional color value multiplied into render copy operations.
    /// </summary>
    /// <remarks>
    /// <para>If the texture is not valid, <see cref="Color.White"/> is returned.</para>
    /// <para>
    /// When this texture is rendered, during the copy operation each source color channel is
    /// modulated by the appropriate color value according to the following formula:
    /// <c>srcC = srcC * (color / 255)</c>
    /// </para>
    /// <para>
    /// Color modulation is not always supported by the renderer.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get or set the color modulation.</exception>
    public Color ColorModulation
    {
        get
        {
            if (_handle.IsInvalid)
                return Color.White;

            QuackNativeException.ThrowIfFailed(Native.SDL_GetTextureColorMod(_handle, out byte r, out byte g, out byte b));
            return Color.FromArgb(r, g, b);
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetTextureColorMod(_handle, value.R, value.G, value.B));
        }
    }

    /// <summary>
    /// Gets or sets the scale mode used for the texture scale operations.
    /// </summary>
    /// <remarks>
    /// <para>The default scale mode is <see cref="ScaleMode.Linear"/>.</para>
    /// <para>If the texture is not valid, <see cref="ScaleMode.Invalid"/> is returned.</para>
    /// <para>If the scale mode is not supported, the closest supported mode is chosen.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get or set the scale.</exception>
    public ScaleMode Scale
    {
        get
        {
            if (_handle.IsInvalid)
                return ScaleMode.Invalid;

            QuackNativeException.ThrowIfFailed(Native.SDL_GetTextureScaleMode(_handle, out ScaleMode scale));
            return scale;
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetTextureScaleMode(_handle, value));
        }
    }

    /// <summary>
    /// Gets the height of the texture.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the pixel format of the texture.
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// Gets the width of the texture.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Releases the unmanaged resources used by the texture.
    /// </summary>
    public void Dispose() => _handle.Dispose();

    internal unsafe void Render(Rect source, Rect destination, double angle, Vector2 center, FlipMode mode)
        => Native.SDL_RenderTextureRotated(_renderer, _handle, &source, &destination, angle, &center, mode);
}

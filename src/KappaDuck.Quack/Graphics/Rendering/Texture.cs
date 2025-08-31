// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Interop.SDL.Handles;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// An efficient driver-specific representation of pixels.
/// </summary>
public sealed partial class Texture : IDisposable
{
    private readonly TextureHandle _handle;
    private readonly Renderer _renderer;

    internal Texture(Renderer renderer, PixelFormat pixelFormat, TextureAccess access, int width, int height)
    {
        _renderer = renderer;

        _handle = _renderer.CreateTexture(pixelFormat, access, width, height);
        QuackNativeException.ThrowIf(_handle.IsInvalid);

        Format = pixelFormat;
        Height = height;
        Width = width;
    }

    internal unsafe Texture(Renderer renderer, Surface surface)
    {
        _renderer = renderer;

        _handle = _renderer.CreateTextureFromSurface(surface);
        QuackNativeException.ThrowIf(_handle.IsInvalid);

        uint properties = SDL_GetTextureProperties(_handle);
        Format = Properties.GetAsEnum(properties, "SDL.texture.format", PixelFormat.Unknown);

        Height = surface.Height;
        Width = surface.Width;
    }

    internal Texture(Renderer renderer, string file)
    {
        _renderer = renderer;

        _handle = _renderer.LoadTexture(file);
        QuackNativeException.ThrowIf(_handle.IsInvalid);

        uint properties = SDL_GetTextureProperties(_handle);
        Format = Properties.GetAsEnum(properties, "SDL.texture.format", PixelFormat.Unknown);

        Height = Properties.GetAsNumber(properties, "SDL.texture.height", 0);
        Width = Properties.GetAsNumber(properties, "SDL.texture.width", 0);
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
    /// <exception cref="QuackNativeException">An error occurred while setting or getting the alpha modulation.</exception>
    public byte AlphaModulation
    {
        get
        {
            if (_handle.IsInvalid)
                return 255;

            QuackNativeException.ThrowIfFailed(SDL_GetTextureAlphaMod(_handle, out byte alpha));
            return alpha;
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(SDL_SetTextureAlphaMod(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the blend mode used for the texture copy operations.
    /// </summary>
    /// <remarks>
    /// <para>If the texture is not valid, the default value of BlendMode.None is returned.</para>
    /// <para>If the blend mode is not supported, the closest supported mode is chosen.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while getting the blend mode.</exception>
    public BlendMode BlendMode
    {
        get
        {
            if (_handle.IsInvalid)
                return BlendMode.None;

            QuackNativeException.ThrowIfFailed(SDL_GetTextureBlendMode(_handle, out BlendMode mode));
            return mode;
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            SDL_SetTextureBlendMode(_handle, value);
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
    /// <exception cref="QuackNativeException">An error occurred while setting or getting the color modulation.</exception>
    public Color ColorModulation
    {
        get
        {
            if (_handle.IsInvalid)
                return Color.White;

            QuackNativeException.ThrowIfFailed(SDL_GetTextureColorMod(_handle, out byte r, out byte g, out byte b));
            return Color.FromArgb(r, g, b);
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(SDL_SetTextureColorMod(_handle, value.R, value.G, value.B));
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
    /// <exception cref="QuackNativeException">An error occurred while getting the scale mode.</exception>
    public ScaleMode Scale
    {
        get
        {
            if (_handle.IsInvalid)
                return ScaleMode.Invalid;

            QuackNativeException.ThrowIfFailed(SDL_GetTextureScaleMode(_handle, out ScaleMode scale));
            return scale;
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            SDL_SetTextureScaleMode(_handle, value);
        }
    }

    /// <summary>
    /// Gets the height of the texture in pixels.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the pixel format of the texture.
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// Gets the width of the texture in pixels.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Releases the unmanaged resources used by the texture.
    /// </summary>
    public void Dispose() => _handle.Dispose();

    /// <summary>
    /// Copy a portion of the texture to the current rendering target at subpixel precision.
    /// </summary>
    /// <param name="source">The portion of the texture to copy, or empty to copy the entire texture.</param>
    /// <param name="destination">The portion of the rendering target to copy the texture to, or empty to copy to the entire target.</param>
    /// <param name="angle">An angle in degrees that indicates the rotation that will be applied to <paramref name="destination"/>, rotating it in a clockwise direction.</param>
    /// <param name="center">A point indicating the point around which <paramref name="destination"/> will be rotated. If <see langword="null"/>, rotation will be done around the center of the destination.</param>
    /// <param name="flip">The flip mode to apply to the texture.</param>
    /// <exception cref="NotImplementedException">Waiting for Sprite.</exception>
    internal void Render(Rect? source = null, Rect? destination = null, double angle = 0.0, Vector2? center = null, FlipMode flip = FlipMode.None) => throw new NotImplementedException();

    /// <summary>
    /// Copy a portion of the source texture to the current rendering target, with affine transform, at subpixel precision.
    /// </summary>
    /// <param name="source">The portion of the texture to copy, or empty to copy the entire texture.</param>
    /// <param name="origin">A point indicating where the top-left corner of <paramref name="source"/> should be mapped to, or <see langword="null"/> for the rendering target's origin.</param>
    /// <param name="right">A point indicating where the top-right corner of <paramref name="source"/> should be mapped to, or <see langword="null"/> for the rendering target's top-right corner.</param>
    /// <param name="down">A point indicating where the bottom-left corner of <paramref name="source"/> should be mapped to, or <see langword="null"/> for the rendering target's bottom-left corner.</param>
    /// <exception cref="NotImplementedException"></exception>
    internal void RenderAffine(Rect? source = null, Vector2? origin = null, Vector2? right = null, Vector2? down = null) => throw new NotImplementedException();

    /// <summary>
    /// Perform a scaled copy using the 9-grid algorithm to the current rendering target at subpixel precision.
    /// </summary>
    /// <remarks>
    /// The pixels in the texture are split into a 3x3 grid, using the different corner sizes for each corner, and the sides and center making up the remaining pixels.
    /// The corners are then scaled using scale and fit into the corners of the destination rectangle.
    /// The sides and center are then stretched into place to cover the remaining destination rectangle.
    /// </remarks>
    /// <param name="source">The portion of the texture to copy, or <see langword="null"/> to copy the entire texture.</param>
    /// <param name="left">The width of the left corners.</param>
    /// <param name="right">The width of the right corners.</param>
    /// <param name="top">The height of the top corners.</param>
    /// <param name="bottom">The height of the bottom corners.</param>
    /// <param name="scale">The scale used to transform the corner of <paramref name="source"/> into the corner of <paramref name="destination"/>, or 0.0f for an unscaled copy.</param>
    /// <param name="destination">The portion of the rendering target to copy the texture to, or <see langword="null"/> to copy to the entire target.</param>
    /// <param name="tileScale">the scale used to transform the borders and center of srcrect into the borders and middle of dstrect, or 1.0f for an unscaled copy.</param>
    /// <exception cref="NotImplementedException">Waiting for Sprite.</exception>
    internal void Render9Grid(Rect? source, float left, float right, float top, float bottom, float scale, Rect? destination, float tileScale = 1.0f)
        => throw new NotImplementedException();

    /// <summary>
    /// Tile a portion of the texture to the current rendering target at subpixel precision.
    /// </summary>
    /// <remarks>
    /// The portion of the texture defined by <paramref name="source"/> is repeatedly drawn to fill the area defined by <paramref name="destination"/>.
    /// </remarks>
    /// <param name="source">The portion of the texture to copy, or <see langword="null"/> to copy the entire texture.</param>
    /// <param name="destination">The portion of the rendering target to copy the texture to, or <see langword="null"/> to copy to the entire target.</param>
    /// <param name="scale">The scale used to transform the texture, or 1.0f for an unscaled copy.</param>
    /// <exception cref="NotImplementedException">Waiting for Sprite.</exception>
    internal void RenderTiled(Rect? source, Rect? destination, float scale)
        => throw new NotImplementedException();
}

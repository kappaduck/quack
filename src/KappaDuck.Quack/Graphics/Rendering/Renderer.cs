// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video;
using KappaDuck.Quack.Video.Pixels;
using KappaDuck.Quack.Windows;
using System.ComponentModel;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// A hardware-accelerated 2D renderer that draws textures, primitives onto a window or a target texture.
/// </summary>
/// <remarks>
/// A window can host only one renderer or surface at a time; creating a renderer binds the
/// window until the renderer is disposed.
/// </remarks>
public sealed class Renderer : IRenderTarget, IDisposable
{
    private const int MaxStackVertices = 256;

    private Window? _window;

    /// <summary>
    /// Creates a renderer that presents to <paramref name="window"/>.
    /// </summary>
    /// <remarks>
    /// If you want a specific renderer, you can find from the available renderers using <see cref="RenderDrivers.All"/>.
    /// You can use a comma-seperated list e.g. vulkan,opengl which the engine will try each name, in the order listed, until one succeeds or all of them fail.
    /// </remarks>
    /// <param name="window">The window to present to.</param>
    /// <param name="driver">The name of the rendering driver to use, or <see langword="null"/> to let the engine choose the most suitable one.</param>
    /// <exception cref="InvalidOperationException">The window is already bound to a renderer.</exception>
    /// <exception cref="QuackInteropException">The renderer could not be created.</exception>
    public Renderer(Window window, string? driver = null)
    {
        window.Bind(this);

        try
        {
            Handle = SDL3.CreateRenderer(window.NativeHandle, driver);
            SDLThrowHelper.ThrowIfNull(Handle);
        }
        catch
        {
            window.Unbind(this);
            throw;
        }

        _window = window;
    }

    /// <summary>
    /// Creates a renderer that presents to <paramref name="window"/>.
    /// </summary>
    /// <param name="window">The window to present to.</param>
    /// <param name="options">The options to configure the renderer</param>
    /// <exception cref="InvalidOperationException">The window is already bound to a renderer.</exception>
    /// <exception cref="QuackInteropException">The renderer could not be created.</exception>
    public Renderer(Window window, RendererOptions options)
    {
        window.Bind(this);

        try
        {
            using Properties properties = new();

            if (!string.IsNullOrEmpty(options.Driver))
                properties.Set("SDL.renderer.create.name", options.Driver);

            properties.Set("SDL.renderer.create.window", window.NativeHandle);
            properties.Set("SDL.renderer.create.present_vsync", options.VSync);
            properties.Set("SDL.renderer.create.output_colorspace", options.Colorspace);

            Handle = SDL3.CreateRendererWithProperties(properties);
            SDLThrowHelper.ThrowIfNull(Handle);
        }
        catch
        {
            window.Unbind(this);
            throw;
        }

        _window = window;
    }

    /// <summary>
    /// Gets or sets the blend mode used for drawing operations.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get or set the blend mode.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public BlendMode BlendMode
    {
        get
        {
            ThrowIfDisposed();
            return field;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderDrawBlendMode(Handle, value));

            field = value;
        }
    }

    /// <summary>
    /// Gets the final presentation rectangle for rendering.
    /// </summary>
    /// <remarks>
    /// It returns the calculated rectangle used for logical presentation, based on the presentation
    /// mode and output size. If logical presentation is <see cref="LogicalPresentation.Disabled"/>, it will fill
    /// the rectangle with the output size, in pixels.
    /// </remarks>
    /// <exception cref="QuackInteropException">Thrown when failed to get the presentation rectangle.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public Rect CalculatedPresentation
    {
        get
        {
            ThrowIfDisposed();

            Rect rect;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetRenderLogicalPresentationRect(Handle, &rect));

            return rect;
        }
    }

    /// <summary>
    /// Gets or sets the clipping rectangle, in render coordinates, or <see langword="null"/> when clipping is disabled.
    /// </summary>
    /// <remarks>Drawing is confined to this rectangle while it is set. Set to <see langword="null"/> to draw to the entire target.</remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the clipping rectangle.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public RectI? Clip
    {
        get
        {
            ThrowIfDisposed();
            return field;
        }
        set
        {
            ThrowIfDisposed();

            if (value is null)
            {
                SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderClipRect(Handle, null));
                return;
            }

            RectI rect = value.Value;
            SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderClipRect(Handle, &rect));

            field = value;
        }
    }

    /// <summary>
    /// Gets or sets an additional color scale multiplied into the color of each drawing operation, in linear space.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The color scale is an additional scale multiplied into the pixel color value while rendering.
    /// This can be used to adjust the brightness of colors during HDR rendering,
    /// or changing HDR video brightness when playing on an SDR display.
    /// </para>
    /// <para>
    /// The color scale does not affect the alpha channel, only the color brightness.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the color scale.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public float ColorScale
    {
        get
        {
            ThrowIfDisposed();

            float scale;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetRenderColorScale(Handle, &scale));

            return scale;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderColorScale(Handle, value));
        }
    }

    /// <summary>
    /// Gets the current output size, in pixels, accounting for any logical presentation in effect.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get the output size.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public Size CurrentOutputSize
    {
        get
        {
            ThrowIfDisposed();

            int width, height;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetCurrentRenderOutputSize(Handle, &width, &height));

            return new Size(width, height);
        }
    }

    /// <summary>
    /// Gets or sets the default scale mode applied to newly created textures.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get or set the default texture scale mode.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public ScaleMode DefaultTextureScaleMode
    {
        get
        {
            ThrowIfDisposed();

            ScaleMode mode;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetDefaultTextureScaleMode(Handle, &mode));

            return mode;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(SDL3.SetDefaultTextureScaleMode(Handle, value));
        }
    }

    /// <summary>
    /// Gets the rendering driver name.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to get the rendering driver name.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public string Name
    {
        get
        {
            ThrowIfDisposed();

            string? driver = SDL3.GetRendererName(Handle);
            SDLThrowHelper.ThrowIfNull(driver);

            return driver;
        }
    }

    /// <summary>
    /// Gets or sets a device independent resolution and presentation mode for rendering.
    /// </summary>
    /// <remarks>
    /// It sets the width and height of the logical rendering output.
    /// The renderer will act as if the window is always the requested dimensions, scaling to the actual window resolution as necessary.
    /// This can be useful for games that expect a fixed size, but would like to scale the output to whatever is available,
    /// regardless of how a user resizes a window, or if the display is high DPI.
    /// You can disable logical coordinates by setting the mode to <see cref="LogicalPresentation.Disabled"/>,
    /// and in that case you get the full pixel resolution of the output window;
    /// it is safe to toggle logical presentation during the rendering of a frame: perhaps most of the rendering is done to specific dimensions
    /// but to make fonts look sharp, the app turns off logical presentation while drawing text.
    /// Letterboxing will only happen if logical presentation is enabled during <see cref="Present"/>; be sure to re-enable it first if you were using it.
    /// You can convert coordinates in an event into rendering coordinates using <see cref="MapCoordinatesFromWindow(PointF)"/>.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">The width or height is negative.</exception>
    /// <exception cref="QuackInteropException">Thrown when failed to get or set the presentation.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public (int Width, int Height, LogicalPresentation Mode) Presentation
    {
        get
        {
            ThrowIfDisposed();

            int width, height;
            LogicalPresentation mode;

            SDLThrowHelper.ThrowIfFailed(SDL3.GetRenderLogicalPresentation(Handle, &width, &height, &mode));
            return (width, height, mode);
        }
        set
        {
            ThrowIfDisposed();

            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderLogicalPresentation(Handle, value.Width, value.Height, value.Mode));
        }
    }

    /// <summary>
    /// Gets the output size, in pixels, ignoring any logical presentation in effect.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="CurrentOutputSize"/> for the current rendering target, with logical size adjustments.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get the output size.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public Size OutputSize
    {
        get
        {
            ThrowIfDisposed();

            int width, height;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetRenderOutputSize(Handle, &width, &height));

            return new Size(width, height);
        }
    }

    /// <summary>
    /// Gets the safe area for rendering within the current viewport.
    /// </summary>
    /// <remarks>
    /// Some devices have portions of the screen which are partially obscured or not interactive,
    /// possibly due to on-screen controls, curved edges, camera notches, TV overscan, etc.
    /// This provides the area of the current viewport which is safe to have interactible content.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get the safe area.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public RectI SafeArea
    {
        get
        {
            ThrowIfDisposed();

            RectI rect;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetRenderSafeArea(Handle, &rect));

            return rect;
        }
    }

    /// <summary>
    /// Gets or sets the horizontal and vertical scale applied to drawing operations.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The drawing coordinates are scaled by the x/y scaling factors before they are used by the renderer.
    /// This allows resolution independent drawing with a single coordinate system.
    /// </para>
    /// <para>
    /// If this results in scaling or subpixel drawing by the rendering backend,
    /// it will be handled using the appropriate quality hints.
    /// For best results use integer scaling factors.
    /// </para>
    /// <para>
    /// Each render target has its own scale. This function sets the scale for the current render target.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the scale.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public Vector2 Scale
    {
        get
        {
            ThrowIfDisposed();

            float x, y;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetRenderScale(Handle, &x, &y));

            return new Vector2(x, y);
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderScale(Handle, value.X, value.Y));
        }
    }

    /// <summary>
    /// Gets or sets the texture addressing mode used in <see cref="Draw(ReadOnlySpan{Vertex}, RenderState)"/> or <see cref="Draw(ReadOnlySpan{Vertex}, ReadOnlySpan{int}, RenderState)"/>.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public (TextureAddressMode Horizontal, TextureAddressMode Vertical) TextureAddressMode
    {
        get
        {
            ThrowIfDisposed();

            TextureAddressMode horizontal, vertical;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetRenderTextureAddressMode(Handle, &horizontal, &vertical));

            return (horizontal, vertical);
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderTextureAddressMode(Handle, value.Horizontal, value.Vertical));
        }
    }

    /// <summary>
    /// Gets or sets the drawing area for rendering on the current target, or <see langword="null"/> to use the entire target.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Drawing will clip to this area (separately from any clipping done with <see cref="Clip"/>),
    /// and the top left of the area will become coordinate (0, 0) for future drawing commands.
    /// </para>
    /// <para>
    /// Each render target has its own viewport.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the viewport.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The width or height is negative or zero</exception>
    public RectI? Viewport
    {
        get
        {
            ThrowIfDisposed();
            return field;
        }
        set
        {
            ThrowIfDisposed();

            if (!value.HasValue)
            {
                SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderViewport(Handle, null));
                return;
            }

            RectI rect = value.Value;

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rect.Width);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rect.Height);

            SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderViewport(Handle, &rect));

            field = value;
        }
    }

    /// <summary>
    /// Gets or sets the vertical synchronization mode.
    /// </summary>
    /// <remarks>
    /// <see cref="VSync.Disabled"/> to disables synchronization, 1 synchronizes with every vertical refresh, and larger values synchronize
    /// with every Nth refresh. <see cref="VSync.Adaptive"/>, where supported, to enables adaptive synchronization. The default is <see cref="VSync.Disabled"/>.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the vertical synchronization mode.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public int VSync
    {
        get
        {
            ThrowIfDisposed();

            int vsync;
            SDLThrowHelper.ThrowIfFailed(SDL3.GetRenderVSync(Handle, &vsync));

            return vsync;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderVSync(Handle, value));
        }
    }

    /// <summary>
    /// Gets the associated window.
    /// </summary>
    public Window Window
    {
        get
        {
            ThrowIfDisposed();

            if (_window is null)
                ThrowHelper.ThrowInvalidOperation("There is no associated window");

            return _window;
        }
    }

    internal SDL_Renderer* Handle { get; private set; }

    /// <summary>
    /// Clear the current rendering target with black color.
    /// </summary>
    /// <remarks>
    /// This clears the entire rendering target, ignoring the viewport and the clip rectangle.
    /// Note, that clearing will also set/fill all pixels of the rendering target to current renderer draw color (black),
    /// so make sure to call <see cref="SetDrawingColor(Color)"/>/<see cref="SetDrawingColor(ColorF)"/> when needed.
    /// </remarks>
    public void Clear()
    {
        ThrowIfDisposed();

        SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderDrawColor(Handle, 0, 0, 0, 1));
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderClear(Handle));
    }

    /// <summary>
    /// Clear the current rendering target with the drawing color.
    /// </summary>
    /// <remarks>
    /// This clears the entire rendering target, ignoring the viewport and the clip rectangle.
    /// Note, that clearing will also set/fill all pixels of the rendering target to current renderer draw color,
    /// so make sure to call <see cref="SetDrawingColor(Color)"/>/<see cref="SetDrawingColor(ColorF)"/> when needed.
    /// </remarks>
    /// <param name="color">The color to clear with.</param>
    public void Clear(Color color)
    {
        ThrowIfDisposed();

        SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A));
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderClear(Handle));
    }

    /// <summary>
    /// Clear the current rendering target with the drawing color.
    /// </summary>
    /// <remarks>
    /// This clears the entire rendering target, ignoring the viewport and the clip rectangle.
    /// Note, that clearing will also set/fill all pixels of the rendering target to current renderer draw color,
    /// so make sure to call <see cref="SetDrawingColor(Color)"/>/<see cref="SetDrawingColor(ColorF)"/> when needed.
    /// </remarks>
    /// <param name="color">The color to clear with.</param>
    public void Clear(ColorF color)
    {
        ThrowIfDisposed();

        SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderDrawColorFloat(Handle, color.R, color.G, color.B, color.A));
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderClear(Handle));
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle is null)
            return;

        _window?.Unbind(this);
        _window = null;

        SDL3.DestroyRenderer(Handle);
        Handle = null;
    }

    /// <inheritdoc/>
    public void Draw(IDrawable drawable) => Draw(drawable, RenderState.Default);

    /// <inheritdoc/>
    public void Draw(IDrawable drawable, RenderState state)
    {
        ThrowIfDisposed();
        drawable.Draw(this, state);
    }

    /// <inheritdoc/>
    public void Draw(ReadOnlySpan<Vertex> vertices, RenderState state) => DrawGeometry(vertices, [], state);

    /// <inheritdoc/>
    public void Draw(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices, RenderState state) => DrawGeometry(vertices, indices, state);

    /// <summary>
    /// Draws a single point.
    /// </summary>
    /// <param name="point">The point to draw, in render coordinates.</param>
    /// <exception cref="QuackInteropException">Failed to draw the point.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void Draw(PointF point)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderPoint(Handle, point.X, point.Y));
    }

    /// <summary>
    /// Draws multiple points.
    /// </summary>
    /// <param name="points">The points to draw, in render coordinates.</param>
    /// <param name="connected"><see langword="true"/> to connect all theses points otherwise <see langword="false"/>.</param>
    /// <exception cref="QuackInteropException">Failed to draw the points.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void Draw(ReadOnlySpan<PointF> points, bool connected = false)
    {
        ThrowIfDisposed();

        if (connected)
            SDLThrowHelper.ThrowIfFailed(SDL3.RenderLines(Handle, points, points.Length));
        else
            SDLThrowHelper.ThrowIfFailed(SDL3.RenderPoints(Handle, points, points.Length));
    }

    /// <summary>
    /// Draws a line between two points.
    /// </summary>
    /// <param name="from">The start point, in render coordinates.</param>
    /// <param name="to">The end point, in render coordinates.</param>
    /// <exception cref="QuackInteropException">Failed to draw the line.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void Draw(PointF from, PointF to)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderLine(Handle, from.X, from.Y, to.X, to.Y));
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to outline, in render coordinates.</param>
    /// <param name="filled">draw the rectangle as outline or filled.</param>
    /// <exception cref="QuackInteropException">Failed to draw the rectangle.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void Draw(Rect rect, bool filled = true)
    {
        ThrowIfDisposed();

        if (filled)
            SDLThrowHelper.ThrowIfFailed(SDL3.RenderFillRect(Handle, &rect));
        else
            SDLThrowHelper.ThrowIfFailed(SDL3.RenderRect(Handle, &rect));
    }

    /// <summary>
    /// Draws multiple rectangles.
    /// </summary>
    /// <param name="rects">The rectangles to outline, in render coordinates.</param>
    /// <param name="filled">draw the multiple rectangles as outline or filled.</param>
    /// <exception cref="QuackInteropException">Failed to draw the rectangles.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void Draw(ReadOnlySpan<Rect> rects, bool filled = true)
    {
        ThrowIfDisposed();

        if (filled)
            SDLThrowHelper.ThrowIfFailed(SDL3.RenderFillRects(Handle, rects, rects.Length));
        else
            SDLThrowHelper.ThrowIfFailed(SDL3.RenderRects(Handle, rects, rects.Length));
    }

    /// <summary>
    /// Draws a texture, or a region of it, onto the current target.
    /// </summary>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="destination">The rectangle to draw into, in render coordinates, or <see langword="null"/> to fill the entire target.</param>
    /// <param name="source">The region of the texture to draw, or <see langword="null"/> to draw the whole texture.</param>
    /// <exception cref="QuackInteropException">Failed to draw the texture.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void Draw(Texture texture, Rect? destination = null, Rect? source = null)
    {
        ThrowIfDisposed();

        Rect src = source.GetValueOrDefault();
        Rect dst = destination.GetValueOrDefault();

        Rect* sourceRect = source.HasValue ? &src : null;
        Rect* destinationRect = destination.HasValue ? &dst : null;

        SDLThrowHelper.ThrowIfFailed(SDL3.RenderTexture(Handle, texture.Handle, sourceRect, destinationRect));
    }

    /// <summary>
    /// Draws a texture into a destination rectangle with rotation and optional flipping.
    /// </summary>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="destination">The rectangle to draw into, in render coordinates.</param>
    /// <param name="angle">The clockwise rotation, applied around <paramref name="center"/>.</param>
    /// <param name="center">The point to rotate around, relative to the top-left of <paramref name="destination"/>, or <see langword="null"/> to rotate around its center.</param>
    /// <param name="flip">The axes to flip the texture along before drawing.</param>
    /// <param name="source">The region of the texture to draw, or <see langword="null"/> to draw the whole texture.</param>
    /// <exception cref="QuackInteropException">Failed to draw the texture.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void Draw(Texture texture, Rect destination, Angle angle, PointF? center = null, FlipMode flip = FlipMode.None, Rect? source = null)
    {
        ThrowIfDisposed();

        Rect src = source.GetValueOrDefault();
        PointF pivot = center.GetValueOrDefault();

        Rect* sourceRect = source.HasValue ? &src : null;
        PointF* centerPoint = center.HasValue ? &pivot : null;

        SDLThrowHelper.ThrowIfFailed(SDL3.RenderTextureRotated(Handle, texture.Handle, sourceRect, &destination, angle.Degrees, centerPoint, flip));
    }

    /// <summary>
    /// Draws a texture repeated to fill a destination rectangle.
    /// </summary>
    /// <param name="texture">The texture to tile.</param>
    /// <param name="destination">The rectangle to fill, in render coordinates.</param>
    /// <param name="scale">The scale applied to the texture before tiling; 1 tiles it at its natural size.</param>
    /// <param name="source">The region of the texture to tile, or <see langword="null"/> to tile the whole texture.</param>
    /// <exception cref="QuackInteropException">Failed to draw the texture.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void DrawTiled(Texture texture, Rect destination, float scale = 1.0f, Rect? source = null)
    {
        ThrowIfDisposed();

        Rect src = source.GetValueOrDefault();
        Rect* sourceRect = source.HasValue ? &src : null;

        SDLThrowHelper.ThrowIfFailed(SDL3.RenderTextureTiled(Handle, texture.Handle, sourceRect, scale, &destination));
    }

    /// <summary>
    /// Draws a texture transformed by an affine mapping defined by three corners.
    /// </summary>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="origin">Where the top-left corner of the texture is mapped to, in render coordinates.</param>
    /// <param name="right">Where the top-right corner of the texture is mapped to, in render coordinates.</param>
    /// <param name="down">Where the bottom-left corner of the texture is mapped to, in render coordinates.</param>
    /// <param name="source">The region of the texture to draw, or <see langword="null"/> to draw the whole texture.</param>
    /// <exception cref="QuackInteropException">Failed to draw the texture.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void DrawAffine(Texture texture, PointF origin, PointF right, PointF down, Rect? source = null)
    {
        ThrowIfDisposed();

        Rect src = source.GetValueOrDefault();
        Rect* sourceRect = source.HasValue ? &src : null;

        SDLThrowHelper.ThrowIfFailed(SDL3.RenderTextureAffine(Handle, texture.Handle, sourceRect, &origin, &right, &down));
    }

    /// <summary>
    /// Draws a texture as a nine-patch, keeping its corners unscaled while the edges and center stretch to fill a destination rectangle.
    /// </summary>
    /// <remarks>Useful for resizable UI panels and buttons that should keep crisp corners.</remarks>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="destination">The rectangle to fill, in render coordinates.</param>
    /// <param name="leftWidth">The width of the left corners, in texture pixels.</param>
    /// <param name="rightWidth">The width of the right corners, in texture pixels.</param>
    /// <param name="topHeight">The height of the top corners, in texture pixels.</param>
    /// <param name="bottomHeight">The height of the bottom corners, in texture pixels.</param>
    /// <param name="scale">The scale applied to the corner sizes, or 0 to copy them unscaled.</param>
    /// <param name="source">The region of the texture to use, or <see langword="null"/> to use the whole texture.</param>
    /// <exception cref="QuackInteropException">Failed to draw the texture.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void DrawNineGrid(Texture texture, Rect destination, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale = 0.0f, Rect? source = null)
    {
        ThrowIfDisposed();

        Rect src = source.GetValueOrDefault();
        Rect* sourceRect = source.HasValue ? &src : null;

        SDLThrowHelper.ThrowIfFailed(SDL3.RenderTexture9Grid(Handle, texture.Handle, sourceRect, leftWidth, rightWidth, topHeight, bottomHeight, scale, &destination));
    }

    /// <summary>
    /// Draws a texture as a nine-patch whose edges and center are tiled rather than stretched to fill a destination rectangle.
    /// </summary>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="destination">The rectangle to fill, in render coordinates.</param>
    /// <param name="leftWidth">The width of the left corners, in texture pixels.</param>
    /// <param name="rightWidth">The width of the right corners, in texture pixels.</param>
    /// <param name="topHeight">The height of the top corners, in texture pixels.</param>
    /// <param name="bottomHeight">The height of the bottom corners, in texture pixels.</param>
    /// <param name="scale">The scale applied to the corner sizes, or 0 to copy them unscaled.</param>
    /// <param name="tileScale">The scale applied to the tiled edges and center; 1 tiles them at their natural size.</param>
    /// <param name="source">The region of the texture to use, or <see langword="null"/> to use the whole texture.</param>
    /// <exception cref="QuackInteropException">Failed to draw the texture.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void DrawNineGridTiled(Texture texture, Rect destination, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale = 0.0f, float tileScale = 1.0f, Rect? source = null)
    {
        ThrowIfDisposed();

        Rect src = source.GetValueOrDefault();
        Rect* sourceRect = source.HasValue ? &src : null;

        SDLThrowHelper.ThrowIfFailed(SDL3.RenderTexture9GridTiled(Handle, texture.Handle, sourceRect, leftWidth, rightWidth, topHeight, bottomHeight, scale, &destination, tileScale));
    }

    /// <summary>
    /// Draws a single line of debug text using the built-in 8x8 font.
    /// </summary>
    /// <remarks>
    /// Intended for diagnostics rather than production text; use a font for high-quality text.
    /// </remarks>
    /// <param name="position">The position of the top-left corner of the text, in render coordinates.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="color">The color to use for the text.</param>
    /// <exception cref="QuackInteropException">Failed to draw the text.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void DrawDebugText(PointF position, string text, Color color)
    {
        ThrowIfDisposed();

        SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A));
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderDebugText(Handle, position.X, position.Y, text));
    }

    /// <summary>
    /// Force the rendering context to flush any pending commands and state.
    /// </summary>
    /// <remarks>
    /// You do not need to (and in fact, shouldn't) call this method unless you are planning to call
    /// into OpenGL/Direct3D/Metal/whatever directly, in addition to using a <see cref="Renderer"/>.
    /// </remarks>
    public void Flush()
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(SDL3.FlushRenderer(Handle));
    }

    /// <summary>
    /// Converts a point from window coordinates to render coordinates.
    /// </summary>
    /// <remarks>This accounts for the current logical presentation, viewport and scale.</remarks>
    /// <param name="point">The point in window coordinates.</param>
    /// <returns>The equivalent point in render coordinates.</returns>
    /// <exception cref="QuackInteropException">Failed to convert the coordinates.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public PointF MapCoordinatesFromWindow(PointF point)
    {
        ThrowIfDisposed();

        float x, y;
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderCoordinatesFromWindow(Handle, point.X, point.Y, &x, &y));

        return new PointF(x, y);
    }

    /// <summary>
    /// Converts a point from render coordinates to window coordinates.
    /// </summary>
    /// <remarks>This accounts for the current logical presentation, viewport and scale.</remarks>
    /// <param name="point">The point in render coordinates.</param>
    /// <returns>The equivalent point in window coordinates.</returns>
    /// <exception cref="QuackInteropException">Failed to convert the coordinates.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public PointF MapCoordinatesToWindow(PointF point)
    {
        ThrowIfDisposed();

        float x, y;
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderCoordinatesToWindow(Handle, point.X, point.Y, &x, &y));

        return new PointF(x, y);
    }

    /// <summary>
    /// Presents everything drawn since the last present to the window.
    /// </summary>
    /// <remarks>The back buffer should be considered invalidated after presenting; clear it before drawing the next frame.</remarks>
    /// <exception cref="QuackInteropException">Failed to present the renderer.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public void Present()
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(SDL3.RenderPresent(Handle));
    }

    /// <summary>
    /// Reads pixels from the current render target into a new surface.
    /// </summary>
    /// <remarks>
    /// This is a slow operation and is not intended for per-frame use. The caller owns the returned surface and must dispose it.
    /// </remarks>
    /// <param name="area">The region to read, in render coordinates, or <see langword="null"/> to read the entire target.</param>
    /// <returns>A new surface containing the pixels that were read.</returns>
    /// <exception cref="QuackInteropException">Failed to read the pixels.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Surface ReadPixels(RectI? area = null)
    {
        ThrowIfDisposed();

        SDL_Surface* surface;

        if (area is null)
        {
            surface = SDL3.RenderReadPixels(Handle, null);
        }
        else
        {
            RectI rect = area.Value;
            surface = SDL3.RenderReadPixels(Handle, &rect);
        }

        SDLThrowHelper.ThrowIfNull(surface);
        return new Surface(surface);
    }

    /// <summary>
    /// Set the color used for drawing operations.
    /// </summary>
    /// <param name="color">The color to use for drawing operations.</param>
    public void SetDrawingColor(Color color)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A));
    }

    /// <summary>
    /// Set the color used for drawing operations.
    /// </summary>
    /// <param name="color">The color to use for drawing operations.</param>
    public void SetDrawingColor(ColorF color)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderDrawColorFloat(Handle, color.R, color.G, color.B, color.A));
    }

    /// <summary>
    /// Redirects drawing into <paramref name="texture"/> until the returned scope is disposed.
    /// </summary>
    /// <remarks>
    /// The texture must have been created with <see cref="TextureAccess.Target"/>. Drawing commands issued while the
    /// scope is alive render into the texture instead of the window; dispose the scope to restore the previous target.
    /// </remarks>
    /// <param name="texture">The texture to draw into.</param>
    /// <returns>A scope that restores the previous render target when disposed.</returns>
    /// <exception cref="QuackInteropException">Failed to set the render target.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public RenderTargetScope Target(Texture texture)
    {
        SDLThrowHelper.ThrowIfFailed(SDL3.SetRenderTarget(Handle, texture.Handle));
        return new RenderTargetScope(Handle);
    }

    /// <summary>
    /// Redirects drawing to the given view: sets <see cref="Viewport"/> from <see cref="View.Viewport"/> and returns a
    /// scope carrying the view's transform, until the scope is disposed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Pass <see cref="ViewScope.State"/> to <see cref="Draw(IDrawable, RenderState)"/> for everything drawn through this view.
    /// </para>
    /// <para>
    /// Uses <see cref="CurrentOutputSize"/>, which accounts for logical <see cref="Presentation"/>, so the view's
    /// normalized <see cref="View.Viewport"/> stays correct whether or not logical presentation is in effect.
    /// </para>
    /// </remarks>
    /// <param name="view">The view to draw through.</param>
    /// <returns>A scope carrying the view's render state that restores the full-target viewport when disposed.</returns>
    /// <exception cref="QuackInteropException">Failed to set the viewport.</exception>
    /// <exception cref="ObjectDisposedException">The renderer is disposed.</exception>
    public ViewScope View(View view)
    {
        ThrowIfDisposed();

        RectI pixelViewport = view.ComputeViewport(CurrentOutputSize);
        Viewport = pixelViewport;

        RenderState state = RenderState.Default with { Transform = view.GetTransform(pixelViewport.Size) };
        return new ViewScope(this, state);
    }

    private void DrawGeometry(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices, RenderState state)
    {
        ThrowIfDisposed();

        BlendMode = state.BlendMode;
        SDL_Texture* texture = state.Texture?.Handle;

        if (state.Transform == Transform.Identity || state.Transform == default)
        {
            Submit(texture, vertices, indices);
            return;
        }

        int count = vertices.Length;

        Span<Vertex> transformed = count > MaxStackVertices ? new Vertex[count] : stackalloc Vertex[count];

        for (int i = 0; i < count; i++)
        {
            Vertex vertex = vertices[i];
            vertex.Position = state.Transform.TransformPoint(vertex.Position);

            transformed[i] = vertex;
        }

        Submit(texture, transformed, indices);
    }

    private void Submit(SDL_Texture* texture, ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices)
    {
        if (indices.IsEmpty)
        {
            SDLThrowHelper.ThrowIfFailed(SDL3.UnsafeRenderGeometry(Handle, texture, vertices, vertices.Length, null, 0));
            return;
        }

        SDLThrowHelper.ThrowIfFailed(SDL3.RenderGeometry(Handle, texture, vertices, vertices.Length, indices, indices.Length));
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(Handle is null, typeof(Renderer));
}

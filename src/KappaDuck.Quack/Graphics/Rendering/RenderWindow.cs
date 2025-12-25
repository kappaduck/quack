// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Video.Displays;
using KappaDuck.Quack.Windows;
using System.ComponentModel;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// Represents a window that can be used to render 2D graphics.
/// </summary>
public sealed class RenderWindow : Window, IRenderTarget
{
    private bool _disposed;
    private SDL_Renderer _renderer = new();

    /// <summary>
    /// Creates an empty window.
    /// </summary>
    /// <remarks>
    /// It does not create the window. Use <see cref="Create(string, int, int, string?)"/> to create a window.
    /// It is useful to delay window creation until necessary.
    /// </remarks>
    public RenderWindow()
    {
    }

    /// <summary>
    /// Creates a window with the title, dimensions, and optional renderer.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="rendererName">The name of the renderer to use for drawing, or <see langword="null"/> to use the default renderer.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window or renderer.</exception>
    public RenderWindow(string title, int width, int height, string? rendererName = null) : base(title, width, height)
        => InitializeRenderer(rendererName);

    /// <summary>
    /// Creates a window with the title, size, and optional renderer.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The dimensions of the window.</param>
    /// <param name="rendererName">The name of the renderer to use for drawing, or <see langword="null"/> to use the default renderer.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window or renderer.</exception>
    public RenderWindow(string title, SizeInt size, string? rendererName = null) : base(title, size)
        => InitializeRenderer(rendererName);

    /// <summary>
    /// Creates a window with the title, position, and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="position">The position of the window on the screen.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="rendererName">The name of the renderer to use for drawing, or <see langword="null"/> to use the default renderer.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window or renderer.</exception>
    public RenderWindow(string title, Vector2Int position, SizeInt size, string? rendererName = null) : base(title, position, size)
        => InitializeRenderer(rendererName);

    /// <summary>
    /// Creates a window with the title and fullscreen display mode.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="mode">The fullscreen display mode.</param>
    /// <param name="rendererName">The name of the renderer to use for drawing, or <see langword="null"/> to use the default renderer.</param>
    /// <remarks>
    /// This creates the window immediately upon instantiation in fullscreen mode using the specified display mode,
    /// and sets the window size to the mode's width and height.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window or renderer.</exception>
    public RenderWindow(string title, DisplayMode mode, string? rendererName = null) : base(title, mode)
        => InitializeRenderer(rendererName);

    /// <summary>
    /// Gets the final presentation rectangle for rendering.
    /// </summary>
    /// <remarks>
    /// It returns the calculated rectangle used for logical presentation, based on the presentation
    /// mode and output size. If logical presentation is <see cref="LogicalPresentation.Disabled"/>, it will fill
    /// the rectangle with the output size, in pixels.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get the presentation rectangle.</exception>
    public Rect CalculatedPresentation
    {
        get
        {
            if (_renderer.IsInvalid)
                return default;

            QuackNativeException.ThrowIfFailed(Native.SDL_GetRenderLogicalPresentationRect(_renderer, out Rect rectangle));
            return rectangle;
        }
    }

    /// <summary>
    /// Gets the current output size in pixels of a rendering context.
    /// </summary>
    /// <remarks>
    /// If a rendering target is active, this will return the size of the rendering target in pixels,
    /// otherwise if a logical size is set, it will return the logical size,
    /// otherwise it will return the value of <see cref="OutputSize"/>.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get the current output size.</exception>
    internal (int Width, int Height) CurrentOutputSize
    {
        get
        {
            if (_renderer.IsInvalid)
                return (0, 0);

            QuackNativeException.ThrowIfFailed(Native.SDL_GetCurrentRenderOutputSize(_renderer, out int w, out int h));
            return (w, h);
        }
    }

    /// <summary>
    /// Gets the output size in pixels of a rendering context.
    /// </summary>
    /// <remarks>
    /// It return the true output size in pixels, ignoring any render targets or logical size and presentation.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get the output size.</exception>
    internal (int Width, int Height) OutputSize
    {
        get
        {
            if (_renderer.IsInvalid)
                return (0, 0);

            QuackNativeException.ThrowIfFailed(Native.SDL_GetRenderOutputSize(_renderer, out int w, out int h));
            return (w, h);
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
    /// You can convert coordinates in an event into rendering coordinates using <see cref="MapEventToCoordinates(ref Event)"/> or <see cref="MapPixelsToCoordinates(Vector2)"/>.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">The width or height is negative.</exception>
    /// <exception cref="QuackNativeException">Thrown when failed to get or set the presentation.</exception>
    public (int Width, int Height, LogicalPresentation Mode) Presentation
    {
        get
        {
            if (_renderer.IsInvalid)
                return (0, 0, LogicalPresentation.Disabled);

            QuackNativeException.ThrowIfFailed(Native.SDL_GetRenderLogicalPresentation(_renderer, out int width, out int height, out LogicalPresentation mode));
            return (width, height, mode);
        }
        set
        {
            if (_renderer.IsInvalid)
                return;

            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            QuackNativeException.ThrowIfFailed(Native.SDL_SetRenderLogicalPresentation(_renderer, value.Width, value.Height, value.Mode));
        }
    }

    /// <summary>
    /// Gets the safe area for rendering within the current viewport
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the window is not open, it will return an empty <see cref="RectInt"/>.
    /// </para>
    /// <para>
    /// Some devices have portions of the screen which are partially obscured or not interactive,
    /// possibly due to on-screen controls, curved edges, camera notches, TV over scan, etc.
    /// This provides the area of the window which is safe to have interactable content.
    /// You should continue rendering into the rest of the render target,
    /// but it should not contain visually important or interactable content.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get the safe area.</exception>
    public RectInt RenderSafeArea
    {
        get
        {
            if (_renderer.IsInvalid)
                return default;

            QuackNativeException.ThrowIfFailed(Native.SDL_GetRenderSafeArea(_renderer, out RectInt rectangle));
            return rectangle;
        }
    }

    /// <summary>
    /// Gets or sets the vertical synchronization (VSync) of the renderer.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a renderer is created, VSync defaults to <see cref="VSync.Disabled"/>.
    /// </para>
    /// <para>
    /// The value can be 1 to synchronize present with every vertical refresh, 2 to synchronize present with every other vertical refresh, and so on.
    /// <see cref="VSync.Adaptive"/> can be used for adaptive VSync or <see cref="VSync.Disabled"/> to disable. Not every value is supported by every driver.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to get or set the VSync.</exception>
    public int VSync
    {
        get;
        set
        {
            if (field == value)
                return;

            field = value;

            if (_renderer.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetRenderVSync(_renderer, field));
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// It will clear the render target with a black color.
    /// If you want to clear the render target with a different color, use <see cref="Clear(Color)"/> instead.
    /// </remarks>
    public void Clear()
    {
        if (_renderer.IsInvalid)
            return;

        Native.SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 1);
        Native.SDL_RenderClear(_renderer);
    }

    /// <inheritdoc/>
    public void Clear(Color color)
    {
        if (_renderer.IsInvalid)
            return;

        Native.SDL_SetRenderDrawColor(_renderer, color.R, color.G, color.B, color.A);
        Native.SDL_RenderClear(_renderer);
    }

    /// <summary>
    /// Creates a window with the title, size and an optional renderer
    /// </summary>
    /// <remarks>
    /// If the window is already created, this method has no effect.
    /// </remarks>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="rendererName">The name of the renderer to use for drawing, or <see langword="null"/> to use the default renderer.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window or renderer.</exception>
    public void Create(string title, int width, int height, string? rendererName)
    {
        if (_renderer.IsInvalid)
            return;

        Create(title, width, height);
        InitializeRenderer(rendererName);
    }

    /// <summary>
    /// Create a texture for a rendering context.
    /// </summary>
    /// <remarks>
    /// <para>The contents of a texture when first created are not defined.</para>
    /// <para>
    /// The texture is bound to the render window who's creating it. It can't be used with other windows.
    /// You must create a new texture for each render window.
    /// </para>
    /// </remarks>
    /// <param name="format">The pixel format of the texture.</param>
    /// <param name="access">The access level of the texture.</param>
    /// <param name="width">The width of the texture.</param>
    /// <param name="height">The height of the texture.</param>
    /// <returns>The created texture.</returns>
    /// <exception cref="QuackNativeException">Thrown when failed to create the texture.</exception>
    public Texture CreateTexture(PixelFormat format, TextureAccess access, int width, int height) => new(_renderer, format, access, width, height);

    /// <summary>
    /// Creates a texture from a surface.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The surface is not modified or freed by this method.
    /// </para>
    /// <para>
    /// The texture access level is set to <see cref="TextureAccess.Static"/>.
    /// </para>
    /// <para>
    /// The pixel format of the created texture may be different from the surface's pixel format.
    /// </para>
    /// <para>
    /// The texture is bound to the render window who's creating it. It can't be used with other windows.
    /// You must create a new texture for each render window.
    /// </para>
    /// </remarks>
    /// <param name="surface">The surface to create the texture from.</param>
    /// <returns>The created texture.</returns>
    /// <exception cref="QuackNativeException">Thrown when failed to create the texture.</exception>
    public Texture CreateTexture(Surface surface) => new(_renderer, surface);

    /// <inheritdoc/>
    public void Draw(IDrawable drawable) => drawable.Draw(this);

    /// <inheritdoc/>
    public unsafe void Draw(ReadOnlySpan<Vertex> vertices)
    {
        if (_renderer.IsInvalid)
            return;

        Native.SDL_RenderGeometry(_renderer, nint.Zero, vertices, vertices.Length);
    }

    /// <inheritdoc/>
    public void Draw(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices)
    {
        if (_renderer.IsInvalid)
            return;

        Native.SDL_RenderGeometry(_renderer, nint.Zero, vertices, vertices.Length, indices, indices.Length);
    }

    /// <summary>
    /// Draw text to the window for debugging.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It will render a string of text to the window. Note that this is a convenience function for debugging,
    /// with severe limitations, and not intended to be used for production apps and games.
    /// </para>
    /// <para>
    /// Among these limitations:
    /// <list type="bullet">
    /// <item>It accepts UTF-8 strings, but will only renders ASCII characters.</item>
    /// <item>It has a single, tiny size (8x8 pixels). One can use logical presentation or scaling to adjust it, but it will be blurry.</item>
    /// <item>It uses a simple, hardcoded bitmap font. It does not allow different font selections and it does not support truetype, for proper scaling.</item>
    /// <item>It does no word-wrapping and does not treat newline characters as a line break. If the text goes out of the window, it's gone.</item>
    /// </list>
    /// </para>
    /// <para>For serious text rendering, there are several good options, such as Font/Text.</para>
    /// <para>
    /// On first use, this will create an internal texture for rendering glyphs. This texture will live until the renderer is destroyed.
    /// </para>
    /// <para>
    /// Be sure to call <see cref="Clear(Color)"/> before drawing the text otherwise it may be overwritten.
    /// </para>
    /// </remarks>
    /// <param name="position">The position to draw the text at.</param>
    /// <param name="text">The text to render.</param>
    /// <param name="color">The color of the text. If <see langword="null"/>, it will use white.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to draw the debug text.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void DrawDebugText(Vector2 position, string text, Color? color = null)
    {
        if (_renderer.IsInvalid)
            return;

        Color textColor = color ?? Color.White;

        Native.SDL_SetRenderDrawColor(_renderer, textColor.R, textColor.G, textColor.B, textColor.A);
        QuackNativeException.ThrowIfFailed(Native.SDL_RenderDebugText(_renderer, position.X, position.Y, text));
    }

    /// <summary>
    /// Load an image from a path into a texture.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This can be significantly more efficient than using a CPU-bound <see cref="Surface"/> if you don't need to manipulate the image directly after loading it.
    /// If the loaded image has transparency or a color key, a texture with an alpha channel will be created.
    /// Otherwise, the engine will attempt to create a texture in the most format that most reasonably represents the image data (but in many cases, this will just end up being 32-bit RGB or 32-bit RGBA).
    /// </para>
    /// <para>
    /// If you would rather decode an image to a <see cref="Surface"/>, call <see cref="Image.Load(string)"/> instead.
    /// </para>
    /// </remarks>
    /// <param name="file">The path to the image file.</param>
    /// <returns>A texture representing the loaded image.</returns>
    public Texture LoadTexture(string file) => new(_renderer, file);

    /// <inheritdoc cref="LoadTexture(string)"/>
    public Texture LoadTexture(FileInfo file) => LoadTexture(file.FullName);

    /// <inheritdoc/>
    public Vector2 MapCoordinatesToPixels(Vector2 point)
    {
        if (_renderer.IsInvalid)
            return default;

        Native.SDL_RenderCoordinatesToWindow(_renderer, point.X, point.Y, out float x, out float y);
        return new Vector2(x, y);
    }

    /// <inheritdoc/>
    public void MapEventToCoordinates(ref Event e)
    {
        if (_renderer.IsInvalid)
            return;

        Native.SDL_ConvertEventToRenderCoordinates(_renderer, ref e);
    }

    /// <inheritdoc/>
    public Vector2 MapPixelsToCoordinates(Vector2 point)
    {
        if (_renderer.IsInvalid)
            return default;

        Native.SDL_RenderCoordinatesFromWindow(_renderer, point.X, point.Y, out float x, out float y);
        return new Vector2(x, y);
    }

    /// <summary>
    /// Presents the current frame to the screen.
    /// </summary>
    public void Present()
    {
        if (_renderer.IsInvalid)
            return;

        Native.SDL_RenderPresent(_renderer);
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        _disposed = true;

        if (disposing)
            _renderer.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeRenderer(string? rendererName)
    {
        _renderer = Native.SDL_CreateRenderer(NativeHandle, rendererName);
        QuackNativeException.ThrowIfHandleInvalid(_renderer);

        QuackNativeException.ThrowIfFailed(Native.SDL_SetRenderVSync(_renderer, VSync));
        QuackNativeException.ThrowIfFailed(Native.SDL_SetRenderLogicalPresentation(_renderer, Presentation.Width, Presentation.Height, Presentation.Mode));
    }
}

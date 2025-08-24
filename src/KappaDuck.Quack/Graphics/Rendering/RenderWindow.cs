// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Interop.SDL.Handles;
using KappaDuck.Quack.Video.Displays;
using KappaDuck.Quack.Windows;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// Represents a window that can be used to render 2D graphics using SDL renderer api.
/// </summary>
public sealed class RenderWindow : IRenderTarget, IDisposable
{
    private readonly Window _window = new();
    private Renderer _renderer = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderWindow"/>.
    /// </summary>
    public RenderWindow()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderWindow"/>.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="rendererName">The name of the rendering driver to initialize, or <see langword="null"/> to let the engine choose one.</param>
    public RenderWindow(string title, int width, int height, string? rendererName = null) => Create(title, width, height, rendererName);

    /// <summary>
    /// Gets or sets a value indicating whether the window is always on top.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window always on top.</exception>
    public bool AlwaysOnTop
    {
        get => _window.AlwaysOnTop;
        set => _window.AlwaysOnTop = value;
    }

    /// <summary>
    /// Gets or sets the aspect ratio of the window's client area.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Minimum or maximum is negative.</exception>
    /// <exception cref="QuackNativeException">An error occurred while setting the window aspect ratio.</exception>
    public (float Minimum, float Maximum) AspectRatio
    {
        get => _window.AspectRatio;
        set => _window.AspectRatio = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is borderless.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window borderless.</exception>
    public bool Borderless
    {
        get => _window.Borderless;
        set => _window.Borderless = value;
    }

    /// <summary>
    /// Gets the size of the window's borders (decorations) around the client area.
    /// </summary>
    /// <remarks>
    /// <para>If the window is not open or borderless, it will return (0, 0, 0, 0).</para>
    /// <para>
    /// It is possible that failed to get the border size because the window has not yet been decorated by the display server
    /// or the information is not supported.
    /// </para>
    /// </remarks>
    public (int Top, int Left, int Bottom, int Right) BordersSize => _window.BordersSize;

    /// <summary>
    /// Gets the current output size in pixels of a rendering context.
    /// </summary>
    /// <remarks>
    /// If a rendering target is active, this will return the size of the rendering target in pixels,
    /// otherwise if a logical size is set, it will return the logical size,
    /// otherwise it will return the value of <see cref="OutputSize"/>.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while getting the current renderer output size.</exception>
    public (int Width, int Height) CurrentOutputSize => _renderer.CurrentOutputSize;

    /// <summary>
    /// Gets the display associated with the window.
    /// </summary>
    /// <remarks>
    /// If the window is not open, it will return null as the display cannot be determined.
    /// </remarks>
    public Display? Display => _window.Display;

    /// <summary>
    /// Gets the content display scale relative to the window's pixel size.
    /// </summary>
    /// <remarks>
    /// <para>If the window is not open, it will return 0.0f.</para>
    /// <para>
    /// This is a combination of the window pixel density and the display content scale,
    /// and is the expected scale for displaying content in this window.
    /// For example, if a 3840x2160 window had a display scale of 2.0,
    /// the user expects the content to take twice as many pixels and be the same physical size
    /// as if it were being displayed in a 1920x1080 window with a display scale of 1.0.
    /// </para>
    /// <para>Conceptually this value corresponds to the scale display setting, and is updated when that setting is changed, or the window moves to a display with a different scale setting.</para>
    /// </remarks>
    public float DisplayScale => _window.DisplayScale;

    /// <summary>
    /// Gets or sets a value indicating whether the window is focusable.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window focusable.</exception>
    public bool Focusable
    {
        get => _window.Focusable;
        set => _window.Focusable = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is fullscreen.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while settings the window fullscreen.</exception>
    public bool Fullscreen
    {
        get => _window.Fullscreen;
        set => _window.Fullscreen = value;
    }

    /// <summary>
    /// Gets or sets the fullscreen display mode to use when
    /// the window is in Fullscreen state.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Setting to <see langword="null"/> will use borderless fullscreen desktop mode,
    /// or one of the fullscreen modes from <see cref="Display.GetFullScreenModes"/> to set an exclusive fullscreen mode.
    /// </para>
    /// <para>
    /// If the window is currently in Fullscreen state, this request is asynchronous on some windowing
    /// systems and the new mode dimensions may not be applied immediately. If an immediate change is needed, call <see cref="Sync"/> to block
    /// until the changes have taken effect.
    /// </para>
    /// <para>
    /// When the new mode takes effect, an <see cref="EventType.WindowResized"/> and/or
    /// an <see cref="EventType.WindowPixelSizeChanged"/> event will be emitted with the new mode dimensions.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window fullscreen mode.</exception>
    public DisplayMode? FullscreenMode
    {
        get => _window.FullscreenMode;
        set => _window.FullscreenMode = value;
    }

    /// <summary>
    /// Gets a value indicating whether the window has keyboard focus.
    /// </summary>
    public bool HasKeyboardFocus => _window.HasKeyboardFocus;

    /// <summary>
    /// Gets a value indicating whether the window has mouse focus.
    /// </summary>
    public bool HasMouseFocus => _window.HasMouseFocus;

    /// <summary>
    /// Gets a safe, non-owning handle to the native window.
    /// </summary>
    /// <remarks>
    /// This handle is valid only while the <see cref="Window"/> is alive.
    /// Disposing this handle will not close the window; use the <see cref="Dispose"/> instead.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">The window has been disposed or not open.</exception>
    public WindowHandle Handle => _window.Handle;

    /// <summary>
    /// Gets or sets the height of the window.
    /// </summary>
    /// <remarks>
    /// <para>Setting the height if the window is in Fullscreen or Maximized state will be ignored.</para>
    /// <para>To change the exclusive fullscreen mode dimensions, use <see cref="FullscreenMode"/>.</para>
    /// <para>It will be restricted by <see cref="MinimumSize"/> and <see cref="MaximumSize"/>.</para>
    /// <para>
    /// On some windowing systems this request is asynchronous and the new window state may not have been applied immediately.
    /// If an immediate change is required, call <see cref="Sync"/> to block until the changes have taken effect.
    /// </para>
    /// <para>
    /// When the window size changes, an <see cref="EventType.WindowResized"/> event will be emitted with the new dimensions.
    /// Note that the new dimensions may not be the same as those requested, as the windowing system may impose its own constraints.
    /// (e.g constraining the size of the content area to remain within the usable desktop bounds). Additionally,
    /// as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Height is less than or equal to 0.</exception>
    /// <exception cref="QuackNativeException">An error occurred while setting the window height.</exception>
    public int Height
    {
        get => _window.Height;
        set => _window.Height = value;
    }

    /// <summary>
    /// Gets the height of the window in pixels.
    /// </summary>
    public int HeightInPixel => _window.HeightInPixel;

    /// <summary>
    /// Gets a value indicating whether the window is hidden.
    /// </summary>
    public bool Hidden
    {
        get => _window.Hidden;
        init => _window.Hidden = value;
    }

    /// <summary>
    /// Gets the window's identifier.
    /// </summary>
    /// <remarks>
    /// The identifier is what <see cref="WindowEvent"/> references.
    /// </remarks>
    public uint Id => _window.Id;

    /// <summary>
    /// Gets a value indicating whether the window is currently open.
    /// </summary>
    public bool IsOpen => _window.IsOpen;

    /// <summary>
    /// Gets a value indicating whether the screen keyboard is visible.
    /// </summary>
    public bool IsScreenKeyboardVisible => _window.IsScreenKeyboardVisible;

    /// <summary>
    /// Gets or sets a value indicating whether the window has grabbed keyboard input.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Keyboard grab enables capture of system keyboard shortcuts like Alt+Tab or the Meta/Super key.
    /// Important to note that not all system keyboard shortcuts can be captured by applications (one example is Ctrl+Alt+Del on Windows).
    /// </para>
    /// <para>
    /// This is primarily intended for specialized applications such as VNC clients or VM frontends. Normal games should not use keyboard grab.
    /// </para>
    /// <para>
    /// When keyboard is enabled, SDL will continue to handle Alt+Tab when
    /// the window is fullscreen to ensure the user is not trapped in your application.
    /// </para>
    /// <para>If the caller enables a grab while another window is currently grabbed, the other window loses its grab in favor of the caller's window.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window keyboard grab.</exception>
    public bool KeyboardGrabbed
    {
        get => _window.KeyboardGrabbed;
        set => _window.KeyboardGrabbed = value;
    }

    /// <summary>
    /// Gets a value indicating whether the window is maximized.
    /// </summary>
    public bool Maximized
    {
        get => _window.Maximized;
        init => _window.Maximized = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>Setting to (0, 0) removes the maximum size limit.</para>
    /// <para>It will influence the window's size when resizing or using <see cref="Maximize"/>.</para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Width or height is negative.</exception>
    /// <exception cref="QuackNativeException">An error occurred while setting the window maximum size.</exception>
    public (int Width, int Height) MaximumSize
    {
        get => _window.MaximumSize;
        set => _window.MaximumSize = value;
    }

    /// <summary>
    /// Gets a value indicating whether the window is minimized.
    /// </summary>
    public bool Minimized
    {
        get => _window.Minimized;
        init => _window.Minimized = value;
    }

    /// <summary>
    /// Gets or sets the minimum size of the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>Setting to (0, 0) removes the maximum size limit.</para>
    /// <para>It will influence the window's size when resizing.</para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Width or height is negative.</exception>
    /// <exception cref="QuackNativeException">An error occurred while setting the window minimum size.</exception>
    public (int Width, int Height) MinimumSize
    {
        get => _window.MinimumSize;
        set => _window.MinimumSize = value;
    }

    /// <summary>
    /// Gets a value indicating whether the window has captured mouse input.
    /// </summary>
    /// <remarks>
    /// It is not related to <see cref="MouseGrabbed"/>.
    /// </remarks>
    public bool MouseCaptured => _window.MouseCaptured;

    /// <summary>
    /// Gets or sets the confined area of the mouse in the window.
    /// </summary>
    /// <remarks>
    /// <para>Setting to <see langword="null"/> or an empty <see cref="RectInt"/> removes the confined area.</para>
    /// <para>This will not grab the cursor, it only defines the area a cursor is restricted to when the window has mouse focus.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window mouse clip.</exception>
    public RectInt? MouseClip
    {
        get => _window.MouseClip;
        set => _window.MouseClip = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has mouse input grabbed.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window mouse grab.</exception>
    public bool MouseGrabbed
    {
        get => _window.MouseGrabbed;
        set => _window.MouseGrabbed = value;
    }

    /// <summary>
    /// Gets a value indicating whether the window has relative mouse mode enabled.
    /// </summary>
    public bool MouseRelativeMode => _window.MouseRelativeMode;

    /// <summary>
    /// Gets a value indicating whether the window is occluded.
    /// </summary>
    public bool Occluded => _window.Occluded;

    /// <summary>
    /// Gets or sets the opacity of the window.
    /// </summary>
    /// <remarks>
    /// <para>The default value is 1.0f.</para>
    /// <para>The opacity value should be in the range 0.0f - 1.0f.</para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Opacity is negative or greater than 1.0f.</exception>
    /// <exception cref="QuackNativeException">An error occurred while setting the window opacity.</exception>
    public float Opacity
    {
        get => _window.Opacity;
        set => _window.Opacity = value;
    }

    /// <summary>
    /// Gets the output size in pixels of a rendering context.
    /// </summary>
    /// <remarks>
    /// It return the true output size in pixels, ignoring any render targets or logical size and presentation.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while getting the renderer output size.</exception>
    public (int Width, int Height) OutputSize => _renderer.OutputSize;

    /// <summary>
    /// Gets or sets the position of the window.
    /// </summary>
    /// <remarks>
    /// <para>Setting the position if the window is in Fullscreen or Maximized state will be ignored.</para>
    /// <para>
    /// This can be used to reposition fullscreen desktop windows onto a different display,
    /// however, as exclusive fullscreen windows are locked to a specific display, they can only be repositioned via <see cref="FullscreenMode"/>.
    /// </para>
    /// <para>
    /// On some windowing systems this request is asynchronous and the new window state may not have been applied immediately.
    /// If an immediate change is required, call <see cref="Sync"/> to block until the changes have taken effect.
    /// </para>
    /// <para>
    /// When the window position changes, an <see cref="EventType.WindowMoved"/> event will be emitted with the new coordinates.
    /// Note that the new coordinates may not be the same as those requested, as the windowing system may impose its own constraints.
    /// (e.g constraining the size of the content area to remain within the usable desktop bounds). Additionally,
    /// as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// <para>This is the current position of the window as last reported by the windowing system.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window position.</exception>
    public Vector2Int Position
    {
        get => _window.Position;
        set => _window.Position = value;
    }

    /// <summary>
    /// Gets the pixel density of the window.
    /// </summary>
    /// <remarks>
    /// <para>If the window is not open, it will return 0.0f.</para>
    /// <para>
    /// This is a ratio of pixel size to window size. For example, if the window is 1920x1080 and it has a
    /// high density back buffer of 3840x2160 pixels, it would have a pixel density of 2.0.
    /// </para>
    /// </remarks>
    public float PixelDensity => _window.PixelDensity;

    /// <summary>
    /// Gets the pixel format associated with the window.
    /// </summary>
    /// <remarks>
    /// If the window is not open, it will return <see cref="PixelFormat.Unknown"/>.
    /// </remarks>
    public PixelFormat PixelFormat => _window.PixelFormat;

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
    /// Letterboxing will only happen if logical presentation is enabled during <see cref="Render"/>; be sure to reenable it first if you were using it.
    /// You can convert coordinates in an event into rendering coordinates using <see cref="MapEventToCoordinates(ref Event)"/> or <see cref="MapPixelsToCoordinates(Vector2)"/>.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">The width or height is negative.</exception>
    /// <exception cref="QuackNativeException">An error occurred while setting the logical presentation.</exception>
    public (int Width, int Height, LogicalPresentation Mode) Presentation
    {
        get => _renderer.Presentation;
        set => _renderer.Presentation = value;
    }

    /// <summary>
    /// Gets the final presentation rectangle for rendering.
    /// </summary>
    /// <remarks>
    /// It returns the calculated rectangle used for logical presentation, based on the presentation
    /// mode and output size. If logical presentation is <see cref="LogicalPresentation.Disabled"/>, it will fill
    /// the rectangle with the output size, in pixels.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while getting the renderer presentation rectangle.</exception>
    public Rect PresentationRectangle => _renderer.PresentationRectangle;

    /// <summary>
    /// Gets or sets a value indicating whether the window is resizable.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window resizable.</exception>
    public bool Resizable
    {
        get => _window.Resizable;
        set => _window.Resizable = value;
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
    /// <exception cref="QuackNativeException">An error occurred while getting the window safe area.</exception>
    public RectInt SafeArea => _renderer.SafeArea;

    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window title.</exception>
    public string Title
    {
        get => _window.Title;
        set => _window.Title = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window uses high pixel density.
    /// </summary>
    public bool UseHighPixelDensity
    {
        get => _window.UseHighPixelDensity;
        set => _window.UseHighPixelDensity = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window uses a transparent buffer.
    /// </summary>
    public bool UseTransparentBuffer
    {
        get => _window.UseTransparentBuffer;
        set => _window.UseTransparentBuffer = value;
    }

    /// <summary>
    /// Gets or sets the vertical synchronization (VSync) of the renderer.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When a renderer is created, VSync defaults to <see cref="VSync.Disabled"/> which means that VSync is disabled.
    /// </para>
    /// <para>
    /// The value can be 1 to synchronize present with every vertical refresh, 2 to synchronize present with every other vertical refresh, and so on.
    /// <see cref="VSync.Adaptive"/> can be used for adaptive VSync or <see cref="VSync.Disabled"/> to disable. Not every value is supported by every driver.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the renderer VSync.</exception>
    public int VSync
    {
        get => _renderer.VSync;
        set => _renderer.VSync = value;
    }

    /// <summary>
    /// Gets or sets the width of the window.
    /// </summary>
    /// <remarks>
    /// <para>Setting the width if the window is in Fullscreen or Maximized state will be ignored.</para>
    /// <para>To change the exclusive fullscreen mode dimensions, use <see cref="FullscreenMode"/>.</para>
    /// <para>It will be restricted by <see cref="MinimumSize"/> and <see cref="MaximumSize"/>.</para>
    /// <para>
    /// On some windowing systems this request is asynchronous and the new window state may not have been applied immediately.
    /// If an immediate change is required, call <see cref="Sync"/> to block until the changes have taken effect.
    /// </para>
    /// <para>
    /// When the window size changes, an <see cref="EventType.WindowResized"/> event will be emitted with the new dimensions.
    /// Note that the new dimensions may not be the same as those requested, as the windowing system may impose its own constraints.
    /// (e.g constraining the size of the content area to remain within the usable desktop bounds). Additionally,
    /// as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Width is less than or equal to 0.</exception>
    /// <exception cref="QuackNativeException">An error occurred while setting the window width.</exception>
    public int Width
    {
        get => _window.Width;
        set => _window.Width = value;
    }

    /// <summary>
    /// Gets the width of the window in pixels.
    /// </summary>
    public int WidthInPixel => _window.WidthInPixel;

    /// <inheritdoc/>
    /// <remarks>
    /// The render target is cleared with a black color.
    /// If you want to clear the render target with a different color, use <see cref="Clear(Color)"/> instead.
    /// </remarks>
    public void Clear() => _renderer.Clear(Color.Black);

    /// <inheritdoc/>
    public void Clear(Color color) => _renderer.Clear(color);

    /// <summary>
    /// Closes the window.
    /// </summary>
    /// <remarks>
    /// <para>Closing the window will release all resources associated with it and you need to create it again with <see cref="Create(string, int, int, string?)"/>.</para>
    /// <para>If the window is already closed or not created, it does nothing.</para>
    /// </remarks>
    public void Close()
    {
        if (!IsOpen)
            return;

        Dispose();
    }

    /// <summary>
    /// Creates the window.
    /// </summary>
    /// <remarks>
    /// If the window is already open, this method does nothing.
    /// </remarks>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="rendererName">The name of the rendering driver to initialize, or <see langword="null"/> to let the engine choose one.</param>
    public void Create(string title, int width, int height, string? rendererName = null)
    {
        if (IsOpen)
            return;

        _window.Create(title, width, height);
        _renderer = new Renderer(_window.Handle, rendererName);
    }

    /// <summary>
    /// Disposes the resources used by the <see cref="Window"/>.
    /// </summary>
    public void Dispose()
    {
        _renderer.Dispose();
        _window.Dispose();
    }

    /// <inheritdoc/>
    public void Draw(IDrawable drawable) => drawable.Draw(this);

    /// <inheritdoc/>
    public void Draw(ReadOnlySpan<Vertex> vertices) => _renderer.Draw(vertices, []);

    /// <inheritdoc/>
    public void Draw(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices) => _renderer.Draw(vertices, indices);

    /// <summary>
    /// Request the window to demand attention from the user.
    /// </summary>
    /// <param name="operation">The operation to perform.</param>
    /// <exception cref="QuackNativeException">An error occurred while flashing the window.</exception>
    public void Flash(FlashOperation operation) => _window.Flash(operation);

    /// <summary>
    /// Hides the window. It can be shown again with <see cref="Show"/>.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while hiding the window.</exception>
    public void Hide() => _window.Hide();

    /// <inheritdoc/>
    public Vector2 MapCoordinatesToPixels(Vector2 point) => _renderer.MapCoordinatesToPixels(point);

    /// <inheritdoc/>
    public void MapEventToCoordinates(ref Event e) => _renderer.MapEventToCoordinates(ref e);

    /// <inheritdoc/>
    public Vector2 MapPixelsToCoordinates(Vector2 point) => _renderer.MapPixelsToCoordinates(point);

    /// <summary>
    /// Request that the window be made as large as possible.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Non-resizable windows can't be maximized. The window must have the Resizable state set.
    /// </para>
    /// <para>
    /// On some windowing systems this request is asynchronous and the new window state may not have been applied immediately.
    /// If an immediate change is required, call <see cref="Sync"/> to block until the changes have taken effect.
    /// </para>
    /// <para>
    /// When the window state changes, an <see cref="EventType.WindowMaximized"/> event will be emitted.
    /// Note that, as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// <para>
    /// When maximizing a window, whether the constraints set via <see cref="MaximumSize"/> are honored depends on the policy of the window manager.
    /// Win32 enforce the constraints when maximizing, while X11 and Wayland window managers may vary.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while maximizing the window.</exception>
    public void Maximize() => _window.Maximize();

    /// <summary>
    /// Request that the window be minimized to an iconic representation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the window is in Fullscreen state, it will has no direct effect.
    /// It may alter the state the window is restored to when leaving fullscreen.
    /// </para>
    /// <para>
    /// On some windowing systems this request is asynchronous and the new window state may not have been applied immediately.
    /// If an immediate change is required, call <see cref="Sync"/> to block until the changes have taken effect.
    /// </para>
    /// <para>
    /// When the window state changes, an <see cref="EventType.WindowMinimized"/> event will be emitted.
    /// Note that, as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while minimizing the window.</exception>
    public void Minimize() => _window.Minimize();

    /// <summary>
    /// Polls events that are associated with the current window.
    /// </summary>
    /// <remarks>
    /// <para>Some events are processed internally by the window.</para>
    /// <para>Will return <see langword="false"/> and empty <see cref="Event"/> if the window is not open.</para>
    /// </remarks>
    /// <param name="e">The next filled event from the queue.</param>
    /// <returns><see langword="true"/> if this got an event or <see langword="false"/> if there are none available.</returns>
    public bool Poll(out Event e) => _window.Poll(out e);

    /// <summary>
    /// Request that the window be raised above other windows and gain the input focus.
    /// </summary>
    /// <remarks>
    /// The result of this request is subject to desktop window manager policy, particularly if raising
    /// the requested window would result in stealing focus from another application.
    /// If the window is successfully raised and gains input focus,
    /// an <see cref="EventType.FocusGained"/> event will be emitted,
    /// and the window will have InputFocus state set.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while raising the window.</exception>
    public void Raise() => _window.Raise();

    /// <summary>
    /// Renders all the graphics to the window since the last call.
    /// </summary>
    public void Render() => _renderer.Render();

    /// <summary>
    /// Request that the size and position of a minimized or maximized window be restored.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the window is in Fullscreen state, it will has no direct effect.
    /// It may alter the state the window is restored to when leaving fullscreen.
    /// </para>
    /// <para>
    /// On some windowing systems this request is asynchronous and the new window state may not have been applied immediately.
    /// If an immediate change is required, call <see cref="Sync"/> to block until the changes have taken effect.
    /// </para>
    /// <para>
    /// When the window state changes, an <see cref="EventType.WindowRestored"/> event will be emitted.
    /// Note that, as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while restoring the window.</exception>
    public void Restore() => _window.Restore();

    /// <summary>
    /// Show the window.
    /// </summary>
    /// <remarks>
    /// It's only the way to show a window that has been hidden
    /// with <see cref="Hide"/> or using Hidden state.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while showing the window.</exception>
    public void Show() => _window.Show();

    /// <summary>
    /// Block until any pending window state is finalized.
    /// </summary>
    /// <remarks>
    /// <para>On windowing systems where changes are immediate, this does nothing.</para>
    /// <para>
    /// On asynchronous windowing systems, this acts as a synchronization barrier for pending window state.
    /// It will attempt to wait until any pending window state has been applied and is guaranteed to return within finite time.
    /// Note that for how long it can potentially block depends on the underlying window system,
    /// as window state changes may involve somewhat lengthy animations that must complete before the window is in its final requested state.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Failed to sync the window.</exception>
    public void Sync() => _window.Sync();

    /// <summary>
    /// Returns a string representation of the window in the format "Window[Id] "Title" (WidthxHeight)".
    /// </summary>
    /// <returns>A string representation of the window.</returns>
    public override string ToString() => _window.ToString();

    /// <summary>
    /// Move the mouse cursor to the given position withing the window.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event if relative mode is not enabled.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="x">The x-coordinate within the window.</param>
    /// <param name="y">The y-coordinate within the window.</param>
    public void WarpMouse(float x, float y) => _window.WarpMouse(x, y);

    /// <summary>
    /// Move the mouse cursor to the given position withing the window.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event if relative mode is not enabled.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="position">The position within the window.</param>
    public void WarpMouse(Vector2 position) => _window.WarpMouse(position);
}

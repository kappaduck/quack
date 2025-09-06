// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Interop.Handles;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Handles;
using KappaDuck.Quack.Video.Displays;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace KappaDuck.Quack.Windows;

/// <summary>
/// Represents a native OS window with no graphics context.
/// </summary>
/// <remarks>
/// <para>
/// This class is primarily used internally by the engine as the foundation for
/// higher-level rendering windows such as <see cref="RenderWindow"/>.
/// </para>
/// <para>
/// By itself, a <see cref="Window"/> does not render anything (it will
/// appear as a blank window). It is public only to allow advanced users to
/// implement their own graphics backends (e.g. Vulkan, OpenGL, DirectX, Metal,
/// or software rendering). Use this class as a composition root for your
/// graphics backend implementation.
/// </para>
/// <para>
/// For most use cases, prefer <see cref="RenderWindow"/> instead.
/// </para>
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Advanced)]
public sealed class Window : IDisposable, ISpanFormattable
{
    private SDL_WindowHandle _windowHandle = new();
    private WindowState _state;
    private Vector2Int? _position;
    private int _width;
    private int _height;
    private string _title = string.Empty;
    private float? _opacity;

    /// <summary>
    /// Initializes a new instance of the <see cref="Window"/>.
    /// </summary>
    public Window()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Window"/>.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    public Window(string title, int width, int height) => Create(title, width, height);

    /// <summary>
    /// Gets or sets a value indicating whether the window is always on top.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window always on top.</exception>
    public bool AlwaysOnTop
    {
        get => (_state & WindowState.AlwaysOnTop) == WindowState.AlwaysOnTop;
        set
        {
            _state = value ? _state | WindowState.AlwaysOnTop : _state & ~WindowState.AlwaysOnTop;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowAlwaysOnTop(_windowHandle, value));
        }
    }

    /// <summary>
    /// Gets or sets the aspect ratio of the window's client area.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Minimum or maximum is negative.</exception>
    /// <exception cref="QuackNativeException">An error occurred while setting the window aspect ratio.</exception>
    public (float Minimum, float Maximum) AspectRatio
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value.Minimum);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Maximum);

            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowAspectRatio(_windowHandle, value.Minimum, value.Maximum));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is borderless.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window borderless.</exception>
    public bool Borderless
    {
        get => (_state & WindowState.Borderless) == WindowState.Borderless;
        set
        {
            _state = value ? _state | WindowState.Borderless : _state & ~WindowState.Borderless;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowBordered(_windowHandle, !value));
        }
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
    public (int Top, int Left, int Bottom, int Right) BordersSize
    {
        get
        {
            if (!IsOpen || Borderless)
                return default;

            SDL.Windows.SDL_GetWindowBordersSize(_windowHandle, out int top, out int left, out int bottom, out int right);
            return (top, left, bottom, right);
        }
    }

    /// <summary>
    /// Gets the display associated with the window.
    /// </summary>
    /// <remarks>
    /// If the window is not open, it will return null as the display cannot be determined.
    /// </remarks>
    public Display? Display
    {
        get
        {
            if (!IsOpen)
                return null;

            return Display.GetDisplay(SDL.Windows.SDL_GetDisplayForWindow(_windowHandle));
        }
    }

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
    public float DisplayScale
    {
        get
        {
            if (!IsOpen)
                return 0.0f;

            return SDL.Windows.SDL_GetWindowDisplayScale(_windowHandle);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is focusable.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window focusable.</exception>
    public bool Focusable
    {
        get => (_state & WindowState.NotFocusable) != WindowState.NotFocusable;
        set
        {
            _state = value ? _state & ~WindowState.NotFocusable : _state | WindowState.NotFocusable;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowFocusable(_windowHandle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is fullscreen.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while settings the window fullscreen.</exception>
    public bool Fullscreen
    {
        get => (_state & WindowState.Fullscreen) == WindowState.Fullscreen;
        set
        {
            _state = value ? _state | WindowState.Fullscreen : _state & ~WindowState.Fullscreen;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowFullscreen(_windowHandle, value));
        }
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
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowFullscreenMode(_windowHandle, value));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window has keyboard focus.
    /// </summary>
    public bool HasKeyboardFocus => (_state & WindowState.InputFocus) == WindowState.InputFocus;

    /// <summary>
    /// Gets a value indicating whether the window has mouse focus.
    /// </summary>
    public bool HasMouseFocus => (_state & WindowState.MouseFocus) == WindowState.MouseFocus;

    /// <summary>
    /// Gets a safe, non-owning handle to the native window.
    /// </summary>
    /// <remarks>
    /// This handle is valid only while the <see cref="Window"/> is alive.
    /// Disposing this handle will not close the window; use the <see cref="Dispose"/> instead.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">The window has been disposed or not open.</exception>
    [field: AllowNull]
    public WindowHandle Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_windowHandle.IsInvalid, typeof(Window));
            return field;
        }
        private set;
    }

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
        get => _height;
        set
        {
            if (Fullscreen || Maximized)
                return;

            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0);

            _height = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowSize(_windowHandle, _width, _height));
        }
    }

    /// <summary>
    /// Gets the height of the window in pixels.
    /// </summary>
    public int HeightInPixel { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the window is hidden.
    /// </summary>
    public bool Hidden
    {
        get => (_state & WindowState.Hidden) == WindowState.Hidden;
        set => _state = value ? _state | WindowState.Hidden : _state & ~WindowState.Hidden;
    }

    /// <summary>
    /// Gets the window's identifier.
    /// </summary>
    /// <remarks>
    /// The identifier is what <see cref="WindowEvent"/> references.
    /// </remarks>
    public uint Id { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the window is currently open.
    /// </summary>
    public bool IsOpen => !_windowHandle.IsInvalid;

    /// <summary>
    /// Gets a value indicating whether the screen keyboard is visible.
    /// </summary>
    public bool IsScreenKeyboardVisible
    {
        get
        {
            if (!IsOpen)
                return false;

            return SDL.Windows.SDL_ScreenKeyboardShown(_windowHandle);
        }
    }

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
        get => (_state & WindowState.KeyboardGrabbed) == WindowState.KeyboardGrabbed;
        set
        {
            _state = value ? _state | WindowState.KeyboardGrabbed : _state & ~WindowState.KeyboardGrabbed;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowKeyboardGrab(_windowHandle, value));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window is maximized.
    /// </summary>
    public bool Maximized
    {
        get => (_state & WindowState.Maximized) == WindowState.Maximized;
        set
        {
            _state = value ? _state | WindowState.Maximized : _state & ~WindowState.Maximized;
            _state &= ~WindowState.Minimized;
        }
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
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowMaximumSize(_windowHandle, value.Width, value.Height));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window is minimized.
    /// </summary>
    public bool Minimized
    {
        get => (_state & WindowState.Minimized) == WindowState.Minimized;
        set
        {
            _state = value ? _state | WindowState.Minimized : _state & ~WindowState.Minimized;
            _state &= ~WindowState.Maximized;
        }
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
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowMinimumSize(_windowHandle, value.Width, value.Height));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window has captured mouse input.
    /// </summary>
    /// <remarks>
    /// It is not related to <see cref="MouseGrabbed"/>.
    /// </remarks>
    public bool MouseCaptured => (_state & WindowState.MouseCapture) == WindowState.MouseCapture;

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
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowMouseRect(_windowHandle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has mouse input grabbed.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window mouse grab.</exception>
    public bool MouseGrabbed
    {
        get => (_state & WindowState.MouseGrabbed) == WindowState.MouseGrabbed;
        set
        {
            _state = value ? _state | WindowState.MouseGrabbed : _state & ~WindowState.MouseGrabbed;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowMouseGrab(_windowHandle, value));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window has relative mouse mode enabled.
    /// </summary>
    public bool MouseRelativeMode => (_state & WindowState.MouseRelativeMode) == WindowState.MouseRelativeMode;

    /// <summary>
    /// Gets a value indicating whether the window is occluded.
    /// </summary>
    public bool Occluded => (_state & WindowState.Occluded) == WindowState.Occluded;

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
        get => _opacity ?? 1.0f;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 1.0f);

            _opacity = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowOpacity(_windowHandle, _opacity.Value));
        }
    }

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
        get
        {
            if (!_position.HasValue)
            {
                QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_GetWindowPosition(_windowHandle, out int x, out int y));
                _position = new Vector2Int(x, y);
            }

            return _position.Value;
        }
        set
        {
            if (Fullscreen || Maximized)
                return;

            _position = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowPosition(_windowHandle, value.X, value.Y));
        }
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
    public float PixelDensity
    {
        get
        {
            if (!IsOpen)
                return 0.0f;

            return SDL.Windows.SDL_GetWindowPixelDensity(_windowHandle);
        }
    }

    /// <summary>
    /// Gets the pixel format associated with the window.
    /// </summary>
    /// <remarks>
    /// If the window is not open, it will return <see cref="PixelFormat.Unknown"/>.
    /// </remarks>
    public PixelFormat PixelFormat
    {
        get
        {
            if (!IsOpen)
                return PixelFormat.Unknown;

            return SDL.Windows.SDL_GetWindowPixelFormat(_windowHandle);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is resizable.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window resizable.</exception>
    public bool Resizable
    {
        get => (_state & WindowState.Resizable) == WindowState.Resizable;
        set
        {
            _state = value ? _state | WindowState.Resizable : _state & ~WindowState.Resizable;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowResizable(_windowHandle, value));
        }
    }

    /// <summary>
    /// Gets the safe area for the window.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the window is not open, it will return an empty <see cref="RectInt"/>.
    /// </para>
    /// <para>
    /// Some devices have portions of the screen which are partially obscured or not interactive,
    /// possibly due to on-screen controls, curved edges, camera notches, TV over scan, etc.
    /// This provides the area of the window which is safe to have interactable content.
    /// You should continue rendering into the rest of the window,
    /// but it should not contain visually important or interactable content.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while getting the window safe area.</exception>
    public RectInt SafeArea
    {
        get
        {
            if (!IsOpen)
                return default;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_GetWindowSafeArea(_windowHandle, out RectInt area));
            return area;
        }
    }

    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while setting the window title.</exception>
    public string Title
    {
        get => _title;
        set
        {
            _title = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowTitle(_windowHandle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window uses high pixel density.
    /// </summary>
    public bool UseHighPixelDensity
    {
        get => (_state & WindowState.HighPixelDensity) == WindowState.HighPixelDensity;
        set => _state = value ? _state | WindowState.HighPixelDensity : _state & ~WindowState.HighPixelDensity;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window uses a transparent buffer.
    /// </summary>
    public bool UseTransparentBuffer
    {
        get => (_state & WindowState.TransparentBuffer) == WindowState.TransparentBuffer;
        set => _state = value ? _state | WindowState.TransparentBuffer : _state & ~WindowState.TransparentBuffer;
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
        get => _width;
        set
        {
            if (Fullscreen || Maximized)
                return;

            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0);

            _width = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowSize(_windowHandle, _width, _height));
        }
    }

    /// <summary>
    /// Gets the width of the window in pixels.
    /// </summary>
    public int WidthInPixel { get; private set; }

    /// <summary>
    /// Gets the SDL window handle.
    /// </summary>
    internal SDL_WindowHandle WindowHandle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_windowHandle.IsInvalid, typeof(Window));
            return _windowHandle.ToNonOwningHandle();
        }
    }

    /// <summary>
    /// Closes the window.
    /// </summary>
    /// <remarks>
    /// <para>Closing the window will release all resources associated with it and you need to create it again with <see cref="Create(string, int, int)"/>.</para>
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
    public void Create(string title, int width, int height)
    {
        if (IsOpen)
            return;

        _windowHandle = CreateWindow(title, width, height);
    }

    /// <summary>
    /// Disposes the resources used by the <see cref="Window"/>.
    /// </summary>
    public void Dispose() => _windowHandle.Dispose();

    /// <summary>
    /// Request the window to demand attention from the user.
    /// </summary>
    /// <param name="operation">The operation to perform.</param>
    /// <exception cref="QuackNativeException">An error occurred while flashing the window.</exception>
    public void Flash(FlashOperation operation)
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_FlashWindow(_windowHandle, operation));
    }

    /// <summary>
    /// Hides the window. It can be shown again with <see cref="Show"/>.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while hiding the window.</exception>
    public void Hide()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_HideWindow(_windowHandle));

        _state |= WindowState.Hidden;
    }

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
    public void Maximize()
    {
        if (!IsOpen || Maximized || !Resizable)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_MaximizeWindow(_windowHandle));

        _state &= ~WindowState.Minimized;
        _state |= WindowState.Maximized;
    }

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
    public void Minimize()
    {
        if (!IsOpen || Minimized)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_MinimizeWindow(_windowHandle));

        _state &= ~WindowState.Maximized;
        _state |= WindowState.Minimized;
    }

    /// <summary>
    /// Polls events that are associated with the current window.
    /// </summary>
    /// <remarks>
    /// <para>Some events are processed internally by the window.</para>
    /// <para>Will return <see langword="false"/> and empty <see cref="Event"/> if the window is not open.</para>
    /// </remarks>
    /// <param name="e">The next filled event from the queue.</param>
    /// <returns><see langword="true"/> if this got an event or <see langword="false"/> if there are none available.</returns>
    public bool Poll(out Event e)
    {
        if (!IsOpen)
        {
            e = default;
            return false;
        }

        bool hasEvent = EventManager.Poll(out e);

        if (e.Window.Id != Id)
            return hasEvent;

        if (e.Type == EventType.WindowExposed)
            _state &= ~WindowState.Occluded;

        if (e.Type == EventType.WindowOccluded)
            _state |= WindowState.Occluded;

        if (e.Type == EventType.WindowResized)
        {
            _width = e.Window.Data1;
            _height = e.Window.Data2;
        }

        if (e.Type == EventType.WindowPixelSizeChanged)
        {
            WidthInPixel = e.Window.Data1;
            HeightInPixel = e.Window.Data2;
        }

        if (e.Type == EventType.WindowMoved)
            _position = new Vector2Int(e.Window.Data1, e.Window.Data2);

        if (e.Type == EventType.MouseEnter)
            _state |= WindowState.MouseFocus;

        if (e.Type == EventType.MouseLeave)
            _state &= ~WindowState.MouseFocus;

        if (e.Type == EventType.FocusGained)
            _state |= WindowState.InputFocus;

        if (e.Type == EventType.FocusLost)
            _state &= ~WindowState.InputFocus;

        if (e.Type == EventType.WindowRestored)
            _state &= ~(WindowState.Minimized | WindowState.Maximized);

        return hasEvent;
    }

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
    public void Raise()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_RaiseWindow(_windowHandle));

        _state |= WindowState.InputFocus;
    }

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
    public void Restore()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_RestoreWindow(_windowHandle));
    }

    /// <summary>
    /// Set an icon for the window.
    /// </summary>
    /// <param name="icon">A surface containing the icon image.</param>
    public void SetIcon(Surface icon)
    {
        if (!IsOpen)
            return;

        unsafe
        {
            QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowIcon(_windowHandle, icon.Handle));
        }
    }

    /// <summary>
    /// Show the window.
    /// </summary>
    /// <remarks>
    /// It's only the way to show a window that has been hidden
    /// with <see cref="Hide"/> or using Hidden state.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while showing the window.</exception>
    public void Show()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_ShowWindow(_windowHandle));

        _state &= ~WindowState.Hidden;
    }

    /// <summary>
    /// Display the system-level window menu.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This default window menu is provided by the system and on some platforms provides functionality for setting or changing privileged state on the window,
    /// such as moving it between workspaces or displays, or toggling the always-on-top property.
    /// </para>
    /// <para>
    /// On platforms or desktops where this is unsupported, this function does nothing.
    /// </para>
    /// </remarks>
    /// <param name="position">The coordinates to show the menu at, relative to the origin (top-left) of the window.</param>
    public void ShowSystemMenu(Vector2Int position) => ShowSystemMenu(position.ToVector2());

    /// <summary>
    /// Display the system-level window menu.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This default window menu is provided by the system and on some platforms provides functionality for setting or changing privileged state on the window,
    /// such as moving it between workspaces or displays, or toggling the always-on-top property.
    /// </para>
    /// <para>
    /// On platforms or desktops where this is unsupported, this function does nothing.
    /// </para>
    /// </remarks>
    /// <param name="position">The coordinates to show the menu at, relative to the origin (top-left) of the window.</param>
    public void ShowSystemMenu(Vector2 position)
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_ShowWindowSystemMenu(_windowHandle, (int)position.X, (int)position.Y));
    }

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
    public void Sync()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SyncWindow(_windowHandle));
    }

    /// <summary>
    /// Returns a string representation of the window in the format "Window[Id] "Title" (WidthxHeight)".
    /// </summary>
    /// <returns>A string representation of the window.</returns>
    public override string ToString() => $"{this}";

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"Window[{Id}] \"{Title}\" ({Width}x{Height})", out charsWritten);

    /// <summary>
    /// Move the mouse cursor to the given position withing the window.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event if relative mode is not enabled.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="x">The x-coordinate within the window.</param>
    /// <param name="y">The y-coordinate within the window.</param>
    public void WarpMouse(float x, float y) => SDL.Windows.SDL_WarpMouseInWindow(_windowHandle, x, y);

    /// <summary>
    /// Move the mouse cursor to the given position withing the window.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event if relative mode is not enabled.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="position">The position within the window.</param>
    public void WarpMouse(Vector2 position) => WarpMouse(position.X, position.Y);

    private SDL_WindowHandle CreateWindow(string title, int width, int height)
    {
        SDL_WindowHandle handle;
        _title = title;
        _width = width;
        _height = height;

        using (Properties properties = new())
        {
            properties.Set("SDL.window.create.always_on_top", AlwaysOnTop);
            properties.Set("SDL.window.create.borderless", Borderless);
            properties.Set("SDL.window.create.focusable", Focusable);
            properties.Set("SDL.window.create.fullscreen", Fullscreen);
            properties.Set("SDL.window.create.hidden", Hidden);
            properties.Set("SDL.window.create.maximized", Maximized);
            properties.Set("SDL.window.create.minimized", Minimized);
            properties.Set("SDL.window.create.mouse_grabbed", MouseGrabbed);
            properties.Set("SDL.window.create.resizable", Resizable);
            properties.Set("SDL.window.create.high_pixel_density", UseHighPixelDensity);
            properties.Set("SDL.window.create.transparent", UseTransparentBuffer);

            properties.Set("SDL.window.create.title", _title);
            properties.Set("SDL.window.create.width", _width);
            properties.Set("SDL.window.create.height", _height);

            if (_position.HasValue)
            {
                properties.Set("SDL.window.create.x", _position.Value.X);
                properties.Set("SDL.window.create.y", _position.Value.Y);
            }

            handle = SDL.Windows.SDL_CreateWindowWithProperties(properties.Id);
            QuackNativeException.ThrowIf(handle.IsInvalid);
        }

        Id = SDL.Windows.SDL_GetWindowID(handle);
        QuackNativeException.ThrowIfZero(Id);

        uint propertiesId = SDL.Windows.SDL_GetWindowProperties(handle);
        Handle = new WindowHandle(Properties.Get(propertiesId, "SDL.window.win32.hwnd", nint.Zero));

        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowAspectRatio(handle, AspectRatio.Minimum, AspectRatio.Maximum));
        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowFullscreenMode(handle, FullscreenMode));
        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowMaximumSize(handle, MaximumSize.Width, MaximumSize.Height));
        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowMinimumSize(handle, MinimumSize.Width, MinimumSize.Height));
        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowMouseRect(handle, MouseClip));
        QuackNativeException.ThrowIfFailed(SDL.Windows.SDL_SetWindowOpacity(handle, Opacity));

        return handle;
    }

    [Flags]
    private enum WindowState : ulong
    {
        /// <summary>
        /// No state to apply to the window.
        /// </summary>
        None = 0x0000000000000000,

        /// <summary>
        /// The window is in fullscreen mode.
        /// </summary>
        Fullscreen = 0x0000000000000001,

        /// <summary>
        /// The window is occluded.
        /// </summary>
        Occluded = 0x0000000000000004,

        /// <summary>
        /// The window is hidden.
        /// </summary>
        Hidden = 0x0000000000000008,

        /// <summary>
        /// The window has no decorations, such as title bar or borders.
        /// </summary>
        Borderless = 0x0000000000000010,

        /// <summary>
        /// The window can be resized by the user.
        /// </summary>
        Resizable = 0x0000000000000020,

        /// <summary>
        /// The window is minimized and not visible to the user.
        /// </summary>
        Minimized = 0x0000000000000040,

        /// <summary>
        /// The window is maximized and occupies the entire screen area.
        /// </summary>
        Maximized = 0x0000000000000080,

        /// <summary>
        /// The has grabbed the mouse input.
        /// </summary>
        MouseGrabbed = 0x0000000000000100,

        /// <summary>
        /// The window has input focus.
        /// </summary>
        InputFocus = 0x0000000000000200,

        /// <summary>
        /// The window has mouse focus.
        /// </summary>
        MouseFocus = 0x0000000000000400,

        /// <summary>
        /// The window uses high pixel density back buffering if available.
        /// </summary>
        HighPixelDensity = 0x0000000000002000,

        /// <summary>
        /// The window has captured the mouse input.
        /// </summary>
        /// <remark>
        /// Unrelated to <see cref="MouseGrabbed"/>.
        /// </remark>
        MouseCapture = 0x0000000000004000,

        /// <summary>
        /// The window is in relative mouse mode
        /// </summary>
        MouseRelativeMode = 0x0000000000008000,

        /// <summary>
        /// The window is always on top of other windows.
        /// </summary>
        AlwaysOnTop = 0x0000000000010000,

        /// <summary>
        /// The window has grabbed the keyboard input.
        /// </summary>
        KeyboardGrabbed = 0x0000000000100000,

        /// <summary>
        /// The window has transparent buffer.
        /// </summary>
        TransparentBuffer = 0x0000000040000000,

        /// <summary>
        /// The window is not focusable.
        /// </summary>
        NotFocusable = 0x0000000080000000
    }
}

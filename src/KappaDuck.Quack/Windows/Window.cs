// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Interop.Handles;
using KappaDuck.Quack.Video.Displays;
using KappaDuck.Quack.Windows.Progress;
using System.Collections.Concurrent;

namespace KappaDuck.Quack.Windows;

/// <summary>
/// Represents a native OS window with no graphics context.
/// </summary>
/// <remarks>
/// You should use one of the specialized windows such as <see cref="RenderWindow"/>.
/// </remarks>
public abstract partial class Window : IDisposable, ISpanFormattable
{
    private readonly int _threadId = Environment.CurrentManagedThreadId;
    private readonly ConcurrentQueue<Action<SDL_Window>> _invocations = [];

    private SDL_Window _handle = SDL_Window.Null;
    private Surface? _icon;
    private State _state;
    private Vector2Int? _position;
    private int _width;
    private int _height;
    private float? _opacity;
    private bool _disposed;

    /// <summary>
    /// Creates an empty window.
    /// </summary>
    /// <remarks>
    /// It does not create the window. Use <see cref="Create(string, int, int)"/> to create a window.
    /// It is useful to delay window creation until necessary.
    /// </remarks>
    protected Window()
    {
        Title = string.Empty;
        Handle = new WindowHandle(nint.Zero);
    }

    /// <summary>
    /// Creates a window with the title, width, and height.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <remarks>It creates the window immediately upon instantiation.</remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window.</exception>
    protected Window(string title, int width, int height)
    {
        Title = title;
        Handle = new WindowHandle(nint.Zero);

        Initialize(width, height);
    }

    /// <summary>
    /// Creates a window with the title and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The dimensions of the window.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window.</exception>
    protected Window(string title, SizeInt size)
    {
        Title = title;
        Handle = new WindowHandle(nint.Zero);

        Initialize(size.Width, size.Height);
    }

    /// <summary>
    /// Creates a window with the title, position, and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="position">The position of the window on the screen.</param>
    /// <param name="size">The size of the window.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window.</exception>
    protected Window(string title, Vector2Int position, SizeInt size)
    {
        Title = title;
        Position = position;
        Handle = new WindowHandle(nint.Zero);

        Initialize(size.Width, size.Height);
    }

    /// <summary>
    /// Creates a window with the title and fullscreen mode.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="mode">The fullscreen display mode.</param>
    /// <remarks>
    /// This creates the window immediately upon instantiation in fullscreen mode using the specified display mode,
    /// and sets the window size to the mode's width and height.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window.</exception>
    protected Window(string title, DisplayMode mode)
    {
        Title = title;
        FullscreenMode = mode;
        Handle = new WindowHandle(nint.Zero);

        Initialize(mode.Width, mode.Height);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is always on top of other windows.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to set the always on top state.</exception>
    public bool AlwaysOnTop
    {
        get => HasState(State.AlwaysOnTop);
        set
        {
            SetState(State.AlwaysOnTop, value);

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowAlwaysOnTop(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the aspect ratio of the window's client area.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the minimum or maximum aspect ratio is negative.</exception>
    /// <exception cref="QuackNativeException">Thrown when failed to set the aspect ratio.</exception>
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

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowAspectRatio(_handle, value.Minimum, value.Maximum));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has no decorations, such as title bar or borders.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to set the borderless state.</exception>
    public bool Borderless
    {
        get => HasState(State.Borderless);
        set
        {
            SetState(State.Borderless, value);

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowBordered(_handle, !value));
        }
    }

    /// <summary>
    /// Gets the size of the window's borders (decorations) around the client area.
    /// </summary>
    /// <remarks>
    /// <para>If the window is not open or is borderless, all values will be zero.</para>
    /// <para>
    /// It is possible it will fail to retrieve the border size because the window has not yet been decorated by the display server or
    /// the information is not supported.
    /// </para>
    /// </remarks>
    public (int Top, int Left, int Bottom, int Right) BordersSize
    {
        get
        {
            if (!IsOpen || Borderless)
                return default;

            Native.SDL_GetWindowBordersSize(_handle, out int top, out int left, out int bottom, out int right);
            return (top, left, bottom, right);
        }
    }

    /// <summary>
    /// Gets the display that the window is currently on.
    /// </summary>
    /// <remarks>If the window is not open, this property returns <see langword="null"/>.</remarks>
    public Display? Display => IsOpen ? Display.GetDisplay(Native.SDL_GetDisplayForWindow(_handle)) : null;

    /// <summary>
    /// Gets the content display scale relative to the window's pixel size.
    /// </summary>
    /// <remarks>
    /// <para>If the window is not open, this property returns <c>0.0f</c>.</para>
    /// <para>
    /// This is a combination of the window pixel density and the display content scale,
    /// and is the expected scale for displaying content in this window.
    /// For example, if a 3840x2160 window had a display scale of 2.0,
    /// the user expects the content to take twice as many pixels and be the same physical size
    /// as if it were being displayed in a 1920x1080 window with a display scale of 1.0.
    /// </para>
    /// <para>
    /// Conceptually this value corresponds to the scale display setting,
    /// and is updated when that setting is changed,
    /// or the window moves to a display with a different scale setting.
    /// </para>
    /// </remarks>
    public float DisplayScale => IsOpen ? Native.SDL_GetWindowDisplayScale(_handle) : 0.0f;

    /// <summary>
    /// Gets or sets a value indicating whether the window can receive input focus.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to set the focusable state.</exception>
    public bool Focusable
    {
        get => !HasState(State.NotFocusable);
        set
        {
            SetState(State.NotFocusable, !value);

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowFocusable(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is in fullscreen mode.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to set the fullscreen state.</exception>
    public bool Fullscreen
    {
        get => HasState(State.Fullscreen);
        set
        {
            SetState(State.Fullscreen, value);

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowFullscreen(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the fullscreen display mode to use when the window is in fullscreen mode.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Setting to <see langword="null"/> will use borderless fullscreen desktop mode,
    /// or one of the fullscreen modes from <see cref="Display.GetFullscreenModes"/> to set an exclusive fullscreen mode.
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
    /// <exception cref="QuackNativeException">Thrown when failed to set the fullscreen mode.</exception>
    public DisplayMode? FullscreenMode
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowFullscreenMode(_handle, value));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window currently has keyboard focus.
    /// </summary>
    public bool HasKeyboardFocus => HasState(State.InputFocus);

    /// <summary>
    /// Gets a value indicating whether the window currently has mouse focus.
    /// </summary>
    public bool HasMouseFocus => HasState(State.MouseFocus);

    /// <summary>
    /// Gets a safe, non-owning handle to the native window.
    /// </summary>
    /// <remarks>
    /// This handle represents different types of window handles depending on the platform:
    /// <list type="bullet">
    /// <item><description>On Windows, it represents an <c>HWND</c>.</description></item>
    /// <item><description>On Linux with X11, it represents a <c>Window</c> (X11 window ID).</description></item>
    /// </list>
    /// <para>
    /// This handle is valid only when the window is open. Disposing this handle will not close the window;
    /// Use <see cref="Close"/> to close the window properly.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when trying to access the handle of a closed window.</exception>
    public WindowHandle Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle.IsInvalid, typeof(Window));
            return field;
        }
        private set;
    }

    /// <summary>
    /// Gets or sets the height of the window's client area.
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
    /// (e.g. constraining the size of the content area to remain within the usable desktop bounds). Additionally,
    /// as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Height is less than or equal to 0.</exception>
    /// <exception cref="QuackNativeException">Thrown when failed to set the window size.</exception>
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

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowSize(_handle, _width, _height));
        }
    }

    /// <summary>
    /// Gets the height of the window's client area in pixels.
    /// </summary>
    public int HeightInPixels { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the window is hidden.
    /// </summary>
    public bool Hidden
    {
        get => HasState(State.Hidden);
        set => SetState(State.Hidden, value);
    }

    /// <summary>
    /// Gets the unique identifier of the window.
    /// </summary>
    /// <remarks>The identifier is what <see cref="WindowEvent"/> uses to identify the window that generated the event.</remarks>
    public uint Id { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the window is currently open.
    /// </summary>
    public bool IsOpen => !_handle.IsInvalid;

    /// <summary>
    /// Gets a value indicating whether the on-screen keyboard is visible for the window.
    /// </summary>
    public bool IsScreenKeyboardVisible => IsOpen && Native.SDL_ScreenKeyboardShown(_handle);

    /// <summary>
    /// Gets or sets a value indicating whether the window has grabbed the keyboard input.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Keyboard grab enables capture of system keyboard shortcuts like Alt+Tab or the Meta/Super key.
    /// Important to note that not all system keyboard shortcuts can be captured by applications (one example is CTRL+Alt+Del on Windows).
    /// </para>
    /// <para>
    /// This is primarily intended for specialized applications such as VNC clients or VM front-ends. Normal games should not use keyboard grab.
    /// </para>
    /// <para>
    /// When keyboard is enabled, Quack! will continue to handle Alt+Tab when
    /// the window is fullscreen to ensure the user is not trapped in your application.
    /// </para>
    /// <para>If the caller enables a grab while another window is currently grabbed, the other window loses its grab in favor of the caller's window.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to set the keyboard grab state.</exception>
    public bool KeyboardGrabbed
    {
        get => HasState(State.KeyboardGrabbed);
        set
        {
            SetState(State.KeyboardGrabbed, value);

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowKeyboardGrab(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is maximized.
    /// </summary>
    public bool Maximized
    {
        get => HasState(State.Maximized);
        set
        {
            SetState(State.Maximized, value);
            _state &= ~State.Minimized;
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
    /// <exception cref="QuackNativeException">Thrown when failed to set the maximum size.</exception>
    public SizeInt MaximumSize
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowMaximumSize(_handle, value.Width, value.Height));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is minimized.
    /// </summary>
    public bool Minimized
    {
        get => HasState(State.Minimized);
        set
        {
            SetState(State.Minimized, value);
            _state &= ~State.Maximized;
        }
    }

    /// <summary>
    /// Gets or sets the minimum size of the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>Setting to (0, 0) removes the minimum size limit.</para>
    /// <para>It will influence the window's size when resizing or using <see cref="Minimize"/>.</para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Width or height is negative.</exception>
    /// <exception cref="QuackNativeException">Thrown when failed to set the minimum size.</exception>
    public SizeInt MinimumSize
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowMinimumSize(_handle, value.Width, value.Height));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has captured the mouse.
    /// </summary>
    /// <remarks>It is not related to <see cref="MouseGrabbed"/>.</remarks>
    public bool MouseCaptured => HasState(State.MouseCapture);

    /// <summary>
    /// Gets or sets the mouse clipping rectangle relative to the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>Setting to <see langword="null"/> or an empty <see cref="RectInt"/> removes the confined area.</para>
    /// <para>This will not grab the cursor, it only defines the area a cursor is restricted to when the window has mouse focus.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to clip the mouse.</exception>
    public RectInt? MouseClip
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowMouseRect(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has grabbed the mouse input.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to set the mouse grab state.</exception>
    public bool MouseGrabbed
    {
        get => HasState(State.MouseGrabbed);
        set
        {
            SetState(State.MouseGrabbed, value);

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowMouseGrab(_handle, value));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window is in relative mouse mode.
    /// </summary>
    public bool MouseRelativeMode => HasState(State.MouseRelativeMode);

    /// <summary>
    /// Gets a value indicating whether the window is occluded (completely covered by other windows).
    /// </summary>
    public bool Occluded => HasState(State.Occluded);

    /// <summary>
    /// Gets or sets the opacity of the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>The default value is 1.0f.</para>
    /// <para>The opacity value should be in the range 0.0f - 1.0f.</para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the opacity is negative or greater than 1.0f.</exception>
    /// <exception cref="QuackNativeException">Thrown when failed to set the window opacity.</exception>
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

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowOpacity(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the position of the window on the screen.
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
    /// (e.g. constraining the size of the content area to remain within the usable desktop bounds). Additionally,
    /// as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// <para>This is the current position of the window as last reported by the windowing system.</para>
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to set the window position.</exception>
    public Vector2Int Position
    {
        get
        {
            if (!_position.HasValue && IsOpen)
            {
                QuackNativeException.ThrowIfFailed(Native.SDL_GetWindowPosition(_handle, out int x, out int y));
                _position = new Vector2Int(x, y);
            }

            return _position ?? Vector2Int.Zero;
        }
        set
        {
            if (Fullscreen || Maximized)
                return;

            _position = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowPosition(_handle, value.X, value.Y));
        }
    }

    /// <summary>
    /// Gets the pixel density of the window.
    /// </summary>
    /// <remarks>
    /// <para>If the window is not open, this property returns <c>0.0f</c>.</para>
    /// <para>
    /// This is a ratio fo pixel size to window size. For example, if a window is 1920x1080, and it has
    /// a high density back buffer of 3840x2160 pixels, it would have a pixel density of 2.0f.
    /// </para>
    /// </remarks>
    public float PixelDensity => IsOpen ? Native.SDL_GetWindowPixelDensity(_handle) : 0.0f;

    /// <summary>
    /// Gets the pixel format of the window's back buffer.
    /// </summary>
    /// <remarks>If the window is not open, it will return <see cref="PixelFormat.Unknown"/>.</remarks>
    public PixelFormat PixelFormat => !IsOpen ? PixelFormat.Unknown : Native.SDL_GetWindowPixelFormat(_handle);

    /// <summary>
    /// Gets or sets a value indicating whether the window can be resized by the user.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to set the resizable state.</exception>
    public bool Resizable
    {
        get => HasState(State.Resizable);
        set
        {
            SetState(State.Resizable, value);

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowResizable(_handle, value));
        }
    }

    /// <summary>
    /// Gets the safe area of the window's client area.
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
    /// <exception cref="QuackNativeException">Thrown when failed to get the safe area.</exception>
    public RectInt SafeArea
    {
        get
        {
            if (!IsOpen)
                return default;

            QuackNativeException.ThrowIfFailed(Native.SDL_GetWindowSafeArea(_handle, out RectInt area));
            return area;
        }
    }

    /// <summary>
    /// Gets or sets the size of the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>Setting the size if the window is in Fullscreen or Maximized state will be ignored.</para>
    /// <para>To change the exclusive fullscreen mode dimensions, use <see cref="FullscreenMode"/>.</para>
    /// <para>It will be restricted by <see cref="MinimumSize"/> and <see cref="MaximumSize"/>.</para>
    /// <para>
    /// On some windowing systems this request is asynchronous and the new window state may not have been applied immediately.
    /// If an immediate change is required, call <see cref="Sync"/> to block until the changes have taken effect.
    /// </para>
    /// <para>
    /// When the window size changes, an <see cref="EventType.WindowResized"/> event will be emitted with the new dimensions.
    /// Note that the new dimensions may not be the same as those requested, as the windowing system may impose its own constraints.
    /// (e.g. constraining the size of the content area to remain within the usable desktop bounds). Additionally,
    /// as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// <para><see cref="Vector2Int.X"/> represents the width, and <see cref="Vector2Int.Y"/> represents the height.</para>
    /// <exception cref="ArgumentOutOfRangeException">Width or height is less than or equal to 0.</exception>
    /// <exception cref="QuackNativeException">Thrown when failed to set the window size.</exception>
    /// </remarks>
    public Vector2Int Size
    {
        get => new(Width, Height);
        set
        {
            if (Fullscreen || Maximized)
                return;

            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value.X, 0);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value.Y, 0);

            _width = value.X;
            _height = value.Y;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowSize(_handle, _width, _height));
        }
    }

    /// <summary>
    /// Gets the window's taskbar progress bar.
    /// </summary>
    public WindowProgressBar ProgressBar => field ??= new WindowProgressBar(this);

    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to set the window title.</exception>
    public string Title
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowTitle(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window uses a high pixel density back buffer.
    /// </summary>
    public bool UseHighPixelDensity
    {
        get => HasState(State.HighPixelDensity);
        set => SetState(State.HighPixelDensity, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has a transparent buffer.
    /// </summary>
    public bool UseTransparentBuffer
    {
        get => HasState(State.TransparentBuffer);
        set => SetState(State.TransparentBuffer, value);
    }

    /// <summary>
    /// Gets or sets the width of the window's client area.
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
    /// (e.g. constraining the size of the content area to remain within the usable desktop bounds). Additionally,
    /// as this is just a request, the windowing system can deny the state change.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Width is less than or equal to 0.</exception>
    /// <exception cref="QuackNativeException">Thrown when failed to set the window size.</exception>
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

            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowSize(_handle, _width, _height));
        }
    }

    /// <summary>
    /// Gets the width of the window's client area in pixels.
    /// </summary>
    public int WidthInPixels { get; private set; }

    internal SDL_Window NativeHandle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_handle.IsInvalid, typeof(Window));
            return _handle.ToNonOwningHandle();
        }
    }

    /// <summary>
    /// Closes the window.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Closing the window will release all associated resources. You need to call <see cref="Create(string, int, int)"/> again to recreate the window.
    /// </para>
    /// <para>If the window is already closed, this method has no effect.</para>
    /// </remarks>
    public void Close()
    {
        if (!IsOpen)
            return;

        Dispose();
    }

    /// <summary>
    /// Creates the window with the specified title, width, and height.
    /// </summary>
    /// <remarks>
    /// If the window is already created, this method has no effect.
    /// </remarks>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the window.</exception>
    public void Create(string title, int width, int height)
    {
        if (IsOpen)
            return;

        Title = title;
        Initialize(width, height);
    }

    /// <summary>
    /// Disposes the window and releases all associated resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Requests the window to flash to get the user's attention.
    /// </summary>
    /// <param name="operation">The flash operation to perform on the window.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to flash the window.</exception>
    public void Flash(FlashOperation operation)
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_FlashWindow(_handle, operation));
    }

    /// <summary>
    /// Hides the window. It can be shown again using <see cref="Show"/>.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to hide the window.</exception>
    public void Hide()
    {
        if (!IsOpen || Hidden)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_HideWindow(_handle));
        _state |= State.Hidden;
    }

    /// <summary>
    /// Requests the window to be maximized.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Non-resizable windows can't be maximized. The window must have the <see cref="Resizable"/> set to <see langword="true"/>.
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
    /// <exception cref="QuackNativeException">Thrown when failed to maximize the window.</exception>
    public void Maximize()
    {
        if (!IsOpen || Maximized || !Resizable)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_MaximizeWindow(_handle));

        _state |= State.Maximized;
        _state &= ~State.Minimized;
    }

    /// <summary>
    /// Requests the window to be minimized to the taskbar or dock.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the window is in Fullscreen state, it will have no direct effect.
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
    /// <exception cref="QuackNativeException">Thrown when failed to minimize the window.</exception>
    public void Minimize()
    {
        if (!IsOpen || Minimized || Fullscreen)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_MinimizeWindow(_handle));

        _state |= State.Minimized;
        _state &= ~State.Maximized;
    }

    /// <summary>
    /// Polls for pending events and updates the window state accordingly.
    /// </summary>
    /// <remarks>
    /// If the window is not open, it will always return <see langword="false"/> with <paramref name="e"/> set to its default value.
    /// </remarks>
    /// <param name="e">The next event in the queue, if any.</param>
    /// <returns><see langword="true"/> if there was an event to process; otherwise, <see langword="false"/>.</returns>
    public bool Poll(out Event e)
    {
        if (!IsOpen)
        {
            e = default;
            return false;
        }

        ProcessDeferredInvocations();
        bool hasEvent = EventManager.Poll(out e);

        if (e.Window.Id != Id)
            return hasEvent;

        if (e.Type == EventType.WindowCloseRequested)
            Close();

        if (e.Type == EventType.WindowExposed)
            _state &= ~State.Occluded;

        if (e.Type == EventType.WindowOccluded)
            _state |= State.Occluded;

        if (e.Type == EventType.WindowResized)
        {
            _width = e.Window.Size.Width;
            _height = e.Window.Size.Height;
        }

        if (e.Type == EventType.WindowPixelSizeChanged)
        {
            WidthInPixels = e.Window.SizeInPixels.Width;
            HeightInPixels = e.Window.SizeInPixels.Height;
        }

        if (e.Type == EventType.WindowMoved)
            _position = e.Window.Position;

        if (e.Type == EventType.MouseEnter)
            _state |= State.MouseFocus;

        if (e.Type == EventType.MouseLeave)
            _state &= ~State.MouseFocus;

        if (e.Type == EventType.FocusGained)
            _state |= State.InputFocus;

        if (e.Type == EventType.FocusLost)
            _state &= ~State.InputFocus;

        if (e.Type == EventType.WindowRestored)
            _state &= ~(State.Minimized | State.Maximized);

        return hasEvent;
    }

    /// <summary>
    /// Requests the window to be raised above other windows and get input focus.
    /// </summary>
    /// <remarks>
    /// The result of this request is subject to desktop window manager policy, particularly if raising
    /// the requested window would result in stealing focus from another application.
    /// If the window is successfully raised and gains input focus,
    /// an <see cref="EventType.FocusGained"/> event will be emitted,
    /// and the window will have InputFocus state set.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to raise the window.</exception>
    public void Raise()
    {
        if (IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_RaiseWindow(_handle));
        _state |= State.InputFocus;
    }

    /// <summary>
    /// Restores the window from maximized or minimized state to its normal size.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the window is in Fullscreen state, it will have no direct effect.
    /// It may alter the window's state to the restored state when leaving fullscreen.
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
    public void Restore()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_RestoreWindow(_handle));
    }

    /// <summary>
    /// Sets the icon which appears in the taskbar.
    /// </summary>
    /// <remarks>
    /// If there is an existing icon, the surface will be disposed and replaced.
    /// </remarks>
    /// <param name="icon">A <see cref="Surface"/> containing the icon image.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to set the window icon.</exception>
    public unsafe void SetIcon(Surface icon)
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowIcon(_handle, icon.Handle));
    }

    /// <summary>
    /// Sets the icon which appears in the taskbar.
    /// </summary>
    /// <remarks>
    /// It loads the image from the specified file path.
    /// If there is an existing icon, it will be disposed.
    /// </remarks>
    /// <param name="path">The file path to the icon image.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to set the window icon.</exception>
    public void SetIcon(string path)
    {
        _icon?.Dispose();

        _icon = Image.Load(path);
        SetIcon(_icon);
    }

    /// <summary>
    /// Shows the window if it is hidden.
    /// </summary>
    /// <remarks>
    /// It's only the way to show a hidden window.
    /// If the window is minimized or maximized, use <see cref="Restore"/> instead.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when failed to show the window.</exception>
    public void Show()
    {
        if (!IsOpen || !Hidden)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_ShowWindow(_handle));
        _state &= ~State.Hidden;
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
    public void ShowSystemMenu(Vector2Int position)
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_ShowWindowSystemMenu(_handle, position.X, position.Y));
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
    public void ShowSystemMenu(Vector2 position)
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_ShowWindowSystemMenu(_handle, (int)position.X, (int)position.Y));
    }

    /// <summary>
    /// Blocks until all pending window state changes have been applied.
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
    /// <exception cref="QuackNativeException">Thrown when failed to sync the window.</exception>
    public void Sync()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(Native.SDL_SyncWindow(_handle));
    }

    /// <summary>
    /// Returns a string representation of the window in the format "Window[ID]" "Title" (WidthxHeight)".
    /// </summary>
    /// <returns>A string representation of the window.</returns>
    public override string ToString() => $"{this}";

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"Window[{Id}] \"{Title}\" ({Width}x{Height})", out charsWritten);

    /// <summary>
    /// Moves the mouse cursor to the specified position within the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event if relative mode is not enabled.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="x">The x-coordinate within the window.</param>
    /// <param name="y">The y-coordinate within the window.</param>
    public void WarpMouse(float x, float y) => Native.SDL_WarpMouseInWindow(_handle, x, y);

    /// <summary>
    /// Moves the mouse cursor to the specified position within the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event if relative mode is not enabled.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="position">The position within the window.</param>
    public void WarpMouse(Vector2 position) => WarpMouse(position.X, position.Y);

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="Window"/> class.
    /// </summary>
    /// <param name="disposing">value indicating whether the method call comes from a <see cref="Dispose()"/> method (its value is <see langword="true"/>) or from a finalizer (its value is <see langword="false"/>).</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        _disposed = true;

        if (disposing)
        {
            _icon?.Dispose();
            _icon = null;

            _handle.Dispose();

            QuackEngine.Release();
        }
    }

    internal void Invoke(Action<SDL_Window> action)
    {
        if (Environment.CurrentManagedThreadId == _threadId)
        {
            action(NativeHandle);
            return;
        }

        _invocations.Enqueue(action);
    }

    private bool HasState(State state) => (_state & state) == state;

    private void SetState(State state, bool apply) => _state = apply ? _state | state : _state & ~state;

    private void ProcessDeferredInvocations()
    {
        while (_invocations.TryDequeue(out Action<SDL_Window>? invocation))
            invocation(NativeHandle);
    }

    private void Initialize(int width, int height)
    {
        QuackEngine.Acquire(Subsystem.Video);

        _width = width;
        _height = height;

        using (Properties properties = new(this))
        {
            _handle = Native.SDL_CreateWindowWithProperties(properties.Id);
            QuackNativeException.ThrowIfHandleInvalid(_handle);
        }

        Id = Native.SDL_GetWindowID(_handle);
        QuackNativeException.ThrowIfZero(Id);

        uint propertiesId = Native.SDL_GetWindowProperties(_handle);
        Handle = new WindowHandle(Native.GetPointerProperty(propertiesId, $"SDL.window.{GetWindowHandleName()}", nint.Zero));

        QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowAspectRatio(_handle, AspectRatio.Minimum, AspectRatio.Maximum));
        QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowFullscreenMode(_handle, FullscreenMode));
        QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowMaximumSize(_handle, MaximumSize.Width, MaximumSize.Height));
        QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowMinimumSize(_handle, MinimumSize.Width, MinimumSize.Height));
        QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowMouseRect(_handle, MouseClip));
        QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowOpacity(_handle, Opacity));
    }

    private static string GetWindowHandleName()
    {
        if (OperatingSystem.IsLinux())
        {
            if (OperatingSystem.IsX11())
                return "x11.display";

            if (OperatingSystem.IsWayland())
                return "wayland.surface";
        }

        return "win32.hwnd";
    }
}

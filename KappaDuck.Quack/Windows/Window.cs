// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Input.Mouse;
using KappaDuck.Quack.Interop.Handles;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Displays;
using System.Collections.Concurrent;
using System.Text.Unicode;

namespace KappaDuck.Quack.Windows;

/// <summary>
/// Represents a OS window.
/// </summary>
/// <remarks>
/// There is no graphics context associated with this window.
/// </remarks>
public sealed class Window : IDisposable, ISpanFormattable, IUtf8SpanFormattable
{
    private readonly int _threadId = Environment.CurrentManagedThreadId;
    private readonly ConcurrentQueue<Action<SDL_Window>> _invocations = [];

    private SDL_Window _handle = SDL_Window.Zero;

    private State _state = State.None;
    private Vector2Int? _position;
    private int _width;
    private int _height;
    private float? _opacity;

    /// <summary>
    /// Creates an empty window.
    /// </summary>
    /// <remarks>
    /// It does not create the window. Use <see cref="Create(string, int, int)"/> to create a window.
    /// It is useful to delay window creation until necessary.
    /// </remarks>
    public Window()
    {
        Title = string.Empty;
        Handle = WindowHandle.Zero;
    }

    /// <summary>
    /// Creates a window with the title, width, and height.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <remarks>It creates the window immediately upon instantiation.</remarks>
    /// <exception cref="QuackInteropException">Thrown when failed to create the window.</exception>
    public Window(string title, int width, int height)
    {
        Title = title;
        Initialize(width, height);
    }

    /// <summary>
    /// Creates a window with the title and display mode.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="mode">The display mode.</param>
    /// <remarks>
    /// This creates the window immediately upon instantiation in fullscreen mode using the specified display mode,
    /// and sets the window size to the mode's width and height.
    /// </remarks>
    /// <exception cref="QuackInteropException">Thrown when failed to create the window.</exception>
    public Window(string title, DisplayMode mode) : this(title, mode.Width, mode.Height)
        => FullscreenMode = mode;

    /// <summary>
    /// Creates a window with the title and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The dimensions of the window.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to create the window.</exception>
    public Window(string title, SizeInt size) : this(title, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Creates a window with the title, position, and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="position">The position of the window on the screen.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to create the window.</exception>
    public Window(string title, SizeInt size, Vector2Int position) : this(title, size.Width, size.Height)
        => Position = position;

    /// <summary>
    /// Gets or sets a value indicating whether the window is always on top of other windows.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to set the always on top state.</exception>
    public bool AlwaysOnTop
    {
        get => HasState(State.AlwaysOnTop);
        set
        {
            SetState(State.AlwaysOnTop, value);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowAlwaysOnTop(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the aspect ratio of the window's client area.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the minimum or maximum aspect ratio is negative.</exception>
    /// <exception cref="QuackInteropException">Thrown when failed to set the aspect ratio.</exception>
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

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowAspectRatio(_handle, field.Minimum, field.Maximum));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has no decorations, such as title bar or borders.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to set the borderless state.</exception>
    public bool Borderless
    {
        get => HasState(State.Borderless);
        set
        {
            SetState(State.Borderless, value);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowBordered(_handle, !value));
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

            SDL3.Windows.GetWindowBordersSize(_handle, out int top, out int left, out int bottom, out int right);
            return (top, left, bottom, right);
        }
    }

    /// <summary>
    /// Gets the display that the window is currently on.
    /// </summary>
    /// <remarks>If the window is not open, this property returns <see langword="null"/>.</remarks>
    public Display? Display => IsOpen ? Display.FromId(SDL3.Windows.GetDisplayForWindow(_handle)) : null;

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
    public float DisplayScale => IsOpen ? SDL3.Windows.GetWindowDisplayScale(_handle) : 0.0f;

    /// <summary>
    /// Gets or sets a value indicating whether the window can receive input focus.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to set the focusable state.</exception>
    public bool Focusable
    {
        get => !HasState(State.NotFocusable);
        set
        {
            SetState(State.NotFocusable, !value);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowFocusable(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is in fullscreen mode.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to set the fullscreen state.</exception>
    public bool Fullscreen
    {
        get => HasState(State.Fullscreen);
        set
        {
            SetState(State.Fullscreen, value);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowFullscreen(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets the fullscreen display mode to use when the window is in fullscreen mode.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Setting to <see langword="null"/> will use borderless fullscreen desktop mode,
    /// or one of the fullscreen modes from <see cref="Display.Modes"/> to set an exclusive fullscreen mode.
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
    /// <exception cref="QuackInteropException">Thrown when failed to set the fullscreen mode.</exception>
    public DisplayMode? FullscreenMode
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowFullscreenMode(_handle, field));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window currently has keyboard focus.
    /// </summary>
    public bool HasKeyboardFocus
    {
        get => HasState(State.InputFocus);
        private set => SetState(State.InputFocus, value);
    }

    /// <summary>
    /// Gets a value indicating whether the window currently has mouse focus.
    /// </summary>
    public bool HasMouseFocus
    {
        get => HasState(State.MouseFocus);
        private set => SetState(State.MouseFocus, value);
    }

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
    /// <exception cref="QuackInteropException">Thrown when failed to set the window size.</exception>
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

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowSize(_handle, _width, _height));
        }
    }

    /// <summary>
    /// Gets the height of the window's client area in pixels.
    /// </summary>
    public int HeightInPixels { get; private set; }

    /// <summary>
    /// Gets or inits a value indicating whether the window is hidden.
    /// </summary>
    public bool Hidden
    {
        get => HasState(State.Hidden);
        init => SetState(State.Hidden, value);
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
    public bool IsScreenKeyboardVisible => IsOpen && SDL3.Windows.ScreenKeyboardShown(_handle);

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
    /// <exception cref="QuackInteropException">Thrown when failed to set the keyboard grab state.</exception>
    public bool KeyboardGrabbed
    {
        get => HasState(State.KeyboardGrabbed);
        set
        {
            SetState(State.KeyboardGrabbed, value);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowKeyboardGrab(_handle, value));
        }
    }

    /// <summary>
    /// Gets or inits a value indicating whether the window is maximized.
    /// </summary>
    public bool Maximized
    {
        get => HasState(State.Maximized);
        init
        {
            SetState(State.Maximized, value);
            RemoveState(State.Minimized);
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
    /// <exception cref="QuackInteropException">Thrown when failed to set the maximum size.</exception>
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

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowMaximumSize(_handle, field.Width, field.Height));
        }
    }

    /// <summary>
    /// Gets or inits a value indicating whether the window is minimized.
    /// </summary>
    public bool Minimized
    {
        get => HasState(State.Minimized);
        init
        {
            SetState(State.Minimized, value);
            RemoveState(State.Maximized);
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
    /// <exception cref="QuackInteropException">Thrown when failed to set the minimum size.</exception>
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

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowMinimumSize(_handle, field.Width, field.Height));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window has captured the mouse.
    /// </summary>
    /// <remarks>
    /// <para>
    /// For more information about mouse capture, see <see cref="Mouse.Capture(bool)"/>.
    /// </para>
    /// <para>
    /// It is not related to <see cref="MouseGrabbed"/>.
    /// </para>
    /// </remarks>
    public bool MouseCaptured
    {
        get
        {
            State state = (State)SDL3.Windows.GetWindowFlags(_handle);
            return (state & State.MouseCapture) == State.MouseCapture;
        }
    }

    /// <summary>
    /// Gets or sets the mouse clipping rectangle relative to the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>Setting to <see langword="null"/> or an empty <see cref="RectInt"/> removes the confined area.</para>
    /// <para>This will not grab the cursor, it only defines the area a cursor is restricted to when the window has mouse focus.</para>
    /// </remarks>
    /// <exception cref="QuackInteropException">Thrown when failed to clip the mouse.</exception>
    public RectInt? MouseClip
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowMouseRect(_handle, field));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has grabbed the mouse input.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to set the mouse grab state.</exception>
    public bool MouseGrabbed
    {
        get => HasState(State.MouseGrabbed);
        set
        {
            SetState(State.MouseGrabbed, value);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowMouseGrab(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use relative mouse mode for the window.
    /// </summary>
    /// <remarks>
    /// <para>
    /// While the window has focus and relative mouse mode is enabled, the cursor is hidden, the mouse position is constrained to the window,
    /// and the engine will report continuous relative mouse motion even if the mouse is at the edge of the window.
    /// </para>
    /// <para>
    /// If you'd like to keep the mouse position fixed while in relative mode, consider <see cref="MouseClip"/>.
    /// </para>
    /// <para>
    /// If you'd like the cursor to be at a specific position when relative mode ends,
    /// consider <see cref="WarpMouse(float, float)"/>/<see cref="WarpMouse(Vector2)"/> before disabling relative mode.
    /// </para>
    /// </remarks>
    public bool MouseRelativeMode
    {
        get => HasState(State.MouseRelativeMode);
        set
        {
            SetState(State.MouseRelativeMode, value);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowRelativeMouseMode(_handle, value));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window is occluded (completely covered by other windows).
    /// </summary>
    public bool Occluded
    {
        get => HasState(State.Occluded);
        private set => SetState(State.Occluded, value);
    }

    /// <summary>
    /// Gets or sets the opacity of the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>The default value is 1.0f.</para>
    /// <para>The opacity value should be in the range 0.0f - 1.0f. Otherwise the value will be clamped.</para>
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the opacity is negative.</exception>
    /// <exception cref="QuackInteropException">Thrown when failed to set the window opacity.</exception>
    public float Opacity
    {
        get => _opacity ?? 1.0f;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            _opacity = Math.Clamp(value, 0.0f, 1.0f);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowOpacity(_handle, _opacity.Value));
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
    /// <exception cref="QuackInteropException">Thrown when failed to set the window position.</exception>
    public Vector2Int Position
    {
        get
        {
            if (!_position.HasValue && IsOpen)
            {
                QuackInteropException.ThrowIfFailed(SDL3.Windows.GetWindowPosition(_handle, out int x, out int y));
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

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowPosition(_handle, value.X, value.Y));
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
    public float PixelDensity => IsOpen ? SDL3.Windows.GetWindowPixelDensity(_handle) : 0.0f;

    /// <summary>
    /// Gets the pixel format of the window's back buffer.
    /// </summary>
    /// <remarks>If the window is not open, it will return <see cref="PixelFormat.Unknown"/>.</remarks>
    public PixelFormat PixelFormat => !IsOpen ? PixelFormat.Unknown : SDL3.Windows.GetWindowPixelFormat(_handle);

    /// <summary>
    /// Gets or sets a value indicating whether the window can be resized by the user.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to set the resizable state.</exception>
    public bool Resizable
    {
        get => HasState(State.Resizable);
        set
        {
            SetState(State.Resizable, value);

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowResizable(_handle, value));
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
    /// <exception cref="QuackInteropException">Thrown when failed to get the safe area.</exception>
    public RectInt SafeArea
    {
        get
        {
            if (!IsOpen)
                return default;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.GetWindowSafeArea(_handle, out RectInt area));
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
    /// <exception cref="QuackInteropException">Thrown when failed to set the window size.</exception>
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

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowSize(_handle, _width, _height));
        }
    }

    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to set the window title.</exception>
    public string Title
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowTitle(_handle, field));
        }
    }

    /// <summary>
    /// Gets or inits a value indicating whether the window uses a high pixel density back buffer.
    /// </summary>
    public bool UseHighPixelDensity
    {
        get => HasState(State.HighPixelDensity);
        init => SetState(State.HighPixelDensity, value);
    }

    /// <summary>
    /// Gets or inits a value indicating whether the window has a transparent buffer.
    /// </summary>
    public bool UseTransparentBuffer
    {
        get => HasState(State.TransparentBuffer);
        init => SetState(State.TransparentBuffer, value);
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
    /// <exception cref="QuackInteropException">Thrown when failed to set the window size.</exception>
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

            QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowSize(_handle, _width, _height));
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

        _handle.Dispose();
        _state = State.None;
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
    /// <exception cref="QuackInteropException">Thrown when failed to create the window.</exception>
    public void Create(string title, int width, int height)
    {
        if (IsOpen)
            return;

        Title = title;
        Initialize(width, height);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _handle.Dispose();
        QuackEngine.Release();
    }

    /// <summary>
    /// Requests the window to flash to get the user's attention.
    /// </summary>
    /// <param name="operation">The flash operation to perform on the window.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to flash the window.</exception>
    public void Flash(FlashOperation operation)
    {
        if (!IsOpen)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.FlashWindow(_handle, operation));
    }

    /// <summary>
    /// Hides the window. It can be shown again using <see cref="Show"/>.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to hide the window.</exception>
    public void Hide()
    {
        if (!IsOpen || Hidden)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.HideWindow(_handle));
        SetState(State.Hidden);
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
    /// <exception cref="QuackInteropException">Thrown when failed to maximize the window.</exception>
    public void Maximize()
    {
        if (!IsOpen || Maximized || !Resizable)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.MaximizeWindow(_handle));

        SetState(State.Maximized);
        RemoveState(State.Minimized);
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
    /// <exception cref="QuackInteropException">Thrown when failed to minimize the window.</exception>
    public void Minimize()
    {
        if (!IsOpen || Minimized || Fullscreen)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.MinimizeWindow(_handle));

        SetState(State.Minimized);
        RemoveState(State.Maximized);
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
            Occluded = false;

        if (e.Type == EventType.WindowOccluded)
            Occluded = true;

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
            HasMouseFocus = true;

        if (e.Type == EventType.MouseLeave)
            HasMouseFocus = false;

        if (e.Type == EventType.FocusGained)
            HasKeyboardFocus = true;

        if (e.Type == EventType.FocusLost)
            HasKeyboardFocus = false;

        if (e.Type == EventType.WindowRestored)
            RemoveState(State.Maximized | State.Minimized);

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
    /// <exception cref="QuackInteropException">Thrown when failed to raise the window.</exception>
    public void Raise()
    {
        if (IsOpen)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.RaiseWindow(_handle));
        HasKeyboardFocus = true;
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
    /// <exception cref="QuackInteropException">Thrown when failed to restore the window.</exception>
    public void Restore()
    {
        if (!IsOpen)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.RestoreWindow(_handle));
        RemoveState(State.Maximized | State.Minimized);
    }

    /// <summary>
    /// Shows the window if it is hidden.
    /// </summary>
    /// <remarks>
    /// It's only the way to show a hidden window.
    /// If the window is minimized or maximized, use <see cref="Restore"/> instead.
    /// </remarks>
    /// <exception cref="QuackInteropException">Thrown when failed to show the window.</exception>
    public void Show()
    {
        if (!IsOpen || !Hidden)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.ShowWindow(_handle));
        RemoveState(State.Hidden);
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
    /// <exception cref="QuackInteropException">Thrown when failed to show the system menu.</exception>
    public void ShowSystemMenu(Vector2Int position)
    {
        if (!IsOpen)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.ShowWindowSystemMenu(_handle, position.X, position.Y));
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
    /// <exception cref="QuackInteropException">Thrown when failed to show the system menu.</exception>
    public void ShowSystemMenu(Vector2 position)
    {
        if (!IsOpen)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.ShowWindowSystemMenu(_handle, (int)position.X, (int)position.Y));
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
    /// <exception cref="QuackInteropException">Thrown when failed to sync the window.</exception>
    public void Sync()
    {
        if (!IsOpen)
            return;

        QuackInteropException.ThrowIfFailed(SDL3.Windows.SyncWindow(_handle));
    }

    /// <inheritdoc/>
    public override string ToString() => $"{this}";

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"Window[{Id}] \"{Title}\" ({Width}x{Height})", out charsWritten);

    /// <inheritdoc/>
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"Window[{Id}] \"{Title}\" ({Width}x{Height})", out bytesWritten);

    /// <summary>
    /// Moves the mouse cursor to the specified position within the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event if relative mode is not enabled.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="x">The x-coordinate within the window.</param>
    /// <param name="y">The y-coordinate within the window.</param>
    public void WarpMouse(float x, float y) => SDL3.Windows.WarpMouseInWindow(_handle, x, y);

    /// <summary>
    /// Moves the mouse cursor to the specified position within the window's client area.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event if relative mode is not enabled.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="position">The position within the window.</param>
    public void WarpMouse(Vector2 position) => WarpMouse(position.X, position.Y);

    internal void Invoke(Action<SDL_Window> action)
    {
        if (Environment.CurrentManagedThreadId == _threadId)
        {
            action(NativeHandle);
            return;
        }

        _invocations.Enqueue(action);
    }

    [MemberNotNull(nameof(Handle))]
    private void Initialize(int width, int height)
    {
        QuackEngine.AddRef(Subsystem.Video);

        _width = width;
        _height = height;

        using (Properties properties = new(this, _position))
        {
            _handle = SDL3.Windows.CreateWindowWithProperties(properties);
            QuackInteropException.ThrowIfHandleInvalid(_handle);
        }

        Id = SDL3.Windows.GetWindowID(_handle);
        QuackInteropException.ThrowIfZero(Id);

        uint propertiesId = SDL3.Windows.GetWindowProperties(_handle);
        Handle = new WindowHandle(SDL3.Properties.GetPointerProperty(propertiesId, $"SDL.window.{GetWindowHandleName()}", nint.Zero));

        QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowAspectRatio(_handle, AspectRatio.Minimum, AspectRatio.Maximum));
        QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowFullscreenMode(_handle, FullscreenMode));
        QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowMaximumSize(_handle, MaximumSize.Width, MaximumSize.Height));
        QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowMinimumSize(_handle, MinimumSize.Width, MinimumSize.Height));
        QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowRelativeMouseMode(_handle, MouseRelativeMode));
        QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowMouseRect(_handle, MouseClip));
        QuackInteropException.ThrowIfFailed(SDL3.Windows.SetWindowOpacity(_handle, Opacity));
    }

    private static string GetWindowHandleName()
    {
        if (OperatingSystem.IsLinux())
        {
            if (IsX11())
                return "x11.display";

            if (IsWayland())
                return "wayland.surface";
        }

        return "win32.hwnd";

        static bool IsWayland() => Environment.GetEnvironmentVariable("XDG_SESSION_TYPE") == "wayland";

        static bool IsX11() => Environment.GetEnvironmentVariable("XDG_SESSION_TYPE") == "x11";
    }

    private bool HasState(State state) => (_state & state) == state;

    private void ProcessDeferredInvocations()
    {
        while (_invocations.TryDequeue(out Action<SDL_Window>? invocation))
            invocation(NativeHandle);
    }

    private void RemoveState(State state) => _state &= ~state;

    private void SetState(State state, bool apply = true) => _state = apply ? _state | state : _state & ~state;
}

file sealed class Properties : SDL3.Properties
{
    internal Properties(Window window, Vector2Int? position)
    {
        Set("SDL.window.create.always_on_top", window.AlwaysOnTop);
        Set("SDL.window.create.borderless", window.Borderless);
        Set("SDL.window.create.focusable", window.Focusable);
        Set("SDL.window.create.fullscreen", window.Fullscreen);
        Set("SDL.window.create.hidden", window.Hidden);
        Set("SDL.window.create.maximized", window.Maximized);
        Set("SDL.window.create.minimized", window.Minimized);
        Set("SDL.window.create.mouse_grabbed", window.MouseGrabbed);
        Set("SDL.window.create.resizable", window.Resizable);
        Set("SDL.window.create.high_pixel_density", window.UseHighPixelDensity);
        Set("SDL.window.create.transparent", window.UseTransparentBuffer);

        Set("SDL.window.create.title", window.Title);
        Set("SDL.window.create.width", window.Width);
        Set("SDL.window.create.height", window.Height);

        if (position.HasValue)
        {
            Set("SDL.window.create.x", position.Value.X);
            Set("SDL.window.create.y", position.Value.Y);
        }
    }
}

[Flags]
internal enum State : ulong
{
    /// <summary>
    /// Indicates that the window has no state set.
    /// </summary>
    None = 0,

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

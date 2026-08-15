// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Input;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video;
using KappaDuck.Quack.Video.Pixels;
using KappaDuck.Quack.Windowing.Handles;
using System.Text.Unicode;

namespace KappaDuck.Quack.Windowing;

/// <summary>
/// Represents an operating-system window.
/// </summary>
public sealed class Window : IDisposable, ISpanFormattable, IUtf8SpanFormattable
{
    private State _state;
    private Point? _position;
    private int _width;
    private int _height;
    private float? _opacity;
    private Surface? _icon;

    /// <summary>
    /// Creates an empty window.
    /// </summary>
    /// <remarks>
    /// It does not create the window. Use <see cref="Create(string, int, int)"/> to create it, which is useful
    /// to delay creation and configure creation-only states first.
    /// </remarks>
    public Window() => Title = string.Empty;

    /// <summary>
    /// Creates a window with the given title and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <remarks>It creates the native window immediately.</remarks>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> is negative or zero.</exception>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
    public Window(string title, int width, int height)
    {
        Title = title;
        Initialize(width, height);
    }

    /// <summary>
    /// Creates a fullscreen window with the given title and display mode.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="mode">The fullscreen display mode to use.</param>
    /// <remarks>It creates the native window immediately in fullscreen using the mode's size.</remarks>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
    public Window(string title, DisplayMode mode) : this(title, mode.Width, mode.Height)
        => FullscreenMode = mode;

    /// <summary>
    /// Creates a window with the given title and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The size of the window.</param>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
    public Window(string title, Size size) : this(title, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Creates a window with the given title, size and position.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="position">The position of the window on the screen.</param>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
    public Window(string title, Size size, Point position) : this(title, size.Width, size.Height)
        => Position = position;

    /// <summary>
    /// Creates a window with the given title, size, and options.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="options">The options that configure how the window is created.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> is negative or zero.</exception>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
    public Window(string title, int width, int height, WindowOptions options)
    {
        Title = title;

        if (options.Position is { } position)
            Position = position;

        AspectRatio = options.AspectRatio;
        MinimumSize = options.MinimumSize;
        MaximumSize = options.MaximumSize;
        FullscreenMode = options.FullscreenMode;

        AlwaysOnTop = options.AlwaysOnTop;
        Borderless = options.Borderless;
        Focusable = options.Focusable;
        Fullscreen = options.Fullscreen;
        Hidden = options.Hidden;
        Maximized = options.Maximized;
        Minimized = options.Minimized;
        Opacity = options.Opacity;
        Resizable = options.Resizable;
        UseHighPixelDensity = options.UseHighPixelDensity;
        UseTransparentBuffer = options.UseTransparentBuffer;

        Initialize(width, height);
    }

    /// <summary>
    /// Creates a window with the given title, size, and options.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The size of the window.</param>
    /// <param name="options">The options that configure how the window is created.</param>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
    public Window(string title, Size size, WindowOptions options) : this(title, size.Width, size.Height, options)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is always on top of other windows.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to set the always on top state.</exception>
    public bool AlwaysOnTop
    {
        get => HasState(State.AlwaysOnTop);
        set
        {
            SetState(State.AlwaysOnTop, value);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowAlwaysOnTop(NativeHandle, value)));
        }
    }

    /// <summary>
    /// Gets or sets the minimum and maximum aspect ratios of the window's client area, where zero means no limit.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">The minimum or maximum aspect ratio is negative.</exception>
    /// <exception cref="QuackInteropException">Failed to set the aspect ratio.</exception>
    public AspectRatio AspectRatio
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowAspectRatio(NativeHandle, field.Minimum, field.Maximum)));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has no decorations, such as a title bar or borders.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to set the borderless state.</exception>
    public bool Borderless
    {
        get => HasState(State.Borderless);
        set
        {
            SetState(State.Borderless, value);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowBordered(NativeHandle, !value)));
        }
    }

    /// <summary>
    /// Gets the size of the window's decorations as (top, left, bottom, right) in screen coordinates.
    /// </summary>
    /// <remarks>All values are zero when the window is not open or is borderless, or if the platform cannot report them.</remarks>
    public BordersSize BordersSize
    {
        get
        {
            if (!IsOpen || Borderless)
                return default;

            unsafe
            {
                SDL3.GetWindowBordersSize(NativeHandle, out int top, out int left, out int bottom, out int right);
                return new(top, left, bottom, right);
            }
        }
    }

    /// <summary>
    /// Gets the display the window is currently on, or <see langword="null"/> if the window is not open.
    /// </summary>
    public Display? Display => IsOpen ? new Display(unsafe (SDL3.GetDisplayForWindow(NativeHandle))) : null;

    /// <summary>
    /// Gets the content display scale relative to the window's pixel size, or <c>0</c> if the window is not open.
    /// </summary>
    public float DisplayScale => IsOpen ? unsafe (SDL3.GetWindowDisplayScale(NativeHandle)) : 0.0f;

    /// <summary>
    /// Gets or sets a value indicating whether the window can receive input focus.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to set the focusable state.</exception>
    public bool Focusable
    {
        get => !HasState(State.NotFocusable);
        set
        {
            SetState(State.NotFocusable, !value);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowFocusable(NativeHandle, value)));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is in fullscreen mode.
    /// </summary>
    /// <remarks>Use <see cref="FullscreenMode"/> to control the resolution. Fullscreen changes may be asynchronous, see <see cref="Sync"/>.</remarks>
    /// <exception cref="QuackInteropException">Failed to set the fullscreen state.</exception>
    public bool Fullscreen
    {
        get => HasState(State.Fullscreen);
        set
        {
            SetState(State.Fullscreen, value);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowFullscreen(NativeHandle, value)));
        }
    }

    /// <summary>
    /// Gets or sets the fullscreen display mode used when the window is in fullscreen mode.
    /// </summary>
    /// <remarks>
    /// Set to <see langword="null"/> to use borderless desktop fullscreen, or one of the modes from
    /// <see cref="Display.FullscreenModes"/> for an exclusive fullscreen mode.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to set the fullscreen mode.</exception>
    public DisplayMode? FullscreenMode
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            if (field is null)
            {
                SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowFullscreenMode(NativeHandle, null)));
                return;
            }

            SDL_DisplayMode native = field.Value.ToNative();
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowFullscreenMode(NativeHandle, &native)));
        }
    }

    /// <summary>
    /// Gets a safe, non-owning handle to the native platform window (HWND on Windows, the X11 or Wayland handle on Linux).
    /// </summary>
    /// <remarks>Disposing the window will make the handle invalid and can't be used anymore.</remarks>
    /// <exception cref="ObjectDisposedException">The window is disposed.</exception>
    public WindowHandle Handle
    {
        get
        {
            ThrowIfDisposed();
            return field;
        }
        private set;
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
    /// Gets or sets the height of the window's client area.
    /// </summary>
    /// <remarks>Setting the height is ignored while the window is in fullscreen or maximized state.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">The height is less than or equal to zero.</exception>
    /// <exception cref="QuackInteropException">Failed to set the window size.</exception>
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

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowSize(NativeHandle, _width, _height)));
        }
    }

    /// <summary>
    /// Gets the height of the window's client area in physical pixels.
    /// </summary>
    public int HeightInPixels { get; private set; }

    /// <summary>
    /// Gets or initializes a value indicating whether the window starts hidden.
    /// </summary>
    /// <remarks>Applied when the window is created; use <see cref="Show"/> and <see cref="Hide"/> at runtime.</remarks>
    public bool Hidden
    {
        get => HasState(State.Hidden);
        init => SetState(State.Hidden, value);
    }

    /// <summary>
    /// Gets the unique identifier of the window, or 0 if it is not open.
    /// </summary>
    /// <remarks>This identifier is what window events use to identify the window that generated them.</remarks>
    public uint Id { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the window is currently open.
    /// </summary>
    public bool IsOpen => unsafe (NativeHandle is not null);

    /// <summary>
    /// Gets a value indicating whether the on-screen keyboard is visible for the window.
    /// </summary>
    public bool IsScreenKeyboardVisible => IsOpen && unsafe (SDL3.ScreenKeyboardShown(NativeHandle));

    /// <summary>
    /// Gets or sets a value indicating whether the window has grabbed the keyboard input.
    /// </summary>
    /// <remarks>
    /// Keyboard grab captures system shortcuts such as Alt+Tab. It is intended for specialized applications such as
    /// VNC clients or VM front-ends; normal games should not use it. Enabling a grab takes it from any other grabbed window.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to set the keyboard grab state.</exception>
    public bool KeyboardGrabbed
    {
        get => HasState(State.KeyboardGrabbed);
        set
        {
            SetState(State.KeyboardGrabbed, value);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowKeyboardGrab(NativeHandle, value)));
        }
    }

    /// <summary>
    /// Gets or initializes a value indicating whether the window starts maximized.
    /// </summary>
    /// <remarks>Applied when the window is created; use <see cref="Maximize"/> and <see cref="Restore"/> at runtime.</remarks>
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
    /// Gets or sets the maximum size of the window's client area, where (0, 0) removes the limit.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">The width or height is negative.</exception>
    /// <exception cref="QuackInteropException">Failed to set the maximum size.</exception>
    public Size MaximumSize
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            field = value;

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowMaximumSize(NativeHandle, field.Width, field.Height)));
        }
    }

    /// <summary>
    /// Gets or initializes a value indicating whether the window starts minimized.
    /// </summary>
    /// <remarks>Applied when the window is created; use <see cref="Minimize"/> and <see cref="Restore"/> at runtime.</remarks>
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
    /// Gets or sets the minimum size of the window's client area, where (0, 0) removes the limit.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">The width or height is negative.</exception>
    /// <exception cref="QuackInteropException">Failed to set the minimum size.</exception>
    public Size MinimumSize
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            field = value;

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowMinimumSize(NativeHandle, field.Width, field.Height)));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window has captured the mouse, unrelated to <see cref="MouseGrabbed"/>.
    /// </summary>
    /// <remarks>
    /// For more information about mouse capture, see <see cref="Mouse.Capture(bool)"/>.
    /// </remarks>
    public bool MouseCaptured => IsOpen && (unsafe (SDL3.GetWindowFlags(NativeHandle) & State.MouseCapture) == State.MouseCapture);

    /// <summary>
    /// Gets or sets the rectangle the mouse is confined to within the window, or <see langword="null"/> if unconfined.
    /// </summary>
    /// <remarks>This does not grab the cursor; it only defines the area the cursor is restricted to while the window has mouse focus.</remarks>
    /// <exception cref="QuackInteropException">Failed to set the mouse clipping rectangle.</exception>
    public RectI? MouseClip
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            if (field is null)
            {
                SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowMouseRect(NativeHandle, null)));
                return;
            }

            RectI rect = field.Value;
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowMouseRect(NativeHandle, &rect)));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has grabbed the mouse input.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to set the mouse grab state.</exception>
    public bool MouseGrabbed
    {
        get => HasState(State.MouseGrabbed);
        set
        {
            SetState(State.MouseGrabbed, value);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowMouseGrab(NativeHandle, value)));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether relative mouse mode is enabled for the window.
    /// </summary>
    /// <remarks>
    /// While the window has focus and relative mode is enabled, the cursor is hidden and constrained to the window and
    /// continuous relative motion is reported, which is ideal for first-person camera control.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to set the relative mouse mode.</exception>
    public bool MouseRelativeMode
    {
        get => HasState(State.MouseRelativeMode);
        set
        {
            SetState(State.MouseRelativeMode, value);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowRelativeMouseMode(NativeHandle, value)));
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
    /// Gets or sets the opacity of the window's client area, from 0 (transparent) to 1 (opaque).
    /// </summary>
    /// <remarks>The default value is 1. Values outside the range are clamped.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">The opacity is negative.</exception>
    /// <exception cref="QuackInteropException">Failed to set the window opacity.</exception>
    public float Opacity
    {
        get => _opacity ?? 1.0f;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            _opacity = Math.Clamp(value, 0.0f, 1.0f);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowOpacity(NativeHandle, _opacity.Value)));
        }
    }

    /// <summary>
    /// Gets the parent of this window, or <see langword="null"/> if it has no parent or is not open.
    /// </summary>
    public Window? Parent => IsOpen ? unsafe (Windows.FromHandle(SDL3.GetWindowParent(NativeHandle))) : null;

    /// <summary>
    /// Gets the pixel density of the window, the ratio of physical pixels to screen coordinates, or <c>0</c> if not open.
    /// </summary>
    public float PixelDensity => IsOpen ? unsafe (SDL3.GetWindowPixelDensity(NativeHandle)) : 0.0f;

    /// <summary>
    /// Gets the pixel format of the window's back buffer, or <see cref="PixelFormat.Unknown"/> if the window is not open.
    /// </summary>
    public PixelFormat PixelFormat => IsOpen ? unsafe (SDL3.GetWindowPixelFormat(NativeHandle)) : PixelFormat.Unknown;

    /// <summary>
    /// Gets or sets the position of the top-left corner of the window on the screen.
    /// </summary>
    /// <remarks>Setting the position is ignored while the window is in fullscreen or maximized state.</remarks>
    /// <exception cref="QuackInteropException">Failed to set the window position.</exception>
    public Point Position
    {
        get
        {
            if (!_position.HasValue && IsOpen)
            {
                SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetWindowPosition(NativeHandle, out int x, out int y)));
                _position = new Point(x, y);
            }

            return _position ?? default;
        }
        set
        {
            if (Fullscreen || Maximized)
                return;

            _position = value;

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowPosition(NativeHandle, value.X, value.Y)));
        }
    }

    /// <summary>
    /// Gets the associated renderer.
    /// </summary>
    public Renderer? Renderer { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the window can be resized by the user.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to set the resizable state.</exception>
    public bool Resizable
    {
        get => HasState(State.Resizable);
        set
        {
            SetState(State.Resizable, value);

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowResizable(NativeHandle, value)));
        }
    }

    /// <summary>
    /// Gets the safe area of the window, the region not obscured by notches, rounded corners or system bars.
    /// </summary>
    /// <remarks>Returns an empty rectangle when the window is not open.</remarks>
    /// <exception cref="QuackInteropException">Failed to get the safe area.</exception>
    public RectI SafeArea
    {
        get
        {
            if (!IsOpen)
                return default;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.GetWindowSafeArea(NativeHandle, out RectI area)));
            return area;
        }
    }

    /// <summary>
    /// Gets or sets the size of the window's client area.
    /// </summary>
    /// <remarks>Setting the size is ignored while the window is in fullscreen or maximized state.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">The width or height is less than or equal to zero.</exception>
    /// <exception cref="QuackInteropException">Failed to set the window size.</exception>
    public Size Size
    {
        get => new(_width, _height);
        set
        {
            if (Fullscreen || Maximized)
                return;

            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value.Width, 0);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value.Height, 0);

            _width = value.Width;
            _height = value.Height;

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowSize(NativeHandle, _width, _height)));
        }
    }

    /// <summary>
    /// Gets the size of the window's client area in physical pixels.
    /// </summary>
    public Size SizeInPixels => new(WidthInPixels, HeightInPixels);

    /// <summary>
    /// Gets the controller for this window's taskbar progress indicator.
    /// </summary>
    public TaskbarProgressBar TaskbarProgressBar => field ??= new TaskbarProgressBar(this);

    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to set the window title.</exception>
    public string Title
    {
        get;
        set
        {
            field = value;

            if (!IsOpen)
                return;

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowTitle(NativeHandle, field)));
        }
    }

    /// <summary>
    /// Gets or initializes a value indicating whether the window uses a high pixel density back buffer.
    /// </summary>
    /// <remarks>Applied when the window is created.</remarks>
    public bool UseHighPixelDensity
    {
        get => HasState(State.HighPixelDensity);
        init => SetState(State.HighPixelDensity, value);
    }

    /// <summary>
    /// Gets or initializes a value indicating whether the window has a transparent buffer.
    /// </summary>
    /// <remarks>Applied when the window is created.</remarks>
    public bool UseTransparentBuffer
    {
        get => HasState(State.TransparentBuffer);
        init => SetState(State.TransparentBuffer, value);
    }

    /// <summary>
    /// Gets or sets the width of the window's client area.
    /// </summary>
    /// <remarks>Setting the width is ignored while the window is in fullscreen or maximized state.</remarks>
    /// <exception cref="ArgumentOutOfRangeException">The width is less than or equal to zero.</exception>
    /// <exception cref="QuackInteropException">Failed to set the window size.</exception>
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

            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowSize(NativeHandle, _width, _height)));
        }
    }

    /// <summary>
    /// Gets the width of the window's client area in physical pixels.
    /// </summary>
    public int WidthInPixels { get; private set; }

    /// <summary>
    /// Gets the CPU-accessible pixel surface currently acquired for this window, or <see langword="null"/> if none.
    /// </summary>
    public WindowSurface? WindowSurface { get; private set; }

    internal SDL_Window* NativeHandle { get; private set; }

    /// <summary>
    /// Clears any in-progress text composition for the window.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to clear the composition.</exception>
    public void ClearComposition()
    {
        if (!IsOpen)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.ClearComposition(NativeHandle)));
    }

    /// <summary>
    /// Closes the window.
    /// </summary>
    /// <remarks>The window can be recreated with <see cref="Create(string, int, int)"/>. Has no effect if already closed.</remarks>
    public void Close()
    {
        if (!IsOpen)
            return;

        Windows.Unregister(this);

        Id = 0;
        _state = State.None;
    }

    /// <summary>
    /// Creates the window with the given title and size.
    /// </summary>
    /// <remarks>Has no effect if the window is already open. Creation-only states set beforehand are applied here.</remarks>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> is negative or zero.</exception>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
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
        unsafe
        {
            if (NativeHandle is null)
                return;

            _icon?.Dispose();
            _icon = null;

            SDL3.DestroyWindow(NativeHandle);
            NativeHandle = null;
        }
    }

    /// <summary>
    /// Requests the window to flash to get the user's attention.
    /// </summary>
    /// <param name="operation">The flash operation to perform.</param>
    /// <exception cref="QuackInteropException">Failed to flash the window.</exception>
    public void Flash(FlashOperation operation)
    {
        if (!IsOpen)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.FlashWindow(NativeHandle, operation)));
    }

    /// <summary>
    /// Hides the window. It can be shown again with <see cref="Show"/>.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to hide the window.</exception>
    public void Hide()
    {
        if (!IsOpen || Hidden)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.HideWindow(NativeHandle)));
        SetState(State.Hidden);
    }

    /// <summary>
    /// Requests the window to be maximized.
    /// </summary>
    /// <remarks>Non-resizable windows cannot be maximized. The change may be asynchronous, see <see cref="Sync"/>.</remarks>
    /// <exception cref="QuackInteropException">Failed to maximize the window.</exception>
    public void Maximize()
    {
        if (!IsOpen || Maximized || !Resizable)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.MaximizeWindow(NativeHandle)));

        SetState(State.Maximized);
        RemoveState(State.Minimized);
    }

    /// <summary>
    /// Requests the window to be minimized.
    /// </summary>
    /// <remarks>Has no effect while the window is in fullscreen state. The change may be asynchronous, see <see cref="Sync"/>.</remarks>
    /// <exception cref="QuackInteropException">Failed to minimize the window.</exception>
    public void Minimize()
    {
        if (!IsOpen || Minimized || Fullscreen)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.MinimizeWindow(NativeHandle)));

        SetState(State.Minimized);
        RemoveState(State.Maximized);
    }

    /// <summary>
    /// Polls the next event from the queue and updates this window's cached state from it.
    /// </summary>
    /// <remarks>
    /// When a close is requested for this window, it is closed so the main loop ends. Resize, move, focus and other
    /// window events update the window's cached properties. The polled event is still returned so the application can react to it.
    /// </remarks>
    /// <param name="e">The next event fetched from the queue.</param>
    /// <returns><see langword="true"/> if an event was fetched; otherwise <see langword="false"/>.</returns>
    public bool Poll(out Event e)
    {
        if (!IsOpen)
        {
            e = default;
            return false;
        }

        if (!EventQueue.Poll(out e))
            return false;

        Update(in e);
        return true;
    }

    /// <summary>
    /// Requests the window to be raised above other windows and given input focus.
    /// </summary>
    /// <remarks>The result is subject to the window manager's focus policy.</remarks>
    /// <exception cref="QuackInteropException">Failed to raise the window.</exception>
    public void Raise()
    {
        if (!IsOpen)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.RaiseWindow(NativeHandle)));
        HasKeyboardFocus = true;
    }

    /// <summary>
    /// Restores the window from a maximized or minimized state to its normal size.
    /// </summary>
    /// <remarks>The change may be asynchronous, see <see cref="Sync"/>.</remarks>
    /// <exception cref="QuackInteropException">Failed to restore the window.</exception>
    public void Restore()
    {
        if (!IsOpen)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.RestoreWindow(NativeHandle)));
        RemoveState(State.Maximized | State.Minimized);
    }

    /// <summary>
    /// Sets the icon of the window.
    /// </summary>
    /// <remarks>
    /// It will dispose the previous icon before to set the new icon.
    /// </remarks>
    /// <param name="icon">The surface to use as the icon.</param>
    /// <exception cref="QuackInteropException">Failed to set the icon.</exception>
    public void SetIcon(Surface icon)
    {
        if (!IsOpen)
            return;

        UpdateIcon(icon);
    }

    /// <summary>
    /// Sets the icon of the window from a file.
    /// </summary>
    /// <remarks>
    /// It will dispose the previous icon before to set the new icon.
    /// </remarks>
    /// <param name="path">The path to the image file.</param>
    /// <exception cref="QuackInteropException">Failed to set the icon.</exception>
    public void SetIcon(string path)
    {
        if (!IsOpen)
            return;

        UpdateIcon(Surface.FromFile(path));
    }

    /// <summary>
    /// Sets the icon of the window from a stream.
    /// </summary>
    /// <param name="stream">The stream to read the image from.</param>
    /// <exception cref="QuackInteropException">Failed to set the icon.</exception>
    public void SetIcon(Stream stream)
    {
        if (!IsOpen)
            return;

        UpdateIcon(Surface.FromStream(stream));
    }

    /// <summary>
    /// Sets the shape of this window from the alpha channel of <paramref name="shape"/>, making transparent areas
    /// click-through, or removes any existing shape when <paramref name="shape"/> is <see langword="null"/>.
    /// </summary>
    /// <remarks>The window must have been created with <see cref="UseTransparentBuffer"/> set to <see langword="true"/>.</remarks>
    /// <param name="shape">The surface whose alpha channel defines the window's shape, or <see langword="null"/> to clear it.</param>
    /// <exception cref="QuackInteropException">Failed to set the window's shape.</exception>
    /// <exception cref="ObjectDisposedException">The window is not open.</exception>
    public void SetShape(Surface? shape)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowShape(NativeHandle, shape?.Handle)));
    }

    /// <summary>
    /// Shows the window if it is hidden.
    /// </summary>
    /// <remarks>If the window is minimized or maximized, use <see cref="Restore"/> instead.</remarks>
    /// <exception cref="QuackInteropException">Failed to show the window.</exception>
    public void Show()
    {
        if (!IsOpen || !Hidden)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.ShowWindow(NativeHandle)));
        RemoveState(State.Hidden);
    }

    /// <summary>
    /// Displays the system window menu at the given position, relative to the top-left of the window.
    /// </summary>
    /// <param name="position">The position of the menu, relative to the window.</param>
    /// <exception cref="QuackInteropException">Failed to show the system menu.</exception>
    public void ShowSystemMenu(Point position)
    {
        if (!IsOpen)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.ShowWindowSystemMenu(NativeHandle, position.X, position.Y)));
    }

    /// <summary>
    /// Blocks until all pending window state changes have been applied by the window manager.
    /// </summary>
    /// <remarks>Does nothing on windowing systems where changes are immediate.</remarks>
    /// <exception cref="QuackInteropException">Failed to synchronize the window.</exception>
    public void Sync()
    {
        if (!IsOpen)
            return;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SyncWindow(NativeHandle)));
    }

    /// <summary>
    /// Moves the mouse cursor to the given position within the window's client area.
    /// </summary>
    /// <param name="position">The position within the window.</param>
    public void WarpMouse(PointF position) => WarpMouse(position.X, position.Y);

    /// <summary>
    /// Moves the mouse cursor to the given position within the window's client area.
    /// </summary>
    /// <param name="x">The x-coordinate within the window.</param>
    /// <param name="y">The y-coordinate within the window.</param>
    public void WarpMouse(float x, float y)
    {
        if (!IsOpen)
            return;

        unsafe
        {
            SDL3.WarpMouseInWindow(NativeHandle, x, y);
        }
    }

    /// <inheritdoc/>
    public override string ToString() => $"Window[{Id}] \"{Title}\" ({_width}x{_height})";

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"Window[{Id}] \"{Title}\" ({_width}x{_height})", out charsWritten);

    /// <inheritdoc/>
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"Window[{Id}] \"{Title}\" ({_width}x{_height})", out bytesWritten);

    internal void Bind(Renderer renderer)
    {
        if (WindowSurface is not null)
            ThrowHelper.ThrowInvalidOperation("This window already has a surface. Dispose it before creating a renderer.");

        if (Renderer is not null && !ReferenceEquals(Renderer, renderer))
            ThrowHelper.ThrowInvalidOperation("This window is already bound to a renderer. Dispose the existing one before creating another.");

        Renderer = renderer;
    }

    internal void Bind(WindowSurface surface)
    {
        if (Renderer is not null)
            ThrowHelper.ThrowInvalidOperation("This window already has a renderer. Dispose it before acquiring its surface.");

        if (WindowSurface is not null && !ReferenceEquals(WindowSurface, surface))
            ThrowHelper.ThrowInvalidOperation("This window already has a surface. Dispose the existing one before creating another.");

        WindowSurface = surface;
    }

    internal void Unbind(Renderer renderer)
    {
        if (!ReferenceEquals(Renderer, renderer))
            return;

        Renderer = null;
    }

    internal void Unbind(WindowSurface surface)
    {
        if (!ReferenceEquals(WindowSurface, surface))
            return;

        WindowSurface = null;
    }

    private unsafe void ApplyDeferredSettings()
    {
        if (AspectRatio.Minimum > 0.0f || AspectRatio.Maximum > 0.0f)
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowAspectRatio(NativeHandle, AspectRatio.Minimum, AspectRatio.Maximum));

        if (MaximumSize.Width > 0 || MaximumSize.Height > 0)
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowMaximumSize(NativeHandle, MaximumSize.Width, MaximumSize.Height));

        if (MinimumSize.Width > 0 || MinimumSize.Height > 0)
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowMinimumSize(NativeHandle, MinimumSize.Width, MinimumSize.Height));

        if (HasState(State.KeyboardGrabbed))
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowKeyboardGrab(NativeHandle, true));

        if (HasState(State.MouseRelativeMode))
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowRelativeMouseMode(NativeHandle, true));

        if (FullscreenMode is not null)
        {
            SDL_DisplayMode native = FullscreenMode.Value.ToNative();
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowFullscreenMode(NativeHandle, &native));
        }

        if (MouseClip is not null)
        {
            RectI rect = MouseClip.Value;
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowMouseRect(NativeHandle, &rect));
        }

        if (_opacity is not null)
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowOpacity(NativeHandle, _opacity.Value));
    }

    private Properties BuildWindowProperties()
    {
        Properties properties = new();

        properties.Set("SDL.window.create.always_on_top", HasState(State.AlwaysOnTop));
        properties.Set("SDL.window.create.borderless", HasState(State.Borderless));
        properties.Set("SDL.window.create.focusable", !HasState(State.NotFocusable));
        properties.Set("SDL.window.create.fullscreen", HasState(State.Fullscreen));
        properties.Set("SDL.window.create.hidden", HasState(State.Hidden));
        properties.Set("SDL.window.create.maximized", HasState(State.Maximized));
        properties.Set("SDL.window.create.minimized", HasState(State.Minimized));
        properties.Set("SDL.window.create.mouse_grabbed", HasState(State.MouseGrabbed));
        properties.Set("SDL.window.create.resizable", HasState(State.Resizable));
        properties.Set("SDL.window.create.high_pixel_density", HasState(State.HighPixelDensity));
        properties.Set("SDL.window.create.transparent", HasState(State.TransparentBuffer));

        properties.Set("SDL.window.create.title", Title);
        properties.Set("SDL.window.create.width", _width);
        properties.Set("SDL.window.create.height", _height);

        if (_position.HasValue)
        {
            properties.Set("SDL.window.create.x", _position.Value.X);
            properties.Set("SDL.window.create.y", _position.Value.Y);
        }

        return properties;
    }

    private static WindowHandle GetWindowHandle(uint properties)
    {
        if (OperatingSystem.IsLinux())
        {
            string? driver = VideoDrivers.Current;

            if (driver == "x11")
            {
                nint display = Properties.Get(properties, "SDL.window.x11.display", nint.Zero);
                int window = Properties.Get(properties, "SDL.window.x11.window", 0);

                return new X11Handle(display, window);
            }

            if (driver == "wayland")
            {
                nint display = Properties.Get(properties, "SDL.window.wayland.display", nint.Zero);
                nint surface = Properties.Get(properties, "SDL.window.wayland.surface", nint.Zero);

                return new WaylandHandle(display, surface);
            }
        }

        nint hwnd = Properties.Get(properties, "SDL.window.win32.hwnd", nint.Zero);
        return new Win32Handle(hwnd);
    }

    private unsafe void Initialize(int width, int height)
    {
        QuackEngine.EnsureInitialized(Subsystem.Video);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

        _width = width;
        _height = height;

        using (Properties properties = BuildWindowProperties())
        {
            NativeHandle = SDL3.CreateWindowWithProperties(properties);
            SDLThrowHelper.ThrowIfNull(NativeHandle);
        }

        Id = SDL3.GetWindowID(NativeHandle);
        SDLThrowHelper.ThrowIfZero(Id);

        Handle = GetWindowHandle(SDL3.GetWindowProperties(NativeHandle));
        Windows.Register(this);

        SDL3.GetWindowSizeInPixels(NativeHandle, out int widthInPixels, out int heightInPixels);
        WidthInPixels = widthInPixels;
        HeightInPixels = heightInPixels;

        ApplyDeferredSettings();
    }

    private void Update(in Event e)
    {
        if (e is WindowCloseRequestedEvent close && close.WindowId == Id)
        {
            Close();
            return;
        }

        if (e is WindowResizedEvent resized && resized.WindowId == Id)
        {
            _width = resized.Size.Width;
            _height = resized.Size.Height;
            return;
        }

        if (e is WindowPixelSizeChangedEvent pixelSize && pixelSize.WindowId == Id)
        {
            WidthInPixels = pixelSize.Size.Width;
            HeightInPixels = pixelSize.Size.Height;
            return;
        }

        if (e is WindowMovedEvent moved && moved.WindowId == Id)
        {
            _position = moved.Position;
            return;
        }

        if (e is WindowExposedEvent exposed && exposed.WindowId == Id)
        {
            Occluded = false;
            return;
        }

        if (e is WindowOccludedEvent occluded && occluded.WindowId == Id)
        {
            Occluded = true;
            return;
        }

        if (e is WindowMouseEnteredEvent mouseEntered && mouseEntered.WindowId == Id)
        {
            HasMouseFocus = true;
            return;
        }

        if (e is WindowMouseLeftEvent mouseLeft && mouseLeft.WindowId == Id)
        {
            HasMouseFocus = false;
            return;
        }

        if (e is WindowFocusGainedEvent focusGained && focusGained.WindowId == Id)
        {
            HasKeyboardFocus = true;
            return;
        }

        if (e is WindowFocusLostEvent focusLost && focusLost.WindowId == Id)
        {
            HasKeyboardFocus = false;
            return;
        }

        if (e is WindowMaximizedEvent maximized && maximized.WindowId == Id)
        {
            SetState(State.Maximized);
            RemoveState(State.Minimized);
            return;
        }

        if (e is WindowMinimizedEvent minimized && minimized.WindowId == Id)
        {
            SetState(State.Minimized);
            RemoveState(State.Maximized);
            return;
        }

        if (e is WindowRestoredEvent restored && restored.WindowId == Id)
        {
            RemoveState(State.Maximized | State.Minimized);
            return;
        }

        if (e is WindowShownEvent shown && shown.WindowId == Id)
        {
            RemoveState(State.Hidden);
            return;
        }

        if (e is WindowHiddenEvent hidden && hidden.WindowId == Id)
        {
            SetState(State.Hidden);
            return;
        }

        if (e is WindowEnteredFullscreenEvent enteredFullscreen && enteredFullscreen.WindowId == Id)
        {
            SetState(State.Fullscreen);
            return;
        }

        if (e is WindowLeftFullscreenEvent leftFullscreen && leftFullscreen.WindowId == Id)
            RemoveState(State.Fullscreen);
    }

    private bool HasState(State state) => (_state & state) == state;

    private void RemoveState(State state) => _state &= ~state;

    private void SetState(State state, bool apply = true) => _state = apply ? _state | state : _state & ~state;

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(unsafe (NativeHandle is null), typeof(Window));

    private void UpdateIcon(Surface icon)
    {
        _icon?.Dispose();
        _icon = icon;

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetWindowIcon(NativeHandle, _icon.Handle)));
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
}

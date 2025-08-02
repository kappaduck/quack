// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Handles;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Windows;

/// <summary>
/// Represents a simple window with no graphics context.
/// </summary>
/// <remarks>
/// You can inherits this class to implement a graphic window using graphics APIs like OpenGL or Vulkan.
/// </remarks>
public partial class Window : IDisposable
{
    private bool _disposed;

    private WindowHandle _handle;
    private WindowState _state;

    /// <summary>
    /// Initializes a new instance of the <see cref="Window"/>.
    /// </summary>
    /// <remarks>
    /// Use <see cref="Create(string, int, int, WindowState)"/> to create at a later time.
    /// </remarks>
    public Window()
    {
        _handle = new WindowHandle();

        Handle = new WindowHandle(_handle);
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Window"/> with the specified title, width, height, and state.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="state">The initial state of the window.</param>
    public Window(string title, int width, int height, WindowState state = WindowState.None)
    {
        _handle = CreateWindow(title, width, height, state);
        IsOpen = true;

        Handle = new WindowHandle(_handle);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is always on top.
    /// </summary>
    /// <remarks>
    /// If you set this property during the creation of an empty <see cref="Window"/> then using <see cref="Create(string, int, int, WindowState)"/>, it will be ignored.
    /// Use <see cref="WindowState"/> parameter instead.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window always on top.</exception>
    public bool AlwaysOnTop
    {
        get => (_state & WindowState.AlwaysOnTop) == WindowState.AlwaysOnTop;
        set
        {
            if (!IsOpen)
                return;

            _state = value ? _state | WindowState.AlwaysOnTop : _state & ~WindowState.AlwaysOnTop;
            QuackNativeException.ThrowIfFailed(SDL_SetWindowAlwaysOnTop(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is borderless.
    /// </summary>
    /// <remarks>
    /// If you set this property during the creation of an empty <see cref="Window"/> then using <see cref="Create(string, int, int, WindowState)"/>, it will be ignored.
    /// Use <see cref="WindowState"/> parameter instead.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window borderless.</exception>
    public bool Borderless
    {
        get => (_state & WindowState.Borderless) == WindowState.Borderless;
        set
        {
            if (!IsOpen)
                return;

            _state = value ? _state | WindowState.Borderless : _state & ~WindowState.Borderless;
            QuackNativeException.ThrowIfFailed(SDL_SetWindowBordered(_handle, !value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is focusable.
    /// </summary>
    /// <remarks>
    /// If you set this property during the creation of an empty <see cref="WindowState"/> then using <see cref="Create(string, int, int, WindowState)"/>, it will be ignored.
    /// Use <see cref="WindowState"/> parameter instead.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window focusable.</exception>
    public bool Focusable
    {
        get => (_state & WindowState.NotFocusable) == WindowState.NotFocusable;
        set
        {
            if (!IsOpen)
                return;

            _state = value ? _state & ~WindowState.NotFocusable : _state | WindowState.NotFocusable;
            QuackNativeException.ThrowIfFailed(SDL_SetWindowFocusable(_handle, value));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is fullscreen.
    /// </summary>
    /// <remarks>
    /// If you set this property during the creation of an empty <see cref="Window"/> then using <see cref="Create(string, int, int, WindowState)"/>, it will be ignored.
    /// Use <see cref="WindowState"/> parameter instead.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while settings the window fullscreen.</exception>
    public bool Fullscreen
    {
        get => (_state & WindowState.Fullscreen) == WindowState.Fullscreen;
        set
        {
            if (!IsOpen)
                return;

            _state = value ? _state | WindowState.Fullscreen : _state & ~WindowState.Fullscreen;

            QuackNativeException.ThrowIfFailed(SDL_SetWindowFullscreen(_handle, value));
        }
    }

    /// <summary>
    /// Gets the window's handle.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This handle is a wrapper around the native window handle and can be used to interact with platform-specific APIs.
    /// </para>
    /// <para>
    /// You do not need to dispose of this handle manually; it is managed by the <see cref="Window"/>.
    /// </para>
    /// </remarks>
    /// <exception cref="ObjectDisposedException">The window has been disposed.</exception>
    public SafeHandle Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, nameof(Window));
            return field;
        }
        private set;
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
    /// Gets a value indicating whether the window is hidden.
    /// </summary>
    public bool Hidden => (_state & WindowState.Hidden) == WindowState.Hidden;

    /// <summary>
    /// Gets a value indicating whether the window uses high pixel density.
    /// </summary>
    public bool HighPixelDensity => (_state & WindowState.HighPixelDensity) == WindowState.HighPixelDensity;

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
    public bool IsOpen
    {
        get => !_handle.IsInvalid && field;
        private set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window has grabbed keyboard input.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If you set this property during the creation of an empty <see cref="Window"/> then using <see cref="Create(string, int, int, WindowState)"/>, it will be ignored.
    /// Use <see cref="WindowState"/> parameter instead.
    /// </para>
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
            if (!IsOpen)
                return;

            _state = value ? _state | WindowState.KeyboardGrabbed : _state & ~WindowState.KeyboardGrabbed;

            QuackNativeException.ThrowIfFailed(SDL_SetWindowKeyboardGrab(_handle, value));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the window is maximized.
    /// </summary>
    public bool Maximized => (_state & WindowState.Maximized) == WindowState.Maximized;

    /// <summary>
    /// Gets a value indicating whether the window is minimized.
    /// </summary>
    public bool Minimized => (_state & WindowState.Minimized) == WindowState.Minimized;

    /// <summary>
    /// Gets a value indicating whether the window has captured mouse input.
    /// </summary>
    /// <remarks>
    /// It is not related to <see cref="MouseGrabbed"/> (<see cref="WindowState.MouseGrabbed"/>).
    /// </remarks>
    public bool MouseCaptured => (_state & WindowState.MouseCapture) == WindowState.MouseCapture;

    /// <summary>
    /// Gets or sets a value indicating whether the window has mouse input grabbed.
    /// </summary>
    /// <remarks>
    /// If you set this property during the creation of an empty <see cref="Window"/> then using <see cref="Create(string, int, int, WindowState)"/>, it will be ignored.
    /// Use <see cref="WindowState"/> parameter instead.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window mouse grab.</exception>
    public bool MouseGrabbed
    {
        get => (_state & WindowState.MouseGrabbed) == WindowState.MouseGrabbed;
        set
        {
            if (!IsOpen)
                return;

            _state = value ? _state | WindowState.MouseGrabbed : _state & ~WindowState.MouseGrabbed;

            QuackNativeException.ThrowIfFailed(SDL_SetWindowMouseGrab(_handle, value));
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
    /// Gets or sets a value indicating whether the window is resizable.
    /// </summary>
    /// <remarks>
    /// If you set this property during the creation of an empty <see cref="Window"/> then using <see cref="Create(string, int, int, WindowState)"/>, it will be ignored.
    /// Use <see cref="WindowState"/> parameter instead.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while setting the window resizable.</exception>
    public bool Resizable
    {
        get => (_state & WindowState.Resizable) == WindowState.Resizable;
        set
        {
            if (!IsOpen)
                return;

            _state = value ? _state | WindowState.Resizable : _state & ~WindowState.Resizable;

            QuackNativeException.ThrowIfFailed(SDL_SetWindowResizable(_handle, value));
        }
    }

    /// <summary>
    /// Closes the window.
    /// </summary>
    /// <remarks>
    /// If the window is already closed or not created, It does nothing.
    /// </remarks>
    public void Close()
    {
        if (!IsOpen)
            return;

        IsOpen = false;
    }

    /// <summary>
    /// Creates the window.
    /// </summary>
    /// <remarks>
    /// If the window is already open, it does nothing.
    /// </remarks>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <param name="state">The initial state of the window.</param>
    /// <exception cref="QuackNativeException">An error occurred while creating the window.</exception>
    public void Create(string title, int width, int height, WindowState state = WindowState.None)
    {
        if (IsOpen)
            return;

        _handle = CreateWindow(title, width, height, state);
        Handle = new WindowHandle(_handle);

        IsOpen = true;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Hides the window. It can be shown again with <see cref="Show"/>.
    /// </summary>
    /// <exception cref="QuackNativeException">An error occurred while hiding the window.</exception>
    public void Hide()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL_HideWindow(_handle));

        _state |= WindowState.Hidden;
    }

    /// <summary>
    /// Request that the window be made as large as possible.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Non-resizable windows can't be maximized. The window must have the <see cref="WindowState.Resizable"/> state set.
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

        QuackNativeException.ThrowIfFailed(SDL_MaximizeWindow(_handle));

        _state &= ~WindowState.Minimized;
        _state |= WindowState.Maximized;
    }

    /// <summary>
    /// Request that the window be minimized to an iconic representation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the window is in <see cref="WindowState.Fullscreen"/> state, it will has no direct effect.
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

        QuackNativeException.ThrowIfFailed(SDL_MinimizeWindow(_handle));

        _state &= ~WindowState.Maximized;
        _state |= WindowState.Minimized;
    }

    /// <summary>
    /// Request that the window be raised above other windows and gain the input focus.
    /// </summary>
    /// <remarks>
    /// The result of this request is subject to desktop window manager policy, particularly if raising
    /// the requested window would result in stealing focus from another application.
    /// If the window is successfully raised and gains input focus,
    /// an <see cref="EventType.FocusGained"/> event will be emitted,
    /// and the window will have <see cref="WindowState.InputFocus"/> state set.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while raising the window.</exception>
    public void Raise()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL_RaiseWindow(_handle));

        _state |= WindowState.InputFocus;
    }

    /// <summary>
    /// Request that the size and position of a minimized or maximized window be restored.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the window is in <see cref="WindowState.Fullscreen"/> state, it will has no direct effect.
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

        QuackNativeException.ThrowIfFailed(SDL_RestoreWindow(_handle));
    }

    /// <summary>
    /// Show the window.
    /// </summary>
    /// <remarks>
    /// It's only the way to show a window that has been hidden
    /// with <see cref="Hide"/> or using <see cref="WindowState.Hidden"/> state.
    /// </remarks>
    /// <exception cref="QuackNativeException">An error occurred while showing the window.</exception>
    public void Show()
    {
        if (!IsOpen)
            return;

        QuackNativeException.ThrowIfFailed(SDL_ShowWindow(_handle));

        _state &= ~WindowState.Hidden;
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

        QuackNativeException.ThrowIfFailed(SDL_SyncWindow(_handle));
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="Window"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            _handle.Dispose();

        _disposed = true;
    }

    private WindowHandle CreateWindow(string title, int width, int height, WindowState state)
    {
        WindowHandle handle = SDL_CreateWindow(title, width, height, state);
        QuackNativeException.ThrowIf(handle.IsInvalid);

        Id = SDL_GetWindowID(handle);
        QuackNativeException.ThrowIfZero(Id);

        _state = state;

        return handle;
    }
}

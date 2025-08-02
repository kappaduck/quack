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
    /// Gets the window's handle.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This handle is a wrapper around the native window handle and can be used to interact with platform-specific APIs.
    /// </para>
    /// <para>
    /// You do not need to dispose of this handle manually; it is managed by the <see cref="Window"/> class.
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

        return handle;
    }
}

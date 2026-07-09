// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// The CPU-accessible pixel surface backing a <see cref="Window"/>, for drawing without a GPU.
/// </summary>
/// <remarks>
/// <para>
/// A window can have either a <see cref="Window.Renderer"/> or a <see cref="WindowSurface"/> at a time,
/// creating one while the other exists throws.
/// </para>
/// <para>
/// Use <see cref="Renderer(WindowSurface)"/> to draw shapes, sprites and textures onto
/// it with the same API as a hardware-accelerated <see cref="Renderer"/>. The underlying surface is
/// invalidated whenever the window is resized; <see cref="Surface"/> reacquires it automatically.
/// Be careful if you using <see cref="Texture"/> because after the window is resized,
/// the texture is no longer available and will throws.
/// </para>
/// </remarks>
public sealed class WindowSurface : IDisposable
{
    private readonly Window _window;
    private Surface? _surface;
    private Size _size;

    /// <summary>
    /// Acquires the pixel surface of <paramref name="window"/>.
    /// </summary>
    /// <param name="window">The window to draw into.</param>
    /// <exception cref="InvalidOperationException">The window already has a renderer or a surface.</exception>
    /// <exception cref="QuackInteropException">Failed to acquire the surface.</exception>
    public WindowSurface(Window window)
    {
        window.Bind(this);

        try
        {
            _surface = Acquire(window);
        }
        catch
        {
            window.Unbind(this);
            throw;
        }

        _window = window;
        _size = window.SizeInPixels;
    }

    /// <summary>
    /// Gets or sets the vertical synchronization interval used when presenting this surface with
    /// <see cref="Update()"/> or <see cref="Update(ReadOnlySpan{RectI})"/>.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="Graphics.Rendering.VSync"/> constants: <see cref="VSync.Disabled"/> presents immediately,
    /// and a positive number N synchronizes with every Nth vertical refresh. Not every value is supported on every
    /// platform; read this back after setting it to see what was actually applied.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to get or set the vertical synchronization interval.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public int VSync
    {
        get
        {
            ThrowIfDisposed();

            SDLThrowHelper.ThrowIfFailed(SDL3.GetWindowSurfaceVSync(_window.NativeHandle, out int vsync));
            return vsync;
        }
        set
        {
            ThrowIfDisposed();
            SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowSurfaceVSync(_window.NativeHandle, value));
        }
    }

    /// <summary>
    /// Gets the current pixel surface, reacquiring it first if the window was resized since the last access.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to reacquire the surface after a resize.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public Surface Surface
    {
        get
        {
            ThrowIfDisposed();
            Refresh();

            return _surface!;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_surface is null)
            return;

        _surface.Dispose();
        _surface = null;

        SDL3.DestroyWindowSurface(_window.NativeHandle);
        _window.Unbind(this);
    }

    /// <summary>
    /// Copies the entire surface to the screen.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to update the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void Update()
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(SDL3.UpdateWindowSurface(_window.NativeHandle));
    }

    /// <summary>
    /// Copies the given areas of the surface to the screen.
    /// </summary>
    /// <remarks>Prefer this over <see cref="Update()"/> when only small regions changed; it can be considerably faster.</remarks>
    /// <param name="rects">The areas to copy, in pixels.</param>
    /// <exception cref="QuackInteropException">Failed to update the surface.</exception>
    /// <exception cref="ObjectDisposedException">The surface is disposed.</exception>
    public void Update(ReadOnlySpan<RectI> rects)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(SDL3.UpdateWindowSurfaceRects(_window.NativeHandle, rects, rects.Length));
    }

    private static Surface Acquire(Window window)
    {
        SDL_Surface* surface = SDL3.GetWindowSurface(window.NativeHandle);
        SDLThrowHelper.ThrowIfNull(surface);

        return new Surface(surface, owned: false);
    }

    private void Refresh()
    {
        if (_window.SizeInPixels == _size)
            return;

        _surface!.Dispose();
        _surface = Acquire(_window);
        _size = _window.SizeInPixels;
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_surface is null, typeof(WindowSurface));
}

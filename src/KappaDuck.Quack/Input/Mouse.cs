// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Input;

/// <summary>
/// Represents a mouse input.
/// </summary>
public static class Mouse
{
    private static MouseMotionTransform? _transform;

    /// <summary>
    /// Gets the asynchronous mouse button state and the desktop-relative platform-cursor position of the mouse.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It immediately queries the platform for the current mouse state, more costly than using <see cref="State"/>.
    /// </para>
    /// <para>
    /// In relative mode, the platform-cursor's position usually contradicts the engine-cursor's position as
    /// manually calculated from <see cref="State"/> and window's position.
    /// </para>
    /// </remarks>
    public static MouseState GlobalState
    {
        get
        {
            MouseButtonState buttons = SDL3.GetGlobalMouseState(out float x, out float y);
            return new MouseState(buttons, new PointF(x, y));
        }
    }

    /// <summary>
    /// Gets the window-relative position of the cursor, from <see cref="State"/>.
    /// </summary>
    public static PointF Position => State.Position;

    /// <summary>
    /// Gets the mouse movement accumulated since the previous call.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The movement comes from the engine cache, based on the last pump of the event queue.
    /// To query the platform for immediate mouse state, use <see cref="GlobalState"/>.
    /// </para>
    /// <para>
    /// Each call consumes the accumulator, so an immediate second call returns near-zero. Read it once per frame.
    /// </para>
    /// <para>
    /// It is useful for reducing overhead by processing relative mouse inputs in one go per-frame
    /// instead of individually per-event, at the expense of losing the order between events within the frame
    /// (e.g. quickly pressing and releasing a button within the same frame).
    /// </para>
    /// </remarks>
    public static Vector2 RelativeMotion
    {
        get
        {
            _ = SDL3.GetRelativeMouseState(out float x, out float y);
            return new Vector2(x, y);
        }
    }

    /// <summary>
    /// Gets the engine cache for synchronous mouse button state and the window-relative position of the mouse.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The cache is based on the last pump of the event queue. To query the
    /// platform for immediate mouse state, use <see cref="GlobalState"/>.
    /// </para>
    /// <para>
    /// In relative mode, the platform-cursor's position usually contradicts the engine-cursor's position as
    /// manually calculated from <see cref="State"/> and window's position.
    /// </para>
    /// </remarks>
    public static MouseState State
    {
        get
        {
            MouseButtonState buttons = SDL3.GetMouseState(out float x, out float y);
            return new MouseState(buttons, new PointF(x, y));
        }
    }

    /// <summary>
    /// Capture the mouse and to track input outside the window.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Capturing enables your app to obtain mouse events globally, instead of just within your window.
    /// Not all video targets support this feature. When capturing is enabled, the current window will get all mouse
    /// events, but unlike relative mode, no change is made to the cursor and it is not restrained to your window.
    /// </para>
    /// <para>
    /// This method may also deny mouse input to other windows, both those in your application and others on
    /// the system, so you should use this method sparingly and in small bursts. For example, you might want to track
    /// the mouse while the user is dragging something, until the user releases a mouse button.
    /// It is not recommended that you capture the mouse for long periods of time, such as the entire time
    /// your app is running. For that, consider using <see cref="Window.MouseRelativeMode"/> or <see cref="Window.MouseGrabbed"/>, depending on your needs.
    /// </para>
    /// <para>
    /// While captured, mouse events still report coordinates relative to the current (foreground) window,
    /// but those coordinates may be outside the bounds of the window (including negative values).
    /// Capturing is only allowed for the foreground window. If the window loses focus while capturing,
    /// the capture will be disabled automatically.
    /// </para>
    /// <para>
    /// While capturing is enabled, the current window will have the <see cref="Window.MouseCaptured"/> set to <see langword="true"/>.
    /// </para>
    /// <para>
    /// Please note that the engine will attempt to "auto capture" the mouse while the user is pressing a button;
    /// this is to try and make mouse behavior more consistent between platforms, and deal with the common case of
    /// a user dragging the mouse outside of the window. This means that if you are calling this method only to
    /// deal with this situation, you do not have to (although it is safe to do so).
    /// </para>
    /// </remarks>
    /// <param name="enabled">value indicating whether to enable or disable mouse capture.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to set mouse capture.</exception>
    public static void Capture(bool enabled) => SDLThrowHelper.ThrowIfFailed(SDL3.CaptureMouse(enabled));

    /// <summary>
    /// Determines whether the specified button is currently pressed in <see cref="State"/>.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns><see langword="true"/> if the button is pressed; otherwise, <see langword="false"/>.</returns>
    public static bool IsDown(MouseButton button) => State.IsDown(button);

    /// <summary>
    /// Determines whether the specified button is currently released in <see cref="State"/>.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns><see langword="true"/> if the button is released; otherwise, <see langword="false"/>.</returns>
    public static bool IsUp(MouseButton button) => State.IsUp(button);

    /// <summary>
    /// Changes the active cursor.
    /// </summary>
    /// <param name="cursor">The new active cursor to set</param>
    public static void SetCursor(Cursor cursor)
    {
        unsafe
        {
            SDL3.SetCursor(cursor.Handle);
        }
    }

    /// <summary>
    /// Installs a transform applied to all relative mouse motion, replacing any previous transform.
    /// </summary>
    /// <remarks>
    /// <para>The transform applies process-wide, not to a single window; the window each motion targets is passed to the delegate.</para>
    /// <para>
    /// The transform may be invoked on a separate, high-priority thread, so keep it fast and avoid heavy work or
    /// long-held locks; stalling it can affect the whole system. Pass <see langword="null"/> to remove the current transform.
    /// </para>
    /// </remarks>
    /// <param name="transform">The transform to apply, or <see langword="null"/> to clear it.</param>
    /// <exception cref="QuackInteropException">Failed to install the transform.</exception>
    public static void SetRelativeMotionTransform(MouseMotionTransform? transform)
    {
        _transform = transform;

        if (transform is null)
        {
            SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetRelativeMouseTransform(null, null)));
            return;
        }

        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3.SetRelativeMouseTransform(&OnTransform, null)));
    }

    /// <summary>
    /// Reset the active cursor to <see cref="Cursor.Default"/>
    /// </summary>
    public static void ResetCursor() => SetCursor(Cursor.Default);

    /// <summary>
    /// Moves the mouse cursor to the given position in global screen space.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="MouseMovedEvent"/> event.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="x">The x-coordinate in global screen space.</param>
    /// <param name="y">The y-coordinate in global screen space.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to warp the mouse.</exception>
    public static void Warp(float x, float y)
        => SDLThrowHelper.ThrowIfFailed(SDL3.WarpMouseGlobal(x, y));

    /// <summary>
    /// Moves the mouse cursor to the given position in global screen space.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="MouseMovedEvent"/> event.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="position">The position in global screen space.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to warp the mouse.</exception>
    public static void Warp(PointF position) => Warp(position.X, position.Y);

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnTransform(void* data, ulong timestamp, SDL_Window* window, uint mouseId, float* x, float* y)
    {
        MouseMotionTransform? transform = _transform;

        if (transform is null)
            return;

        unsafe
        {
            float dx = *x;
            float dy = *y;

            transform(WindowManager.FromHandle(window), mouseId, ref dx, ref dy);

            *x = dx;
            *y = dy;
        }
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Input.Mouse;

/// <summary>
/// Represents a mouse input.
/// </summary>
public static class Mouse
{
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
            MouseButtonState buttons = SDL3.Input.GetGlobalMouseState(out float x, out float y);
            return new MouseState(buttons, new Vector2(x, y));
        }
    }

    /// <summary>
    /// Gets the engine cache for synchronous mouse button state and the relative movement of the mouse since the last query.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The cache is based on the last pump of the event queue. To query the
    /// platform for immediate mouse state, use <see cref="GlobalState"/>.
    /// </para>
    /// <para>
    /// It is useful for reducing overhead by processing relative mouse inputs in one go per-frame
    /// instead of individually per-event, at the expense of losing the order between events within the frame
    /// (e.g. quickly pressing and releasing a button within the same frame).
    /// </para>
    /// </remarks>
    public static MouseState RelativeState
    {
        get
        {
            MouseButtonState buttons = SDL3.Input.GetRelativeMouseState(out float x, out float y);
            return new MouseState(buttons, new Vector2(x, y));
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
            MouseButtonState buttons = SDL3.Input.GetMouseState(out float x, out float y);
            return new MouseState(buttons, new Vector2(x, y));
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
    /// Please note that the engine will attemp to "auto capture" the mouse while the user is pressing a button;
    /// this is to try and make mouse behavior more consistent between platforms, and deal with the common case of
    /// a user dragging the mouse outside of the window. This means that if you are calling this method only to
    /// deal with this situation, you do not have to (although it is safe to do so).
    /// </para>
    /// </remarks>
    /// <param name="enabled">value indicating whether to enable or disable mouse capture.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to set mouse capture.</exception>
    public static void Capture(bool enabled) => QuackInteropException.ThrowIfFailed(SDL3.Input.CaptureMouse(enabled));

    /// <summary>
    /// Determines whether the specified button is currently pressed in <see cref="State"/>
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns><see langword="true"/> if the button is pressed; otherwise, <see langword="false"/>.</returns>
    public static bool IsDown(MouseButton button) => State.IsDown(button);

    /// <summary>
    /// Moves the mouse cursor to the given position in global screen space.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="x">The x-coordinate in global screen space.</param>
    /// <param name="y">The y-coordinate in global screen space.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to warp the mouse.</exception>
    public static void Warp(float x, float y)
        => QuackInteropException.ThrowIfFailed(SDL3.Input.WarpMouseGlobal(x, y));

    /// <summary>
    /// Moves the mouse cursor to the given position in global screen space.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="position">The position in global screen space.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to warp the mouse.</exception>
    public static void Warp(Vector2 position) => Warp(position.X, position.Y);
}

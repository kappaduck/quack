// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Inputs;

/// <summary>
/// Represents a mouse input device.
/// </summary>
public sealed class Mouse
{
    internal Mouse(uint id)
    {
        Id = id;
        Name = Native.SDL_GetMouseNameForID(id);
    }

    /// <summary>
    /// Gets the unique mouse identifier.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets the name of the mouse.
    /// </summary>
    public string Name { get; }

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
    public static CachedState GlobalState
    {
        get
        {
            MouseButtonState buttons = Native.SDL_GetGlobalMouseState(out float x, out float y);
            return new CachedState(buttons, new Vector2(x, y));
        }
    }

    /// <summary>
    /// Gets a value indicating whether a mouse is connected.
    /// </summary>
    public static bool HasMouse => Native.SDL_HasMouse();

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
    public static CachedState RelativeState
    {
        get
        {
            MouseButtonState buttons = Native.SDL_GetRelativeMouseState(out float x, out float y);
            return new CachedState(buttons, new Vector2(x, y));
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
    public static CachedState State
    {
        get
        {
            MouseButtonState buttons = Native.SDL_GetMouseState(out float x, out float y);
            return new CachedState(buttons, new Vector2(x, y));
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
    /// your app is running. For that, consider using <see cref="WindowBase.MouseRelativeMode"/> or <see cref="WindowBase.MouseGrabbed"/>, depending on your needs.
    /// </para>
    /// <para>
    /// While captured, mouse events still report coordinates relative to the current (foreground) window,
    /// but those coordinates may be outside the bounds of the window (including negative values).
    /// Capturing is only allowed for the foreground window. If the window loses focus while capturing,
    /// the capture will be disabled automatically.
    /// </para>
    /// <para>
    /// While capturing is enabled, the current window will have the <see cref="WindowBase.MouseCaptured"/> set to <see langword="true"/>.
    /// </para>
    /// <para>
    /// Please note that the engine will attemp to "auto capture" the mouse while the user is pressing a button;
    /// this is to try and make mouse behavior more consistent between platforms, and deal with the common case of
    /// a user dragging the mouse outside of the window. This means that if you are calling this method only to
    /// deal with this situation, you do not have to (although it is safe to do so).
    /// </para>
    /// </remarks>
    /// <param name="enabled">value indicating whether to enable or disable mouse capture.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to set mouse capture.</exception>
    public static void Capture(bool enabled) => QuackNativeException.ThrowIfFailed(Native.SDL_CaptureMouse(enabled));

    /// <summary>
    /// Retrieves a mouse device with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the mouse.</param>
    /// <returns>The mouse with the specified identifier.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="id"/> is negative or zero.</exception>
    public static Mouse Get(uint id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        return new Mouse(id);
    }

    /// <summary>
    /// Retrieves all connected mice.
    /// </summary>
    /// <remarks>
    /// It will include any device or virtual driver that provides mouse functionality,
    /// including some game controllers, KVM switches, etc. You should wait for input from
    /// a device before you consider it actively in use.
    /// </remarks>
    /// <returns>All connected mice.</returns>
    public static Mouse[] GetMice()
    {
        ReadOnlySpan<uint> ids = Native.SDL_GetMice(out _);

        if (ids.IsEmpty)
            return [];

        Mouse[] mice = new Mouse[ids.Length];

        for (int i = 0; i < ids.Length; i++)
            mice[i] = new Mouse(ids[i]);

        return mice;
    }

    /// <summary>
    /// Determines whether the specified button is currently pressed in the cached state.
    /// </summary>
    /// <remarks>It uses the cached <see cref="State"/> to determine the button state.</remarks>
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
    /// <exception cref="QuackNativeException">Thrown when failed to warp the mouse.</exception>
    public static void Warp(float x, float y)
        => QuackNativeException.ThrowIfFailed(Native.SDL_WarpMouseGlobal(x, y));

    /// <summary>
    /// Moves the mouse cursor to the given position in global screen space.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="position">The position in global screen space.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to warp the mouse.</exception>
    public static void Warp(Vector2 position) => Warp(position.X, position.Y);

    /// <summary>
    /// The cached state of the mouse.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public readonly struct CachedState
    {
        internal CachedState(MouseButtonState buttons, Vector2 position)
        {
            Buttons = buttons;
            Position = position;
        }

        /// <summary>
        /// Gets the state of the mouse buttons.
        /// </summary>
        public MouseButtonState Buttons { get; }

        /// <summary>
        /// Gets the position of the mouse.
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// Determines whether the specified button is currently pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns><see langword="true"/> if the button is pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsDown(MouseButton button) => (Buttons & (MouseButtonState)(1 << ((int)button - 1))) != MouseButtonState.None;
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;

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
            ButtonState buttons = Native.SDL_GetGlobalMouseState(out float x, out float y);
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
            ButtonState buttons = Native.SDL_GetRelativeMouseState(out float x, out float y);
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
            ButtonState buttons = Native.SDL_GetMouseState(out float x, out float y);
            return new CachedState(buttons, new Vector2(x, y));
        }
    }

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
    public static bool IsDown(Button button) => State.IsDown(button);

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
        internal CachedState(ButtonState buttons, Vector2 position)
        {
            Buttons = buttons;
            Position = position;
        }

        /// <summary>
        /// Gets the state of the mouse buttons.
        /// </summary>
        public ButtonState Buttons { get; }

        /// <summary>
        /// Gets the position of the mouse.
        /// </summary>
        public Vector2 Position { get; }

        /// <summary>
        /// Determines whether the specified button is currently pressed.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns><see langword="true"/> if the button is pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsDown(Button button) => (Buttons & (ButtonState)(1 << ((int)button - 1))) != ButtonState.None;
    }

    /// <summary>
    /// Represents a mouse button.
    /// </summary>
    public enum Button : byte
    {
        /// <summary>
        /// The left mouse button.
        /// </summary>
        Left = 1,

        /// <summary>
        /// The middle mouse button.
        /// </summary>
        Middle = 2,

        /// <summary>
        /// The right mouse button.
        /// </summary>
        Right = 3,

        /// <summary>
        /// The first extra mouse button.
        /// </summary>
        /// <remarks>
        /// Generally is the side mouse button.
        /// </remarks>
        X1 = 4,

        /// <summary>
        /// The second extra mouse button.
        /// </summary>
        /// <remarks>
        /// Generally is the side mouse button.
        /// </remarks>
        X2 = 5
    }

    /// <summary>
    /// Represents the state of the mouse buttons.
    /// </summary>
    /// <remarks>
    /// It is a mask of the current button state.
    /// </remarks>
    [Flags]
    public enum ButtonState : uint
    {
        /// <summary>
        /// No mouse button.
        /// </summary>
        None = 0,

        /// <summary>
        /// The left mouse button.
        /// </summary>
        Left = 0x1,

        /// <summary>
        /// The middle mouse button.
        /// </summary>
        Middle = 0x2,

        /// <summary>
        /// The right mouse button.
        /// </summary>
        Right = 0x4,

        /// <summary>
        /// The first extra mouse button.
        /// </summary>
        /// <remarks>
        /// Generally is the side mouse button.
        /// </remarks>
        X1 = 0x08,

        /// <summary>
        /// The second extra mouse button.
        /// </summary>
        /// <remarks>
        /// Generally is the side mouse button.
        /// </remarks>
        X2 = 0x10
    }

    /// <summary>
    /// Represents a mouse wheel direction.
    /// </summary>
    public enum WheelDirection
    {
        /// <summary>
        /// The scroll direction is normal.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// The scroll direction is flipped.
        /// </summary>
        Flipped = 1
    }
}

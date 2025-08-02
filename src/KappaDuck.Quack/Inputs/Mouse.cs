// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Marshallers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Inputs;

/// <summary>
/// A mouse input.
/// </summary>
public sealed partial class Mouse
{
    internal Mouse(uint id)
    {
        Id = id;
        Name = SDL_GetMouseNameForID(id);
    }

    /// <summary>
    /// Gets engine cache for the synchronous mouse button state and the window-relative engine-cursor position.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It returns the cached synchronous state as engine understands it from the last pump of the event queue.
    /// </para>
    /// <para>To query the platform for immediate asynchronous state, use <see cref="GlobalState"/>.</para>
    /// <para>
    /// In relative mode, the platform-cursor's position usually contradicts the engine-cursor's position as
    /// manually calculated from <see cref="CachedState"/> and window's position.
    /// </para>
    /// </remarks>
    public static State CachedState
    {
        get
        {
            ButtonState buttons = SDL_GetMouseState(out float x, out float y);
            return new State(buttons, new Vector2(x, y));
        }
    }

    /// <summary>
    /// Gets the asynchronous mouse button state and the desktop-relative platform-cursor position.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It immediately queries the platform for the most recent asynchronous state,
    /// more costly than retrieving <see cref="CachedState"/>.
    /// </para>
    /// <para>
    /// In relative mode, the platform-cursor's position usually contradicts the engine-cursor's position as
    /// manually calculated from <see cref="CachedState"/> and window's position.
    /// </para>
    /// </remarks>
    public static State GlobalState
    {
        get
        {
            ButtonState buttons = SDL_GetGlobalMouseState(out float x, out float y);
            return new State(buttons, new Vector2(x, y));
        }
    }

    /// <summary>
    /// Gets a value indicating whether a mouse is currently connected.
    /// </summary>
    public static bool HasMouse => SDL_HasMouse();

    /// <summary>
    /// Gets the instance id of the mouse.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets the name of the mouse.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets engine's cache for the synchronous mouse button state and accumulated mouse delta since last call.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It returns the cached synchronous state as engine understands it from the last pump of the event queue.
    /// </para>
    /// <para>To query the platform for immediate asynchronous state, use <see cref="GlobalState"/>.</para>
    /// <para>
    /// It is useful for reducing overhead by processing relative mouse inputs in one go per-frame
    /// instead of individually per-event, at the expense of losing the order between events within the frame
    /// (e.g. quickly pressing and releasing a button within the same frame).
    /// </para>
    /// </remarks>
    public static State RelativeState
    {
        get
        {
            ButtonState buttons = SDL_GetRelativeMouseState(out float x, out float y);
            return new State(buttons, new Vector2(x, y));
        }
    }

    /// <summary>
    /// Get a list of currently connected mice.
    /// </summary>
    /// <remarks>
    /// This will include any device or virtual driver that includes mouse functionality,
    /// including some game controllers, KVM switches, etc. You should wait for input from a device
    /// before you consider it actively in use.
    /// </remarks>
    /// <returns>The list of connected mice.</returns>
    public static Mouse[] GetMice()
    {
        ReadOnlySpan<uint> ids = SDL_GetMice(out _);

        if (ids.IsEmpty)
            return [];

        Mouse[] mice = new Mouse[ids.Length];

        for (int i = 0; i < ids.Length; i++)
            mice[i] = new Mouse(ids[i]);

        return mice;
    }

    /// <summary>
    /// Move the mouse to the given position in global screen space.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="x">The x-coordinate in global screen space.</param>
    /// <param name="y">The y-coordinate in global screen space.</param>
    /// <exception cref="QuackNativeException">An error occurred while moving the mouse.</exception>
    public static void Warp(float x, float y)
        => QuackNativeException.ThrowIfFailed(SDL_WarpMouseGlobal(x, y));

    /// <summary>
    /// Move the mouse to the given position in global screen space.
    /// </summary>
    /// <remarks>
    /// <para>It generates a <see cref="EventType.MouseMotion"/> event.</para>
    /// <para>It will not move the mouse when used over Microsoft Remote Desktop.</para>
    /// </remarks>
    /// <param name="position">The position in global screen space.</param>
    /// <exception cref="QuackNativeException">An error occurred while moving the mouse.</exception>
    public static void Warp(Vector2 position) => Warp(position.X, position.Y);

    /// <summary>
    /// Represents the state of the mouse.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public readonly struct State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> struct.
        /// </summary>
        /// <param name="buttons">The state of the mouse buttons.</param>
        /// <param name="position">The current position of the mouse.</param>
        internal State(ButtonState buttons, Vector2 position)
        {
            Buttons = buttons;
            Position = position;
        }

        /// <summary>
        /// Gets the state of the mouse buttons.
        /// </summary>
        public readonly ButtonState Buttons { get; }

        /// <summary>
        /// Gets the current position of the mouse.
        /// </summary>
        public readonly Vector2 Position { get; }

        /// <summary>
        /// Gets a value indicating whether the button is pressed.
        /// </summary>
        /// <param name="button">The mouse button.</param>
        /// <returns><see langword="true"/> if the button is pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsButtonDown(Button button) => (Buttons & (ButtonState)(1 << ((int)button - 1))) != ButtonState.None;
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

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ButtonState SDL_GetGlobalMouseState(out float x, out float y);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedArrayMarshaller<,>), CountElementName = "length")]
    private static partial Span<uint> SDL_GetMice(out int length);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ButtonState SDL_GetMouseState(out float x, out float y);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ButtonState SDL_GetRelativeMouseState(out float x, out float y);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    private static partial string SDL_GetMouseNameForID(uint mouse);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_HasMouse();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_WarpMouseGlobal(float x, float y);
}

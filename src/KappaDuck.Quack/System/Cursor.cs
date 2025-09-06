// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Handles;

namespace KappaDuck.Quack.System;

/// <summary>
/// Represents a cursor.
/// </summary>
public sealed class Cursor : IDisposable
{
    private readonly SDL_CursorHandle _handle;
    private static Cursor? _cursor;

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="pixels">The pixel data for the cursor.</param>
    /// <param name="mask">The mask data for the cursor.</param>
    /// <param name="width">The width of the cursor.</param>
    /// <param name="height">The height of the cursor.</param>
    /// <param name="hotSpotX">the x-axis offset from the left of the cursor image to the mouse x position, in the range of 0 to <paramref name="width"/> - 1.</param>
    /// <param name="hotSpotY">The y-axis offset from the top of the cursor image to the mouse y position, in the range of 0 to <paramref name="height"/> - 1.</param>
    public Cursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, int width, int height, int hotSpotX, int hotSpotY)
        => _handle = SDL.System.SDL_CreateCursor(pixels, mask, width, height, hotSpotX, hotSpotY);

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="pixels">The pixel data for the cursor.</param>
    /// <param name="mask">The mask data for the cursor.</param>
    /// <param name="width">The width of the cursor.</param>
    /// <param name="height">The height of the cursor.</param>
    /// <param name="hotSpot">The hot spot of the cursor.</param>
    public Cursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, int width, int height, Vector2Int hotSpot)
        => _handle = SDL.System.SDL_CreateCursor(pixels, mask, width, height, hotSpot.X, hotSpot.Y);

    /// <summary>
    /// Initializes a system cursor.
    /// </summary>
    /// <param name="type">The type of the cursor.</param>
    public Cursor(Type type) => _handle = SDL.System.SDL_CreateSystemCursor(type);

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="surface">The surface to use for the cursor.</param>
    /// <param name="hotSpotX">The x-coordinate of the cursor's hot spot.</param>
    /// <param name="hotSpotY">The y-coordinate of the cursor's hot spot.</param>
    public unsafe Cursor(Surface surface, int hotSpotX, int hotSpotY)
        => _handle = SDL.System.SDL_CreateColorCursor(surface.Handle, hotSpotX, hotSpotY);

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="surface">The surface to use for the cursor.</param>
    /// <param name="hotSpot">The hot spot of the cursor.</param>
    public Cursor(Surface surface, Vector2Int hotSpot) : this(surface, hotSpot.X, hotSpot.Y)
    {
    }

    private Cursor(SDL_CursorHandle handle) => _handle = handle;

    /// <summary>
    /// Gets the current cursor.
    /// </summary>
    public static Cursor Current => _cursor ?? new Cursor(SDL.System.SDL_GetDefaultCursor());

    /// <summary>
    /// Gets a value indicating whether the cursor is visible.
    /// </summary>
    public bool Visible
    {
        get => field;
        set
        {
            if (field == value)
                return;

            field = value;
            if (field)
            {
                SDL.System.SDL_ShowCursor();
                return;
            }

            SDL.System.SDL_HideCursor();
        }
    }

    /// <summary>
    /// Releases the resources used by the cursor.
    /// </summary>
    public void Dispose() => _handle.Dispose();

    /// <summary>
    /// Sets the current cursor.
    /// </summary>
    /// <param name="cursor">The cursor to set as current.</param>
    public static void Set(Cursor cursor)
    {
        _cursor = cursor;
        SDL.System.SDL_SetCursor(_cursor._handle);
    }

    /// <summary>
    /// Cursor types.
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// Default cursor. Usually an arrow.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Text selection. Usually an I-beam.
        /// </summary>
        Text = 1,

        /// <summary>
        /// Wait. Usually an hourglass or watch or spinning ball.
        /// </summary>
        Wait = 2,

        /// <summary>
        /// Crosshair.
        /// </summary>
        Crosshair = 3,

        /// <summary>
        /// Program is busy but still interactive. Usually it's WAIT with an arrow.
        /// </summary>
        Progress = 4,

        /// <summary>
        /// Double arrow pointing northwest and southeast.
        /// </summary>
        DoubleResizeNorthWestSouthEast = 5,

        /// <summary>
        /// Double arrow pointing northeast and southwest.
        /// </summary>
        DoubleResizeNorthEastSouthWest = 6,

        /// <summary>
        /// Double arrow pointing west and east.
        /// </summary>
        DoubleResizeEastWest = 7,

        /// <summary>
        /// Double arrow pointing north and south.
        /// </summary>
        DoubleResizeNorthSouth = 8,

        /// <summary>
        /// Four pointed arrow pointing north, south, east, and west.
        /// </summary>
        Move = 9,
        /// <summary>
        /// Not permitted. Usually a slashed circle or crossbones.
        /// </summary>
        NotAllowed = 10,

        /// <summary>
        /// Pointer that indicates a link. Usually a pointing hand.
        /// </summary>
        PointingHand = 11,

        /// <summary>
        /// Window resize top-left. This may be a single arrow or a double arrow like <see cref="DoubleResizeNorthWestSouthEast"/>.
        /// </summary>
        ResizeNorthWestSouthEast = 12,

        /// <summary>
        /// Window resize top. May be <see cref="ResizeNorthSouth"/>.
        /// </summary>
        ResizeNorthSouth = 13,

        /// <summary>
        /// Window resize top-right. May be <see cref="ResizeNorthEast"/>.
        /// </summary>
        ResizeNorthEast = 14,

        /// <summary>
        /// Window resize right. May be <see cref="ResizeEastWest"/>.
        /// </summary>
        ResizeEastWest = 15,

        /// <summary>
        /// Window resize bottom-right. May be <see cref="ResizeSouthEast"/>.
        /// </summary>
        ResizeSouthEast = 16,

        /// <summary>
        /// Window resize bottom. May be <see cref="ResizeSouth"/>.
        /// </summary>
        ResizeSouth = 17,

        /// <summary>
        /// Window resize bottom-left. May be <see cref="ResizeSouthWest"/>.
        /// </summary>
        ResizeSouthWest = 18,

        /// <summary>
        /// Window resize left. May be <see cref="ResizeWest"/>.
        /// </summary>
        ResizeWest = 19
    }
}

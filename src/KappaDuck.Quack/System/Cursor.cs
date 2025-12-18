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
        => _handle = Native.SDL_CreateCursor(pixels, mask, width, height, hotSpotX, hotSpotY);

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="pixels">The pixel data for the cursor.</param>
    /// <param name="mask">The mask data for the cursor.</param>
    /// <param name="width">The width of the cursor.</param>
    /// <param name="height">The height of the cursor.</param>
    /// <param name="hotSpot">The hot spot of the cursor.</param>
    public Cursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, int width, int height, Vector2Int hotSpot)
        => _handle = Native.SDL_CreateCursor(pixels, mask, width, height, hotSpot.X, hotSpot.Y);

    /// <summary>
    /// Initializes a system cursor.
    /// </summary>
    /// <param name="type">The type of the cursor.</param>
    public Cursor(CursorType type) => _handle = Native.SDL_CreateSystemCursor(type);

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="surface">The surface to use for the cursor.</param>
    /// <param name="hotSpotX">The x-coordinate of the cursor's hot spot.</param>
    /// <param name="hotSpotY">The y-coordinate of the cursor's hot spot.</param>
    public unsafe Cursor(Surface surface, int hotSpotX, int hotSpotY)
        => _handle = Native.SDL_CreateColorCursor(surface.Handle, hotSpotX, hotSpotY);

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
    public static Cursor Current => _cursor ?? new Cursor(Native.SDL_GetDefaultCursor());

    /// <summary>
    /// Gets a value indicating whether the cursor is visible.
    /// </summary>
    public bool Visible
    {
        get;
        set
        {
            if (field == value)
                return;

            field = value;
            if (field)
            {
                Native.SDL_ShowCursor();
                return;
            }

            Native.SDL_HideCursor();
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
        Native.SDL_SetCursor(_cursor._handle);
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;

namespace KappaDuck.Quack.System;

/// <summary>
/// Represents a cursor.
/// </summary>
public sealed class Cursor : IDisposable
{
    private readonly SDL_Cursor _handle;

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="pixels">The pixel data for the cursor.</param>
    /// <param name="mask">The mask data for the cursor.</param>
    /// <param name="width">The width of the cursor.</param>
    /// <param name="height">The height of the cursor.</param>
    /// <param name="hotSpotX">the x-axis offset from the left of the cursor image to the mouse x position, in the range of 0 to <paramref name="width"/> - 1.</param>
    /// <param name="hotSpotY">The y-axis offset from the top of the cursor image to the mouse y position, in the range of 0 to <paramref name="height"/> - 1.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the cursor.</exception>
    public Cursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, int width, int height, int hotSpotX, int hotSpotY)
    {
        QuackEngine.Acquire(Subsystem.Video);

        _handle = Native.SDL_CreateCursor(pixels, mask, width, height, hotSpotX, hotSpotY);
        QuackNativeException.ThrowIfHandleInvalid(_handle);
    }

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="pixels">The pixel data for the cursor.</param>
    /// <param name="mask">The mask data for the cursor.</param>
    /// <param name="width">The width of the cursor.</param>
    /// <param name="height">The height of the cursor.</param>
    /// <param name="hotSpot">The hot spot of the cursor.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the cursor.</exception>
    public Cursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, int width, int height, Vector2Int hotSpot) : this(pixels, mask, width, height, hotSpot.X, hotSpot.Y)
    {
    }

    /// <summary>
    /// Creates a system cursor.
    ///</summary>
    /// <param name="type">The type of the cursor.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the cursor.</exception>
    public Cursor(CursorType type)
    {
        QuackEngine.Acquire(Subsystem.Video);

        _handle = Native.SDL_CreateSystemCursor(type);
        QuackNativeException.ThrowIfHandleInvalid(_handle);
    }

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="surface">The surface to use for the cursor.</param>
    /// <param name="hotSpotX">The x-coordinate of the cursor's hot spot.</param>
    /// <param name="hotSpotY">The y-coordinate of the cursor's hot spot.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to create the cursor.</exception>
    public unsafe Cursor(Surface surface, int hotSpotX, int hotSpotY)
    {
        QuackEngine.Acquire(Subsystem.Video);

        _handle = Native.SDL_CreateColorCursor(surface.Handle, hotSpotX, hotSpotY);
        QuackNativeException.ThrowIfHandleInvalid(_handle);
    }

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="surface">The surface to use for the cursor.</param>
    /// <param name="hotSpot">The hot spot of the cursor.</param>
    public Cursor(Surface surface, Vector2Int hotSpot) : this(surface, hotSpot.X, hotSpot.Y)
    {
    }

    private Cursor()
    {
        QuackEngine.Acquire(Subsystem.Video);

        _handle = Native.SDL_GetDefaultCursor();
        QuackNativeException.ThrowIfHandleInvalid(_handle);
    }

    /// <summary>
    /// Gets the current cursor.
    /// </summary>
    /// <remarks>
    /// If no cursor has been set, this property returns the default cursor.
    /// </remarks>
    public static Cursor Current
    {
        get => field ?? Default;
        private set;
    }

    /// <summary>
    /// Gets the system's default cursor.
    /// </summary>
    public static Cursor Default { get; } = new Cursor();

    /// <summary>
    /// Gets or sets a value indicating whether the cursor is visible.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when failed to show or hide the cursor.</exception>
    public bool Visible
    {
        get;
        set
        {
            if (field == value)
                return;

            field = value;

            if (value)
            {
                QuackNativeException.ThrowIfFailed(Native.SDL_ShowCursor());
                return;
            }

            QuackNativeException.ThrowIfFailed(Native.SDL_HideCursor());
        }
    }

    /// <summary>
    /// Releases the resources used by the cursor.
    /// </summary>
    public void Dispose()
    {
        _handle.Dispose();
        QuackEngine.Release();
    }

    /// <summary>
    /// Sets the current cursor.
    /// </summary>
    /// <param name="cursor">The cursor to set as current.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to set the cursor.</exception>
    public static void Set(Cursor cursor)
    {
        Current = cursor;
        QuackNativeException.ThrowIfFailed(Native.SDL_SetCursor(cursor._handle));
    }
}

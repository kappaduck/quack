// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.System;

/// <summary>
/// Represents the system cursor.
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
    /// <exception cref="QuackInteropException">Thrown when failed to create the cursor.</exception>
    public Cursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, int width, int height, int hotSpotX, int hotSpotY)
    {
        QuackEngine.AddRef(Subsystem.Video);

        _handle = SDL3.System.CreateCursor(pixels, mask, width, height, hotSpotX, hotSpotY);
        QuackInteropException.ThrowIfHandleInvalid(_handle);
    }

    /// <summary>
    /// Creates a custom cursor.
    /// </summary>
    /// <param name="pixels">The pixel data for the cursor.</param>
    /// <param name="mask">The mask data for the cursor.</param>
    /// <param name="size">The size of the cursor.</param>
    /// <param name="hotSpot">The hot spot of the cursor.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to create the cursor.</exception>
    public Cursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, SizeInt size, Vector2Int hotSpot) : this(pixels, mask, size.Width, size.Height, hotSpot.X, hotSpot.Y)
    {
    }

    /// <summary>
    /// Creates a system cursor based on the cursor type.
    /// </summary>
    /// <param name="type">The type of the system cursor.</param>
    /// <exception cref="QuackInteropException">Thrown when failed to create the cursor.</exception>
    public Cursor(CursorType type)
    {
        QuackEngine.AddRef(Subsystem.Video);

        _handle = SDL3.System.CreateSystemCursor(type);
        QuackInteropException.ThrowIfHandleInvalid(_handle);
    }

    private Cursor()
    {
        QuackEngine.AddRef(Subsystem.Video);

        _handle = SDL3.System.GetDefaultCursor();
        QuackInteropException.ThrowIfHandleInvalid(_handle);
    }

    /// <summary>
    /// Gets or sets the current cursor.
    /// </summary>
    /// <remarks>
    /// If no cursor is active, returns the default cursor.
    /// </remarks>
    /// <exception cref="QuackInteropException">Thrown when failed to set the cursor.</exception>
    public static Cursor Current
    {
        get => field ?? Default;
        set
        {
            field = value;
            QuackInteropException.ThrowIfFailed(SDL3.System.SetCursor(value._handle));
        }
    }

    /// <summary>
    /// Gets the system's default cursor.
    /// </summary>
    public static Cursor Default { get; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether the cursor is visible.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to show or hide the cursor.</exception>
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
                QuackInteropException.ThrowIfFailed(SDL3.System.ShowCursor());
                return;
            }

            QuackInteropException.ThrowIfFailed(SDL3.System.HideCursor());
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _handle.Dispose();
        QuackEngine.Release();
    }
}

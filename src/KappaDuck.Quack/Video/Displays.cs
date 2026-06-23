// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Video;

/// <summary>
/// Provides access to the displays (monitors) currently connected to the system.
/// </summary>
public static class Displays
{
    /// <summary>
    /// Gets all currently connected displays.
    /// </summary>
    /// <remarks>
    /// The first display is the primary one. The order may change as displays are connected or disconnected.
    /// </remarks>
    /// <exception cref="QuackException">The video subsystem is not initialized.</exception>
    public static IReadOnlyList<Display> All
    {
        get
        {
            QuackEngine.EnsureInitialized(Subsystem.Video);

            ReadOnlySpan<uint> ids = SDL3.GetDisplays(out _);

            if (ids.IsEmpty)
                return [];

            Display[] displays = new Display[ids.Length];

            for (int i = 0; i < ids.Length; i++)
                displays[i] = new Display(ids[i]);

            return displays;
        }
    }

    /// <summary>
    /// Gets the primary display.
    /// </summary>
    /// <exception cref="QuackException">The video subsystem is not initialized.</exception>
    /// <exception cref="QuackInteropException">Failed to retrieve the primary display.</exception>
    public static Display Primary
    {
        get
        {
            QuackEngine.EnsureInitialized(Subsystem.Video);

            uint id = SDL3.GetPrimaryDisplay();

            SDLThrowHelper.ThrowIfZero(id);
            return new Display(id);
        }
    }

    /// <summary>
    /// Gets the display containing the given point.
    /// </summary>
    /// <param name="point">The point in screen coordinates.</param>
    /// <returns>The display containing the point.</returns>
    /// <exception cref="QuackException">The video subsystem is not initialized.</exception>
    /// <exception cref="QuackInteropException">Failed to retrieve the display</exception>
    public static Display GetForPoint(Point point)
    {
        QuackEngine.EnsureInitialized(Subsystem.Video);

        uint id = SDL3.GetDisplayForPoint(point);
        SDLThrowHelper.ThrowIfZero(id);

        return new Display(id);
    }

    /// <summary>
    /// Gets the display that best contains the given rectangle.
    /// </summary>
    /// <remarks>
    /// If the rectangle spans several displays, the one with the largest overlap is returned.
    /// </remarks>
    /// <param name="rect">The rectangle in screen coordinates.</param>
    /// <returns>The display containing the rectangle.</returns>
    /// <exception cref="QuackException">The video subsystem is not initialized.</exception>
    /// <exception cref="QuackInteropException">Failed to retrieve the display</exception>
    public static Display GetForRect(RectI rect)
    {
        QuackEngine.EnsureInitialized(Subsystem.Video);

        uint id = SDL3.GetDisplayForRect(rect);
        SDLThrowHelper.ThrowIfZero(id);

        return new Display(id);
    }
}

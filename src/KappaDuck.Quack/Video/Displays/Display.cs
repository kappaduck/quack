// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL;

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Represents a display such as monitor.
/// </summary>
public sealed partial class Display
{
    private const string HdrEnabledProperty = "SDL.display.HDR_enabled";

    internal Display(uint id) => Id = id;

    /// <summary>
    /// Gets the Display Id.
    /// </summary>
    public uint Id { get; init; }

    /// <summary>
    /// Gets the name of the display.
    /// </summary>
    public string Name => SDL_GetDisplayName(Id);

    /// <summary>
    /// Gets the bounds of the display.
    /// </summary>
    /// <exception cref="QuackNativeException">Failed to get the display bounds.</exception>
    public RectInt Bounds
    {
        get
        {
            QuackNativeException.ThrowIfFailed(SDL_GetDisplayBounds(Id, out RectInt bounds));
            return bounds;
        }
    }

    /// <summary>
    /// Gets the usable desktop area represented by a display, in screen coordinates.
    /// </summary>
    /// <remarks>
    /// This is the same area as <see cref="Bounds"/> reports, but with portions reserved by the system removed.
    /// </remarks>
    /// <exception cref="QuackNativeException">Failed to get the usable display bounds.</exception>
    public RectInt UsableBounds
    {
        get
        {
            QuackNativeException.ThrowIfFailed(SDL_GetDisplayUsableBounds(Id, out RectInt bounds));
            return bounds;
        }
    }

    /// <summary>
    /// Gets content scale of the display.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The content scale is the expected scale for content based on the DPI settings of the display.
    /// For example, a 4K display might have a 2.0 (200%) display scale,
    /// which means that the user expects UI elements to be twice as big on this display, to aid in readability.
    /// </para>
    /// <para>
    /// After window creation, SDL_GetWindowDisplayScale() should be used to query the content scale factor for individual windows
    /// instead of querying the display for a window and calling this function, as the per-window content scale factor may differ
    /// from the base value of the display it is on, particularly on high-DPI and/or multi-monitor desktop configurations.
    /// </para>
    /// </remarks>
    public float ContentScale => SDL_GetDisplayContentScale(Id);

    /// <summary>
    /// Gets information about the current display mode.
    /// </summary>
    /// <remarks>
    /// There's a difference between <see cref="CurrentMode"/> and <see cref="DesktopMode"/>.
    /// When SDL run fullscreen and has changed the resolution. In that case, <see cref="CurrentMode"/> will return the current resolution,
    /// and not the previous native display mode.
    /// </remarks>
    /// <exception cref="QuackNativeException">Failed to get the current display mode.</exception>
    public DisplayMode CurrentMode
    {
        get
        {
            unsafe
            {
                DisplayMode* mode = SDL_GetCurrentDisplayMode(Id);

                QuackNativeException.ThrowIfNull(mode);

                return *mode;
            }
        }
    }

    /// <summary>
    /// Gets information about the desktop display mode.
    /// </summary>
    /// <remarks>
    /// There's a difference between <see cref="DesktopMode"/> and <see cref="CurrentMode"/>.
    /// When SDL run fullscreen and has changed the resolution. In that case, <see cref="DesktopMode"/> will return the previous native display mode,
    /// and not the current display mode.
    /// </remarks>
    /// <exception cref="QuackNativeException">Failed to get the desktop display mode.</exception>
    public DisplayMode DesktopMode
    {
        get
        {
            unsafe
            {
                DisplayMode* mode = SDL_GetDesktopDisplayMode(Id);

                QuackNativeException.ThrowIfNull(mode);

                return *mode;
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether HDR is enabled on the display.
    /// </summary>
    public bool HdrEnabled
    {
        get
        {
            uint properties = SDL_GetDisplayProperties(Id);
            return Properties.Get(properties, HdrEnabledProperty, defaultValue: false);
        }
    }

    /// <summary>
    /// Gets the orientation of a display.
    /// </summary>
    public DisplayOrientation Orientation => SDL_GetCurrentDisplayOrientation(Id);

    /// <summary>
    /// Gets the orientation of a display when it is unrotated.
    /// </summary>
    public DisplayOrientation NaturalOrientation => SDL_GetNaturalDisplayOrientation(Id);

    /// <summary>
    /// Get a list of fullscreen display modes available for the display.
    /// </summary>
    /// <returns>A list of fullscreen display modes.</returns>
    /// <exception cref="QuackNativeException">Failed to get the fullscreen display modes.</exception>
    public DisplayMode[] GetFullScreenModes()
    {
        DisplayMode[] displayModes;

        unsafe
        {
            DisplayMode** modes = SDL_GetFullscreenDisplayModes(Id, out int length);

            QuackNativeException.ThrowIf(modes is null);

            displayModes = new DisplayMode[length];

            for (int i = 0; i < length; i++)
                displayModes[i] = *modes[i];

            SDLNative.Free(modes);
        }

        return displayModes;
    }

    /// <summary>
    /// Get the display with the specified identifier.
    /// </summary>
    /// <param name="displayId">The display id.</param>
    /// <returns>The display.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="displayId"/> is zero or negative.</exception>
    public static Display GetDisplay(uint displayId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(displayId);

        return new Display(displayId);
    }

    /// <summary>
    /// Get the display containing the specified point.
    /// </summary>
    /// <param name="point">The point to query.</param>
    /// <returns>The display containing the specified point.</returns>
    /// <exception cref="QuackNativeException">Failed to get the display containing the specified point.</exception>
    public static Display GetDisplay(Vector2Int point)
    {
        uint display;

        unsafe
        {
            display = SDL_GetDisplayForPoint(&point);
        }

        QuackNativeException.ThrowIfZero(display);

        return new Display(display);
    }

    /// <summary>
    /// Get the display containing the specified rectangle.
    /// </summary>
    /// <param name="rectangle">The rectangle to query.</param>
    /// <returns>The display entirely containing the specified rectangle or closest to the center of the rectangle.</returns>
    /// <exception cref="QuackNativeException">Failed to get the display containing the specified rectangle.</exception>
    public static Display GetDisplay(RectInt rectangle)
    {
        uint display;

        unsafe
        {
            display = SDL_GetDisplayForRect(&rectangle);
        }

        QuackNativeException.ThrowIfZero(display);

        return new Display(display);
    }

    /// <summary>
    /// Get a list of currently connected displays.
    /// </summary>
    /// <returns>A list of currently connected displays.</returns>
    public static Display[] GetDisplays()
    {
        ReadOnlySpan<uint> ids = SDL_GetDisplays(out _);

        if (ids.IsEmpty)
            return [];

        Display[] displays = new Display[ids.Length];

        for (int i = 0; i < ids.Length; i++)
            displays[i] = new Display(ids[i]);

        return displays;
    }

    /// <summary>
    /// Search the closest matching display mode for the display.
    /// </summary>
    /// <param name="width">The width in pixels of the desired display mode.</param>
    /// <param name="height">The height in pixels of the desired display mode.</param>
    /// <param name="refreshRate">The refresh rate of the desired display mode, or 0 for the desktop refresh rate.</param>
    /// <param name="includeHighDensityMode">include high density modes in the search.</param>
    /// <returns>The closest display mode equal to or larger than the desired mode.</returns>
    /// <exception cref="QuackNativeException">Failed to search the display mode.</exception>
    public DisplayMode SearchDisplayMode(int width, int height, float refreshRate, bool includeHighDensityMode)
    {
        QuackNativeException.ThrowIfFailed(SDL_GetClosestFullscreenDisplayMode(Id, width, height, refreshRate, includeHighDensityMode, out DisplayMode displayMode));
        return displayMode;
    }

    /// <summary>
    /// Get the primary display.
    /// </summary>
    /// <returns>The primary display.</returns>
    public static Display GetPrimaryDisplay() => new(SDL_GetPrimaryDisplay());
}

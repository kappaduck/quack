// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Represents a display.
/// </summary>
public sealed class Display
{
    private const string HdrEnabledProperty = "SDL.display.HDR_enabled";

    internal Display(uint id)
    {
        Id = id;
        Name = Native.SDL_GetDisplayName(Id);
    }

    /// <summary>
    /// Gets the unique identifier for the display.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets the name of the display.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the bounds of the display.
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public RectInt Bounds
    {
        get
        {
            QuackNativeException.ThrowIfFailed(Native.SDL_GetDisplayBounds(Id, out RectInt bounds));
            return bounds;
        }
    }

    /// <summary>
    /// Gets the usable bounds of the display (excluding taskbars, docks, etc.).
    /// </summary>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public RectInt UsableBounds
    {
        get
        {
            QuackNativeException.ThrowIfFailed(Native.SDL_GetDisplayUsableBounds(Id, out RectInt bounds));
            return bounds;
        }
    }

    /// <summary>
    /// Gets the content scale of the display.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The content scale is the expected scale for content based on the DPI settings of the display.
    /// For example, a 4K display might have a 2.0 (200%) content scale to make text and UI elements more readable.
    /// </para>
    /// <para>
    /// After the window is created, <see cref="Window.DisplayScale"/> should be used to query the content scale factor for individual windows
    /// instead of this property, as the per-window content scale factor may differ from the global display content scale factor. Especially on
    /// high-DPI and/or multi-monitor setups.
    /// </para>
    /// </remarks>
    public float ContentScale => Native.SDL_GetDisplayContentScale(Id);

    /// <summary>
    /// Gets the current display mode.
    /// </summary>
    /// <remarks>
    /// There's a difference between <see cref="CurrentMode"/> and <see cref="DesktopMode"/>.
    /// When the display is in fullscreen mode, <see cref="CurrentMode"/> will return the mode that the display is currently using,
    /// and <see cref="DesktopMode"/> will return the mode that the desktop was using before going fullscreen.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public DisplayMode CurrentMode
    {
        get
        {
            unsafe
            {
                DisplayMode* mode = Native.SDL_GetCurrentDisplayMode(Id);

                QuackNativeException.ThrowIfNull(mode);
                return *mode;
            }
        }
    }

    /// <summary>
    /// Gets the desktop display mode.
    /// </summary>
    /// <remarks>
    /// There's a difference between <see cref="DesktopMode"/> and <see cref="CurrentMode"/>.
    /// When the display is in fullscreen mode and changed resolution.
    /// In that case, <see cref="DesktopMode"/> will return the mode that the desktop was using before going fullscreen,
    /// and <see cref="CurrentMode"/> will return the mode that the display is currently using.
    /// </remarks>
    public DisplayMode DesktopMode
    {
        get
        {
            unsafe
            {
                DisplayMode* mode = Native.SDL_GetDesktopDisplayMode(Id);

                QuackNativeException.ThrowIfNull(mode);
                return *mode;
            }
        }
    }

    /// <summary>
    /// Gets whether HDR is enabled on the display.
    /// </summary>
    public bool HdrEnabled
    {
        get
        {
            uint properties = Native.SDL_GetDisplayProperties(Id);
            return Native.GetBooleanProperty(properties, HdrEnabledProperty, defaultValue: false);
        }
    }

    /// <summary>
    /// Gets the orientation of the display.
    /// </summary>
    public DisplayOrientation Orientation => Native.SDL_GetCurrentDisplayOrientation(Id);

    /// <summary>
    /// Gets the default orientation of the display when no rotation is applied.
    /// </summary>
    public DisplayOrientation DefaultOrientation => Native.SDL_GetNaturalDisplayOrientation(Id);

    /// <summary>
    /// Gets all available fullscreen display modes for the display.
    /// </summary>
    /// <returns>All available fullscreen display modes for the display.</returns>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public DisplayMode[] GetFullscreenModes()
    {
        DisplayMode[] fullscreenModes;

        unsafe
        {
            DisplayMode** modes = Native.SDL_GetFullscreenDisplayModes(Id, out int length);

            QuackNativeException.ThrowIf(modes is null || length == 0);
            fullscreenModes = new DisplayMode[length];

            for (int i = 0; i < length; i++)
                fullscreenModes[i] = *modes[i];

            Native.Free(modes);
        }

        return fullscreenModes;
    }

    /// <summary>
    /// Searches for the closest matching fullscreen display mode.
    /// </summary>
    /// <param name="query">The display mode query.</param>
    /// <returns>The closest matching fullscreen display mode.</returns>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public DisplayMode SearchDisplayMode(DisplayModeQuery query)
    {
        bool success = Native.SDL_GetClosestFullscreenDisplayMode(Id, query.Width, query.Height, query.RefreshRate ?? 0, query.HighDensity, out DisplayMode mode);
        QuackNativeException.ThrowIfFailed(success);

        return mode;
    }

    /// <summary>
    /// Gets a display by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the display.</param>
    /// <returns>The display with the specified identifier.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="id"/> is zero or negative.</exception>
    public static Display GetDisplay(uint id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        return new Display(id);
    }

    /// <summary>
    /// Gets the display that contains the specified point.
    /// </summary>
    /// <param name="point">The point to check.</param>
    /// <returns>The display that contains the specified point.</returns>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public static Display GetDisplay(Vector2Int point)
    {
        uint id;

        unsafe
        {
            id = Native.SDL_GetDisplayForPoint(&point);
        }

        QuackNativeException.ThrowIfZero(id);
        return new Display(id);
    }

    /// <summary>
    /// Gets the display containing the specified rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to check.</param>
    /// <returns>The display containing the specified rectangle.</returns>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public static Display GetDisplay(RectInt rect)
    {
        uint id;

        unsafe
        {
            id = Native.SDL_GetDisplayForRect(&rect);
        }

        QuackNativeException.ThrowIfZero(id);
        return new Display(id);
    }

    /// <summary>
    /// Gets all connected displays.
    /// </summary>
    /// <returns>All connected displays.</returns>
    public static Display[] GetDisplays()
    {
        ReadOnlySpan<uint> ids = Native.SDL_GetDisplays(out _);

        if (ids.IsEmpty)
            return [];

        Display[] displays = new Display[ids.Length];

        for (int i = 0; i < ids.Length; i++)
            displays[i] = new Display(ids[i]);

        return displays;
    }

    /// <summary>
    /// Gets the primary display.
    /// </summary>
    /// <returns>The primary display.</returns>
    public static Display GetPrimaryDisplay() => new(Native.SDL_GetPrimaryDisplay());
}

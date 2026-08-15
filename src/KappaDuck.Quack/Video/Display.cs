// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Video;

/// <summary>
/// Represents a connected display (monitor) and exposes its current geometry, orientation and modes.
/// </summary>
/// <remarks>
/// A display is identified by a stable id. The properties query the system on each access, so they always
/// reflect the current state. Use <see cref="Displays"/> to enumerate displays or obtain the primary one.
/// </remarks>
public readonly struct Display : IEquatable<Display>
{
    internal Display(uint id) => Id = id;

    /// <summary>
    /// Gets the desktop area of the display in screen coordinates, with the primary display always at (0, 0).
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to retrieve the display bounds.</exception>
    public RectI Bounds
    {
        get
        {
            SDLThrowHelper.ThrowIfFailed(SDL3.GetDisplayBounds(Id, out RectI bounds));
            return bounds;
        }
    }

    /// <summary>
    /// Gets the content scale of the display, where 1.0 means no scaling.
    /// </summary>
    /// <remarks>
    /// This is the suggested amount to scale UI elements so that they are a comfortable size regardless
    /// of the display's pixel density. For example, a 4K display at 200% scale reports 2.0.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to retrieve the content scale.</exception>
    public float ContentScale
    {
        get
        {
            float scale = SDL3.GetDisplayContentScale(Id);

            SDLThrowHelper.ThrowIfZero(scale);
            return scale;
        }
    }

    /// <summary>
    /// Gets the current mode of the display.
    /// </summary>
    /// <remarks>
    /// This may differ from <see cref="DesktopMode"/> if a fullscreen window has changed the mode.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to retrieve the current mode.</exception>
    public DisplayMode CurrentMode
    {
        get
        {
            unsafe
            {
                SDL_DisplayMode* mode = SDL3.GetCurrentDisplayMode(Id);

                SDLThrowHelper.ThrowIfNull(mode);
                return new DisplayMode(*mode);
            }
        }
    }

    /// <summary>
    /// Gets the mode of the display as configured on the desktop.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to retrieve the desktop mode.</exception>
    public DisplayMode DesktopMode
    {
        get
        {
            unsafe
            {
                SDL_DisplayMode* mode = SDL3.GetDesktopDisplayMode(Id);

                SDLThrowHelper.ThrowIfNull(mode);
                return new DisplayMode(*mode);
            }
        }
    }

    /// <summary>
    /// Gets the fullscreen modes available on the display.
    /// </summary>
    /// <remarks>
    /// The modes are sorted from largest to smallest, by width then height then refresh rate then pixel format.
    /// </remarks>
    /// <returns>The available fullscreen modes, or an empty collection if none are available.</returns>
    /// <exception cref="QuackInteropException">Failed to retrieve the fullscreen modes.</exception>
    public IReadOnlyList<DisplayMode> FullscreenModes
    {
        get
        {
            unsafe
            {
                SDL_DisplayMode** modes = SDL3.GetFullscreenDisplayModes(Id, out int count);
                SDLThrowHelper.ThrowIfNull(modes);

                try
                {
                    DisplayMode[] result = new DisplayMode[count];

                    for (int i = 0; i < count; i++)
                        result[i] = new DisplayMode(*modes[i]);

                    return result;
                }
                finally
                {
                    SDL3.Free(modes);
                }
            }
        }
    }

    /// <summary>
    /// Gets the unique identifier of the display.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets a value indicating whether the display has HDR headroom above the SDR white point.
    /// </summary>
    /// <remarks>
    /// This is informational only; not all platforms report HDR capability at the display level.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to retrieve the display properties.</exception>
    public bool IsHDREnabled
    {
        get
        {
            uint properties = SDL3.GetDisplayProperties(Id);

            SDLThrowHelper.ThrowIfZero(properties);
            return Properties.Get(properties, "SDL.display.HDR_enabled", false);
        }
    }

    /// <summary>
    /// Gets the name of the display.
    /// </summary>
    /// <exception cref="QuackInteropException">Failed to retrieve the display name.</exception>
    public string Name
    {
        get
        {
            string? name = SDL3.GetDisplayName(Id);

            SDLThrowHelper.ThrowIfNull(name);
            return name;
        }
    }

    /// <summary>
    /// Gets the natural orientation of the display, which is the orientation it reports when not rotated.
    /// </summary>
    public DisplayOrientation NaturalOrientation => SDL3.GetNaturalDisplayOrientation(Id);

    /// <summary>
    /// Gets the current orientation of the display.
    /// </summary>
    public DisplayOrientation Orientation => SDL3.GetCurrentDisplayOrientation(Id);

    /// <summary>
    /// Gets the usable desktop area of the display in screen coordinates, excluding system reserved
    /// regions such as the taskbar or a global menu bar.
    /// </summary>
    /// <remarks>
    /// This is a best-effort hint and may not be available or accurate on every platform.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to retrieve the usable display bounds.</exception>
    public RectI UsableBounds
    {
        get
        {
            SDLThrowHelper.ThrowIfFailed(SDL3.GetDisplayUsableBounds(Id, out RectI bounds));
            return bounds;
        }
    }

    /// <summary>
    /// Finds the fullscreen mode of the display that most closely matches the requested mode.
    /// </summary>
    /// <remarks>
    /// Modes are matched with size as the highest priority, then pixel format, then refresh rate. A
    /// <paramref name="refreshRate"/> of 0 defaults to the desktop refresh rate.
    /// </remarks>
    /// <param name="dimension">The desired dimension in screen coordinates.</param>
    /// <param name="refreshRate">The desired refresh rate in hertz, or 0 to use the desktop refresh rate.</param>
    /// <param name="includeHighDensityModes">
    /// <see langword="true"/> to include high pixel density modes in the search; otherwise <see langword="false"/>.
    /// </param>
    /// <returns>The closest matching mode, or <see langword="null"/> if every available mode is too small.</returns>
    public DisplayMode? GetClosestFullscreenMode(Size dimension, float refreshRate = 0f, bool includeHighDensityModes = false)
        => GetClosestFullscreenMode(dimension.Width, dimension.Height, refreshRate, includeHighDensityModes);

    /// <summary>
    /// Finds the fullscreen mode of the display that most closely matches the requested mode.
    /// </summary>
    /// <remarks>
    /// Modes are matched with size as the highest priority, then pixel format, then refresh rate. A
    /// <paramref name="refreshRate"/> of 0 defaults to the desktop refresh rate.
    /// </remarks>
    /// <param name="width">The desired width in screen coordinates.</param>
    /// <param name="height">The desired height in screen coordinates.</param>
    /// <param name="refreshRate">The desired refresh rate in hertz, or 0 to use the desktop refresh rate.</param>
    /// <param name="includeHighDensityModes">
    /// <see langword="true"/> to include high pixel density modes in the search; otherwise <see langword="false"/>.
    /// </param>
    /// <returns>The closest matching mode, or <see langword="null"/> if every available mode is too small.</returns>
    public DisplayMode? GetClosestFullscreenMode(int width, int height, float refreshRate = 0f, bool includeHighDensityModes = false)
    {
        if (!SDL3.GetClosestFullscreenDisplayMode(Id, width, height, refreshRate, includeHighDensityModes, out SDL_DisplayMode closest))
            return null;

        return new DisplayMode(closest);
    }

    /// <summary>
    /// Determines whether two displays refer to the same display.
    /// </summary>
    /// <param name="left">The left display.</param>
    /// <param name="right">The right display.</param>
    /// <returns><see langword="true"/> if the displays are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Display left, Display right) => left.Equals(right);

    /// <summary>
    /// Determines whether two displays refer to different displays.
    /// </summary>
    /// <param name="left">The left display.</param>
    /// <param name="right">The right display.</param>
    /// <returns><see langword="true"/> if the displays are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Display left, Display right) => !left.Equals(right);

    /// <inheritdoc/>
    public bool Equals(Display other) => Id == other.Id;

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Display other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => Id.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => $"Display {Id} ({Name})";
}

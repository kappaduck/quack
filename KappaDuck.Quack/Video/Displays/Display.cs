// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Windows;
using System.Text.Unicode;

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Represents a display.
/// </summary>
public sealed class Display : ISpanFormattable, IUtf8SpanFormattable
{
    private DisplayMode[]? _modes;

    internal Display(uint id)
    {
        Id = id;
        Name = SDL3.Video.GetDisplayName(Id);
    }

    /// <summary>
    /// Gets the display's id.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets the display's name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the display's bounds.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to get the display bounds.</exception>
    public RectInt Bounds
    {
        get
        {
            QuackInteropException.ThrowIfFailed(SDL3.Video.GetDisplayBounds(Id, out RectInt bounds));
            return bounds;
        }
    }

    /// <summary>
    /// Gets the usable bounds of the display (excluding taskbar, docks, etc.).
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to get the usable display bounds.</exception>
    public RectInt UsableBounds
    {
        get
        {
            QuackInteropException.ThrowIfFailed(SDL3.Video.GetDisplayUsableBounds(Id, out RectInt bounds));
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
    public float ContentScale => SDL3.Video.GetDisplayContentScale(Id);

    /// <summary>
    /// Gets a value indicating whether the display supports HDR (High Dynamic Range).
    /// </summary>
    public bool HdrEnabled
    {
        get
        {
            uint properties = SDL3.Video.GetDisplayProperties(Id);
            return SDL3.Properties.GetBooleanProperty(properties, "SDL.display.HDR_enabled", defaultValue: false);
        }
    }

    /// <summary>
    /// Gets the current orientation of the display.
    /// </summary>
    public DisplayOrientation Orientation => SDL3.Video.GetCurrentDisplayOrientation(Id);

    /// <summary>
    /// Gets the default orientation of the display when no rotation has been applied.
    /// </summary>
    public DisplayOrientation DefaultOrientation => SDL3.Video.GetNaturalDisplayOrientation(Id);

    /// <summary>
    /// Gets the current display mode.
    /// </summary>
    /// <remarks>
    /// There's a difference between <see cref="CurrentMode"/> and <see cref="DesktopMode"/>.
    /// When the display is in fullscreen mode, <see cref="CurrentMode"/> will return the mode that the display is currently using,
    /// and <see cref="DesktopMode"/> will return the mode that the desktop was using before going fullscreen.
    /// </remarks>
    /// <exception cref="QuackInteropException">Thrown when failed to get the current display mode.</exception>
    public unsafe DisplayMode CurrentMode
    {
        get
        {
            DisplayMode* mode = SDL3.Video.GetCurrentDisplayMode(Id);

            QuackInteropException.ThrowIfNull(mode);
            return *mode;
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
    public unsafe DisplayMode DesktopMode
    {
        get
        {
            DisplayMode* mode = SDL3.Video.GetDesktopDisplayMode(Id);

            QuackInteropException.ThrowIfNull(mode);
            return *mode;
        }
    }

    /// <summary>
    /// Gets all display modes supported by the display.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to get the display modes.</exception>
    public unsafe IReadOnlyList<DisplayMode> Modes
    {
        get
        {
            if (_modes is not null)
                return _modes;

            DisplayMode** modes = SDL3.Video.GetFullscreenDisplayModes(Id, out int length);

            QuackInteropException.ThrowIf(modes is null || length == 0);
            _modes = new DisplayMode[length];

            for (int i = 0; i < length; i++)
                _modes[i] = *modes[i];

            SDL3.Memory.Free(modes);
            return _modes;
        }
    }

    /// <summary>
    /// Gets all connected displays.
    /// </summary>
    public static IReadOnlyList<Display> All
    {
        get
        {
            QuackEngine.DangerousAddRef(Subsystem.Video);

            ReadOnlySpan<uint> ids = SDL3.Video.GetDisplays(out _);

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
    public static Display Primary
    {
        get
        {
            QuackEngine.DangerousAddRef(Subsystem.Video);
            return FromId(SDL3.Video.GetPrimaryDisplay());
        }
    }

    /// <summary>
    /// Tries to get the closest display mode matching the specified query.
    /// </summary>
    /// <param name="query">The display mode query.</param>
    /// <param name="mode">The closest display mode matching the query, or null if no matching video mode was available.</param>
    /// <returns><see langword="true"/> if a matching video mode was available; otherwise, <see langword="false"/>.</returns>
    public bool TryGetClosestMode(DisplayModeQuery query, [NotNullWhen(true)] out DisplayMode? mode)
    {
        bool found = SDL3.Video.GetClosestFullscreenDisplayMode(Id, query.Width, query.Height, query.RefreshRate ?? 0, query.HighDensity, out DisplayMode closestMode);

        mode = closestMode;
        return found;
    }

    /// <summary>
    /// Gets the display with the specified id.
    /// </summary>
    /// <param name="id">The id of the display to get.</param>
    /// <returns>The display with the specified id.</returns>
    public static Display FromId(uint id)
    {
        QuackInteropException.ThrowIfZero(id);
        return new Display(id);
    }

    /// <summary>
    /// Gets the display that contains the specified point.
    /// </summary>
    /// <param name="point">The point to check.</param>
    /// <returns>The display that contains the specified point.</returns>
    public static unsafe Display FromPoint(Vector2Int point)
    {
        QuackEngine.DangerousAddRef(Subsystem.Video);

        uint id = SDL3.Video.GetDisplayForPoint(&point);
        return FromId(id);
    }

    /// <summary>
    /// Gets the display that contains the specified point.
    /// </summary>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <returns>The display that contains the specified point.</returns>
    public static Display FromPoint(int x, int y) => FromPoint(new Vector2Int(x, y));

    /// <summary>
    /// Gets the display that most closely intersects the specified rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to check.</param>
    /// <returns>The display that most closely intersects the specified rectangle.</returns>
    public static unsafe Display FromRect(RectInt rect)
    {
        QuackEngine.DangerousAddRef(Subsystem.Video);

        uint id = SDL3.Video.GetDisplayForRect(&rect);
        return FromId(id);
    }

    /// <inheritdoc/>
    public override string ToString() => $"{this}";

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"{Name} ({Bounds.Width}x{Bounds.Height})", out bytesWritten);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"{Name} ({Bounds.Width}x{Bounds.Height})", out charsWritten);
}

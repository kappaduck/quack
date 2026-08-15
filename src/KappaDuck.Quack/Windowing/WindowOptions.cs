// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Windowing;

/// <summary>
/// Options that configure how a <see cref="Window"/> is created.
/// </summary>
public sealed record WindowOptions
{
    /// <summary>
    /// Gets a value indicating whether the window is always on top of other windows.
    /// </summary>
    public bool AlwaysOnTop { get; init; }

    /// <summary>
    /// Gets the minimum and maximum aspect ratios of the window's client area, where zero means no limit.
    /// </summary>
    public AspectRatio AspectRatio { get; init; }

    /// <summary>
    /// Gets a value indicating whether the window has no decorations, such as a title bar or borders.
    /// </summary>
    public bool Borderless { get; init; }

    /// <summary>
    /// Gets a value indicating whether the window can receive input focus. Defaults to <see langword="true"/>.
    /// </summary>
    public bool Focusable { get; init; } = true;

    /// <summary>
    /// Gets a value indicating whether the window starts in fullscreen mode.
    /// </summary>
    public bool Fullscreen { get; init; }

    /// <summary>
    /// Gets the fullscreen display mode used when <see cref="Fullscreen"/> is set, or <see langword="null"/> for
    /// borderless desktop fullscreen.
    /// </summary>
    public DisplayMode? FullscreenMode { get; init; }

    /// <summary>
    /// Gets a value indicating whether the window starts hidden.
    /// </summary>
    public bool Hidden { get; init; }

    /// <summary>
    /// Gets a value indicating whether the window starts maximized.
    /// </summary>
    public bool Maximized { get; init; }

    /// <summary>
    /// Gets the maximum size of the window's client area, where zero means no limit.
    /// </summary>
    public Size MaximumSize { get; init; }

    /// <summary>
    /// Gets a value indicating whether the window starts minimized.
    /// </summary>
    public bool Minimized { get; init; }

    /// <summary>
    /// Gets the minimum size of the window's client area, where zero means no limit.
    /// </summary>
    public Size MinimumSize { get; init; }

    /// <summary>
    /// Gets the opacity of the window, from 0 (transparent) to 1 (opaque). Defaults to <c>1f</c>.
    /// </summary>
    public float Opacity { get; init; } = 1f;

    /// <summary>
    /// Gets the position of the top-left corner on the screen, or <see langword="null"/> to let the system place the window.
    /// </summary>
    public Point? Position { get; init; }

    /// <summary>
    /// Gets a value indicating whether the window can be resized by the user.
    /// </summary>
    public bool Resizable { get; init; }

    /// <summary>
    /// Gets a value indicating whether the window uses high pixel density back buffering when available.
    /// </summary>
    public bool UseHighPixelDensity { get; init; }

    /// <summary>
    /// Gets a value indicating whether the window uses a transparent back buffer.
    /// </summary>
    public bool UseTransparentBuffer { get; init; }
}

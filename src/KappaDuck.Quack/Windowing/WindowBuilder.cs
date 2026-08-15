// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Windowing;

/// <summary>
/// A fluent builder to build a window.
/// </summary>
/// <remarks>
/// A builder can be reused to open several windows that share the same configuration; every build
/// call returns a new, independent window that you own and must dispose.
/// </remarks>
/// <remarks>
/// Initializes a new instance of the <see cref="WindowBuilder"/> seeded from an existing configuration.
/// </remarks>
/// <param name="options">The options to start from.</param>
public sealed class WindowBuilder(WindowOptions options)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowBuilder"/> with default settings.
    /// </summary>
    public WindowBuilder() : this(new WindowOptions())
    {
    }

    /// <summary>
    /// Gets the current window options which will be used for the window creation.
    /// </summary>
    public WindowOptions Options { get; private set; } = options;

    /// <summary>
    /// Keeps the window above all other windows.
    /// </summary>
    /// <param name="alwaysOnTop">Whether the window is always on top.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder AlwaysOnTop(bool alwaysOnTop = true)
    {
        Options = Options with { AlwaysOnTop = alwaysOnTop };
        return this;
    }

    /// <summary>
    /// Constrains the window to a range of aspect ratios, where <c>0</c> on either bound leaves it unconstrained.
    /// </summary>
    /// <param name="minimum">The narrowest permitted ratio.</param>
    /// <param name="maximum">The widest permitted ratio.</param>
    /// <returns>The same builder for chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">A bound is negative or not finite.</exception>
    /// <exception cref="ArgumentException">Both bounds are constrained and <paramref name="minimum"/> exceeds <paramref name="maximum"/>.</exception>
    public WindowBuilder AspectRatio(float minimum, float maximum)
    {
        Options = Options with { AspectRatio = new AspectRatio(minimum, maximum) };
        return this;
    }

    /// <summary>
    /// Locks the window to a single fixed aspect ratio.
    /// </summary>
    /// <param name="ratio">The exact width-to-height ratio.</param>
    /// <returns>The same builder for chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="ratio"/> is negative or not finite.</exception>
    public WindowBuilder AspectRatio(float ratio)
    {
        Options = Options with { AspectRatio = new AspectRatio(ratio) };
        return this;
    }

    /// <summary>
    /// Removes the window's decorations, such as the title bar and borders.
    /// </summary>
    /// <param name="borderless">Whether the window is borderless.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Borderless(bool borderless = true)
    {
        Options = Options with { Borderless = borderless };
        return this;
    }

    /// <summary>
    /// Builds the window with the given title and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="size">The size of the window.</param>
    /// <returns>The created window.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is negative or zero.</exception>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
    public Window Build(string title, Size size) => new(title, size, Options);

    /// <summary>
    /// Builds the window with the given title and size.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="width">The width of the window.</param>
    /// <param name="height">The height of the window.</param>
    /// <returns>The created window.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> is negative or zero.</exception>
    /// <exception cref="QuackInteropException">Failed to create the window.</exception>
    public Window Build(string title, int width, int height) => new(title, width, height, Options);

    /// <summary>
    /// Sets whether the window can receive input focus. Windows are focusable by default.
    /// </summary>
    /// <param name="focusable">Whether the window is focusable.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Focusable(bool focusable = true)
    {
        Options = Options with { Focusable = focusable };
        return this;
    }

    /// <summary>
    /// Starts the window in fullscreen, clearing the maximized and minimized states.
    /// </summary>
    /// <param name="mode">The display mode to use, or <see langword="null"/> for borderless desktop fullscreen.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Fullscreen(DisplayMode? mode = null)
    {
        Options = Options with
        {
            Fullscreen = true,
            FullscreenMode = mode,
            Maximized = false,
            Minimized = false
        };

        return this;
    }

    /// <summary>
    /// Starts the window hidden. Independent of the maximized, minimized, and fullscreen states.
    /// </summary>
    /// <param name="hidden">Whether the window starts hidden.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Hidden(bool hidden = true)
    {
        Options = Options with { Hidden = hidden };
        return this;
    }

    /// <summary>
    /// Enables high pixel density back buffering when the display supports it.
    /// </summary>
    /// <param name="enabled">Whether high pixel density is used.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder HighPixelDensity(bool enabled = true)
    {
        Options = Options with { UseHighPixelDensity = enabled };
        return this;
    }

    /// <summary>
    /// Starts the window maximized, clearing the minimized and fullscreen states.
    /// </summary>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Maximized()
    {
        Options = Options with
        {
            Maximized = true,
            Minimized = false,
            Fullscreen = false
        };

        return this;
    }

    /// <summary>
    /// Sets the maximum size of the window's client area, where zero means no limit.
    /// </summary>
    /// <param name="size">The maximum size.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder MaximumSize(Size size)
    {
        Options = Options with { MaximumSize = size };
        return this;
    }

    /// <summary>
    /// Sets the maximum size of the window's client area, where zero means no limit.
    /// </summary>
    /// <param name="width">The maximum width.</param>
    /// <param name="height">The maximum height.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder MaximumSize(int width, int height) => MaximumSize(new Size(width, height));

    /// <summary>
    /// Starts the window minimized, clearing the maximized and fullscreen states.
    /// </summary>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Minimized()
    {
        Options = Options with
        {
            Minimized = true,
            Maximized = false,
            Fullscreen = false
        };

        return this;
    }

    /// <summary>
    /// Sets the minimum size of the window's client area, where zero means no limit.
    /// </summary>
    /// <param name="size">The minimum size.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder MinimumSize(Size size)
    {
        Options = Options with { MinimumSize = size };
        return this;
    }

    /// <summary>
    /// Sets the minimum size of the window's client area, where zero means no limit.
    /// </summary>
    /// <param name="width">The minimum width.</param>
    /// <param name="height">The minimum height.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder MinimumSize(int width, int height) => MinimumSize(new Size(width, height));

    /// <summary>
    /// Sets the opacity of the window, from 0 (transparent) to 1 (opaque).
    /// </summary>
    /// <param name="opacity">The opacity.</param>
    /// <returns>The same builder for chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="opacity"/> is negative.</exception>
    public WindowBuilder Opacity(float opacity)
    {
        Options = Options with { Opacity = opacity };
        return this;
    }

    /// <summary>
    /// Sets the position of the top-left corner of the window on the screen.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Position(Point position)
    {
        Options = Options with { Position = position };
        return this;
    }

    /// <summary>
    /// Sets the position of the top-left corner of the window on the screen.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Position(int x, int y) => Position(new Point(x, y));

    /// <summary>
    /// Allows the user to resize the window.
    /// </summary>
    /// <param name="resizable">Whether the window is resizable.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Resizable(bool resizable = true)
    {
        Options = Options with { Resizable = resizable };
        return this;
    }

    /// <summary>
    /// Restores the builder to its default settings.
    /// </summary>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder Reset()
    {
        Options = new WindowOptions();
        return this;
    }

    /// <summary>
    /// Uses a transparent back buffer for the window.
    /// </summary>
    /// <param name="enabled">Whether the back buffer is transparent.</param>
    /// <returns>The same builder for chaining.</returns>
    public WindowBuilder TransparentBuffer(bool enabled = true)
    {
        Options = Options with { UseTransparentBuffer = enabled };
        return this;
    }
}

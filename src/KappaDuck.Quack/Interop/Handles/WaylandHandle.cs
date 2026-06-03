// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// Represents a Wayland handle.
/// </summary>
public readonly struct WaylandHandle
{
    internal WaylandHandle(nint display, nint surface)
    {
        Display = display;
        Surface = surface;
    }

    /// <summary>
    /// Gets the wl_display associated with the window.
    /// </summary>
    public nint Display { get; }

    /// <summary>
    /// Gets the wl_surface associated with the window.
    /// </summary>
    public nint Surface { get; }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Windows.Handles;

/// <summary>
/// Represents a X11 handle.
/// </summary>
public readonly struct X11Handle
{
    internal X11Handle(nint display, int window)
    {
        Display = display;
        Window = window;
    }

    /// <summary>
    /// Gets the X11 display associated with the window.
    /// </summary>
    public nint Display { get; }

    /// <summary>
    /// Gets the X11 window associated with the window.
    /// </summary>
    public int Window { get; }
}

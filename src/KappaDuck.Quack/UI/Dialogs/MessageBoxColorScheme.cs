// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.UI.Dialogs;

/// <summary>
/// A custom set of colors used to draw a message box.
/// </summary>
/// <remarks>
/// A color scheme is only applied on platforms where the message box is drawn by the framework itself
/// (such as X11). Native system dialogs ignore it and use the operating system's own styling.
/// Only the red, green and blue channels are used; the alpha channel is ignored.
/// </remarks>
public sealed record MessageBoxColorScheme
{
    /// <summary>
    /// Gets the background color of the message box.
    /// </summary>
    public Color Background { get; init; }

    /// <summary>
    /// Gets the background color of the buttons.
    /// </summary>
    public Color ButtonBackground { get; init; }

    /// <summary>
    /// Gets the border color of the buttons.
    /// </summary>
    public Color ButtonBorder { get; init; }

    /// <summary>
    /// Gets the background color of the button currently selected.
    /// </summary>
    public Color ButtonSelected { get; init; }

    /// <summary>
    /// Gets the color of the message text.
    /// </summary>
    public Color Text { get; init; }
}

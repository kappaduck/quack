// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.UI.Dialogs;

/// <summary>
/// A single button shown in a message box.
/// </summary>
/// <remarks>
/// <see cref="MessageBox.Show(MessageBoxOptions)"/> returns the button the user activated, so you can
/// compare it or read its caller-defined <see cref="Id"/> to tell which button was pressed.
/// </remarks>
public readonly record struct MessageBoxButton
{
    /// <summary>
    /// Initializes a new instance of <see cref="MessageBoxButton"/>.
    /// </summary>
    /// <param name="id">The caller-defined id returned when the user activates the button.</param>
    /// <param name="text">The text shown on the button.</param>
    public MessageBoxButton(int id, string text)
    {
        Id = id;
        Text = text;
    }

    /// <summary>
    /// Gets the caller-defined id returned when the user activates the button.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets a value indicating whether this button is activated when the user presses the Escape key
    /// or otherwise closes the message box.
    /// </summary>
    public bool IsEscapeDefault { get; init; }

    /// <summary>
    /// Gets a value indicating whether this button is activated when the user presses the Return key.
    /// </summary>
    public bool IsReturnDefault { get; init; }

    /// <summary>
    /// Gets the text shown on the button.
    /// </summary>
    public string Text { get; init; }
}

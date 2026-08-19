// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Windowing;

namespace KappaDuck.Quack.UI.Dialogs;

/// <summary>
/// Options that configure a customizable message box shown with <see cref="MessageBox.Show(MessageBoxOptions)"/>.
/// </summary>
public sealed record MessageBoxOptions
{
    /// <summary>
    /// Gets the layout order of the <see cref="Buttons"/>. Defaults to <see cref="MessageBoxButtonOrder.LeftToRight"/>.
    /// </summary>
    public MessageBoxButtonOrder ButtonOrder { get; init; }

    /// <summary>
    /// Gets the buttons shown to the user, from which one must be provided.
    /// </summary>
    public IReadOnlyList<MessageBoxButton> Buttons { get; init; } = [];

    /// <summary>
    /// Gets a custom color scheme, or <see langword="null"/> to use the system colors.
    /// </summary>
    public MessageBoxColorScheme? ColorScheme { get; init; }

    /// <summary>
    /// Gets the message shown below the <see cref="Title"/>.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// Gets the window the message box is modal for, or <see langword="null"/> for no parent.
    /// </summary>
    public Window? Parent { get; init; }

    /// <summary>
    /// Gets the severity of the message box. Defaults to <see cref="MessageBoxSeverity.Information"/>.
    /// </summary>
    public MessageBoxSeverity Severity { get; init; } = MessageBoxSeverity.Information;

    /// <summary>
    /// Gets the title shown in the message box.
    /// </summary>
    public required string Title { get; init; }
}

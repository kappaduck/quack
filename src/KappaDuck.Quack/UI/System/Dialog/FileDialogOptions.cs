// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.UI.System.Dialog;

/// <summary>
/// Represents configuration options for customizing the behavior of a <see cref="FileDialog"/>.
/// </summary>
public sealed record FileDialogOptions : IDialogOptions
{
    /// <summary>
    /// Gets or inits the title of the dialog.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Gets or inits the label displayed for the accept button.
    /// </summary>
    public string? AcceptLabel { get; init; }

    /// <summary>
    /// Gets or inits the label displayed on the cancel button.
    /// </summary>
    public string? CancelLabel { get; init; }

    /// <summary>
    /// Gets or inits the path of the default location to open in the dialog.
    /// </summary>
    public string? Location { get; init; }

    /// <summary>
    /// Gets or inits the window that the dialog should be modal.
    /// </summary>
    public WindowBase? Window { get; init; }

    /// <summary>
    /// Gets or inits the filters to apply in the dialog.
    /// </summary>
    public FileDialogFilter[] Filters { get; init; } = [];
}

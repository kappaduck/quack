// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Windowing;

namespace KappaDuck.Quack.UI.Dialogs;

/// <summary>
/// Options that configure a dialog used to choose a file to save to, shown with
/// <see cref="FileDialog.SaveFileAsync(SaveFileDialogOptions?)"/>.
/// </summary>
/// <remarks>
/// Every option is a hint. Platforms are free to ignore any of them.
/// </remarks>
public sealed record SaveFileDialogOptions
{
    /// <summary>
    /// Gets the label of the accept button, or <see langword="null"/> to use the system default.
    /// </summary>
    public string? AcceptLabel { get; init; }

    /// <summary>
    /// Gets the label of the cancel button, or <see langword="null"/> to use the system default.
    /// </summary>
    public string? CancelLabel { get; init; }

    /// <summary>
    /// Gets the filters that determine the file types offered and the default extension applied to the saved file.
    /// </summary>
    public IReadOnlyList<FileDialogFilter> Filters { get; init; } = [];

    /// <summary>
    /// Gets the folder or file the dialog starts at, or <see langword="null"/> for the platform default.
    /// </summary>
    public string? Location { get; init; }

    /// <summary>
    /// Gets the window the dialog is modal for, or <see langword="null"/> for no parent.
    /// </summary>
    public Window? Parent { get; init; }

    /// <summary>
    /// Gets the title of the dialog, or <see langword="null"/> to use the system default.
    /// </summary>
    public string? Title { get; init; }
}

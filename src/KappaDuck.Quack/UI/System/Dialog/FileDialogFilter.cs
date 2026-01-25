// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Dialog;

/// <summary>
/// Represents a file dialog filter that can be used in file dialogs.
/// </summary>
/// <param name="Name">The name of the file dialog filter.</param>
/// <param name="Patterns">The file patterns associated with the filter that contain alphanumeric characters, hyphens, underscores, periods and the wildcard character '*'.</param>
public sealed record FileDialogFilter(string Name, params string[] Patterns)
{
    /// <summary>
    /// Gets the name of the file dialog filter.
    /// </summary>
    public string Name { get; } = Name;

    /// <summary>
    /// Gets the file patterns associated with the filter.
    /// </summary>
    public string[] Patterns { get; init; } = Patterns;
}

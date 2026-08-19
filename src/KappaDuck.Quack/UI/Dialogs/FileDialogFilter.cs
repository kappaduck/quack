// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.UI.Dialogs;

/// <summary>
/// A filter that restricts which files a file dialog shows to the user.
/// </summary>
/// <remarks>
/// Filters are a hint. Not every platform honors them, and some let the user bypass them.
/// </remarks>
/// <param name="Name">A human-readable label for the filter, such as <c>"Image files"</c>.</param>
/// <param name="Pattern">
/// A semicolon-separated list of file extensions, such as <c>"png;jpg;jpeg"</c>. Extensions may only
/// contain alphanumeric characters, hyphens, underscores and periods. Use a single <c>"*"</c> to match all files.
/// </param>
public sealed record FileDialogFilter(string Name, string Pattern);

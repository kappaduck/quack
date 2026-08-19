// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.UI.Dialogs;

/// <summary>
/// The severity of a message box.
/// </summary>
public enum MessageBoxSeverity
{
    /// <summary>
    /// No specific severity.
    /// </summary>
    None = 0,

    /// <summary>
    /// An informational message box.
    /// </summary>
    Information = 1,

    /// <summary>
    /// A warning message box.
    /// </summary>
    Warning = 2,

    /// <summary>
    /// An error message box.
    /// </summary>
    Error = 3
}

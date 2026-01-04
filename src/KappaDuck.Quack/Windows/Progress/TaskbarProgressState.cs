// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// The taskbar progress state.
/// </summary>
public enum TaskbarProgressState
{
    /// <summary>
    /// Show no progress bar.
    /// </summary>
    None = 0,

    /// <summary>
    /// Show a progress bar in indeterminate state.
    /// </summary>
    Indeterminate = 1,

    /// <summary>
    /// Show a normal progress bar.
    /// </summary>
    Normal = 2,

    /// <summary>
    /// Show a paused progress bar.
    /// </summary>
    Paused = 3,

    /// <summary>
    /// Show an error progress bar to indicate the application has encountered an error.
    /// </summary>
    Error = 4
}

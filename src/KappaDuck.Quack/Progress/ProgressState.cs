// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Progress;

/// <summary>
/// Represents the state of a <see cref="ProgressBar"/>.
/// </summary>
public enum ProgressState
{
    /// <summary>
    /// No progress is shown; the bar is hidden or cleared.
    /// </summary>
    None = 0,

    /// <summary>
    /// A normal, determinate progress value is shown.
    /// </summary>
    Normal = 1,

    /// <summary>
    /// An ongoing operation with no measurable progress is shown.
    /// </summary>
    Indeterminate = 2,

    /// <summary>
    /// An error state is shown.
    /// </summary>
    Error = 3
}

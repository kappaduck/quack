// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.UI.Progress;

/// <summary>
/// Provides data for the <see cref="ProgressBar.ProgressChanged"/> event.
/// </summary>
/// <param name="value">The normalized progress value between <c>0</c> and <c>1</c>.</param>
public sealed class ProgressValueEventArgs(float value) : EventArgs
{
    /// <summary>
    /// Gets the normalized progress value between <c>0</c> and <c>1</c>.
    /// </summary>
    public float Value { get; } = value;
}

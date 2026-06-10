// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Progress;

/// <summary>
/// Provides data for the <see cref="ProgressBar.ErrorOccurred"/> event.
/// </summary>
/// <param name="exception">The exception that was caught during reporting.</param>
public sealed class ProgressErrorEventArgs(Exception exception) : EventArgs
{
    /// <summary>
    /// Gets the exception that was caught during reporting.
    /// </summary>
    public Exception Exception { get; } = exception;
}

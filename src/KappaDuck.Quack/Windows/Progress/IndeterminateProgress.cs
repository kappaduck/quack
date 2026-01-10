// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// Represents a synchronous indeterminate progress reporting for <see cref="WindowProgressBar"/>.
/// </summary>
public sealed class IndeterminateProgress
{
    internal IndeterminateProgress()
    {
    }

    internal bool IsCancelled { get; private set; }

    /// <summary>
    /// Requests cancellation of the progress operation.
    /// </summary>
    /// <remarks>
    /// This will stop any further progress reporting, triggering <see cref="WindowProgressBar.Cancelled"/> and
    /// resetting the progress bar to its default state.
    /// </remarks>
    public void Cancel() => IsCancelled = true;
}

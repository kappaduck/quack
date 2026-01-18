// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;

namespace KappaDuck.Quack.UI.Window.Progress;

/// <summary>
/// Represents a synchronous indeterminate progress reporting for <see cref="WindowProgressBar"/>.
/// </summary>
public sealed class IndeterminateProgress
{
    internal IndeterminateProgress()
    {
    }

    /// <summary>
    /// Requests cancellation of the progress operation.
    /// </summary>
    /// <remarks>
    /// This will stop any further progress reporting, triggering <see cref="WindowProgressBar.Cancelled"/> and
    /// resetting the progress bar to its default state.
    /// </remarks>
    /// <exception cref="OperationCanceledException">Thrown when the operation is cancelled.</exception>
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "The method is part of the instance API.")]
    public void Cancel() => throw new OperationCanceledException("The indeterminate progress operation has been cancelled.");
}

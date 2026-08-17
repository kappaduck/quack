// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;

namespace KappaDuck.Quack.UI.Progress;

/// <summary>
/// Synchronous indeterminate progress reporter handed to
/// <see cref="ProgressBarBase.StartIndeterminate(Action{IndeterminateProgressReporter})"/>.
/// </summary>
public sealed class IndeterminateProgressReporter
{
    internal IndeterminateProgressReporter()
    {
    }

    /// <summary>
    /// Requests cancellation of the progress operation.
    /// </summary>
    /// <remarks>
    /// Throws <see cref="OperationCanceledException"/>, which the base catches to trigger
    /// <see cref="ProgressBarBase.Cancelled"/> and reset the bar.
    /// </remarks>
    /// <exception cref="OperationCanceledException">Always thrown.</exception>
    [DoesNotReturn]
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "The method is part of the instance API.")]
    public void Cancel() => ThrowHelper.ThrowOperationCanceled("The indeterminate progress operation has been cancelled.");
}

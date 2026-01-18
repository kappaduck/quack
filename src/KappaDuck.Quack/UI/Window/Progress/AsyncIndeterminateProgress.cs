// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.Window.Progress;

/// <summary>
/// Represents an asynchronous indeterminate progress reporting for <see cref="WindowProgressBar"/>.
/// </summary>
public sealed class AsyncIndeterminateProgress : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    internal AsyncIndeterminateProgress()
    {
    }

    /// <summary>
    /// Gets the token to monitor the cancellation request during the progress reporting.
    /// </summary>
    public CancellationToken CancellationToken => _cts.Token;

    /// <summary>
    /// Requests cancellation of the progress reporting.
    /// </summary>
    /// <remarks>
    /// This will stop any further progress reporting, triggering <see cref="WindowProgressBar.Cancelled"/> and
    /// resetting the progress bar to its default state.
    /// </remarks>
    public void Cancel()
    {
        if (CancellationToken.IsCancellationRequested)
            return;

        _cts.Cancel();
    }

    /// <summary>
    /// Requests cancellation of the progress reporting after a delay.
    /// </summary>
    /// <param name="delay">The delay after which to cancel the progress reporting.</param>
    public void CancelAfter(TimeSpan delay)
    {
        if (CancellationToken.IsCancellationRequested)
            return;

        _cts.CancelAfter(delay);
    }

    /// <inheritdoc cref="CancelAfter(TimeSpan)"/>
    public void CancelAfter(int millisecondsDelay) => CancelAfter(TimeSpan.FromMilliseconds(millisecondsDelay));

    /// <summary>
    /// Asynchronously requests cancellation of the progress reporting.
    /// </summary>
    /// <returns>A task that represents the asynchronous cancellation.</returns>
    public Task CancelAsync()
    {
        if (CancellationToken.IsCancellationRequested)
            return Task.CompletedTask;

        return _cts.CancelAsync();
    }

    /// <inheritdoc/>
    public void Dispose() => _cts.Dispose();
}

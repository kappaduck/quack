// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.UI.Progress.Reporters;

/// <summary>
/// Asynchronous indeterminate progress reporter handed to
/// <see cref="ProgressBar.StartIndeterminateAsync(Func{AsyncIndeterminateProgressReporter, ValueTask})"/>.
/// </summary>
public sealed class AsyncIndeterminateProgressReporter : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    internal AsyncIndeterminateProgressReporter()
    {
    }

    /// <summary>
    /// Gets the token to observe for cancellation during the reporting.
    /// </summary>
    public CancellationToken CancellationToken => _cts.Token;

    /// <summary>
    /// Requests cancellation of the progress operation.
    /// </summary>
    /// <remarks>
    /// Stops any further reporting, triggers <see cref="ProgressBar.Cancelled"/> and resets the bar.
    /// </remarks>
    public void Cancel()
    {
        if (CancellationToken.IsCancellationRequested)
            return;

        _cts.Cancel();
    }

    /// <summary>
    /// Requests cancellation of the progress operation after a delay.
    /// </summary>
    /// <param name="delay">The delay after which to cancel.</param>
    public void CancelAfter(TimeSpan delay)
    {
        if (CancellationToken.IsCancellationRequested)
            return;

        _cts.CancelAfter(delay);
    }

    /// <inheritdoc cref="CancelAfter(TimeSpan)"/>
    public void CancelAfter(int millisecondsDelay) => CancelAfter(TimeSpan.FromMilliseconds(millisecondsDelay));

    /// <summary>
    /// Asynchronously requests cancellation of the progress operation.
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


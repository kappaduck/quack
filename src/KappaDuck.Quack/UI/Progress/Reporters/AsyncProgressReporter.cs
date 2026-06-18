// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.UI.Progress.Reporters;

/// <summary>
/// Asynchronous determinate progress reporter handed to
/// <see cref="ProgressBar.StartAsync(Func{AsyncProgressReporter, ValueTask}, int)"/>.
/// </summary>
public sealed class AsyncProgressReporter : IDisposable
{
    private readonly IProgressOperation _operation;
    private readonly CancellationTokenSource _cts = new();
    private readonly int _total;

    private int _current;

    internal AsyncProgressReporter(IProgressOperation operation, int total)
    {
        _operation = operation;
        _total = total;
    }

    /// <summary>
    /// Gets the token to observe for cancellation during the reporting.
    /// </summary>
    public CancellationToken CancellationToken => _cts.Token;

    /// <summary>
    /// Reports progress by incrementing by <c>1</c>.
    /// </summary>
    /// <remarks>
    /// This is a shorthand for <see cref="Increment(int)"/> with <c>steps</c> of <c>1</c>.
    /// </remarks>
    /// <exception cref="OperationCanceledException">Cancellation has been requested.</exception>
    public void Advance() => Increment(1);

    /// <summary>
    /// Requests cancellation of the progress operation.
    /// </summary>
    /// <remarks>
    /// The next call to <see cref="Increment(int)"/> or <see cref="Report(int)"/> throws
    /// <see cref="OperationCanceledException"/>, which the base catches to trigger
    /// <see cref="ProgressBar.Cancelled"/> and reset the bar.
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

    /// <summary>
    /// Reports progress by incrementing by a step.
    /// </summary>
    /// <param name="steps">The number of steps to increment.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="steps"/> is negative.</exception>
    /// <exception cref="OperationCanceledException">Cancellation has been requested.</exception>
    public void Increment(int steps)
    {
        CancellationToken.ThrowIfCancellationRequested();

        ArgumentOutOfRangeException.ThrowIfNegative(steps);
        Report(_current + steps);
    }

    /// <summary>
    /// Reports the absolute current progress.
    /// </summary>
    /// <remarks>
    /// The total provided to <see cref="ProgressBar.StartAsync(Func{AsyncProgressReporter, ValueTask}, int)"/> is
    /// used as the maximum limit if <paramref name="current"/> is greater than the total.
    /// </remarks>
    /// <param name="current">The current progress.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="current"/> is negative.</exception>
    /// <exception cref="OperationCanceledException">Cancellation has been requested.</exception>
    public void Report(int current)
    {
        CancellationToken.ThrowIfCancellationRequested();

        ArgumentOutOfRangeException.ThrowIfNegative(current);

        _current = Math.Min(current, _total);
        _operation.Report((float)_current / _total);
    }
}

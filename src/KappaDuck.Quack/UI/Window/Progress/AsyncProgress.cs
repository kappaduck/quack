// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.Window.Progress;

/// <summary>
/// Represents an asynchronous progress reporting for <see cref="WindowProgressBar"/>.
/// </summary>
public sealed class AsyncProgress : IDisposable
{
    private readonly WindowProgressBar _progressBar;
    private readonly CancellationTokenSource _cts = new();
    private readonly int _total;

    private int _current;

    internal AsyncProgress(WindowProgressBar progressBar, int total)
    {
        _progressBar = progressBar;
        _total = total;
    }

    /// <summary>
    /// Gets the token to monitor the cancellation request during the progress reporting.
    /// </summary>
    public CancellationToken CancellationToken => _cts.Token;

    /// <summary>
    /// Reports progress by incrementing by <c>1</c>
    /// </summary>
    /// <remarks>
    /// This is a shorthand for <see cref="Increment(int)"/> with <c>steps</c> of <c>1</c>.
    /// </remarks>
    public void Advance() => Increment(1);

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

    /// <summary>
    /// Reports progress by incrementing by a step.
    /// </summary>
    /// <remarks>
    /// The total provided from <see cref="WindowProgressBar.StartAsync(Func{AsyncProgress, ValueTask}, int)"/> will be used as the maximum limit
    /// if the resulting current progress is greater than the total.
    /// </remarks>
    /// <param name="steps">The number of steps to increment.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="steps"/> is negative.</exception>
    public void Increment(int steps)
    {
        CancellationToken.ThrowIfCancellationRequested();

        ArgumentOutOfRangeException.ThrowIfNegative(steps);
        Report(_current + steps);
    }

    /// <summary>
    /// Reports the current progress.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Uses this method if you want to report an absolute value.
    /// Otherwise use <see cref="Advance"/> or <see cref="Increment(int)"/> for relative value.
    /// </para>
    /// <para>
    /// The total provided from <see cref="WindowProgressBar.StartAsync(Func{AsyncProgress, ValueTask}, int)"/> will be used as the maximum limit
    /// if the <paramref name="current"/> is greater than the total.
    /// </para>
    /// </remarks>
    /// <param name="current">The current progress.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="current"/> is negative.</exception>
    public void Report(int current)
    {
        CancellationToken.ThrowIfCancellationRequested();

        ArgumentOutOfRangeException.ThrowIfNegative(current);

        _current = Math.Min(current, _total);
        _progressBar.Report((float)_current / _total);
    }
}

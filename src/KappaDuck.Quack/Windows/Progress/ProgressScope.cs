// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// Represents a synchronous progress reporting scope for <see cref="WindowProgressBar"/>.
/// </summary>
public sealed class ProgressScope : IDisposable
{
    private readonly WindowProgressBar _taskbar;
    private readonly CancellationTokenSource _cts = new();
    private readonly int _total;

    private int _current;

    internal ProgressScope(WindowProgressBar taskbar, int total)
    {
        _taskbar = taskbar;
        _total = total;
    }

    /// <summary>
    /// Requests cancellation of the progress operation.
    /// </summary>
    /// <remarks>
    /// This will stop any further progress reporting, triggering <see cref="WindowProgressBar.Cancelled"/> and
    /// resetting the progress bar to its default state.
    /// </remarks>
    public void Cancel()
    {
        if (_cts.IsCancellationRequested)
            return;

        _cts.Cancel();
        _taskbar.Cancel();
    }

    /// <summary>
    /// Resets the window progress bar to its default state.
    /// </summary>
    public void Dispose()
    {
        if (_cts.IsCancellationRequested)
            return;

        _cts.Dispose();
        _taskbar.Reset();
    }

    /// <summary>
    /// Reports progress by incrementing by <c>1</c>
    /// </summary>
    /// <remarks>
    /// This is a shorthand for <see cref="Increment(int)"/> with <c>steps</c> of <c>1</c>.
    /// </remarks>
    public void Advance() => Increment(1);

    /// <summary>
    /// Reports progress by incrementing by a step.
    /// </summary>
    /// <remarks>
    /// The total provided from <see cref="WindowProgressBar.Start(int)"/> will be used as the maximum limit
    /// if the resulting current progress is greater than the total.
    /// </remarks>
    /// <param name="steps">The number of steps to increment.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="steps"/> is negative.</exception>
    public void Increment(int steps)
    {
        if (_cts.IsCancellationRequested)
            return;

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
    /// The total provided from <see cref="WindowProgressBar.Start(int)"/> will be used as the maximum limit
    /// if the <paramref name="current"/> is greater than the total.
    /// </para>
    /// </remarks>
    /// <param name="current">The current progress.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="current"/> is negative.</exception>
    public void Report(int current)
    {
        if (_cts.IsCancellationRequested)
            return;

        ArgumentOutOfRangeException.ThrowIfNegative(current);

        _current = Math.Min(current, _total);
        _taskbar.Report((float)_current / _total);
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Progress;

/// <summary>
/// Synchronous determinate progress reporter handed to <see cref="ProgressBar.Start(Action{Progress}, int)"/>.
/// </summary>
public sealed class ProgressReporter
{
    private readonly IProgressOperation _operation;
    private readonly int _total;

    private bool _isCancelled;
    private int _current;

    internal ProgressReporter(IProgressOperation operation, int total)
    {
        _operation = operation;
        _total = total;
    }

    /// <summary>
    /// Reports progress by incrementing by <c>1</c>.
    /// </summary>
    /// <remarks>
    /// This is a shorthand for <see cref="Increment(int)"/> with <c>steps</c> of <c>1</c>.
    /// </remarks>
    public void Advance() => Increment(1);

    /// <summary>
    /// Requests cancellation of the progress operation.
    /// </summary>
    /// <remarks>
    /// Stops any further reporting, triggers <see cref="ProgressBar.Cancelled"/> and resets the bar.
    /// </remarks>
    public void Cancel()
    {
        if (_isCancelled)
            return;

        _isCancelled = true;
        _operation.Cancel();
    }

    /// <summary>
    /// Reports progress by incrementing by a step.
    /// </summary>
    /// <param name="steps">The number of steps to increment.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="steps"/> is negative.</exception>
    public void Increment(int steps)
    {
        if (_isCancelled)
            return;

        ArgumentOutOfRangeException.ThrowIfNegative(steps);
        Report(_current + steps);
    }

    /// <summary>
    /// Reports the absolute current progress.
    /// </summary>
    /// <remarks>
    /// The total provided to <see cref="ProgressBar.Start(Action{Progress}, int)"/> is used as the maximum
    /// limit if <paramref name="current"/> is greater than the total.
    /// </remarks>
    /// <param name="current">The current progress.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="current"/> is negative.</exception>
    public void Report(int current)
    {
        if (_isCancelled)
            return;

        ArgumentOutOfRangeException.ThrowIfNegative(current);

        _current = Math.Min(current, _total);
        _operation.Report((float)_current / _total);
    }
}

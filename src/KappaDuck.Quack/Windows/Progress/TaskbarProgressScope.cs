// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// Represents a synchronous progress scope for reporting progress to the taskbar.
/// </summary>
public sealed class TaskbarProgressScope : IDisposable
{
    private readonly TaskbarProgress _progress;
    private readonly int _total;

    private int _current;

    internal TaskbarProgressScope(TaskbarProgress progress, int total)
    {
        _progress = progress;
        _total = Math.Max(1, total);
    }

    /// <summary>
    /// Sets the taskbar progress to 100%.
    /// </summary>
    public void Dispose()
    {
        if (IsOperationFinished())
            return;

        _progress.State = TaskbarProgressState.None;
    }

    /// <summary>
    /// Reports progress by incrementing the current step count.
    /// </summary>
    /// <param name="steps">The number of steps to increment.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="steps"/> is negative or zero</exception>
    public void ReportStep(int steps = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(steps);

        if (IsOperationFinished())
            return;

        _current += steps;
        _progress.Report((float)_current / _total);
    }

    /// <summary>
    /// Reports the current progress value.
    /// </summary>
    /// <param name="current">The current progress value to be reported.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="current"/> is negative or zero</exception>
    public void Report(int current)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(current);

        if (IsOperationFinished())
            return;

        _current = current;
        _progress.Report((float)_current / _total);
    }

    private bool IsOperationFinished() => _progress.IsCompleted || _progress.IsCancelled;
}

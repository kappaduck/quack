// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// Represents an asynchronous progress reporter for reporting progress to the taskbar.
/// </summary>
public readonly struct AsyncProgress
{
    private readonly TaskbarProgress _progress;
    private readonly int _total;

    internal AsyncProgress(TaskbarProgress progress, int total, CancellationToken token)
    {
        _progress = progress;
        _total = Math.Max(1, total);

        CancellationToken = token;
    }

    /// <summary>
    /// Gets the cancellation token which was passed from <see cref="TaskbarProgress.CreateScopeAsync(Func{AsyncProgress, ValueTask}, int, bool, CancellationToken)"/>
    /// </summary>
    public CancellationToken CancellationToken { get; }

    private bool IsCancelled => _progress.IsCancelled || CancellationToken.IsCancellationRequested;

    /// <summary>
    /// Reports progress by incrementing the current step count.
    /// </summary>
    /// <remarks>
    /// If the <see cref="TaskbarProgress.CancellationToken"/> or <see cref="CancellationToken"/> is cancelled, the method will do nothing.
    /// </remarks>
    /// <param name="steps">The number of steps to increment.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="steps"/> is negative or zero</exception>
    public void ReportStep(int steps = 1)
    {
        if (IsCancelled)
            return;

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(steps);

        _progress.Report(Math.Clamp(((_progress.Value * _total) + steps) / _total, 0f, 1f));
    }

    /// <summary>
    /// Reports the current progress value.
    /// </summary>
    /// <remarks>
    /// If the <see cref="TaskbarProgress.CancellationToken"/> or <see cref="CancellationToken"/> is cancelled, the method will do nothing.
    /// </remarks>
    /// <param name="current">The current progress value to be reported.</param>
    public void Report(int current)
    {
        if (IsCancelled)
            return;

        _progress.Report(Math.Clamp((float)current / _total, 0f, 1f));
    }

    /// <summary>
    /// Reports the current progress value.
    /// </summary>
    /// <remarks>
    /// If the <see cref="TaskbarProgress.CancellationToken"/> or <see cref="CancellationToken"/> is cancelled, the method will do nothing.
    /// </remarks>
    /// <param name="current">The current progress value to be reported.</param>
    /// <param name="total">The maximum value representing 100% progress.</param>
    public void Report(int current, int total)
    {
        if (IsCancelled)
            return;

        _progress.Report(Math.Clamp((float)current / Math.Max(1, total), 0f, 1f));
    }
}

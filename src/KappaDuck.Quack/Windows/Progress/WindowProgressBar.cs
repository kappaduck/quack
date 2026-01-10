// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// Represents the progress bar for a window's taskbar icon.
/// </summary>
public sealed class WindowProgressBar
{
    private const int ThrottleInMilliseconds = 35;

    private readonly Window _window;

    private float _oldValue = -1f;
    private double _lastAppliedMilliseconds;
    private bool _isCompleted;
    private bool _isReporting;

    internal WindowProgressBar(Window window) => _window = window;

    /// <summary>
    /// Occurs when the reporting is cancelled.
    /// </summary>
    public event Action? Cancelled;

    /// <summary>
    /// Occurs when the reporting is completed.
    /// </summary>
    public event Action? Completed;

    /// <summary>
    /// Occurs when the progress value changes.
    /// </summary>
    public event Action<float>? ProgressChanged;

    /// <summary>
    /// Gets or sets a value indicating whether to reset the window progress bar after completion.
    /// </summary>
    /// <remarks>
    /// By default, the value is <see langword="true"/>.
    /// </remarks>
    public bool ResetAfterCompletion { get; set; } = true;

    /// <summary>
    /// Start a new synchronous progress operation which will reporting to the taskbar.
    /// </summary>
    /// <param name="total">The maximum value representing the 100% for the progress.</param>
    /// <returns>The created scope for the progress operation.</returns>
    public ProgressScope Start(int total = 100)
    {
        QuackException.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");

        StartReporting(WindowProgressState.Normal);
        return new(this, total);
    }

    /// <summary>
    /// Start a new synchronous progress operation in indeterminate state which will reporting to the taskbar.
    /// </summary>
    /// <returns>The created scope for the indeterminate progress operation.</returns>
    public IndeterminateScope StartIndeterminate()
    {
        QuackException.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");

        StartReporting(WindowProgressState.Indeterminate);
        return new(this);
    }

    internal void Cancel()
    {
        Cancelled?.Invoke();
        Reset();
    }

    internal void Complete()
    {
        _isCompleted = true;
        _isReporting = false;

        Completed?.Invoke();
    }

    internal void Report(float value)
    {
        if (_isCompleted)
            return;

        float progress = Math.Clamp(value, 0f, 1f);

        if (MathF.IsNearlyZero(progress - _oldValue))
            return;

        SetValue(progress);
        ProgressChanged?.Invoke(progress);

        if (CanComplete())
        {
            Complete();

            if (ResetAfterCompletion)
                Reset();
        }

        bool CanComplete() => !_isCompleted && progress >= 1f;
    }

    internal void Reset()
    {
        _isCompleted = false;
        _isReporting = false;

        _window.Invoke(static handle =>
        {
            Native.SDL_SetWindowProgressState(handle, WindowProgressState.None);
            Native.SDL_SetWindowProgressValue(handle, 0);
        });
    }

    private void StartReporting(WindowProgressState state)
    {
        _isCompleted = false;
        _isReporting = true;

        _window.Invoke(handle => Native.SDL_SetWindowProgressState(handle, state));
    }

    private void SetValue(float value)
    {
        TimeSpan now = QuackEngine.Ticks;

        if (MathF.IsNearlyZero(value - _oldValue) && now.TotalMilliseconds - _lastAppliedMilliseconds < ThrottleInMilliseconds)
            return;

        _oldValue = value;
        _lastAppliedMilliseconds = now.TotalMilliseconds;

        _window.Invoke(handle => Native.SDL_SetWindowProgressValue(handle, value));
    }
}

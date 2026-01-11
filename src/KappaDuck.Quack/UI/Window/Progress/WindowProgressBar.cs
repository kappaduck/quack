// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.UI.Window.Progress;

/// <summary>
/// Represents the progress bar for a window's taskbar icon.
/// </summary>
public sealed class WindowProgressBar
{
    private const int ThrottleInMilliseconds = 35;

    private readonly WindowBase _window;

    private float _oldValue = -1f;
    private double _lastAppliedMilliseconds;

    private bool _isCompleted;
    private bool _isReporting;

    internal WindowProgressBar(WindowBase window) => _window = window;

    /// <summary>
    /// Occurs when the reporting is cancelled.
    /// </summary>
    public event Action? Cancelled;

    /// <summary>
    /// Occurs when the reporting is completed.
    /// </summary>
    public event Action? Completed;

    /// <summary>
    /// Occurs when an error is encountered during the reporting.
    /// </summary>
    public event Action<Exception>? ErrorOccurred;

    /// <summary>
    /// Occurs when the progress value changes.
    /// </summary>
    public event Action<float>? ProgressChanged;

    /// <summary>
    /// Resets the window's progress state and value to their default settings.
    /// </summary>
    /// <remarks>
    /// Use this method to clear any existing progress state or value from the window's taskbar icon. Especially useful after encountering an error.
    /// </remarks>
    /// <exception cref="QuackNativeException">Thrown if the underlying native call fails.</exception>
    public void Reset()
    {
        _isCompleted = false;
        _isReporting = false;

        _window.Invoke(static handle =>
        {
            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowProgressState(handle, WindowProgressState.None));
            QuackNativeException.ThrowIfFailed(Native.SDL_SetWindowProgressValue(handle, 0));
        });
    }

    /// <summary>
    /// Start a synchronous progress operation which will reporting to the taskbar within the provided action.
    /// </summary>
    /// <remarks>
    /// Any unhandled exceptions thrown within the action will be caught and reported to <see cref="ErrorOccurred"/> event and set window's taskbar icon to error state.
    /// </remarks>
    /// <param name="action">The action that performs the progress operation.</param>
    /// <param name="total">The maximum value representing the 100% for the progress.</param>
    /// <exception cref="QuackException">Thrown if a progress report is already in progress.</exception>
    public void Start(Action<Progress> action, int total = 100)
    {
        QuackException.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");
        StartReporting(WindowProgressState.Normal);

        try
        {
            Progress progress = new(this, total);
            action(progress);
        }
        catch (Exception ex)
        {
            Fail(ex);
        }
    }

    /// <summary>
    /// Start a new asynchronous progress operation which will reporting to the taskbar within the provided action.
    /// </summary>
    /// <remarks>
    /// Any unhandled exceptions thrown within the action will be caught and reported to <see cref="ErrorOccurred"/> event and set window's taskbar icon to error state.
    /// </remarks>
    /// <param name="action">The action that performs the progress operation.</param>
    /// <param name="total">The maximum value representing the 100% for the progress.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    public async ValueTask StartAsync(Func<AsyncProgress, ValueTask> action, int total = 100)
    {
        QuackException.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");
        StartReporting(WindowProgressState.Normal);

        try
        {
            using AsyncProgress progress = new(this, total);
            await action(progress).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            Cancel();
        }
        catch (Exception ex)
        {
            Fail(ex);
        }
    }

    /// <summary>
    /// Start a synchronous indeterminate progress operation which will reporting to the taskbar within the provided action.
    /// </summary>
    /// <remarks>
    /// Any unhandled exceptions thrown within the action will be caught and reported to <see cref="ErrorOccurred"/> event and set window's taskbar icon to error state.
    /// </remarks>
    /// <param name="action">The action that performs the progress operation.</param>
    /// <exception cref="QuackException">Thrown if a progress report is already in progress.</exception>"
    public void StartIndeterminate(Action<IndeterminateProgress> action)
    {
        QuackException.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");
        StartReporting(WindowProgressState.Indeterminate);

        try
        {
            IndeterminateProgress progress = new();
            action(progress);

            Complete();
        }
        catch (OperationCanceledException)
        {
            Cancel();
        }
        catch (Exception ex)
        {
            SetValue(0.5f);
            Fail(ex);
        }
    }

    /// <summary>
    /// Start a new asynchronous progress operation in indeterminate state which will reporting to the taskbar within the provided action.
    /// </summary>
    /// <remarks>
    /// Any unhandled exceptions thrown within the action will be caught and reported to <see cref="ErrorOccurred"/> event and set window's taskbar icon to error state.
    /// </remarks>
    /// <param name="action">The action that performs the indeterminate progress operation.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    public async ValueTask StartIndeterminateAsync(Func<AsyncIndeterminateProgress, ValueTask> action)
    {
        QuackException.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");
        StartReporting(WindowProgressState.Indeterminate);

        try
        {
            using AsyncIndeterminateProgress progress = new();
            await action(progress).ConfigureAwait(false);

            Complete();
        }
        catch (OperationCanceledException)
        {
            Cancel();
        }
        catch (Exception ex)
        {
            SetValue(0.5f);
            Fail(ex);
        }
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
        Reset();
    }

    private void Fail(Exception ex)
    {
        _isCompleted = false;
        _isReporting = false;

        ErrorOccurred?.Invoke(ex);
        _window.Invoke(handle => Native.SDL_SetWindowProgressState(handle, WindowProgressState.Error));
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
            Complete();

        bool CanComplete() => !_isCompleted && progress >= 1f;
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

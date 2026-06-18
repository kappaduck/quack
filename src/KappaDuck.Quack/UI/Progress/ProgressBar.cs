// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.UI.Progress.Reporters;

namespace KappaDuck.Quack.UI.Progress;
#pragma warning disable SYSLIB5007, CA2252

/// <summary>
/// Base class for a progress bar backend, e.g. a window's taskbar icon, an on-screen control or a console bar.
/// </summary>
/// <remarks>
/// <para>
/// Owns the reporting lifecycle, value normalization, completion detection and events. A concrete backend
/// only translates a <see cref="ProgressState"/> and a normalized value into its own representation by
/// implementing <see cref="SetState(ProgressState)"/> and <see cref="SetValue(float)"/>.
/// </para>
/// <para>
/// The base is intentionally thread-agnostic: reporting and event raising run on whichever thread calls into
/// it. Any thread affinity (e.g. marshalling onto a UI/main thread) is a backend concern and belongs in the
/// overrides. See <see cref="OnProgressChanged(float)"/> and friends for the event hooks.
/// </para>
/// </remarks>
public abstract class ProgressBar : IProgressOperation
{
    private float _lastValue = -1f;
    private bool _isReporting;
    private bool _isCompleted;

    /// <summary>
    /// Occurs when the reporting is cancelled.
    /// </summary>
    public event EventHandler? Cancelled;

    /// <summary>
    /// Occurs when the reporting is completed.
    /// </summary>
    public event EventHandler? Completed;

    /// <summary>
    /// Occurs when an unhandled exception is encountered during the reporting.
    /// </summary>
    public event EventHandler<ProgressErrorEventArgs>? ErrorOccurred;

    /// <summary>
    /// Occurs when the normalized progress value changes.
    /// </summary>
    public event EventHandler<ProgressValueEventArgs>? ProgressChanged;

    /// <summary>
    /// Raises <see cref="ProgressChanged"/>. Override to control the thread it runs on.
    /// </summary>
    /// <param name="value">The normalized value.</param>
    protected virtual void OnProgressChanged(float value) => ProgressChanged?.Invoke(this, new ProgressValueEventArgs(value));

    /// <summary>
    /// Raises <see cref="Completed"/>. Override to control the thread it runs on.
    /// </summary>
    protected virtual void OnCompleted() => Completed?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Raises <see cref="Cancelled"/>. Override to control the thread it runs on.
    /// </summary>
    protected virtual void OnCancelled() => Cancelled?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Raises <see cref="ErrorOccurred"/>. Override to control the thread it runs on.
    /// </summary>
    /// <param name="exception">The exception that was caught.</param>
    protected virtual void OnErrorOccurred(Exception exception) => ErrorOccurred?.Invoke(this, new ProgressErrorEventArgs(exception));

    /// <summary>
    /// Applies a <paramref name="state"/> to the backend.
    /// </summary>
    /// <param name="state">The state to apply.</param>
    protected abstract void SetState(ProgressState state);

    /// <summary>
    /// Applies a normalized <paramref name="value"/> between <c>0</c> and <c>1</c> to the backend.
    /// </summary>
    /// <remarks>
    /// Already clamped by the base; the backend receives only meaningful changes.
    /// </remarks>
    /// <param name="value">The normalized value between <c>0</c> and <c>1</c>.</param>
    protected abstract void SetValue(float value);

    /// <summary>
    /// Starts a synchronous determinate progress operation.
    /// </summary>
    /// <remarks>
    /// Any unhandled exception thrown within <paramref name="action"/> is caught, reported to
    /// <see cref="ErrorOccurred"/> and switches the backend to <see cref="ProgressState.Error"/>.
    /// </remarks>
    /// <param name="action">The action that performs the progress operation.</param>
    /// <param name="total">The value representing 100% of the progress.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="total"/> value is negative or zero.</exception>
    /// <exception cref="QuackException">A progress report is already in progress.</exception>
    public void Start(Action<ProgressReporter> action, int total = 100)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(total);
        ThrowHelper.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");

        BeginReport(ProgressState.Normal);

        try
        {
            ProgressReporter reporter = new(this, total);
            action(reporter);
        }
        catch (Exception exception)
        {
            Fail(exception);
        }
    }

    /// <summary>
    /// Starts an asynchronous determinate progress operation.
    /// </summary>
    /// <inheritdoc cref="Start(Action{ProgressReporter}, int)" path="/remarks"/>
    /// <param name="action">The action that performs the progress operation.</param>
    /// <param name="total">The value representing 100% of the progress.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    /// <inheritdoc cref="Start(Action{ProgressReporter}, int)" path="/exception"/>
    public async ValueTask StartAsync(Func<AsyncProgressReporter, ValueTask> action, int total = 100)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(total);
        ThrowHelper.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");

        BeginReport(ProgressState.Normal);

        try
        {
            using AsyncProgressReporter reporter = new(this, total);
            await action(reporter).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            Cancel();
        }
        catch (Exception exception)
        {
            Fail(exception);
        }
    }

    /// <summary>
    /// Starts a synchronous indeterminate progress operation.
    /// </summary>
    /// <inheritdoc cref="Start(Action{ProgressReporter}, int)" path="/remarks"/>
    /// <param name="action">The action that performs the progress operation.</param>
    /// <exception cref="QuackException">A progress report is already in progress.</exception>
    public void StartIndeterminate(Action<IndeterminateProgressReporter> action)
    {
        ThrowHelper.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");

        BeginReport(ProgressState.Indeterminate);

        try
        {
            IndeterminateProgressReporter progress = new();
            action(progress);

            Complete();
        }
        catch (OperationCanceledException)
        {
            Cancel();
        }
        catch (Exception exception)
        {
            SetValue(0.5f);
            Fail(exception);
        }
    }

    /// <summary>
    /// Starts an asynchronous indeterminate progress operation.
    /// </summary>
    /// <inheritdoc cref="Start(Action{ProgressReporter}, int)" path="/remarks"/>
    /// <param name="action">The action that performs the progress operation.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    /// <exception cref="QuackException">A progress report is already in progress.</exception>
    public async ValueTask StartIndeterminateAsync(Func<AsyncIndeterminateProgressReporter, ValueTask> action)
    {
        ThrowHelper.ThrowIf(_isReporting, "Cannot begin a new progress report while another is in progress.");

        BeginReport(ProgressState.Indeterminate);

        try
        {
            using AsyncIndeterminateProgressReporter progress = new();
            await action(progress).ConfigureAwait(false);

            Complete();
        }
        catch (OperationCanceledException)
        {
            Cancel();
        }
        catch (Exception exception)
        {
            SetValue(0.5f);
            Fail(exception);
        }
    }

    /// <summary>
    /// Resets the progress bar to its default, empty state.
    /// </summary>
    /// <remarks>
    /// Useful to clear any existing state or value, especially after encountering an error.
    /// </remarks>
    public void Reset()
    {
        _isReporting = false;
        _isCompleted = false;
        _lastValue = -1f;

        SetValue(0f);
        SetState(ProgressState.None);
    }

    void IProgressOperation.Report(float value)
    {
        if (_isCompleted)
            return;

        float progress = Math.Clamp(value, 0f, 1f);

        if (MathF.ApproximatelyZero(progress - _lastValue))
            return;

        _lastValue = progress;

        SetValue(progress);
        OnProgressChanged(progress);

        if (progress >= 1f)
            Complete();
    }

    void IProgressOperation.Cancel() => Cancel();

    private void BeginReport(ProgressState state)
    {
        _isCompleted = false;
        _isReporting = true;
        _lastValue = -1f;

        SetState(state);
    }

    private void Cancel()
    {
        OnCancelled();
        Reset();
    }

    private void Complete()
    {
        if (_isCompleted)
            return;

        _isCompleted = true;
        _isReporting = false;

        OnCompleted();
        Reset();
    }

    private void Fail(Exception exception)
    {
        _isCompleted = false;
        _isReporting = false;

        OnErrorOccurred(exception);
        SetState(ProgressState.Error);
    }
}
#pragma warning restore SYSLIB5007, CA2252

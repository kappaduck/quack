// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// Represents the taskbar progress bar for a window.
/// </summary>
public sealed class TaskbarProgress : IProgress<float>, IDisposable
{
    private const int ThrottleInMilliseconds = 35;

    private readonly Window _window;

    private float _oldValue = -1f;
    private long _lastAppliedTicks;
    private CancellationTokenSource _tokenSource = new();

    internal TaskbarProgress(Window window) => _window = window;

    /// <summary>
    /// Occurs when the operation is canceled.
    /// </summary>
    public event Action? Canceled;

    /// <summary>
    /// Occurs when the operation is completed.
    /// </summary>
    public event Action? Completed;

    /// <summary>
    /// Occurs when an error is encountered during the operation.
    /// </summary>
    public event Action<Exception>? ErrorOccurred;

    /// <summary>
    /// Occurs when the progress value changes.
    /// </summary>
    public event Action<float>? ProgressChanged;

    /// <summary>
    /// Gets the cancellation token associated with this taskbar progress.
    /// </summary>
    public CancellationToken CancellationToken => _tokenSource.Token;

    /// <summary>
    /// Gets a value indicating whether cancellation has been requested for the associated operation.
    /// </summary>
    public bool IsCancelled => CancellationToken.IsCancellationRequested;

    /// <summary>
    /// Gets a value indicating whether the operation has completed.
    /// </summary>
    public bool IsCompleted { get; private set; }

    /// <summary>
    /// Gets or sets the taskbar progress bar state.
    /// </summary>
    public TaskbarProgressState State
    {
        get;
        set
        {
            if (field == value)
                return;

            field = value;
            IsCompleted = false;

            _window.Invoke(() => Native.SDL_SetWindowProgressState(_window.NativeHandle, field));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to reset the taskbar progress after the operation completes.
    /// </summary>
    /// <remarks>
    /// By default, the value is <see langword="true"/> which resets the state to <see cref="TaskbarProgressState.None"/> and the progress to <c>0</c>.
    /// </remarks>
    public bool ResetAfterCompletion { get; set; } = true;

    /// <summary>
    /// Gets the progress value.
    /// </summary>
    /// <remarks>
    /// The value is clamped between <c>0</c> and <c>1</c>.
    /// </remarks>
    public float Value { get; private set; }

    /// <summary>
    /// Requests to cancel the operation.
    /// </summary>
    public void Cancel()
    {
        if (_tokenSource.IsCancellationRequested)
            return;

        _tokenSource.Cancel();
        Canceled?.Invoke();

        Reset();
    }

    /// <summary>
    /// Creates a indeterminate scope which reports the progress to the taskbar.
    /// </summary>
    /// <remarks>
    /// The scope will set the taskbar progress state to <see cref="TaskbarProgressState.Indeterminate"/> during
    /// the progress and will reset the state to <see cref="TaskbarProgressState.None"/> upon disposal.
    /// </remarks>
    /// <returns>The created indeterminate scope.</returns>
    public IDisposable CreateIndeterminateScope()
    {
        ResetCancellation();

        State = TaskbarProgressState.Indeterminate;
        return new IndeterminateScope(this);
    }

    /// <summary>
    /// Creates a scope which reports progress to the taskbar.
    /// </summary>
    /// <remarks>
    /// The scope will set the taskbar progress state to <see cref="TaskbarProgressState.Normal"/>
    /// upon creation and will set the progress value to 100% upon disposal.
    /// </remarks>
    /// <param name="total">The maximum value representing 100% progress.</param>
    /// <returns>The created progress scope.</returns>
    public TaskbarProgressScope CreateScope(int total = 100)
    {
        ResetCancellation();

        State = TaskbarProgressState.Normal;
        Report(0);

        return new(this, total);
    }

    /// <summary>
    /// Creates an asynchronous scope which reports progress to the taskbar.
    /// </summary>
    /// <param name="action">The asynchronous action to execute with progress reporting.</param>
    /// <param name="total">The maximum value representing 100% progress.</param>
    /// <param name="indeterminate"><see langword="true"/> to set the progress state to indeterminate; otherwise, <see langword="false"/>.</param>
    /// <param name="cancellationToken">The cancellation token to observe.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    public async ValueTask CreateScopeAsync(Func<AsyncProgress, ValueTask> action, int total = 100, bool indeterminate = false, CancellationToken cancellationToken = default)
    {
        ResetCancellation();

        State = !indeterminate ? TaskbarProgressState.Normal : TaskbarProgressState.Indeterminate;
        Report(0);

        //using CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(_tokenSource.Token, cancellationToken);

        try
        {
            AsyncProgress progress = new(this, total, cancellationToken);
            await action(progress).ConfigureAwait(false);

            State = TaskbarProgressState.None;
        }
        catch (OperationCanceledException)
        {
            Canceled?.Invoke();
            Reset();
        }
        catch (Exception ex)
        {
            State = TaskbarProgressState.Error;
            ErrorOccurred?.Invoke(ex);
        }
    }

    /// <summary>
    /// Resets the taskbar progress to <see cref="TaskbarProgressState.None"/> and the progress to <c>0</c>.
    /// </summary>
    public void Reset()
    {
        State = TaskbarProgressState.None;
        Report(0);
    }

    /// <inheritdoc/>
    public void Dispose() => _tokenSource.Dispose();

    /// <summary>
    /// Converts the taskbar progress bar to a weighted taskbar progress.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this method when a weighted aggregation of progress is required, such as when combining
    /// multiple progress sources with different weights.
    /// </para>
    /// <para>
    /// This will set the state to <see cref="TaskbarProgressState.Normal"/> and set the progress to <c>0</c>.
    /// </para>
    /// </remarks>
    /// <returns>The weighted root.</returns>
    public IWeightedProgress ToWeightedRoot()
    {
        State = TaskbarProgressState.Normal;
        Report(0);

        return new WeightedProgress(this, 0f, 1f);
    }

    internal void Complete(bool forceReset = false)
    {
        IsCompleted = true;
        Completed?.Invoke();

        if (ResetAfterCompletion || forceReset)
            Reset();
    }

    /// <inheritdoc/>
    public void Report(float value)
    {
        if (IsCancelled || IsCompleted)
            return;

        float progress = Math.Clamp(value, 0f, 1f);

        if (MathF.IsNearlyZero(Value - progress))
            return;

        ApplyValue(progress);
        ProgressChanged?.Invoke(progress);

        if (CanComplete())
            Complete();

        bool CanComplete() => !IsCompleted && State is TaskbarProgressState.Normal && progress >= 1f;
    }

    private void ApplyValue(float value)
    {
        TimeSpan now = QuackEngine.Ticks;

        if (MathF.IsNearlyZero(value - _oldValue) && now.Ticks - _lastAppliedTicks < ThrottleInMilliseconds)
            return;

        Value = value;
        _oldValue = value;
        _lastAppliedTicks = now.Ticks;

        _window.Invoke(() => Native.SDL_SetWindowProgressValue(_window.NativeHandle, value));
    }

    private void ResetCancellation()
    {
        if (_tokenSource.IsCancellationRequested)
        {
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
        }
    }
}

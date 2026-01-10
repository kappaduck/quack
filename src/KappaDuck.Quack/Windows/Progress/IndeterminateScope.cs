// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// Represents a synchronous indeterminate progress reporting scope for <see cref="WindowProgressBar"/>.
/// </summary>
public sealed class IndeterminateScope : IDisposable
{
    private readonly WindowProgressBar _progressBar;
    private bool _isCancelled;

    internal IndeterminateScope(WindowProgressBar progressBar) => _progressBar = progressBar;

    /// <summary>
    /// Requests cancellation of the progress operation.
    /// </summary>
    /// <remarks>
    /// This will stop any further progress reporting, triggering <see cref="WindowProgressBar.Cancelled"/> and
    /// resetting the progress bar to its default state.
    /// </remarks>
    public void Cancel()
    {
        if (_isCancelled)
            return;

        _isCancelled = true;
        _progressBar.Cancel();
    }

    /// <summary>
    /// Resets the window progress bar to its default state.
    /// </summary>
    public void Dispose()
    {
        if (_isCancelled)
            return;

        _isCancelled = false;

        _progressBar.Complete();
        _progressBar.Reset();
    }
}

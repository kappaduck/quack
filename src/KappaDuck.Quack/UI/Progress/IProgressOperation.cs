// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.UI.Progress;

/// <summary>
/// Represents a progress operation that can be reported or cancelled.
/// </summary>
public interface IProgressOperation
{
    /// <summary>
    /// Requests cancellation of the current progress operation.
    /// </summary>
    void Cancel();

    /// <summary>
    /// Pauses the current progress operation, holding its value and switching to <see cref="ProgressState.Paused"/>.
    /// </summary>
    /// <remarks>
    /// Further reporting is ignored until <see cref="Resume"/> is called.
    /// </remarks>
    void Pause();

    /// <summary>
    /// Reports a normalized progress value between <c>0</c> and <c>1</c>.
    /// </summary>
    /// <param name="value">The normalized value. Values outside the range are clamped by the sink.</param>
    void Report(float value);

    /// <summary>
    /// Resumes a paused progress operation, returning to its previous state.
    /// </summary>
    void Resume();
}

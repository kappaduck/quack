// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Progress;

/// <summary>
/// Represents a progress operation that can be reported or cancelled.
/// </summary>
public interface IProgressOperation
{
    /// <summary>
    /// Reports a normalized progress value between <c>0</c> and <c>1</c>.
    /// </summary>
    /// <param name="value">The normalized value. Values outside the range are clamped by the sink.</param>
    void Report(float value);

    /// <summary>
    /// Requests cancellation of the current progress operation.
    /// </summary>
    void Cancel();

}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

/// <summary>
/// Represents a progress that supports weighting for composite progress reporting.
/// </summary>
public interface IWeightedProgress : IProgress<float>
{
    /// <summary>
    /// Creates a weighted progress.
    /// </summary>
    /// <param name="weight">The weight of the progress.</param>
    /// <returns>The created weighted progress.</returns>
    IWeightedProgress Create(float weight);
}

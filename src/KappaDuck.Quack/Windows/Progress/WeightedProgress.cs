// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

internal sealed class WeightedProgress(TaskbarProgress root, float start, float range) : IWeightedProgress
{
    private float _allocated;

    public IWeightedProgress Create(float weight)
    {
        float clampedWeight = Math.Clamp(weight, 0f, 1f);
        float childRange = range * clampedWeight;

        WeightedProgress child = new(root, start + _allocated, childRange);
        _allocated += childRange;

        return child;
    }

    public void Report(float value) => root.Report(start + (range * Math.Clamp(value, 0f, 1f)));
}

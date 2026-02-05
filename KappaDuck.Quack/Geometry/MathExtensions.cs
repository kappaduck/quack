// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;

namespace KappaDuck.Quack.Geometry;

internal static class MathExtensions
{
    private const float GeometryEpsilon = 1e-6f;

    internal const float GeometryEpsilonSquared = GeometryEpsilon * GeometryEpsilon;

    internal const float NormalizedEpsilon = 1e-6f;

    extension(Math)
    {
        internal static void ThrowIfDividedByZero<T>(T value) where T : INumber<T>
        {
            if (T.IsZero(value))
                throw new DivideByZeroException();
        }
    }

    extension(MathF)
    {
        internal static bool ApproximatelyZero(float value) => MathF.Abs(value) < GeometryEpsilon;

        internal static bool ApproximatelyEqual(float left, float right) => MathF.Abs(left - right) < GeometryEpsilon;
    }
}

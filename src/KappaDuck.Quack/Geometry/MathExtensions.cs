// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;

namespace KappaDuck.Quack.Geometry;

internal static class MathExtensions
{
    /// <summary>
    /// The machine epsilon for <see cref="float"/>. It is based on C++'s FLT_EPSILON.
    /// </summary>
    private const float MachineEpsilon = 1.192092896e-07f;

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
        internal static bool IsNearlyZero(float value) => MathF.Abs(value) < MachineEpsilon;
    }
}

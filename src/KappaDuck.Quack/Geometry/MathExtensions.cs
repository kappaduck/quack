// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;

namespace KappaDuck.Quack.Geometry;

internal static class MathExtensions
{
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
        /// <summary>
        /// Determines whether the value is zero within a small tolerance.
        /// </summary>
        /// <remarks>
        /// The tolerance is based on the C++'s FLT_EPSILON constant.
        /// </remarks>
        /// <param name="value">The value to check.</param>
        /// <returns><see langword="true"/> if the value is nearly zero; otherwise, <see langword="false"/>.</returns>
        internal static bool IsNearlyZero(float value) => MathF.Abs(value) < 1.192092896e-07f;
    }
}

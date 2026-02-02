// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;

namespace KappaDuck.Quack.Geometry;

internal static class MathExtensions
{
    [SuppressMessage("Style", "IDE0051:Remove unused private members", Justification = "Visual Studio has a bug where it thinks the property is not used.")]
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
        internal static bool ApproximatelyZero(float value) => MathF.Abs(value) < MachineEpsilon;

        internal static bool ApproximatelyEqual(float left, float right) => MathF.Abs(left - right) < MachineEpsilon;
    }
}

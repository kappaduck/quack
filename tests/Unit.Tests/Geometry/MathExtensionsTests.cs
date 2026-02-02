// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

public sealed class MathExtensionsTests
{
    [Test]
    public async Task ThrowIfDividedByZeroShouldThrowTheGoodException()
    {
        await Assert.That(() => MathExtensions.ThrowIfDividedByZero(0)).ThrowsExactly<DivideByZeroException>();
    }

    [Test]
    [Arguments(0)]
    [Arguments(1.192092896e-08f)]
    public async Task ApproximatelyZeroShouldReturnTrueWhenValueIsZero(float value)
    {
        bool result = MathExtensions.ApproximatelyZero(value);
        await Assert.That(result).IsTrue();
    }

    [Test]
    [Arguments(1)]
    [Arguments(0.5f)]
    public async Task ApproximatelyZeroShouldReturnFalseWhenValueIsNotZero(float value)
    {
        bool result = MathExtensions.ApproximatelyZero(value);
        await Assert.That(result).IsFalse();
    }

    [Test]
    [Arguments(5, 5)]
    [Arguments(-3, -3)]
    [Arguments(42.0001, 42.0001)]
    public async Task ApproximatelyEqualsShouldReturnTrueWhenValueAreEqual(float left, float right)
    {
        bool result = MathExtensions.ApproximatelyEqual(left, right);
        await Assert.That(result).IsTrue();
    }

    [Test]
    [Arguments(5.5, 5)]
    [Arguments(-3, -4)]
    [Arguments(42.0001, 42.00001)]
    public async Task ApproximatelyEqualsShouldReturnFalseWhenValueAreNotEqual(float left, float right)
    {
        bool result = MathExtensions.ApproximatelyEqual(left, right);
        await Assert.That(result).IsFalse();
    }
}

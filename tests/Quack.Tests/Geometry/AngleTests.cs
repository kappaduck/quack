// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using System.Globalization;
using TUnit.Assertions.AssertConditions.Throws;

namespace Quack.Tests.Geometry;

public sealed class AngleTests
{
    [Test]
    public async Task FromRadiansShouldCreateAngleWithGoodValues()
    {
        Angle angle = Angle.FromRadians(MathF.PI);

        await Assert.That(angle.Degrees).IsEqualTo(180f);
        await Assert.That(angle.Radians).IsEqualTo(MathF.PI);
    }

    [Test]
    public async Task FromDegreesShouldCreateAngleWithGoodValues()
    {
        Angle angle = Angle.FromDegrees(180f);

        await Assert.That(angle.Degrees).IsEqualTo(180f);
        await Assert.That(angle.Radians).IsEqualTo(MathF.PI);
    }

    [Test]
    public async Task SinShouldCalculateSine()
    {
        Angle angle = Angle.FromDegrees(90f);

        await Assert.That(angle.Sin).IsEqualTo(1f);
    }

    [Test]
    public async Task CosShouldCalculateCosine()
    {
        Angle angle = Angle.FromDegrees(0f);

        await Assert.That(angle.Cos).IsEqualTo(1f);
    }

    [Test]
    public async Task TanShouldCalculateTangent()
    {
        Angle angle = Angle.FromDegrees(45f);

        await Assert.That(angle.Tan).IsEqualTo(1f);
    }

    [Test]
    public async Task ZeroShouldBeZeroAngle()
    {
        Angle angle = Angle.Zero;

        await Assert.That(angle.Radians).IsEqualTo(0f);
        await Assert.That(angle.Degrees).IsEqualTo(0f);
    }

    [Test]
    public async Task AngleShouldBeImplicitlyConvertibleFromFloat()
    {
        Angle angle = 1.0f;

        await Assert.That(angle.Radians).IsEqualTo(1.0f);
    }

    [Test]
    public async Task AngleShouldBeExplicitlyConvertibleToFloat()
    {
        Angle angle = Angle.FromRadians(1.0f);
        float radians = (float)angle;

        await Assert.That(radians).IsEqualTo(1.0f);
    }

    [Test]
    public async Task AngleShouldBeAddable()
    {
        Angle left = Angle.FromDegrees(30f);
        Angle right = Angle.FromDegrees(60f);

        Angle result = left + right;

        await Assert.That(result.Degrees).IsEqualTo(90f);
        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 2);
    }

    [Test]
    public async Task AngleShouldBeSubtractable()
    {
        Angle left = Angle.FromDegrees(90f);
        Angle right = Angle.FromDegrees(30f);

        Angle result = left - right;

        await Assert.That(result.Degrees).IsEqualTo(60f);
        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 3);
    }

    [Test]
    public async Task AngleShouldBeMultipliableByScalar()
    {
        Angle angle = Angle.FromDegrees(30f);
        const float scalar = 2f;

        Angle result = angle * scalar;

        await Assert.That(result.Degrees).IsEqualTo(60f);
        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 3);
    }

    [Test]
    public async Task AngleShouldBeInvertedMultipliableByScalar()
    {
        Angle angle = Angle.FromDegrees(60f);
        const float scalar = 0.5f;

        Angle result = scalar * angle;

        await Assert.That(result.Degrees).IsEqualTo(30f);
        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 6);
    }

    [Test]
    public async Task AngleShouldBeDivisibleByScalar()
    {
        Angle angle = Angle.FromDegrees(90f);
        const float scalar = 2f;

        Angle result = angle / scalar;

        await Assert.That(result.Degrees).IsEqualTo(45f);
        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 4);
    }

    [Test]
    public async Task AngleShouldThrowOnDivisionByZero()
    {
        Angle angle = Angle.FromDegrees(90f);
        const float scalar = 0f;

        await Assert.That(() => angle / scalar).ThrowsExactly<DivideByZeroException>();
    }

    [Test]
    public async Task UnaryMinusShouldNegateAngle()
    {
        Angle angle = Angle.FromDegrees(30f);
        Angle result = -angle;

        await Assert.That(result.Degrees).IsEqualTo(-30f);
        await Assert.That(result.Radians).IsEqualTo(-MathF.PI / 6);
    }

    [Test]
    public async Task TwoSameAngleUsingEqualOperatorShouldReturnTrue()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(45f);

        bool areEqual = left == right;

        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task TwoDifferentAnglesUsingEqualOperatorShouldReturnFalse()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(90f);

        bool areEqual = left == right;

        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task TwoSameAnglesUsingInequalityOperatorShouldReturnFalse()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(45f);

        bool areNotEqual = left != right;

        await Assert.That(areNotEqual).IsFalse();
    }

    [Test]
    public async Task TwoDifferentAnglesUsingInequalityOperatorShouldReturnTrue()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(90f);

        bool areNotEqual = left != right;

        await Assert.That(areNotEqual).IsTrue();
    }

    [Test]
    public async Task LessThanOperatorShouldReturnTrueForSmallerAngle()
    {
        Angle left = Angle.FromDegrees(30f);
        Angle right = Angle.FromDegrees(60f);

        bool isLessThan = left < right;

        await Assert.That(isLessThan).IsTrue();
    }

    [Test]
    public async Task LessThanOperatorShouldReturnFalseForLargerAngle()
    {
        Angle left = Angle.FromDegrees(60f);
        Angle right = Angle.FromDegrees(30f);

        bool isLessThan = left < right;

        await Assert.That(isLessThan).IsFalse();
    }

    [Test]
    public async Task GreaterThanOperatorShouldReturnTrueForLargerAngle()
    {
        Angle left = Angle.FromDegrees(60f);
        Angle right = Angle.FromDegrees(30f);

        bool isGreaterThan = left > right;

        await Assert.That(isGreaterThan).IsTrue();
    }

    [Test]
    public async Task GreaterThanOperatorShouldReturnFalseForSmallerAngle()
    {
        Angle left = Angle.FromDegrees(30f);
        Angle right = Angle.FromDegrees(60f);

        bool isGreaterThan = left > right;

        await Assert.That(isGreaterThan).IsFalse();
    }

    [Test]
    public async Task LessThanOrEqualOperatorShouldReturnTrueForEqualAngles()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(45f);

        bool isLessThanOrEqual = left <= right;

        await Assert.That(isLessThanOrEqual).IsTrue();
    }

    [Test]
    public async Task LessThanOrEqualOperatorShouldReturnTrueForSmallerAngle()
    {
        Angle left = Angle.FromDegrees(30f);
        Angle right = Angle.FromDegrees(60f);

        bool isLessThanOrEqual = left <= right;

        await Assert.That(isLessThanOrEqual).IsTrue();
    }

    [Test]
    public async Task LessThanOrEqualOperatorShouldReturnFalseForLargerAngle()
    {
        Angle left = Angle.FromDegrees(60f);
        Angle right = Angle.FromDegrees(30f);

        bool isLessThanOrEqual = left <= right;

        await Assert.That(isLessThanOrEqual).IsFalse();
    }

    [Test]
    public async Task GreaterThanOrEqualOperatorShouldReturnTrueForEqualAngles()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(45f);

        bool isGreaterThanOrEqual = left >= right;

        await Assert.That(isGreaterThanOrEqual).IsTrue();
    }

    [Test]
    public async Task GreaterThanOrEqualOperatorShouldReturnTrueForLargerAngle()
    {
        Angle left = Angle.FromDegrees(60f);
        Angle right = Angle.FromDegrees(30f);

        bool isGreaterThanOrEqual = left >= right;

        await Assert.That(isGreaterThanOrEqual).IsTrue();
    }

    [Test]
    public async Task GreaterThanOrEqualOperatorShouldReturnFalseForSmallerAngle()
    {
        Angle left = Angle.FromDegrees(30f);
        Angle right = Angle.FromDegrees(60f);

        bool isGreaterThanOrEqual = left >= right;

        await Assert.That(isGreaterThanOrEqual).IsFalse();
    }

    [Test]
    public async Task NormalizeShouldReturnNormalizedAngle()
    {
        Angle angle = Angle.FromDegrees(450f);

        Angle normalized = angle.Normalize();

        await Assert.That(normalized.Degrees).IsEqualTo(90f);
    }

    [Test]
    public async Task NormalizeShouldReturnNormalizedAngleWithCustomRange()
    {
        Angle angle = Angle.FromDegrees(450f);

        Angle normalized = angle.Normalize(0f, 180f);

        await Assert.That(normalized.Degrees).IsEqualTo(90f);
    }

    [Test]
    public async Task NormalizeShouldReturnNormalizedAngleWithNegativeRange()
    {
        Angle angle = Angle.FromDegrees(-450f);

        Angle normalized = angle.Normalize(-180f, 0f);

        await Assert.That(normalized.Degrees).IsEqualTo(-90f);
    }

    [Test]
    public async Task CompareToShouldReturnZeroForEqualAngles()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(45f);

        int comparison = left.CompareTo(right);

        await Assert.That(comparison).IsEqualTo(0);
    }

    [Test]
    public async Task CompareToShouldReturnNegativeForSmallerAngle()
    {
        Angle left = Angle.FromDegrees(30f);
        Angle right = Angle.FromDegrees(60f);

        int comparison = left.CompareTo(right);

        await Assert.That(comparison).IsEqualTo(-1);
    }

    [Test]
    public async Task CompareToShouldReturnPositiveForLargerAngle()
    {
        Angle left = Angle.FromDegrees(60f);
        Angle right = Angle.FromDegrees(30f);

        int comparison = left.CompareTo(right);

        await Assert.That(comparison).IsEqualTo(1);
    }

    [Test]
    public async Task CompareToShouldReturnPositiveForNullObject()
    {
        Angle angle = Angle.FromDegrees(45f);

        int comparison = angle.CompareTo(null);

        await Assert.That(comparison).IsEqualTo(1);
    }

    [Test]
    public async Task CompareToShouldThrowForNonAngleObject()
    {
        Angle angle = Angle.FromDegrees(45f);

        await Assert.That(() => angle.CompareTo("not an angle"))
            .ThrowsExactly<ArgumentException>()
            .WithMessage("Object must be of type Angle (Parameter 'obj')")
            .WithParameterName("obj");
    }

    [Test]
    public async Task CompareToShouldCompareWhenObjectIsAngle()
    {
        Angle left = Angle.FromDegrees(45f);
        object right = Angle.FromDegrees(30f);

        int comparison = left.CompareTo(right);

        await Assert.That(comparison).IsGreaterThan(0);
    }

    [Test]
    public async Task EqualsShouldReturnTrueForSameAngle()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(45f);

        bool isEqual = left.Equals(right);

        await Assert.That(isEqual).IsTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForDifferentAngles()
    {
        Angle left = Angle.FromDegrees(45f);
        Angle right = Angle.FromDegrees(30f);

        bool isEqual = left.Equals(right);

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForNull()
    {
        Angle angle = Angle.FromDegrees(45f);

        bool isEqual = angle.Equals(null);

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForNonAngleObject()
    {
        Angle angle = Angle.FromDegrees(45f);

        bool isEqual = angle.Equals("not an angle");

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueForSameAngleAsObject()
    {
        Angle left = Angle.FromDegrees(45f);
        object right = Angle.FromDegrees(45f);

        bool isEqual = left.Equals(right);

        await Assert.That(isEqual).IsTrue();
    }

    [Test]
    public async Task ToStringShouldReturnFormattedAngle()
    {
        Angle angle = Angle.FromDegrees(45f);

        string result = angle.ToString();

        await Assert.That(result).IsEqualTo("45°");
    }

    [Test]
    public async Task ToStringShouldReturnFormattedAngleWithCustomFormat()
    {
        Angle angle = Angle.FromDegrees(45f);

        string result = angle.ToString("F2", CultureInfo.InvariantCulture);

        await Assert.That(result).IsEqualTo("45°");
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using TUnit.Assertions.AssertConditions.Throws;

namespace Quack.Tests.Geometry;

public sealed class AngleTests
{
    [Test]
    public async Task RadiansShouldReturnCorrectValue()
    {
        const float radians = MathF.PI / 4;
        Angle angle = Angle.FromRadians(radians);

        await Assert.That(angle.Radians).IsEqualTo(radians);
    }

    [Test]
    public async Task RadiansShouldConvertDegreesToRadians()
    {
        Angle angle = Angle.FromDegrees(90.0f);

        await Assert.That(angle.Radians).IsEqualTo(MathF.PI / 2);
    }

    [Test]
    public async Task DegreesShouldConvertRadiansToDegrees()
    {
        Angle angle = Angle.FromRadians(MathF.PI / 2);

        await Assert.That(angle.Degrees).IsEqualTo(90.0f);
    }

    [Test]
    public async Task DegreesShouldReturnCorrectValue()
    {
        const float degrees = 45.0f;
        Angle angle = Angle.FromDegrees(degrees);

        await Assert.That(angle.Degrees).IsEqualTo(degrees);
    }

    [Test]
    public async Task SinShouldReturnCorrectValue()
    {
        const float degrees = 30.0f;
        Angle angle = Angle.FromDegrees(degrees);

        await Assert.That(angle.Sin).IsEqualTo(0.5f);
    }

    [Test]
    public async Task CosShouldReturnCorrectValue()
    {
        const float degrees = 60.0f;
        Angle angle = Angle.FromDegrees(degrees);

        await Assert.That(angle.Cos).IsEqualTo(0.5f).Within(0.0001f);
    }

    [Test]
    public async Task TanShouldReturnCorrectValue()
    {
        const float degrees = 45.0f;
        Angle angle = Angle.FromDegrees(degrees);

        await Assert.That(angle.Tan).IsEqualTo(1.0f);
    }

    [Test]
    public async Task ZeroAngleShouldHaveZeroInRadiansAndDegrees()
    {
        Angle zero = Angle.Zero;

        await Assert.That(zero.Radians).IsEqualTo(0f);
        await Assert.That(zero.Degrees).IsEqualTo(0f);
    }

    [Test]
    public async Task ImplicitConversionFromFloatToAngleShouldWork()
    {
        const float radians = MathF.PI / 4;
        Angle angle = radians;

        await Assert.That(angle.Radians).IsEqualTo(radians);
    }

    [Test]
    public async Task ImplicitConversionFromAngleToFloatShouldWork()
    {
        Angle angle = Angle.FromDegrees(90.0f);

        float result = (float)angle;

        await Assert.That(result).IsEqualTo(angle.Radians);
    }

    [Test]
    public async Task AdditionShouldReturnCorrectResult()
    {
        Angle left = Angle.FromDegrees(30.0f);
        Angle right = Angle.FromDegrees(60.0f);

        Angle result = left + right;

        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 2);
        await Assert.That(result.Degrees).IsEqualTo(90.0f);
    }

    [Test]
    public async Task SubtractionShouldReturnCorrectResult()
    {
        Angle left = Angle.FromDegrees(60.0f);
        Angle right = Angle.FromDegrees(30.0f);

        Angle result = left - right;

        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 6);
        await Assert.That(result.Degrees).IsEqualTo(30.0f);
    }

    [Test]
    public async Task MultiplicationShouldReturnCorrectResult()
    {
        Angle angle = Angle.FromDegrees(30.0f);
        const float multiplier = 2.0f;

        Angle result = angle * multiplier;

        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 3);
        await Assert.That(result.Degrees).IsEqualTo(60.0f);
    }

    [Test]
    public async Task InvertedMultiplicationShouldReturnCorrectResult()
    {
        Angle angle = Angle.FromDegrees(30.0f);
        const float multiplier = 2.0f;

        Angle result = multiplier * angle;

        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 3);
        await Assert.That(result.Degrees).IsEqualTo(60.0f);
    }

    [Test]
    public async Task DivisionShouldReturnCorrectResult()
    {
        Angle angle = Angle.FromDegrees(60.0f);
        const float divisor = 2.0f;

        Angle result = angle / divisor;

        await Assert.That(result.Radians).IsEqualTo(MathF.PI / 6);
        await Assert.That(result.Degrees).IsEqualTo(30.0f);
    }

    [Test]
    public void DivisionByZeroShouldThrowException()
    {
        Angle angle = Angle.FromDegrees(30.0f);

        Assert.Throws<DivideByZeroException>(() => _ = angle / 0f);
    }

    [Test]
    public async Task UnaryMinusShouldReturnCorrectResult()
    {
        Angle angle = Angle.FromDegrees(30.0f);

        Angle result = -angle;

        await Assert.That(result.Radians).IsEqualTo(-MathF.PI / 6);
        await Assert.That(result.Degrees).IsEqualTo(-30.0f);
    }

    [Test]
    public async Task TwoSameAnglesShouldBeEqual()
    {
        Angle angle1 = Angle.FromDegrees(45.0f);
        Angle angle2 = Angle.FromDegrees(45.0f);

        bool areEqual = angle1 == angle2;

        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task TwoDifferentAnglesShouldNotBeEqual()
    {
        Angle angle1 = Angle.FromDegrees(30.0f);
        Angle angle2 = Angle.FromDegrees(60.0f);

        bool areNotEqual = angle1 != angle2;

        await Assert.That(areNotEqual).IsTrue();
    }

    [Test]
    public async Task AngleShouldBeLessThanAnotherAngle()
    {
        Angle angle1 = Angle.FromDegrees(30.0f);
        Angle angle2 = Angle.FromDegrees(60.0f);

        bool isLessThan = angle1 < angle2;

        await Assert.That(isLessThan).IsTrue();
    }

    [Test]
    public async Task AngleShouldBeGreaterThanAnotherAngle()
    {
        Angle angle1 = Angle.FromDegrees(60.0f);
        Angle angle2 = Angle.FromDegrees(30.0f);

        bool isGreaterThan = angle1 > angle2;

        await Assert.That(isGreaterThan).IsTrue();
    }

    [Test]
    public async Task AngleShouldBeLessThanOrEqualToAnotherAngle()
    {
        Angle angle1 = Angle.FromDegrees(30.0f);
        Angle angle2 = Angle.FromDegrees(30.0f);

        bool isLessThanOrEqual = angle1 <= angle2;

        await Assert.That(isLessThanOrEqual).IsTrue();
    }

    [Test]
    public async Task AngleShouldBeGreaterThanOrEqualToAnotherAngle()
    {
        Angle angle1 = Angle.FromDegrees(60.0f);
        Angle angle2 = Angle.FromDegrees(60.0f);

        bool isGreaterThanOrEqual = angle1 >= angle2;

        await Assert.That(isGreaterThanOrEqual).IsTrue();
    }

    [Test]
    public async Task NormalizeShouldReturnCorrectAngle()
    {
        Angle angle = Angle.FromDegrees(370.0f);
        Angle normalized = angle.Normalize();

        await Assert.That(normalized.Degrees).IsEqualTo(10.0f);
    }

    [Test]
    public async Task NormalizeWithCustomRangeShouldReturnCorrectAngle()
    {
        Angle angle = Angle.FromDegrees(370.0f);
        Angle normalized = angle.Normalize(0f, 720f);

        await Assert.That(normalized.Degrees).IsEqualTo(370.0f);
    }

    [Test]
    public async Task NormalizeWithNegativeRangeShouldReturnCorrectAngle()
    {
        Angle angle = Angle.FromDegrees(-30.0f);
        Angle normalized = angle.Normalize(-360f, 0f);

        await Assert.That(normalized.Degrees).IsEqualTo(-30.0f);
    }

    [Test]
    public async Task CompareToShouldReturnZeroForEqualAngles()
    {
        Angle angle1 = Angle.FromDegrees(45.0f);
        Angle angle2 = Angle.FromDegrees(45.0f);

        int comparison = angle1.CompareTo(angle2);

        await Assert.That(comparison).IsEqualTo(0);
    }

    [Test]
    public async Task CompareToShouldReturnNegativeForLessThanAngle()
    {
        Angle angle1 = Angle.FromDegrees(30.0f);
        Angle angle2 = Angle.FromDegrees(60.0f);

        int comparison = angle1.CompareTo(angle2);

        await Assert.That(comparison).IsEqualTo(-1);
    }

    [Test]
    public async Task CompareToShouldReturnPositiveForGreaterThanAngle()
    {
        Angle angle1 = Angle.FromDegrees(60.0f);
        Angle angle2 = Angle.FromDegrees(30.0f);

        int comparison = angle1.CompareTo(angle2);

        await Assert.That(comparison).IsEqualTo(1);
    }

    [Test]
    public async Task CompareToNullShouldReturnPositive()
    {
        Angle angle = Angle.FromDegrees(30.0f);

        int comparison = angle.CompareTo(null);

        await Assert.That(comparison).IsEqualTo(1);
    }

    [Test]
    public async Task CompareToNonAngleObjectShouldThrowException()
    {
        Angle angle = Angle.FromDegrees(30.0f);

        await Assert.That(() => angle.CompareTo("not an angle"))
            .Throws<ArgumentException>()
            .WithMessage("Object must be of type Angle (Parameter 'obj')")
            .WithParameterName("obj");
    }

    [Test]
    public async Task EqualsShouldReturnTrueForEqualAngles()
    {
        Angle angle1 = Angle.FromDegrees(45.0f);
        Angle angle2 = Angle.FromDegrees(45.0f);

        bool areEqual = angle1.Equals(angle2);

        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForDifferentAngles()
    {
        Angle angle1 = Angle.FromDegrees(30.0f);
        Angle angle2 = Angle.FromDegrees(60.0f);

        bool areNotEqual = angle1.Equals(angle2);

        await Assert.That(areNotEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForNull()
    {
        Angle angle = Angle.FromDegrees(30.0f);

        bool isEqualToNull = angle.Equals(null);

        await Assert.That(isEqualToNull).IsFalse();
    }

    [Test]
    public async Task ToStringShouldDegrees()
    {
        Angle angle = Angle.FromDegrees(45.0f);

        string result = angle.ToString();

        await Assert.That(result).IsEqualTo("45°");
    }
}

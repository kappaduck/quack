// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

public sealed class AngleTests
{
    [Test]
    public async Task FromDegreesShouldReturnCorrectAngle()
    {
        const float degrees = 45.0f;
        Angle angle = Angle.FromDegrees(degrees);

        await Assert.That(angle.Degrees).IsEqualTo(degrees);
        await Assert.That(angle.Radians).IsEqualTo(degrees * (MathF.PI / 180.0f));
    }

    [Test]
    public async Task FromRadiansShouldReturnCorrectAngle()
    {
        const float radians = MathF.PI / 4.0f;
        Angle angle = Angle.FromRadians(radians);

        await Assert.That(angle.Radians).IsEqualTo(radians);
        await Assert.That(angle.Degrees).IsEqualTo(45.0f);
    }

    [Test]
    public async Task SinShouldReturnCorrectValue()
    {
        Angle angle = Angle.FromDegrees(30.0f);
        await Assert.That(angle.Sin).IsEqualTo(0.5f).Within(0.0001f);
    }

    [Test]
    public async Task CosShouldReturnCorrectValue()
    {
        Angle angle = Angle.FromDegrees(60.0f);
        await Assert.That(angle.Cos).IsEqualTo(0.5f).Within(0.0001f);
    }

    [Test]
    public async Task TanShouldReturnCorrectValue()
    {
        Angle angle = Angle.FromDegrees(45.0f);
        await Assert.That(angle.Tan).IsEqualTo(1.0f).Within(0.0001f);
    }

    [Test]
    public async Task ZeroShouldReturnZeroAngle()
    {
        Angle zeroAngle = Angle.Zero;

        await Assert.That(zeroAngle.Radians).IsEqualTo(0.0f);
        await Assert.That(zeroAngle.Degrees).IsEqualTo(0.0f);
    }

    [Test]
    [Arguments(0.0f, 360.0f, 90.0f)]
    [Arguments(-90.0f, 270.0f, 90f)]
    [Arguments(0.0f, 450.0f, 0f)]
    public async Task NormalizeShouldReturnAngleInTheSpecifiedRange(float min, float max, float expected)
    {
        Angle angle = Angle.FromDegrees(450.0f);
        Angle normalized = angle.Normalize(min, max);

        await Assert.That(normalized.Degrees).IsEqualTo(expected);
    }

    [Test]
    public async Task NormalizeShouldThrowWhenMinIsGreaterThanOrEqualToMax()
    {
        Angle angle = Angle.FromDegrees(90.0f);

        await Assert.That(() => angle.Normalize(360.0f, 0.0f)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    [Arguments(45f, 90f, -1)]
    [Arguments(90f, 45f, 1)]
    [Arguments(60f, 60f, 0)]
    public async Task CompareToShouldReturnCorrectValue(float left, float right, int expected)
    {
        Angle angle1 = Angle.FromDegrees(left);
        Angle angle2 = Angle.FromDegrees(right);

        int result = angle1.CompareTo(angle2);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 90f, -1)]
    [Arguments(90f, 45f, 1)]
    [Arguments(60f, 60f, 0)]
    public async Task CompareToWithObjectShouldReturnCorrectValue(float left, float right, int expected)
    {
        Angle angle1 = Angle.FromDegrees(left);
        Angle angle2 = Angle.FromDegrees(right);

        int result = angle1.CompareTo((object?)angle2);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task CompareToWithNullShouldReturnPositive()
    {
        Angle left = Angle.FromDegrees(90f);

        int result = left.CompareTo(null);
        await Assert.That(result).IsPositive();
    }

    [Test]
    public async Task CompareToWithAnyTypeExceptAngleShouldThrowArgumentException()
    {
        Angle left = Angle.FromDegrees(90f);
        Size size = new(10, 10);

        await Assert.That(() => left.CompareTo(size))
            .Throws<ArgumentException>()
            .WithMessage("Object is not an Angle. (Parameter 'obj')");
    }

    [Test]
    [Arguments(45f, 45f, true)]
    [Arguments(45f, 60f, false)]
    public async Task EqualsShouldReturnGoodResult(float left, float right, bool expected)
    {
        Angle angle1 = Angle.FromDegrees(left);
        Angle angle2 = Angle.FromDegrees(right);

        bool result = angle1.Equals(angle2);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 45f, true)]
    [Arguments(45f, 60f, false)]
    public async Task EqualsWithObjectShouldReturnGoodResult(float left, float right, bool expected)
    {
        Angle angle1 = Angle.FromDegrees(left);
        Angle angle2 = Angle.FromDegrees(right);

        bool result = angle1.Equals((object?)angle2);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptAngleShouldReturnFalse()
    {
        Angle left = Angle.FromDegrees(45f);
        Size size = new(10, 10);

        bool result = left.Equals(size);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Angle left = Angle.FromDegrees(45f);

        bool result = left.Equals(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task ToStringShouldReturnInDegreesFormatByDefault()
    {
        Angle angle = Angle.FromDegrees(90.0f);
        await Assert.That(angle.ToString()).IsEqualTo("90°");
    }

    [Test]
    [Arguments("R")]
    [Arguments("r")]
    public async Task ToStringShouldReturnInRadiansFormatWhenSpecified(string format)
    {
        Angle angle = Angle.FromDegrees(180.0f);

        string result = angle.ToString(format, null);
        await Assert.That(result).IsEqualTo($"{MathF.PI} rad");
    }

    [Test]
    public async Task ToStringShouldReturnInDegreesWhenThereIsNoFormat()
    {
        Angle angle = Angle.FromDegrees(180.0f);

        string result = angle.ToString(null, null);
        await Assert.That(result).IsEqualTo("180°");
    }

    [Test]
    public async Task ImplicitConversionFromDegreesShouldWork()
    {
        const float degrees = 90.0f;
        Angle angle = degrees;

        await Assert.That(angle.Degrees).IsEqualTo(degrees);
        await Assert.That(angle.Radians).IsEqualTo(degrees * (MathF.PI / 180.0f));
    }

    [Test]
    public async Task ExplicitConversionToDegreesShouldWork()
    {
        Angle angle = Angle.FromDegrees(180.0f);
        float degrees = (float)angle;

        await Assert.That(degrees).IsEqualTo(180.0f);
    }

    [Test]
    public async Task ShouldAddTwoAngles()
    {
        Angle left = Angle.FromDegrees(30.0f);
        Angle right = Angle.FromDegrees(45.0f);

        Angle result = left + right;
        await Assert.That(result.Degrees).IsEqualTo(75.0f);
    }

    [Test]
    public async Task ShouldSubstractTwoAngles()
    {
        Angle left = Angle.FromDegrees(90.0f);
        Angle right = Angle.FromDegrees(45.0f);

        Angle result = left - right;
        await Assert.That(result.Degrees).IsEqualTo(45.0f);
    }

    [Test]
    public async Task ShouldMultiplyAngleByScalar()
    {
        const float scalar = 2.0f;
        Angle angle = Angle.FromDegrees(30.0f);

        Angle result = angle * scalar;
        await Assert.That(result.Degrees).IsEqualTo(60.0f);
    }

    [Test]
    public async Task ShouldMultiplyAngleByScalarCommutative()
    {
        const float scalar = 3.0f;
        Angle angle = Angle.FromDegrees(20.0f);

        Angle result = scalar * angle;
        await Assert.That(result.Degrees).IsEqualTo(60.0f);
    }

    [Test]
    public async Task ShouldDivideAngleByScalar()
    {
        const float scalar = 2.0f;
        Angle angle = Angle.FromDegrees(90.0f);

        Angle result = angle / scalar;
        await Assert.That(result.Degrees).IsEqualTo(45.0f);
    }

    [Test]
    public async Task DivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const float scalar = 0.0f;
        Angle angle = Angle.FromDegrees(90.0f);

        await Assert.That(() => angle / scalar).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task ShouldNegateTheAngle()
    {
        Angle angle = Angle.FromDegrees(45.0f);

        Angle result = -angle;
        await Assert.That(result.Degrees).IsEqualTo(-45.0f);
    }

    [Test]
    [Arguments(60f, 60f, true)]
    [Arguments(45f, 90f, false)]
    public async Task OperatorEqualsShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) == Angle.FromDegrees(right);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 90f, true)]
    [Arguments(60f, 60f, false)]
    public async Task OperatorNotEqualsShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) != Angle.FromDegrees(right);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 60f, true)]
    [Arguments(60f, 45f, false)]
    [Arguments(60f, 60f, false)]
    public async Task OperatorLessThanShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) < Angle.FromDegrees(right);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(60f, 45f, true)]
    [Arguments(45f, 60f, false)]
    [Arguments(60f, 60f, false)]
    public async Task OperatorGreaterThanShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) > Angle.FromDegrees(right);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 60f, true)]
    [Arguments(60f, 45f, false)]
    [Arguments(60f, 60f, true)]
    public async Task OperatorLessThanOrEqualShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) <= Angle.FromDegrees(right);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(60f, 45f, true)]
    [Arguments(45f, 60f, false)]
    [Arguments(60f, 60f, true)]
    public async Task OperatorGreaterThanOrEqualShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) >= Angle.FromDegrees(right);
        await Assert.That(result).IsEqualTo(expected);
    }
}

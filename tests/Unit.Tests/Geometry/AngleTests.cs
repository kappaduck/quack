// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class AngleTests
{
    [Test]
    public async Task FromDegreesShouldReturnCorrectAngle()
    {
        const float degrees = 45.0f;
        Angle angle = Angle.FromDegrees(degrees);

        await angle.Degrees.Should().BeEqualTo(degrees);
        await angle.Radians.Should().BeEqualTo(degrees * (MathF.PI / 180.0f));
    }

    [Test]
    public async Task FromRadiansShouldReturnCorrectAngle()
    {
        const float radians = MathF.PI / 4.0f;
        Angle angle = Angle.FromRadians(radians);

        await angle.Radians.Should().BeEqualTo(radians);
        await angle.Degrees.Should().BeEqualTo(45.0f);
    }

    [Test]
    public async Task SinShouldReturnCorrectValue()
    {
        Angle angle = Angle.FromDegrees(30.0f);
        await angle.Sin.Should().BeCloseTo(0.5f, 0.0001f);
    }

    [Test]
    public async Task CosShouldReturnCorrectValue()
    {
        Angle angle = Angle.FromDegrees(60.0f);
        await angle.Cos.Should().BeCloseTo(0.5f, 0.0001f);
    }

    [Test]
    public async Task TanShouldReturnCorrectValue()
    {
        Angle angle = Angle.FromDegrees(45.0f);
        await angle.Tan.Should().BeCloseTo(1.0f, 0.0001f);
    }

    [Test]
    public async Task ZeroShouldReturnZeroAngle()
    {
        Angle zeroAngle = Angle.Zero;

        await zeroAngle.Radians.Should().BeZero();
        await zeroAngle.Degrees.Should().BeZero();
    }

    [Test]
    [Arguments(0.0f, 360.0f, 90.0f)]
    [Arguments(-90.0f, 270.0f, 90f)]
    [Arguments(0.0f, 450.0f, 0f)]
    public async Task NormalizeShouldReturnAngleInTheSpecifiedRange(float min, float max, float expected)
    {
        Angle angle = Angle.FromDegrees(450.0f);
        Angle normalized = angle.Normalize(min, max);

        await normalized.Degrees.Should().BeEqualTo(expected);
    }

    [Test]
    public async Task NormalizeShouldThrowWhenMinIsGreaterThanOrEqualToMax()
    {
        Angle angle = Angle.FromDegrees(90.0f);

        await Assert.That(() => angle.Normalize(360.0f, 0.0f)).Throws<ArgumentOutOfRangeException>();
    }

    [Test]
    [Arguments(45f, 45f, true)]
    [Arguments(45f, 60f, false)]
    public async Task EqualsShouldReturnGoodResult(float left, float right, bool expected)
    {
        Angle angle1 = Angle.FromDegrees(left);
        Angle angle2 = Angle.FromDegrees(right);

        bool result = angle1.Equals(angle2);
        await result.Should().BeEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 45f, true)]
    [Arguments(45f, 60f, false)]
    public async Task EqualsWithObjectShouldReturnGoodResult(float left, float right, bool expected)
    {
        Angle angle1 = Angle.FromDegrees(left);
        object angle2 = Angle.FromDegrees(right);

        bool result = angle1.Equals(angle2);
        await result.Should().BeEqualTo(expected);
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptAngleShouldReturnFalse()
    {
        Angle left = Angle.FromDegrees(45f);
        const float right = 45f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Angle left = Angle.FromDegrees(45f);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnInDegreesFormatByDefault()
    {
        Angle angle = Angle.FromDegrees(90.0f);
        await angle.ToString().Should().BeEqualTo("90°");
    }

    [Test]
    [Arguments("R")]
    [Arguments("r")]
    public async Task ToStringShouldReturnInRadiansFormatWhenSpecified(string format)
    {
        Angle angle = Angle.FromDegrees(180.0f);

        string result = angle.ToString(format, null);
        await result.Should().BeEqualTo($"{MathF.PI} rad");
    }

    [Test]
    public async Task ToStringShouldReturnInDegreesWhenThereIsNoFormat()
    {
        Angle angle = Angle.FromDegrees(180.0f);

        string result = angle.ToString(null, null);
        await result.Should().BeEqualTo("180°");
    }

    [Test]
    public async Task OperatorAddShouldAddTwoAngles()
    {
        Angle left = Angle.FromDegrees(30.0f);
        Angle right = Angle.FromDegrees(45.0f);

        Angle result = left + right;
        await result.Degrees.Should().BeEqualTo(75.0f);
    }

    [Test]
    public async Task OperatorSubstractShouldSubstractTwoAngles()
    {
        Angle left = Angle.FromDegrees(90.0f);
        Angle right = Angle.FromDegrees(45.0f);

        Angle result = left - right;
        await result.Degrees.Should().BeEqualTo(45.0f);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyAngleByScalar()
    {
        const float scalar = 2.0f;
        Angle angle = Angle.FromDegrees(30.0f);

        Angle result = angle * scalar;
        await result.Degrees.Should().BeEqualTo(60.0f);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyAngleByScalarReversed()
    {
        const float scalar = 3.0f;
        Angle angle = Angle.FromDegrees(20.0f);

        Angle result = scalar * angle;
        await result.Degrees.Should().BeEqualTo(60.0f);
    }

    [Test]
    public async Task OperatorDivideShouldDivideAngleByScalar()
    {
        const float scalar = 2.0f;
        Angle angle = Angle.FromDegrees(90.0f);

        Angle result = angle / scalar;
        await result.Degrees.Should().BeEqualTo(45.0f);
    }

    [Test]
    public async Task OperatorDivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const float scalar = 0.0f;
        Angle angle = Angle.FromDegrees(90.0f);

        await Assert.That(() => angle / scalar).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task OperatorNegateShouldNegateTheAngle()
    {
        Angle angle = Angle.FromDegrees(45.0f);

        Angle result = -angle;
        await result.Degrees.Should().BeEqualTo(-45.0f);
    }

    [Test]
    [Arguments(60f, 60f, true)]
    [Arguments(45f, 90f, false)]
    public async Task OperatorEqualsShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) == Angle.FromDegrees(right);
        await result.Should().BeEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 90f, true)]
    [Arguments(60f, 60f, false)]
    public async Task OperatorNotEqualsShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) != Angle.FromDegrees(right);
        await result.Should().BeEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 60f, true)]
    [Arguments(60f, 45f, false)]
    [Arguments(60f, 60f, false)]
    public async Task OperatorLessThanShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) < Angle.FromDegrees(right);
        await result.Should().BeEqualTo(expected);
    }

    [Test]
    [Arguments(60f, 45f, true)]
    [Arguments(45f, 60f, false)]
    [Arguments(60f, 60f, false)]
    public async Task OperatorGreaterThanShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) > Angle.FromDegrees(right);
        await result.Should().BeEqualTo(expected);
    }

    [Test]
    [Arguments(45f, 60f, true)]
    [Arguments(60f, 45f, false)]
    [Arguments(60f, 60f, true)]
    public async Task OperatorLessThanOrEqualShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) <= Angle.FromDegrees(right);
        await result.Should().BeEqualTo(expected);
    }

    [Test]
    [Arguments(60f, 45f, true)]
    [Arguments(45f, 60f, false)]
    [Arguments(60f, 60f, true)]
    public async Task OperatorGreaterThanOrEqualShouldReturnTheGoodResult(float left, float right, bool expected)
    {
        bool result = Angle.FromDegrees(left) >= Angle.FromDegrees(right);
        await result.Should().BeEqualTo(expected);
    }
}

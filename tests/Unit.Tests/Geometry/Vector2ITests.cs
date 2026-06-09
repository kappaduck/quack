// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class Vector2ITests
{
    [Test]
    public async Task VectorShouldGiveTheMagnitudeSquared()
    {
        Vector2I vector = new(3, 4);
        await vector.MagnitudeSquared.Should().BeEqualTo(25);
    }

    [Test]
    public async Task VectorShouldGiveTheMagnitude()
    {
        Vector2I vector = new(3, 4);
        await vector.Magnitude.Should().BeEqualTo(5);
    }

    [Test]
    public async Task IsZeroShouldReturnTrueWhenVectorIsZero()
    {
        Vector2I vector = new(0, 0);
        await vector.IsZero.Should().BeTrue();
    }

    [Test]
    public async Task IsZeroShouldReturnFalseWhenVectorIsNotZero()
    {
        Vector2I vector = new(3, 4);
        await vector.IsZero.Should().BeFalse();
    }

    [Test]
    public async Task LeftPerpendicularShouldReturnTheLeftPerpendicularVector()
    {
        Vector2I vector = new(3, 4);
        Vector2I leftPerpendicular = vector.LeftPerpendicular;

        await leftPerpendicular.X.Should().BeEqualTo(-4);
        await leftPerpendicular.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task RightPerpendicularShouldReturnTheRightPerpendicularVector()
    {
        Vector2I vector = new(3, 4);
        Vector2I rightPerpendicular = vector.RightPerpendicular;

        await rightPerpendicular.X.Should().BeEqualTo(4);
        await rightPerpendicular.Y.Should().BeEqualTo(-3);
    }

    [Test]
    public async Task DownShouldReturnTheDownVector()
    {
        Vector2I down = Vector2I.Down;

        await down.X.Should().BeEqualTo(0);
        await down.Y.Should().BeEqualTo(1);
    }

    [Test]
    public async Task LeftShouldReturnTheLeftVector()
    {
        Vector2I left = Vector2I.Left;

        await left.X.Should().BeEqualTo(-1);
        await left.Y.Should().BeEqualTo(0);
    }

    [Test]
    public async Task RightShouldReturnTheRightVector()
    {
        Vector2I right = Vector2I.Right;

        await right.X.Should().BeEqualTo(1);
        await right.Y.Should().BeEqualTo(0);
    }

    [Test]
    public async Task UpShouldReturnTheUpVector()
    {
        Vector2I up = Vector2I.Up;

        await up.X.Should().BeEqualTo(0);
        await up.Y.Should().BeEqualTo(-1);
    }

    [Test]
    public async Task ZeroShouldReturnTheZeroVector()
    {
        Vector2I zero = Vector2I.Zero;

        await zero.X.Should().BeZero();
        await zero.Y.Should().BeZero();
    }

    [Test]
    public async Task OperatorAddShouldAddTwoVectors()
    {
        Vector2I left = new(1, 2);
        Vector2I right = new(3, 4);

        Vector2I result = left + right;

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task OperatorSubstractShouldSubstractTwoVectors()
    {
        Vector2I left = new(5, 7);
        Vector2I right = new(3, 4);

        Vector2I result = left - right;

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyTwoVectors()
    {
        Vector2I left = new(2, 3);
        Vector2I right = new(4, 5);

        Vector2I result = left * right;

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(15);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyByScalar()
    {
        const int scalar = 4;
        Vector2I vector = new(2, 3);

        Vector2I result = vector * scalar;

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(12);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyByScalarReversed()
    {
        const int scalar = 4;
        Vector2I vector = new(2, 3);

        Vector2I result = scalar * vector;

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(12);
    }

    [Test]
    public async Task OperatorDivideShouldDivideByScalar()
    {
        const int scalar = 2;
        Vector2I vector = new(8, 12);

        Vector2I result = vector / scalar;

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task OperatorDivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const int scalar = 0;
        Vector2I vector = new(8, 12);

        await Assert.That(() => vector / scalar).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task OperatorNegateShouldNegateVector()
    {
        Vector2I vector = new(3, -4);

        Vector2I result = -vector;

        await result.X.Should().BeEqualTo(-3);
        await result.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2I left = new(3, 4);
        Vector2I right = new(3, 4);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2I left = new(3, 4);
        Vector2I right = new(3, 5);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenVectorsAreNotEquals()
    {
        Vector2I left = new(3, 4);
        Vector2I right = new(3, 5);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenVectorsAreEquals()
    {
        Vector2I left = new(3, 4);
        Vector2I right = new(3, 4);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2I left = new(3, 4);
        Vector2I right = new(3, 4);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2I left = new(3, 4);
        Vector2I right = new(4, 4);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2I left = new(3, 4);
        object right = new Vector2I(3, 4);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2I left = new(3, 4);
        object right = new Vector2I(4, 4);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptVector2iShouldReturnFalse()
    {
        Vector2I left = new(3, 4);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Vector2I left = new(3, 4);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Vector2I vector = new(3, 4);

        string result = vector.ToString();
        await result.Should().BeEqualTo("(3, 4)");
    }

    [Test]
    public async Task ShouldDeconstructVector2iIntoItsComponents()
    {
        (int x, int y) = new Vector2I(3, 4);

        await x.Should().BeEqualTo(3);
        await y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task ToVector2ShouldConvertToVector2()
    {
        Vector2I vector = new(3, 4);

        Vector2 result = vector.ToVector2();

        await result.X.Should().BeEqualTo(vector.X);
        await result.Y.Should().BeEqualTo(vector.Y);
    }

    [Test]
    public async Task StaticClampShouldClampTheVectorToMaximumLength()
    {
        const int maxLength = 4;
        Vector2I vector = new(3, 4);

        Vector2I clamped = Vector2I.Clamp(vector, maxLength);

        await clamped.Magnitude.Should().BeCloseTo(3.6f, 0.1f);
        await clamped.X.Should().BeEqualTo(2);
        await clamped.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task StaticClampShouldReturnTheSameVectorWhenItsLengthIsLessThanMaxLength()
    {
        const int maxLength = 6;
        Vector2I vector = new(3, 4);

        Vector2I clamped = Vector2I.Clamp(vector, maxLength);

        await clamped.Magnitude.Should().BeEqualTo(5);
        await clamped.X.Should().BeEqualTo(3);
        await clamped.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task StaticCrossShouldComputeCrossProductBetweenTwoVectors()
    {
        Vector2I left = new(1, 2);
        Vector2I right = new(3, 4);

        int cross = Vector2I.Cross(left, right);
        await cross.Should().BeEqualTo(-2);
    }

    [Test]
    public async Task StaticDistanceShouldReturnTheDistanceBetweenTwoVectors()
    {
        Vector2I left = new(1, 2);
        Vector2I right = new(4, 6);

        float distance = Vector2I.Distance(left, right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task StaticDotShouldReturnTheCorrectDotProductBetweenTwoVectors()
    {
        Vector2I left = new(1, 2);
        Vector2I right = new(3, 4);

        int dot = Vector2I.Dot(left, right);
        await dot.Should().BeEqualTo(11);
    }

    [Test]
    public async Task MaxShouldReturnTheVectorWithMaximumComponents()
    {
        Vector2I left = new(1, 5);
        Vector2I right = new(3, 4);

        Vector2I result = Vector2I.Max(left, right);

        await result.X.Should().BeEqualTo(3);
        await result.Y.Should().BeEqualTo(5);
    }

    [Test]
    public async Task MinShouldReturnTheVectorWithMinimumComponents()
    {
        Vector2I left = new(1, 5);
        Vector2I right = new(3, 4);

        Vector2I result = Vector2I.Min(left, right);

        await result.X.Should().BeEqualTo(1);
        await result.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task StaticMoveTowardsShouldMoveTheVectorTowardsTargetByMaxDistance()
    {
        const int maxDistance = 2;
        Vector2I current = new(1, 2);
        Vector2I target = new(4, 6);

        Vector2I result = Vector2I.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task StaticMoveTowardsShouldReturnTargetWhenWithinMaxDistance()
    {
        const int maxDistance = 10;
        Vector2I current = new(1, 2);
        Vector2I target = new(4, 6);

        Vector2I result = Vector2I.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task StaticMoveTowardsShouldReturnTargetWhenDistanceIsZero()
    {
        const int maxDistance = 0;
        Vector2I current = new(1, 2);
        Vector2I target = new(4, 6);

        Vector2I result = Vector2I.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(1);
        await result.Y.Should().BeEqualTo(2);
    }

    [Test]
    public async Task ClampShouldClampTheVectorToMaximumLength()
    {
        const int maxLength = 4;
        Vector2I vector = new(3, 4);

        Vector2I clamped = vector.Clamp(maxLength);

        await clamped.Magnitude.Should().BeCloseTo(3.6f, 0.1f);
        await clamped.X.Should().BeEqualTo(2);
        await clamped.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task ClampShouldReturnTheSameVectorWhenItsLengthIsLessThanMaxLength()
    {
        const int maxLength = 6;
        Vector2I vector = new(3, 4);

        Vector2I clamped = vector.Clamp(maxLength);

        await clamped.Magnitude.Should().BeEqualTo(5);
        await clamped.X.Should().BeEqualTo(3);
        await clamped.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task CrossShouldComputeCrossProductBetweenTwoVectors()
    {
        Vector2I left = new(1, 2);
        Vector2I right = new(3, 4);

        float cross = left.Cross(right);
        await cross.Should().BeEqualTo(-2);
    }

    [Test]
    public async Task DistanceShouldReturnTheDistanceBetweenTwoVectors()
    {
        Vector2I left = new(1, 2);
        Vector2I right = new(4, 6);

        float distance = left.Distance(right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task DotShouldReturnTheCorrectDotProductBetweenTwoVectors()
    {
        Vector2I left = new(1, 2);
        Vector2I right = new(3, 4);

        float dot = left.Dot(right);
        await dot.Should().BeEqualTo(11);
    }

    [Test]
    public async Task MoveTowardsShouldMoveTheVectorTowardsTargetByMaxDistance()
    {
        const int maxDistance = 2;
        Vector2I current = new(1, 2);
        Vector2I target = new(4, 6);

        Vector2I result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenWithinMaxDistance()
    {
        const int maxDistance = 10;
        Vector2I current = new(1, 2);
        Vector2I target = new(4, 6);

        Vector2I result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenDistanceIsZero()
    {
        const int maxDistance = 0;
        Vector2I current = new(1, 2);
        Vector2I target = new(4, 6);

        Vector2I result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(1);
        await result.Y.Should().BeEqualTo(2);
    }
}

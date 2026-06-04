// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class Vector2iTests
{
    [Test]
    public async Task VectorShouldGiveTheMagnitudeSquared()
    {
        Vector2i vector = new(3, 4);
        await vector.MagnitudeSquared.Should().BeEqualTo(25);
    }

    [Test]
    public async Task VectorShouldGiveTheMagnitude()
    {
        Vector2i vector = new(3, 4);
        await vector.Magnitude.Should().BeEqualTo(5);
    }

    [Test]
    public async Task IsZeroShouldReturnTrueWhenVectorIsZero()
    {
        Vector2i vector = new(0, 0);
        await vector.IsZero.Should().BeTrue();
    }

    [Test]
    public async Task IsZeroShouldReturnFalseWhenVectorIsNotZero()
    {
        Vector2i vector = new(3, 4);
        await vector.IsZero.Should().BeFalse();
    }

    [Test]
    public async Task LeftPerpendicularShouldReturnTheLeftPerpendicularVector()
    {
        Vector2i vector = new(3, 4);
        Vector2i leftPerpendicular = vector.LeftPerpendicular;

        await leftPerpendicular.X.Should().BeEqualTo(-4);
        await leftPerpendicular.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task RightPerpendicularShouldReturnTheRightPerpendicularVector()
    {
        Vector2i vector = new(3, 4);
        Vector2i rightPerpendicular = vector.RightPerpendicular;

        await rightPerpendicular.X.Should().BeEqualTo(4);
        await rightPerpendicular.Y.Should().BeEqualTo(-3);
    }

    [Test]
    public async Task DownShouldReturnTheDownVector()
    {
        Vector2i down = Vector2i.Down;

        await down.X.Should().BeEqualTo(0);
        await down.Y.Should().BeEqualTo(1);
    }

    [Test]
    public async Task LeftShouldReturnTheLeftVector()
    {
        Vector2i left = Vector2i.Left;

        await left.X.Should().BeEqualTo(-1);
        await left.Y.Should().BeEqualTo(0);
    }

    [Test]
    public async Task RightShouldReturnTheRightVector()
    {
        Vector2i right = Vector2i.Right;

        await right.X.Should().BeEqualTo(1);
        await right.Y.Should().BeEqualTo(0);
    }

    [Test]
    public async Task UpShouldReturnTheUpVector()
    {
        Vector2i up = Vector2i.Up;

        await up.X.Should().BeEqualTo(0);
        await up.Y.Should().BeEqualTo(-1);
    }

    [Test]
    public async Task ZeroShouldReturnTheZeroVector()
    {
        Vector2i zero = Vector2i.Zero;

        await zero.X.Should().BeEqualTo(0);
        await zero.Y.Should().BeEqualTo(0);
    }

    [Test]
    public async Task AddShouldAddTwoVectors()
    {
        Vector2i left = new(1, 2);
        Vector2i right = new(3, 4);

        Vector2i result = Vector2i.Add(left, right);

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task SubstractShouldSubstractTwoVectors()
    {
        Vector2i left = new(5, 7);
        Vector2i right = new(3, 4);

        Vector2i result = Vector2i.Subtract(left, right);

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task MultiplyShouldMultiplyTwoVectors()
    {
        Vector2i left = new(2, 3);
        Vector2i right = new(4, 5);

        Vector2i result = Vector2i.Multiply(left, right);

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(15);
    }

    [Test]
    public async Task MultiplyShouldMultiplyByScalar()
    {
        const int scalar = 4;
        Vector2i vector = new(2, 3);

        Vector2i result = Vector2i.Multiply(vector, scalar);

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(12);
    }

    [Test]
    public async Task DivideShouldDivideByScalar()
    {
        const int scalar = 2;
        Vector2i vector = new(8, 12);

        Vector2i result = Vector2i.Divide(vector, scalar);

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task DivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const int scalar = 0;
        Vector2i vector = new(8, 12);

        await Assert.That(() => Vector2i.Divide(vector, scalar)).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task NegateShouldNegateVector()
    {
        Vector2i vector = new(3, -4);

        Vector2i result = Vector2i.Negate(vector);

        await result.X.Should().BeEqualTo(-3);
        await result.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task OperatorAddShouldAddTwoVectors()
    {
        Vector2i left = new(1, 2);
        Vector2i right = new(3, 4);

        Vector2i result = left + right;

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task OperatorSubstractShouldSubstractTwoVectors()
    {
        Vector2i left = new(5, 7);
        Vector2i right = new(3, 4);

        Vector2i result = left - right;

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyTwoVectors()
    {
        Vector2i left = new(2, 3);
        Vector2i right = new(4, 5);

        Vector2i result = left * right;

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(15);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyByScalar()
    {
        const int scalar = 4;
        Vector2i vector = new(2, 3);

        Vector2i result = vector * scalar;

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(12);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyByScalarReversed()
    {
        const int scalar = 4;
        Vector2i vector = new(2, 3);

        Vector2i result = scalar * vector;

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(12);
    }

    [Test]
    public async Task OperatorDivideShouldDivideByScalar()
    {
        const int scalar = 2;
        Vector2i vector = new(8, 12);

        Vector2i result = vector / scalar;

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task OperatorDivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const int scalar = 0;
        Vector2i vector = new(8, 12);

        await Assert.That(() => vector / scalar).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task OperatorNegateShouldNegateVector()
    {
        Vector2i vector = new(3, -4);

        Vector2i result = -vector;

        await result.X.Should().BeEqualTo(-3);
        await result.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2i left = new(3, 4);
        Vector2i right = new(3, 4);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2i left = new(3, 4);
        Vector2i right = new(3, 5);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenVectorsAreNotEquals()
    {
        Vector2i left = new(3, 4);
        Vector2i right = new(3, 5);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenVectorsAreEquals()
    {
        Vector2i left = new(3, 4);
        Vector2i right = new(3, 4);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2i left = new(3, 4);
        Vector2i right = new(3, 4);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2i left = new(3, 4);
        Vector2i right = new(4, 4);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2i left = new(3, 4);
        Vector2i right = new(3, 4);

        bool result = left.Equals((object)right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2i left = new(3, 4);
        Vector2i right = new(4, 4);

        bool result = left.Equals((object)right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptVector2iShouldReturnFalse()
    {
        Vector2i left = new(3, 4);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Vector2i left = new(3, 4);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Vector2i vector = new(3, 4);

        string result = vector.ToString();
        await result.Should().BeEqualTo("(3, 4)");
    }

    [Test]
    public async Task ShouldDeconstructVector2iIntoItsComponents()
    {
        (int x, int y) = new Vector2i(3, 4);

        await x.Should().BeEqualTo(3);
        await y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task ToVector2ShouldConvertToVector2()
    {
        Vector2i vector = new(3, 4);

        Vector2 result = vector.ToVector2();

        await result.X.Should().BeEqualTo(vector.X);
        await result.Y.Should().BeEqualTo(vector.Y);
    }

    [Test]
    public async Task StaticClampShouldClampTheVectorToMaximumLength()
    {
        const int maxLength = 4;
        Vector2i vector = new(3, 4);

        Vector2i clamped = Vector2i.Clamp(vector, maxLength);

        await clamped.Magnitude.Should().BeCloseTo(3.6f, 0.1f);
        await clamped.X.Should().BeEqualTo(2);
        await clamped.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task StaticClampShouldReturnTheSameVectorWhenItsLengthIsLessThanMaxLength()
    {
        const int maxLength = 6;
        Vector2i vector = new(3, 4);

        Vector2i clamped = Vector2i.Clamp(vector, maxLength);

        await clamped.Magnitude.Should().BeEqualTo(5);
        await clamped.X.Should().BeEqualTo(3);
        await clamped.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task StaticCrossShouldComputeCrossProductBetweenTwoVectors()
    {
        Vector2i left = new(1, 2);
        Vector2i right = new(3, 4);

        int cross = Vector2i.Cross(left, right);
        await cross.Should().BeEqualTo(-2);
    }

    [Test]
    public async Task StaticDistanceShouldReturnTheDistanceBetweenTwoVectors()
    {
        Vector2i left = new(1, 2);
        Vector2i right = new(4, 6);

        float distance = Vector2i.Distance(left, right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task StaticDotShouldReturnTheCorrectDotProductBetweenTwoVectors()
    {
        Vector2i left = new(1, 2);
        Vector2i right = new(3, 4);

        int dot = Vector2i.Dot(left, right);
        await dot.Should().BeEqualTo(11);
    }

    [Test]
    public async Task MaxShouldReturnTheVectorWithMaximumComponents()
    {
        Vector2i left = new(1, 5);
        Vector2i right = new(3, 4);

        Vector2i result = Vector2i.Max(left, right);

        await result.X.Should().BeEqualTo(3);
        await result.Y.Should().BeEqualTo(5);
    }

    [Test]
    public async Task MinShouldReturnTheVectorWithMinimumComponents()
    {
        Vector2i left = new(1, 5);
        Vector2i right = new(3, 4);

        Vector2i result = Vector2i.Min(left, right);

        await result.X.Should().BeEqualTo(1);
        await result.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task StaticMoveTowardsShouldMoveTheVectorTowardsTargetByMaxDistance()
    {
        const int maxDistance = 2;
        Vector2i current = new(1, 2);
        Vector2i target = new(4, 6);

        Vector2i result = Vector2i.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task StaticMoveTowardsShouldReturnTargetWhenWithinMaxDistance()
    {
        const int maxDistance = 10;
        Vector2i current = new(1, 2);
        Vector2i target = new(4, 6);

        Vector2i result = Vector2i.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task StaticMoveTowardsShouldReturnTargetWhenDistanceIsZero()
    {
        const int maxDistance = 0;
        Vector2i current = new(1, 2);
        Vector2i target = new(4, 6);

        Vector2i result = Vector2i.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(1);
        await result.Y.Should().BeEqualTo(2);
    }

    [Test]
    public async Task ClampShouldClampTheVectorToMaximumLength()
    {
        const int maxLength = 4;
        Vector2i vector = new(3, 4);

        Vector2i clamped = vector.Clamp(maxLength);

        await clamped.Magnitude.Should().BeCloseTo(3.6f, 0.1f);
        await clamped.X.Should().BeEqualTo(2);
        await clamped.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task ClampShouldReturnTheSameVectorWhenItsLengthIsLessThanMaxLength()
    {
        const int maxLength = 6;
        Vector2i vector = new(3, 4);

        Vector2i clamped = vector.Clamp(maxLength);

        await clamped.Magnitude.Should().BeEqualTo(5);
        await clamped.X.Should().BeEqualTo(3);
        await clamped.Y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task CrossShouldComputeCrossProductBetweenTwoVectors()
    {
        Vector2i left = new(1, 2);
        Vector2i right = new(3, 4);

        float cross = left.Cross(right);
        await cross.Should().BeEqualTo(-2);
    }

    [Test]
    public async Task DistanceShouldReturnTheDistanceBetweenTwoVectors()
    {
        Vector2i left = new(1, 2);
        Vector2i right = new(4, 6);

        float distance = left.Distance(right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task DotShouldReturnTheCorrectDotProductBetweenTwoVectors()
    {
        Vector2i left = new(1, 2);
        Vector2i right = new(3, 4);

        float dot = left.Dot(right);
        await dot.Should().BeEqualTo(11);
    }

    [Test]
    public async Task MoveTowardsShouldMoveTheVectorTowardsTargetByMaxDistance()
    {
        const int maxDistance = 2;
        Vector2i current = new(1, 2);
        Vector2i target = new(4, 6);

        Vector2i result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenWithinMaxDistance()
    {
        const int maxDistance = 10;
        Vector2i current = new(1, 2);
        Vector2i target = new(4, 6);

        Vector2i result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenDistanceIsZero()
    {
        const int maxDistance = 0;
        Vector2i current = new(1, 2);
        Vector2i target = new(4, 6);

        Vector2i result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(1);
        await result.Y.Should().BeEqualTo(2);
    }
}

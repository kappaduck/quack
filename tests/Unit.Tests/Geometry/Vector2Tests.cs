// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class Vector2Tests
{
    [Test]
    public async Task ShouldCreateVector2FromPolarCoordinates()
    {
        const float radius = 5f;
        Angle angle = Angle.FromDegrees(53.13f);

        Vector2 vector = new(radius, angle);

        await vector.X.Should().BeCloseTo(3f, 0.01f);
        await vector.Y.Should().BeCloseTo(4f, 0.01f);
    }

    [Test]
    public async Task VectorShouldGiveTheMagnitudeSquared()
    {
        Vector2 vector = new(3f, 4f);
        await vector.MagnitudeSquared.Should().BeEqualTo(25f);
    }

    [Test]
    public async Task VectorShouldGiveTheMagnitude()
    {
        Vector2 vector = new(3f, 4f);
        await vector.Magnitude.Should().BeEqualTo(5f);
    }

    [Test]
    public async Task IsZeroShouldReturnTrueWhenVectorIsZero()
    {
        Vector2 vector = new(0f, 0f);
        await vector.IsZero.Should().BeTrue();
    }

    [Test]
    public async Task IsZeroShouldReturnFalseWhenVectorIsNotZero()
    {
        Vector2 vector = new(3f, 4f);
        await vector.IsZero.Should().BeFalse();
    }

    [Test]
    public async Task IsNormalizedShouldReturnTrueWhenVectorIsNormalized()
    {
        Vector2 vector = new(0f, 1f);
        await vector.IsNormalized.Should().BeTrue();
    }

    [Test]
    public async Task IsNormalizedShouldReturnFalseWhenVectorIsNotNormalized()
    {
        Vector2 vector = new(3f, 4f);
        await vector.IsNormalized.Should().BeFalse();
    }

    [Test]
    public async Task NormalizedShouldReturnTheNormalizedVectorWhenIsNotNormalized()
    {
        Vector2 vector = new(3f, 4f);
        Vector2 normalized = vector.Normalized;

        await normalized.Magnitude.Should().BeEqualTo(1f);
        await normalized.X.Should().BeEqualTo(0.6f);
        await normalized.Y.Should().BeEqualTo(0.8f);
    }

    [Test]
    public async Task NormalizedShouldReturnTheSameVectorWhenIsAlreadyNormalized()
    {
        Vector2 vector = new(0f, 1f);
        Vector2 normalized = vector.Normalized;

        await normalized.Magnitude.Should().BeEqualTo(1f);
        await normalized.X.Should().BeEqualTo(0f);
        await normalized.Y.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task LeftPerpendicularShouldReturnTheLeftPerpendicularVector()
    {
        Vector2 vector = new(3f, 4f);
        Vector2 leftPerpendicular = vector.LeftPerpendicular;

        await leftPerpendicular.X.Should().BeEqualTo(-4f);
        await leftPerpendicular.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task RightPerpendicularShouldReturnTheRightPerpendicularVector()
    {
        Vector2 vector = new(3f, 4f);
        Vector2 rightPerpendicular = vector.RightPerpendicular;

        await rightPerpendicular.X.Should().BeEqualTo(4f);
        await rightPerpendicular.Y.Should().BeEqualTo(-3f);
    }

    [Test]
    public async Task DownShouldReturnTheDownVector()
    {
        Vector2 down = Vector2.Down;

        await down.X.Should().BeEqualTo(0f);
        await down.Y.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task LeftShouldReturnTheLeftVector()
    {
        Vector2 left = Vector2.Left;

        await left.X.Should().BeEqualTo(-1f);
        await left.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task RightShouldReturnTheRightVector()
    {
        Vector2 right = Vector2.Right;

        await right.X.Should().BeEqualTo(1f);
        await right.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task UpShouldReturnTheUpVector()
    {
        Vector2 up = Vector2.Up;

        await up.X.Should().BeEqualTo(0f);
        await up.Y.Should().BeEqualTo(-1f);
    }

    [Test]
    public async Task ZeroShouldReturnTheZeroVector()
    {
        Vector2 zero = Vector2.Zero;

        await zero.X.Should().BeEqualTo(0f);
        await zero.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task AddShouldAddTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        Vector2 result = Vector2.Add(left, right);

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task SubstractShouldSubstractTwoVectors()
    {
        Vector2 left = new(5f, 7f);
        Vector2 right = new(3f, 4f);

        Vector2 result = Vector2.Subtract(left, right);

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task MultiplyShouldMultiplyTwoVectors()
    {
        Vector2 left = new(2f, 3f);
        Vector2 right = new(4f, 5f);

        Vector2 result = Vector2.Multiply(left, right);

        await result.X.Should().BeEqualTo(8f);
        await result.Y.Should().BeEqualTo(15f);
    }

    [Test]
    public async Task MultiplyShouldMultiplyByScalar()
    {
        const float scalar = 4f;
        Vector2 vector = new(2f, 3f);

        Vector2 result = Vector2.Multiply(vector, scalar);

        await result.X.Should().BeEqualTo(8f);
        await result.Y.Should().BeEqualTo(12f);
    }

    [Test]
    public async Task DivideShouldDivideByScalar()
    {
        const float scalar = 2f;
        Vector2 vector = new(8f, 12f);

        Vector2 result = Vector2.Divide(vector, scalar);

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task DivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const float scalar = 0f;
        Vector2 vector = new(8f, 12f);

        await Assert.That(() => Vector2.Divide(vector, scalar)).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task NegateShouldNegateVector()
    {
        Vector2 vector = new(3f, -4f);

        Vector2 result = Vector2.Negate(vector);

        await result.X.Should().BeEqualTo(-3f);
        await result.Y.Should().BeEqualTo(4f);
    }

    [Test]
    public async Task OperatorAddShouldAddTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        Vector2 result = left + right;

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task OperatorSubstractShouldSubstractTwoVectors()
    {
        Vector2 left = new(5f, 7f);
        Vector2 right = new(3f, 4f);

        Vector2 result = left - right;

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyTwoVectors()
    {
        Vector2 left = new(2f, 3f);
        Vector2 right = new(4f, 5f);

        Vector2 result = left * right;

        await result.X.Should().BeEqualTo(8f);
        await result.Y.Should().BeEqualTo(15f);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyByScalar()
    {
        const float scalar = 4f;
        Vector2 vector = new(2f, 3f);

        Vector2 result = vector * scalar;

        await result.X.Should().BeEqualTo(8f);
        await result.Y.Should().BeEqualTo(12f);
    }

    [Test]
    public async Task OperatorMultiplyShouldMultiplyByScalarReversed()
    {
        const float scalar = 4f;
        Vector2 vector = new(2f, 3f);

        Vector2 result = scalar * vector;

        await result.X.Should().BeEqualTo(8f);
        await result.Y.Should().BeEqualTo(12f);
    }

    [Test]
    public async Task OperatorDivideShouldDivideByScalar()
    {
        const float scalar = 2f;
        Vector2 vector = new(8f, 12f);

        Vector2 result = vector / scalar;

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task OperatorDivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const float scalar = 0f;
        Vector2 vector = new(8f, 12f);

        await Assert.That(() => vector / scalar).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task OperatorNegateShouldNegateVector()
    {
        Vector2 vector = new(3f, -4f);

        Vector2 result = -vector;

        await result.X.Should().BeEqualTo(-3f);
        await result.Y.Should().BeEqualTo(4f);
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 4f);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 5f);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenVectorsAreNotEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 5f);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenVectorsAreEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 4f);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 4f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(4f, 4f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 4f);

        bool result = left.Equals((object)right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(4f, 4f);

        bool result = left.Equals((object)right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptVector2ShouldReturnFalse()
    {
        Vector2 left = new(3f, 4f);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Vector2 left = new(3f, 4f);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Vector2 vector = new(3f, 4f);

        string result = vector.ToString();
        await result.Should().BeEqualTo("(3, 4)");
    }

    [Test]
    public async Task BetweenShouldReturnTheAngleBetweenTwoVectors()
    {
        Vector2 from = new(1f, 0f);
        Vector2 to = new(0f, 1f);

        Angle angle = Vector2.Between(from, to);
        await angle.Degrees.Should().BeEqualTo(90f);
    }

    [Test]
    public async Task StaticClampShouldClampTheVectorToMaximumLength()
    {
        const float maxLength = 4f;
        Vector2 vector = new(3f, 4f);

        Vector2 clamped = Vector2.Clamp(vector, maxLength);

        await clamped.Magnitude.Should().BeEqualTo(4f);
        await clamped.X.Should().BeEqualTo(2.4f);
        await clamped.Y.Should().BeEqualTo(3.2f);
    }

    [Test]
    public async Task StaticClampShouldReturnTheSameVectorWhenItsLengthIsLessThanMaxLength()
    {
        const float maxLength = 6f;
        Vector2 vector = new(3f, 4f);

        Vector2 clamped = Vector2.Clamp(vector, maxLength);

        await clamped.Magnitude.Should().BeEqualTo(5f);
        await clamped.X.Should().BeEqualTo(3f);
        await clamped.Y.Should().BeEqualTo(4f);
    }

    [Test]
    public async Task StaticCrossShouldComputeCrossProductBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float cross = Vector2.Cross(left, right);
        await cross.Should().BeEqualTo(-2f);
    }

    [Test]
    public async Task StaticDistanceShouldReturnTheDistanceBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(4f, 6f);

        float distance = Vector2.Distance(left, right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task StaticDotShouldReturnTheCorrectDotProductBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float dot = Vector2.Dot(left, right);
        await dot.Should().BeEqualTo(11f);
    }

    [Test]
    public async Task LerpShouldInterpolateBetweenTwoVectors()
    {
        const float t = 0.5f;
        Vector2 from = new(1f, 2f);
        Vector2 to = new(3f, 4f);

        Vector2 result = Vector2.Lerp(from, to, t);

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task LerpUnclampedShouldInterpolateBetweenTwoVectorsWithoutClamping()
    {
        const float t = 1.5f;
        Vector2 from = new(1f, 2f);
        Vector2 to = new(3f, 4f);

        Vector2 result = Vector2.LerpUnclamped(from, to, t);

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(5f);
    }

    [Test]
    public async Task MaxShouldReturnTheVectorWithMaximumComponents()
    {
        Vector2 left = new(1f, 5f);
        Vector2 right = new(3f, 4f);

        Vector2 result = Vector2.Max(left, right);

        await result.X.Should().BeEqualTo(3f);
        await result.Y.Should().BeEqualTo(5f);
    }

    [Test]
    public async Task MinShouldReturnTheVectorWithMinimumComponents()
    {
        Vector2 left = new(1f, 5f);
        Vector2 right = new(3f, 4f);

        Vector2 result = Vector2.Min(left, right);

        await result.X.Should().BeEqualTo(1f);
        await result.Y.Should().BeEqualTo(4f);
    }

    [Test]
    public async Task StaticMoveTowardsShouldMoveTheVectorTowardsTargetByMaxDistance()
    {
        const float maxDistance = 2f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = Vector2.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(2.2f);
        await result.Y.Should().BeEqualTo(3.6f);
    }

    [Test]
    public async Task StaticMoveTowardsShouldReturnTargetWhenWithinMaxDistance()
    {
        const float maxDistance = 10f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = Vector2.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task StaticMoveTowardsShouldReturnTargetWhenDistanceIsZero()
    {
        const float maxDistance = 0f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = Vector2.MoveTowards(current, target, maxDistance);

        await result.X.Should().BeEqualTo(1f);
        await result.Y.Should().BeEqualTo(2f);
    }

    [Test]
    public async Task StaticReflectShouldReturnTheReflectedVector()
    {
        Vector2 vector = new(1f, -1f);
        Vector2 normal = new Vector2(0f, 1f).Normalized;

        Vector2 reflected = Vector2.Reflect(vector, normal);

        await reflected.X.Should().BeEqualTo(1f);
        await reflected.Y.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task StaticRotateShouldReturnTheRotatedVector()
    {
        Vector2 vector = new(1f, 0f);
        Angle angle = Angle.FromDegrees(90f);

        Vector2 rotated = Vector2.Rotate(vector, angle);

        await rotated.X.Should().BeCloseTo(0f, 0.01f);
        await rotated.Y.Should().BeCloseTo(1f, 0.01f);
    }

    [Test]
    public async Task AngleShouldReturnTheAngleBetweenTwoVectors()
    {
        Vector2 from = new(1f, 0f);
        Vector2 to = new(0f, 1f);

        Angle angle = from.Angle(to);
        await angle.Degrees.Should().BeEqualTo(90f);
    }

    [Test]
    public async Task ClampShouldClampTheVectorToMaximumLength()
    {
        const float maxLength = 4f;
        Vector2 vector = new(3f, 4f);

        Vector2 clamped = vector.Clamp(maxLength);

        await clamped.Magnitude.Should().BeEqualTo(4f);
        await clamped.X.Should().BeEqualTo(2.4f);
        await clamped.Y.Should().BeEqualTo(3.2f);
    }

    [Test]
    public async Task ClampShouldReturnTheSameVectorWhenItsLengthIsLessThanMaxLength()
    {
        const float maxLength = 6f;
        Vector2 vector = new(3f, 4f);

        Vector2 clamped = vector.Clamp(maxLength);

        await clamped.Magnitude.Should().BeEqualTo(5f);
        await clamped.X.Should().BeEqualTo(3f);
        await clamped.Y.Should().BeEqualTo(4f);
    }

    [Test]
    public async Task CrossShouldComputeCrossProductBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float cross = left.Cross(right);
        await cross.Should().BeEqualTo(-2f);
    }

    [Test]
    public async Task ShouldDeconstructVector2IntoItsComponents()
    {
        (float x, float y) = new Vector2(3f, 4f);

        await x.Should().BeEqualTo(3f);
        await y.Should().BeEqualTo(4f);
    }

    [Test]
    public async Task DistanceShouldReturnTheDistanceBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(4f, 6f);

        float distance = left.Distance(right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task DotShouldReturnTheCorrectDotProductBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float dot = left.Dot(right);
        await dot.Should().BeEqualTo(11f);
    }

    [Test]
    public async Task MoveTowardsShouldMoveTheVectorTowardsTargetByMaxDistance()
    {
        const float maxDistance = 2f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(2.2f);
        await result.Y.Should().BeEqualTo(3.6f);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenWithinMaxDistance()
    {
        const float maxDistance = 10f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenDistanceIsZero()
    {
        const float maxDistance = 0f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = current.MoveTowards(target, maxDistance);

        await result.X.Should().BeEqualTo(1f);
        await result.Y.Should().BeEqualTo(2f);
    }

    [Test]
    public async Task ReflectShouldReturnTheReflectedVector()
    {
        Vector2 vector = new(1f, -1f);
        Vector2 normal = new Vector2(0f, 1f).Normalized;

        Vector2 reflected = vector.Reflect(normal);

        await reflected.X.Should().BeEqualTo(1f);
        await reflected.Y.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task RotateShouldReturnTheRotatedVector()
    {
        Vector2 vector = new(1f, 0f);
        Angle angle = Angle.FromDegrees(90f);

        Vector2 rotated = vector.Rotate(angle);

        await rotated.X.Should().BeCloseTo(0f, 0.01f);
        await rotated.Y.Should().BeCloseTo(1f, 0.01f);
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

public sealed class Vector2Tests
{
    [Test]
    public async Task ShouldCreateVector2FromPolarCoordinates()
    {
        const float radius = 5f;
        Angle angle = Angle.FromDegrees(53.13f);

        Vector2 vector = new(radius, angle);

        await Assert.That(vector.X).IsEqualTo(3f).Within(0.01f);
        await Assert.That(vector.Y).IsEqualTo(4f).Within(0.01f);
    }

    [Test]
    public async Task VectorShouldGiveTheMagnitudeSquared()
    {
        Vector2 vector = new(3f, 4f);
        await Assert.That(vector.MagnitudeSquared).IsEqualTo(25f);
    }

    [Test]
    public async Task VectorShouldGiveTheMagnitude()
    {
        Vector2 vector = new(3f, 4f);
        await Assert.That(vector.Magnitude).IsEqualTo(5f);
    }

    [Test]
    public async Task IsZeroShouldReturnTrueWhenVectorIsZero()
    {
        Vector2 vector = new(0f, 0f);
        await Assert.That(vector.IsZero).IsTrue();
    }

    [Test]
    public async Task IsZeroShouldReturnFalseWhenVectorIsNotZero()
    {
        Vector2 vector = new(3f, 4f);
        await Assert.That(vector.IsZero).IsFalse();
    }

    [Test]
    public async Task IsNormalizedShouldReturnTrueWhenVectorIsNormalized()
    {
        Vector2 vector = new(0f, 1f);
        await Assert.That(vector.IsNormalized).IsTrue();
    }

    [Test]
    public async Task IsNormalizedShouldReturnFalseWhenVectorIsNotNormalized()
    {
        Vector2 vector = new(3f, 4f);
        await Assert.That(vector.IsNormalized).IsFalse();
    }

    [Test]
    public async Task NormalizedShouldReturnTheNormalizedVectorWhenIsNotNormalized()
    {
        Vector2 vector = new(3f, 4f);
        Vector2 normalized = vector.Normalized;

        await Assert.That(normalized.Magnitude).IsEqualTo(1f).Within(0.0001f);
        await Assert.That(normalized.X).IsEqualTo(0.6f).Within(0.0001f);
        await Assert.That(normalized.Y).IsEqualTo(0.8f).Within(0.0001f);
    }

    [Test]
    public async Task NormalizedShouldReturnTheSameVectorWhenIsAlreadyNormalized()
    {
        Vector2 vector = new(0f, 1f);
        Vector2 normalized = vector.Normalized;

        await Assert.That(normalized.Magnitude).IsEqualTo(1f).Within(0.0001f);
        await Assert.That(normalized.X).IsEqualTo(0f).Within(0.0001f);
        await Assert.That(normalized.Y).IsEqualTo(1f).Within(0.0001f);
    }

    [Test]
    public async Task LeftPerpendicularShouldReturnTheLeftPerpendicularVector()
    {
        Vector2 vector = new(3f, 4f);
        Vector2 leftPerpendicular = vector.LeftPerpendicular;

        await Assert.That(leftPerpendicular.X).IsEqualTo(-4f).Within(0.0001f);
        await Assert.That(leftPerpendicular.Y).IsEqualTo(3f).Within(0.0001f);
    }

    [Test]
    public async Task RightPerpendicularShouldReturnTheRightPerpendicularVector()
    {
        Vector2 vector = new(3f, 4f);
        Vector2 rightPerpendicular = vector.RightPerpendicular;

        await Assert.That(rightPerpendicular.X).IsEqualTo(4f).Within(0.0001f);
        await Assert.That(rightPerpendicular.Y).IsEqualTo(-3f).Within(0.0001f);
    }

    [Test]
    public async Task DownShouldReturnTheDownVector()
    {
        Vector2 down = Vector2.Down;

        await Assert.That(down.X).IsEqualTo(0f).Within(0.0001f);
        await Assert.That(down.Y).IsEqualTo(1f).Within(0.0001f);
    }

    [Test]
    public async Task LeftShouldReturnTheLeftVector()
    {
        Vector2 left = Vector2.Left;

        await Assert.That(left.X).IsEqualTo(-1f).Within(0.0001f);
        await Assert.That(left.Y).IsEqualTo(0f).Within(0.0001f);
    }

    [Test]
    public async Task RightShouldReturnTheRightVector()
    {
        Vector2 right = Vector2.Right;

        await Assert.That(right.X).IsEqualTo(1f).Within(0.0001f);
        await Assert.That(right.Y).IsEqualTo(0f).Within(0.0001f);
    }

    [Test]
    public async Task UpShouldReturnTheUpVector()
    {
        Vector2 up = Vector2.Up;

        await Assert.That(up.X).IsEqualTo(0f).Within(0.0001f);
        await Assert.That(up.Y).IsEqualTo(-1f).Within(0.0001f);
    }

    [Test]
    public async Task ZeroShouldReturnTheZeroVector()
    {
        Vector2 zero = Vector2.Zero;

        await Assert.That(zero.X).IsEqualTo(0f);
        await Assert.That(zero.Y).IsEqualTo(0f);
    }

    [Test]
    public async Task ShouldAddTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        Vector2 result = left + right;
        await Assert.That(result.X).IsEqualTo(4f);
        await Assert.That(result.Y).IsEqualTo(6f);
    }

    [Test]
    public async Task ShouldSubstractTwoVectors()
    {
        Vector2 left = new(5f, 7f);
        Vector2 right = new(3f, 4f);

        Vector2 result = left - right;
        await Assert.That(result.X).IsEqualTo(2f);
        await Assert.That(result.Y).IsEqualTo(3f);
    }

    [Test]
    public async Task ShouldMultiplyTwoVectors()
    {
        Vector2 left = new(2f, 3f);
        Vector2 right = new(4f, 5f);

        Vector2 result = left * right;
        await Assert.That(result.X).IsEqualTo(8f);
        await Assert.That(result.Y).IsEqualTo(15f);
    }

    [Test]
    public async Task ShouldMultiplyByScalar()
    {
        const float scalar = 4f;
        Vector2 vector = new(2f, 3f);

        Vector2 result = vector * scalar;
        await Assert.That(result.X).IsEqualTo(8f);
        await Assert.That(result.Y).IsEqualTo(12f);
    }

    [Test]
    public async Task ShouldMultiplyByScalarReversed()
    {
        const float scalar = 4f;
        Vector2 vector = new(2f, 3f);

        Vector2 result = scalar * vector;
        await Assert.That(result.X).IsEqualTo(8f);
        await Assert.That(result.Y).IsEqualTo(12f);
    }

    [Test]
    public async Task ShouldDivideByScalar()
    {
        const float scalar = 2f;
        Vector2 vector = new(8f, 12f);

        Vector2 result = vector / scalar;
        await Assert.That(result.X).IsEqualTo(4f);
        await Assert.That(result.Y).IsEqualTo(6f);
    }

    [Test]
    public async Task DivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const float scalar = 0f;
        Vector2 vector = new(8f, 12f);

        await Assert.That(() => vector / scalar).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task ShouldNegateVector()
    {
        Vector2 vector = new(3f, -4f);

        Vector2 result = -vector;
        await Assert.That(result.X).IsEqualTo(-3f);
        await Assert.That(result.Y).IsEqualTo(4f);
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 4f);

        bool result = left == right;
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 5f);

        bool result = left == right;
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenVectorsAreNotEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 5f);

        bool result = left != right;
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenVectorsAreEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 4f);

        bool result = left != right;
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 4f);

        bool result = left.Equals(right);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(4f, 4f);

        bool result = left.Equals(right);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(3f, 4f);

        bool result = left.Equals((object)right);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2 left = new(3f, 4f);
        Vector2 right = new(4f, 4f);

        bool result = left.Equals((object)right);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptVector2ShouldReturnFalse()
    {
        Vector2 left = new(3f, 4f);
        Size right = new(10f, 10f);

        bool result = left.Equals(right);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Vector2 left = new(3f, 4f);

        bool result = left.Equals(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Vector2 vector = new(3f, 4f);

        string result = vector.ToString();
        await Assert.That(result).IsEqualTo("(3, 4)");
    }

    [Test]
    public async Task BetweenShouldReturnTheAngleBetweenTwoVectors()
    {
        Vector2 from = new(1f, 0f);
        Vector2 to = new(0f, 1f);

        Angle angle = Vector2.Between(from, to);
        await Assert.That(angle.Degrees).IsEqualTo(90f).Within(0.01f);
    }

    [Test]
    public async Task StaticClampShouldClampTheVectorToMaximumLength()
    {
        const float maxLength = 4f;
        Vector2 vector = new(3f, 4f);

        Vector2 clamped = Vector2.Clamp(vector, maxLength);

        await Assert.That(clamped.Magnitude).IsEqualTo(4f).Within(0.0001f);
        await Assert.That(clamped.X).IsEqualTo(2.4f).Within(0.0001f);
        await Assert.That(clamped.Y).IsEqualTo(3.2f).Within(0.0001f);
    }

    [Test]
    public async Task StaticClampShouldReturnTheSameVectorWhenItsLengthIsLessThanMaxLength()
    {
        const float maxLength = 6f;
        Vector2 vector = new(3f, 4f);

        Vector2 clamped = Vector2.Clamp(vector, maxLength);

        await Assert.That(clamped.Magnitude).IsEqualTo(5f).Within(0.0001f);
        await Assert.That(clamped.X).IsEqualTo(3f).Within(0.0001f);
        await Assert.That(clamped.Y).IsEqualTo(4f).Within(0.0001f);
    }

    [Test]
    public async Task StaticCrossShouldComputeCrossProductBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float cross = Vector2.Cross(left, right);
        await Assert.That(cross).IsEqualTo(-2f);
    }

    [Test]
    public async Task StaticDistanceShouldReturnTheDistanceBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(4f, 6f);

        float distance = Vector2.Distance(left, right);
        await Assert.That(distance).IsEqualTo(5f).Within(0.0001f);
    }

    [Test]
    public async Task StaticDotShouldReturnTheCorrectDotProductBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float dot = Vector2.Dot(left, right);
        await Assert.That(dot).IsEqualTo(11f);
    }

    [Test]
    public async Task LerpShouldInterpolateBetweenTwoVectors()
    {
        const float t = 0.5f;
        Vector2 from = new(1f, 2f);
        Vector2 to = new(3f, 4f);

        Vector2 result = Vector2.Lerp(from, to, t);
        await Assert.That(result.X).IsEqualTo(2f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(3f).Within(0.0001f);
    }

    [Test]
    public async Task LerpUnclampedShouldInterpolateBetweenTwoVectorsWithoutClamping()
    {
        const float t = 1.5f;
        Vector2 from = new(1f, 2f);
        Vector2 to = new(3f, 4f);

        Vector2 result = Vector2.LerpUnclamped(from, to, t);
        await Assert.That(result.X).IsEqualTo(4f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(5f).Within(0.0001f);
    }

    [Test]
    public async Task MaxShouldReturnTheVectorWithMaximumComponents()
    {
        Vector2 left = new(1f, 5f);
        Vector2 right = new(3f, 4f);

        Vector2 result = Vector2.Max(left, right);
        await Assert.That(result.X).IsEqualTo(3f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(5f).Within(0.0001f);
    }

    [Test]
    public async Task MinShouldReturnTheVectorWithMinimumComponents()
    {
        Vector2 left = new(1f, 5f);
        Vector2 right = new(3f, 4f);

        Vector2 result = Vector2.Min(left, right);
        await Assert.That(result.X).IsEqualTo(1f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(4f).Within(0.0001f);
    }

    [Test]
    public async Task StaticMoveTowardsShouldMoveTheVectorTowardsTargetByMaxDistance()
    {
        const float maxDistance = 2f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = Vector2.MoveTowards(current, target, maxDistance);
        await Assert.That(result.X).IsEqualTo(2.2f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(3.6f).Within(0.0001f);
    }

    [Test]
    public async Task StaticMoveTowardsShouldReturnTargetWhenWithinMaxDistance()
    {
        const float maxDistance = 10f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = Vector2.MoveTowards(current, target, maxDistance);
        await Assert.That(result.X).IsEqualTo(4f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(6f).Within(0.0001f);
    }

    [Test]
    public async Task StaticMoveTowardsShouldReturnTargetWhenDistanceIsZero()
    {
        const float maxDistance = 0f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = Vector2.MoveTowards(current, target, maxDistance);
        await Assert.That(result.X).IsEqualTo(1f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(2f).Within(0.0001f);
    }

    [Test]
    public async Task StaticReflectShouldReturnTheReflectedVector()
    {
        Vector2 vector = new(1f, -1f);
        Vector2 normal = new Vector2(0f, 1f).Normalized;

        Vector2 reflected = Vector2.Reflect(vector, normal);
        await Assert.That(reflected.X).IsEqualTo(1f).Within(0.0001f);
        await Assert.That(reflected.Y).IsEqualTo(1f).Within(0.0001f);
    }

    [Test]
    public async Task StaticRotateShouldReturnTheRotatedVector()
    {
        Vector2 vector = new(1f, 0f);
        Angle angle = Angle.FromDegrees(90f);

        Vector2 rotated = Vector2.Rotate(vector, angle);
        await Assert.That(rotated.X).IsEqualTo(0f).Within(0.01f);
        await Assert.That(rotated.Y).IsEqualTo(1f).Within(0.01f);
    }

    [Test]
    public async Task StaticScaleShouldReturnTheScaledVector()
    {
        Vector2 vector = new(2f, 3f);
        Vector2 scale = new(4f, 5f);

        Vector2 result = Vector2.Scale(vector, scale);
        await Assert.That(result.X).IsEqualTo(8f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(15f).Within(0.0001f);
    }

    [Test]
    public async Task AngleShouldReturnTheAngleBetweenTwoVectors()
    {
        Vector2 from = new(1f, 0f);
        Vector2 to = new(0f, 1f);

        Angle angle = from.Angle(to);
        await Assert.That(angle.Degrees).IsEqualTo(90f).Within(0.01f);
    }

    [Test]
    public async Task ClampShouldClampTheVectorToMaximumLength()
    {
        const float maxLength = 4f;
        Vector2 vector = new(3f, 4f);

        Vector2 clamped = vector.Clamp(maxLength);

        await Assert.That(clamped.Magnitude).IsEqualTo(4f).Within(0.0001f);
        await Assert.That(clamped.X).IsEqualTo(2.4f).Within(0.0001f);
        await Assert.That(clamped.Y).IsEqualTo(3.2f).Within(0.0001f);
    }

    [Test]
    public async Task ClampShouldReturnTheSameVectorWhenItsLengthIsLessThanMaxLength()
    {
        const float maxLength = 6f;
        Vector2 vector = new(3f, 4f);

        Vector2 clamped = vector.Clamp(maxLength);

        await Assert.That(clamped.Magnitude).IsEqualTo(5f).Within(0.0001f);
        await Assert.That(clamped.X).IsEqualTo(3f).Within(0.0001f);
        await Assert.That(clamped.Y).IsEqualTo(4f).Within(0.0001f);
    }

    [Test]
    public async Task CrossShouldComputeCrossProductBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float cross = left.Cross(right);
        await Assert.That(cross).IsEqualTo(-2f);
    }

    [Test]
    public async Task ShouldDeconstructVector2IntoItsComponents()
    {
        (float x, float y) = new Vector2(3f, 4f);

        await Assert.That(x).IsEqualTo(3f);
        await Assert.That(y).IsEqualTo(4f);
    }

    [Test]
    public async Task DistanceShouldReturnTheDistanceBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(4f, 6f);

        float distance = left.Distance(right);
        await Assert.That(distance).IsEqualTo(5f).Within(0.0001f);
    }

    [Test]
    public async Task DotShouldReturnTheCorrectDotProductBetweenTwoVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float dot = left.Dot(right);
        await Assert.That(dot).IsEqualTo(11f);
    }

    [Test]
    public async Task MoveTowardsShouldMoveTheVectorTowardsTargetByMaxDistance()
    {
        const float maxDistance = 2f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = current.MoveTowards(target, maxDistance);
        await Assert.That(result.X).IsEqualTo(2.2f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(3.6f).Within(0.0001f);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenWithinMaxDistance()
    {
        const float maxDistance = 10f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = current.MoveTowards(target, maxDistance);
        await Assert.That(result.X).IsEqualTo(4f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(6f).Within(0.0001f);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenDistanceIsZero()
    {
        const float maxDistance = 0f;
        Vector2 current = new(1f, 2f);
        Vector2 target = new(4f, 6f);

        Vector2 result = current.MoveTowards(target, maxDistance);
        await Assert.That(result.X).IsEqualTo(1f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(2f).Within(0.0001f);
    }

    [Test]
    public async Task ReflectShouldReturnTheReflectedVector()
    {
        Vector2 vector = new(1f, -1f);
        Vector2 normal = new Vector2(0f, 1f).Normalized;

        Vector2 reflected = vector.Reflect(normal);
        await Assert.That(reflected.X).IsEqualTo(1f).Within(0.0001f);
        await Assert.That(reflected.Y).IsEqualTo(1f).Within(0.0001f);
    }

    [Test]
    public async Task RotateShouldReturnTheRotatedVector()
    {
        Vector2 vector = new(1f, 0f);
        Angle angle = Angle.FromDegrees(90f);

        Vector2 rotated = vector.Rotate(angle);
        await Assert.That(rotated.X).IsEqualTo(0f).Within(0.01f);
        await Assert.That(rotated.Y).IsEqualTo(1f).Within(0.01f);
    }

    [Test]
    public async Task ScaleShouldReturnTheScaledVector()
    {
        Vector2 vector = new(2f, 3f);
        Vector2 scale = new(4f, 5f);

        Vector2 result = vector.Scale(scale);
        await Assert.That(result.X).IsEqualTo(8f).Within(0.0001f);
        await Assert.That(result.Y).IsEqualTo(15f).Within(0.0001f);
    }
}

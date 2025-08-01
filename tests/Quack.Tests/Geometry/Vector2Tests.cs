// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using TUnit.Assertions.AssertConditions.Throws;

namespace Quack.Tests.Geometry;

public sealed class Vector2Tests
{
    [Test]
    public async Task ConstructorWithAngleShouldInitializeWithPolarCoordinates()
    {
        const float radius = 5f;
        Angle angle = Angle.FromRadians(45f);

        Vector2 vector = new(radius, angle);

        await Assert.That(vector.X).IsEqualTo(radius * MathF.Cos(angle.Radians));
        await Assert.That(vector.Y).IsEqualTo(radius * MathF.Sin(angle.Radians));
    }

    [Test]
    public async Task EmptyConstructorShouldInitializeToZero()
    {
        Vector2 vector = new();

        await Assert.That(vector.X).IsEqualTo(0f);
        await Assert.That(vector.Y).IsEqualTo(0f);
    }

    [Test]
    public async Task IsNormalizedShouldReturnTrueWhenVectorIsNormalized()
    {
        Vector2 vector = new(0.6f, 0.8f);

        await Assert.That(vector.IsNormalized).IsTrue();
    }

    [Test]
    public async Task IsNormalizedShouldReturnFalseWhenVectorIsNotNormalized()
    {
        Vector2 vector = new(0.6f, 0.9f);

        await Assert.That(vector.IsNormalized).IsFalse();
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
        Vector2 vector = new(1f, 0f);

        await Assert.That(vector.IsZero).IsFalse();
    }

    [Test]
    public async Task MagnitudeSquaredShouldReturnCorrectValue()
    {
        Vector2 vector = new(3f, 4f);

        await Assert.That(vector.MagnitudeSquared).IsEqualTo(25f);
    }

    [Test]
    public async Task MagnitudeShouldReturnCorrectValue()
    {
        Vector2 vector = new(3f, 4f);

        await Assert.That(vector.Magnitude).IsEqualTo(5f);
    }

    [Test]
    public async Task NormalizedShouldReturnHimselfWhenAlreadyNormalized()
    {
        Vector2 vector = new(0.6f, 0.8f);
        Vector2 normalized = vector.Normalized;

        await Assert.That(normalized.X).IsEqualTo(vector.X);
        await Assert.That(normalized.Y).IsEqualTo(vector.Y);
    }

    [Test]
    public async Task NormalizedShouldReturnZeroWhenMagnitudeIsZero()
    {
        Vector2 vector = new(0f, 0f);
        Vector2 normalized = vector.Normalized;

        await Assert.That(normalized).IsEqualTo(Vector2.Zero);
    }

    [Test]
    public async Task NormalizedShouldReturnCorrectNormalizedVector()
    {
        Vector2 vector = new(3f, 4f);
        Vector2 normalized = vector.Normalized;

        await Assert.That(normalized.X).IsEqualTo(0.6f);
        await Assert.That(normalized.Y).IsEqualTo(0.8f);
    }

    [Test]
    public async Task DownShouldReturnCorrectVector()
    {
        Vector2 down = Vector2.Down;

        await Assert.That(down.X).IsEqualTo(0f);
        await Assert.That(down.Y).IsEqualTo(-1f);
    }

    [Test]
    public async Task UpShouldReturnCorrectVector()
    {
        Vector2 up = Vector2.Up;

        await Assert.That(up.X).IsEqualTo(0f);
        await Assert.That(up.Y).IsEqualTo(1f);
    }

    [Test]
    public async Task RightShouldReturnCorrectVector()
    {
        Vector2 right = Vector2.Right;

        await Assert.That(right.X).IsEqualTo(1f);
        await Assert.That(right.Y).IsEqualTo(0f);
    }

    [Test]
    public async Task LeftShouldReturnCorrectVector()
    {
        Vector2 left = Vector2.Left;

        await Assert.That(left.X).IsEqualTo(-1f);
        await Assert.That(left.Y).IsEqualTo(0f);
    }

    [Test]
    public async Task OneShouldReturnCorrectVector()
    {
        Vector2 one = Vector2.One;

        await Assert.That(one.X).IsEqualTo(1f);
        await Assert.That(one.Y).IsEqualTo(1f);
    }

    [Test]
    public async Task PerpendicularShouldReturnCorrectPerpendicularVector()
    {
        Vector2 vector = new(3f, 4f);
        Vector2 perpendicular = vector.Perpendicular;

        await Assert.That(perpendicular.X).IsEqualTo(-4f);
        await Assert.That(perpendicular.Y).IsEqualTo(3f);
    }

    [Test]
    public async Task ZeroShouldReturnCorrectZeroVector()
    {
        Vector2 zero = Vector2.Zero;

        await Assert.That(zero.X).IsEqualTo(0f);
        await Assert.That(zero.Y).IsEqualTo(0f);
    }

    [Test]
    public async Task AddShouldReturnCorrectSumOfVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        Vector2 sum = left + right;

        await Assert.That(sum.X).IsEqualTo(4f);
        await Assert.That(sum.Y).IsEqualTo(6f);
    }

    [Test]
    public async Task SubtractShouldReturnCorrectDifferenceOfVectors()
    {
        Vector2 left = new(5f, 6f);
        Vector2 right = new(3f, 4f);

        Vector2 difference = left - right;

        await Assert.That(difference.X).IsEqualTo(2f);
        await Assert.That(difference.Y).IsEqualTo(2f);
    }

    [Test]
    public async Task MultiplyByScalarShouldReturnCorrectScaledVector()
    {
        Vector2 vector = new(2f, 3f);
        const float scalar = 4f;

        Vector2 scaled = vector * scalar;

        await Assert.That(scaled.X).IsEqualTo(8f);
        await Assert.That(scaled.Y).IsEqualTo(12f);
    }

    [Test]
    public async Task InvertedMultiplyByScalarShouldReturnCorrectScaledVector()
    {
        Vector2 vector = new(2f, 3f);
        const float scalar = 4f;

        Vector2 scaled = scalar * vector;

        await Assert.That(scaled.X).IsEqualTo(8f);
        await Assert.That(scaled.Y).IsEqualTo(12f);
    }

    [Test]
    public async Task DivideByScalarShouldReturnCorrectScaledVector()
    {
        Vector2 vector = new(8f, 12f);
        const float scalar = 4f;

        Vector2 scaled = vector / scalar;

        await Assert.That(scaled.X).IsEqualTo(2f);
        await Assert.That(scaled.Y).IsEqualTo(3f);
    }

    [Test]
    public async Task DivideByZeroShouldThrows()
    {
        Vector2 vector = new(1f, 2f);
        const float scalar = 0f;

        await Assert.That(() => vector / scalar).ThrowsExactly<DivideByZeroException>();
    }

    [Test]
    public async Task UnaryMinusShouldReturnCorrectNegatedVector()
    {
        Vector2 vector = new(1f, -2f);
        Vector2 negated = -vector;

        await Assert.That(negated.X).IsEqualTo(-1f);
        await Assert.That(negated.Y).IsEqualTo(2f);
    }

    [Test]
    public async Task EqualityOperatorShouldReturnTrueForEqualVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(1f, 2f);

        bool areEqual = left == right;

        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task EqualityOperatorShouldReturnFalseForDifferentVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        bool areEqual = left == right;

        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task InequalityOperatorShouldReturnTrueForDifferentVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        bool areNotEqual = left != right;

        await Assert.That(areNotEqual).IsTrue();
    }

    [Test]
    public async Task InequalityOperatorShouldReturnFalseForEqualVectors()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(1f, 2f);

        bool areNotEqual = left != right;

        await Assert.That(areNotEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueForEqualVectors()
    {
        Vector2 vector = new(1f, 2f);
        object other = new Vector2(1f, 2f);

        bool isEqual = vector.Equals(other);

        await Assert.That(isEqual).IsTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForDifferentVectors()
    {
        Vector2 vector = new(1f, 2f);
        object other = new Vector2(3f, 4f);

        bool isEqual = vector.Equals(other);

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForNull()
    {
        Vector2 vector = new(1f, 2f);
        object? other = null;

        bool isEqual = vector.Equals(other);

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForDifferentType()
    {
        Vector2 vector = new(1f, 2f);
        object other = "Not a Vector2";

        bool isEqual = vector.Equals(other);

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task ToStringShouldReturnCorrectStringRepresentation()
    {
        Vector2 vector = new(1f, 2f);

        string result = vector.ToString();

        await Assert.That(result).IsEqualTo("(1, 2)");
    }

    [Test]
    public async Task DotProductShouldReturnCorrectValue()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        float dotProduct = Vector2.Dot(left, right);

        await Assert.That(dotProduct).IsEqualTo(11f);
    }

    [Test]
    public async Task AngleShouldReturnCorrectAngleBetweenVectors()
    {
        Vector2 from = new(1f, 0f);
        Vector2 to = new(0f, 1f);

        Angle angle = Vector2.Angle(from, to);

        await Assert.That(angle.Degrees).IsEqualTo(90f);
    }

    [Test]
    public async Task ClampShouldReturnClampedVectorWhenMagnitudeExceedsMaxLength()
    {
        Vector2 vector = new(3f, 4f);
        const float maxLength = 5f;

        Vector2 clamped = Vector2.Clamp(vector, maxLength);

        await Assert.That(clamped.X).IsEqualTo(3f);
        await Assert.That(clamped.Y).IsEqualTo(4f);
    }

    [Test]
    public async Task ClampShouldReturnOriginalVectorWhenMagnitudeIsWithinMaxLength()
    {
        Vector2 vector = new(1f, 2f);
        const float maxLength = 5f;

        Vector2 clamped = Vector2.Clamp(vector, maxLength);

        await Assert.That(clamped.X).IsEqualTo(1f);
        await Assert.That(clamped.Y).IsEqualTo(2f);
    }

    [Test]
    public async Task DistanceShouldReturnCorrectDistanceBetweenVectors()
    {
        Vector2 from = new(1f, 2f);
        Vector2 to = new(4f, 6f);

        float distance = Vector2.Distance(from, to);

        await Assert.That(distance).IsEqualTo(5f);
    }

    [Test]
    public async Task LerpShouldReturnCorrectInterpolatedVector()
    {
        Vector2 start = new(1f, 2f);
        Vector2 end = new(4f, 6f);
        const float t = 0.5f;

        Vector2 result = Vector2.Lerp(start, end, t);

        await Assert.That(result.X).IsEqualTo(2.5f);
        await Assert.That(result.Y).IsEqualTo(4f);
    }

    [Test]
    public async Task LerpUnclampedShouldReturnCorrectInterpolatedVector()
    {
        Vector2 start = new(1f, 2f);
        Vector2 end = new(4f, 6f);
        const float t = 0.5f;

        Vector2 result = Vector2.LerpUnclamped(start, end, t);

        await Assert.That(result.X).IsEqualTo(2.5f);
        await Assert.That(result.Y).IsEqualTo(4f);
    }

    [Test]
    public async Task MaxShouldReturnCorrectMaxVector()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        Vector2 max = Vector2.Max(left, right);

        await Assert.That(max).IsEqualTo(right);
    }

    [Test]
    public async Task MinShouldReturnCorrectMinVector()
    {
        Vector2 left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        Vector2 min = Vector2.Min(left, right);

        await Assert.That(min).IsEqualTo(left);
    }

    [Test]
    public async Task MoveTowardsShouldReturnCorrectVector()
    {
        Vector2 current = new(1f, 2f);
        Vector2 target = new(3f, 4f);
        const float maxDistanceDelta = 5f;

        Vector2 result = Vector2.MoveTowards(current, target, maxDistanceDelta);

        await Assert.That(result.X).IsEqualTo(3f);
        await Assert.That(result.Y).IsEqualTo(4f);
    }

    [Test]
    public async Task MoveTowardsShouldReturnTargetWhenCurrentIsAlreadyAtTarget()
    {
        Vector2 current = new(3f, 4f);
        Vector2 target = new(3f, 4f);
        const float maxDistanceDelta = 2f;

        Vector2 result = Vector2.MoveTowards(current, target, maxDistanceDelta);

        await Assert.That(result).IsEqualTo(target);
    }

    [Test]
    public async Task ScaleShouldReturnCorrectScaledVector()
    {
        Vector2 vector = new(2f, 3f);
        Vector2 scale = new(4f, 5f);

        Vector2 scaled = Vector2.Scale(vector, scale);

        await Assert.That(scaled.X).IsEqualTo(8f);
        await Assert.That(scaled.Y).IsEqualTo(15f);
    }

    [Test]
    public async Task ReflectShouldReturnCorrectReflectedVector()
    {
        Vector2 vector = new(1f, -1f);
        Vector2 normal = new(0f, 1f);

        Vector2 reflected = Vector2.Reflect(vector, normal);

        await Assert.That(reflected.X).IsEqualTo(1f);
        await Assert.That(reflected.Y).IsEqualTo(1f);
    }

    [Test]
    public async Task RotateShouldReturnCorrectRotatedVector()
    {
        Vector2 vector = new(0f, 1f);
        Angle angle = Angle.FromDegrees(90f);

        Vector2 rotated = Vector2.Rotate(vector, angle);

        await Assert.That(rotated.X).IsEqualTo(-1f);
        await Assert.That(rotated.Y).IsEqualTo(0f).Within(0.0001f);
    }
}

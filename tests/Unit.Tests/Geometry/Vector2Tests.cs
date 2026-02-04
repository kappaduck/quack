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
}

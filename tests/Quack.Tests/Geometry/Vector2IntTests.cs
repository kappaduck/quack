// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using TUnit.Assertions.AssertConditions.Throws;

namespace Quack.Tests.Geometry;

public sealed class Vector2IntTests
{
    [Test]
    public async Task IsZeroShouldReturnTrueWhenVectorIsZero()
    {
        Vector2Int vector = new(0, 0);

        await Assert.That(vector.IsZero).IsTrue();
    }

    [Test]
    [Arguments(1, 0)]
    [Arguments(0, 1)]
    public async Task IsZeroShouldReturnFalseWhenVectorIsNotZero(int x, int y)
    {
        Vector2Int vector = new(x, y);

        await Assert.That(vector.IsZero).IsFalse();
    }

    [Test]
    public async Task MagnitudeSquaredShouldReturnCorrectValue()
    {
        Vector2Int vector = new(3, 4);

        await Assert.That(vector.MagnitudeSquared).IsEqualTo(25);
    }

    [Test]
    public async Task MagnitudeShouldReturnCorrectValue()
    {
        Vector2Int vector = new(3, 4);

        await Assert.That(vector.Magnitude).IsEqualTo(5);
    }

    [Test]
    public async Task DownShouldReturnCorrectVector()
    {
        Vector2Int down = Vector2Int.Down;

        await Assert.That(down.X).IsEqualTo(0);
        await Assert.That(down.Y).IsEqualTo(-1);
    }

    [Test]
    public async Task UpShouldReturnCorrectVector()
    {
        Vector2Int up = Vector2Int.Up;

        await Assert.That(up.X).IsEqualTo(0);
        await Assert.That(up.Y).IsEqualTo(1);
    }

    [Test]
    public async Task RightShouldReturnCorrectVector()
    {
        Vector2Int right = Vector2Int.Right;

        await Assert.That(right.X).IsEqualTo(1);
        await Assert.That(right.Y).IsEqualTo(0);
    }

    [Test]
    public async Task LeftShouldReturnCorrectVector()
    {
        Vector2Int left = Vector2Int.Left;

        await Assert.That(left.X).IsEqualTo(-1);
        await Assert.That(left.Y).IsEqualTo(0);
    }

    [Test]
    public async Task OneShouldReturnCorrectVector()
    {
        Vector2Int one = Vector2Int.One;

        await Assert.That(one.X).IsEqualTo(1);
        await Assert.That(one.Y).IsEqualTo(1);
    }

    [Test]
    public async Task PerpendicularShouldReturnCorrectPerpendicularVector()
    {
        Vector2Int vector = new(3, 4);
        Vector2Int perpendicular = vector.Perpendicular;

        await Assert.That(perpendicular.X).IsEqualTo(-4);
        await Assert.That(perpendicular.Y).IsEqualTo(3);
    }

    [Test]
    public async Task ZeroShouldReturnCorrectZeroVector()
    {
        Vector2Int zero = Vector2Int.Zero;

        await Assert.That(zero.X).IsEqualTo(0);
        await Assert.That(zero.Y).IsEqualTo(0);
    }

    [Test]
    public async Task Vector2IntShouldBeConvertibleToVector2()
    {
        Vector2 vector2 = new Vector2Int(3, 4);

        await Assert.That(vector2.X).IsEqualTo(3f);
        await Assert.That(vector2.Y).IsEqualTo(4f);
    }

    [Test]
    public async Task AddShouldReturnCorrectSumOfVectors()
    {
        Vector2Int left = new(1, 2);
        Vector2Int right = new(3, 4);

        Vector2Int sum = left + right;

        await Assert.That(sum.X).IsEqualTo(4);
        await Assert.That(sum.Y).IsEqualTo(6);
    }

    [Test]
    public async Task SubtractShouldReturnCorrectDifferenceOfVectors()
    {
        Vector2Int left = new(5, 6);
        Vector2Int right = new(3, 4);

        Vector2Int difference = left - right;

        await Assert.That(difference.X).IsEqualTo(2);
        await Assert.That(difference.Y).IsEqualTo(2);
    }

    [Test]
    public async Task MultiplyByScalarShouldReturnCorrectScaledVector()
    {
        Vector2Int vector = new(2, 3);
        const int scalar = 4;

        Vector2Int scaled = vector * scalar;

        await Assert.That(scaled.X).IsEqualTo(8);
        await Assert.That(scaled.Y).IsEqualTo(12);
    }

    [Test]
    public async Task InvertedMultiplyByScalarShouldReturnCorrectScaledVector()
    {
        Vector2Int vector = new(2, 3);
        const int scalar = 4;

        Vector2Int scaled = scalar * vector;

        await Assert.That(scaled.X).IsEqualTo(8);
        await Assert.That(scaled.Y).IsEqualTo(12);
    }

    [Test]
    public async Task DivideByScalarShouldReturnCorrectScaledVector()
    {
        Vector2Int vector = new(8, 12);
        const int scalar = 4;

        Vector2Int scaled = vector / scalar;

        await Assert.That(scaled.X).IsEqualTo(2);
        await Assert.That(scaled.Y).IsEqualTo(3);
    }

    [Test]
    public async Task DivideByZeroShouldThrows()
    {
        Vector2Int vector = new(1, 2);
        const int scalar = 0;

        await Assert.That(() => vector / scalar).ThrowsExactly<DivideByZeroException>();
    }

    [Test]
    public async Task UnaryMinusShouldReturnCorrectNegatedVector()
    {
        Vector2Int vector = new(1, -2);
        Vector2Int negated = -vector;

        await Assert.That(negated.X).IsEqualTo(-1);
        await Assert.That(negated.Y).IsEqualTo(2);
    }

    [Test]
    public async Task EqualityOperatorShouldReturnTrueForEqualVectors()
    {
        Vector2Int left = new(1, 2);
        Vector2Int right = new(1, 2);

        bool areEqual = left == right;

        await Assert.That(areEqual).IsTrue();
    }

    [Test]
    public async Task EqualityOperatorShouldReturnFalseForDifferentVectors()
    {
        Vector2Int left = new(1, 2);
        Vector2Int right = new(3, 4);

        bool areEqual = left == right;

        await Assert.That(areEqual).IsFalse();
    }

    [Test]
    public async Task InequalityOperatorShouldReturnTrueForDifferentVectors()
    {
        Vector2Int left = new(1, 2);
        Vector2Int right = new(3, 4);

        bool areNotEqual = left != right;

        await Assert.That(areNotEqual).IsTrue();
    }

    [Test]
    public async Task InequalityOperatorShouldReturnFalseForEqualVectors()
    {
        Vector2Int left = new(1, 2);
        Vector2Int right = new(1, 2);

        bool areNotEqual = left != right;

        await Assert.That(areNotEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueForEqualVectors()
    {
        Vector2Int vector = new(1, 2);
        object other = new Vector2Int(1, 2);

        bool isEqual = vector.Equals(other);

        await Assert.That(isEqual).IsTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForDifferentVectors()
    {
        Vector2Int vector = new(1, 2);
        object other = new Vector2Int(3, 4);

        bool isEqual = vector.Equals(other);

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForNull()
    {
        Vector2Int vector = new(1, 2);
        object? other = null;

        bool isEqual = vector.Equals(other);

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForDifferentType()
    {
        Vector2Int vector = new(1, 2);
        object other = "Not a Vector2";

        bool isEqual = vector.Equals(other);

        await Assert.That(isEqual).IsFalse();
    }

    [Test]
    public async Task ToStringShouldReturnCorrectStringRepresentation()
    {
        Vector2Int vector = new(1, 2);

        string result = vector.ToString();

        await Assert.That(result).IsEqualTo("(1, 2)");
    }

    [Test]
    public async Task ToVector2ShouldConvertVector2IntToVector2()
    {
        Vector2Int vector = new(3, 4);
        Vector2 converted = vector.ToVector2();

        await Assert.That(converted.X).IsEqualTo(3f);
        await Assert.That(converted.Y).IsEqualTo(4f);
    }
}

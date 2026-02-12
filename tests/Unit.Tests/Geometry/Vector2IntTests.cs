// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

public sealed class Vector2IntTests
{
    [Test]
    public async Task VectorShouldGiveTheMagnitudeSquared()
    {
        Vector2Int vector = new(3, 4);
        await Assert.That(vector.MagnitudeSquared).IsEqualTo(25);
    }

    [Test]
    public async Task VectorShouldGiveTheMagnitude()
    {
        Vector2Int vector = new(3, 4);
        await Assert.That(vector.Magnitude).IsEqualTo(5);
    }

    [Test]
    public async Task IsZeroShouldReturnTrueWhenVectorIsZero()
    {
        Vector2Int vector = new(0, 0);
        await Assert.That(vector.IsZero).IsTrue();
    }

    [Test]
    public async Task IsZeroShouldReturnFalseWhenVectorIsNotZero()
    {
        Vector2Int vector = new(3, 4);
        await Assert.That(vector.IsZero).IsFalse();
    }

    [Test]
    public async Task LeftPerpendicularShouldReturnTheLeftPerpendicularVector()
    {
        Vector2Int vector = new(3, 4);
        Vector2Int leftPerpendicular = vector.LeftPerpendicular;

        await Assert.That(leftPerpendicular.X).IsEqualTo(-4);
        await Assert.That(leftPerpendicular.Y).IsEqualTo(3);
    }

    [Test]
    public async Task RightPerpendicularShouldReturnTheRightPerpendicularVector()
    {
        Vector2Int vector = new(3, 4);
        Vector2Int rightPerpendicular = vector.RightPerpendicular;

        await Assert.That(rightPerpendicular.X).IsEqualTo(4);
        await Assert.That(rightPerpendicular.Y).IsEqualTo(-3);
    }

    [Test]
    public async Task DownShouldReturnTheDownVector()
    {
        Vector2Int down = Vector2Int.Down;

        await Assert.That(down.X).IsEqualTo(0);
        await Assert.That(down.Y).IsEqualTo(1);
    }

    [Test]
    public async Task LeftShouldReturnTheLeftVector()
    {
        Vector2Int left = Vector2Int.Left;

        await Assert.That(left.X).IsEqualTo(-1);
        await Assert.That(left.Y).IsEqualTo(0);
    }

    [Test]
    public async Task RightShouldReturnTheRightVector()
    {
        Vector2Int right = Vector2Int.Right;

        await Assert.That(right.X).IsEqualTo(1);
        await Assert.That(right.Y).IsEqualTo(0);
    }

    [Test]
    public async Task UpShouldReturnTheUpVector()
    {
        Vector2Int up = Vector2Int.Up;

        await Assert.That(up.X).IsEqualTo(0);
        await Assert.That(up.Y).IsEqualTo(-1);
    }

    [Test]
    public async Task ZeroShouldReturnTheZeroVector()
    {
        Vector2Int zero = Vector2Int.Zero;

        await Assert.That(zero.X).IsEqualTo(0);
        await Assert.That(zero.Y).IsEqualTo(0);
    }

    [Test]
    public async Task ShouldAddTwoVectors()
    {
        Vector2Int left = new(1, 2);
        Vector2Int right = new(3, 4);

        Vector2Int result = left + right;
        await Assert.That(result.X).IsEqualTo(4);
        await Assert.That(result.Y).IsEqualTo(6);
    }

    [Test]
    public async Task ShouldSubstractTwoVectors()
    {
        Vector2Int left = new(5, 7);
        Vector2Int right = new(3, 4);

        Vector2Int result = left - right;
        await Assert.That(result.X).IsEqualTo(2);
        await Assert.That(result.Y).IsEqualTo(3);
    }

    [Test]
    public async Task ShouldMultiplyTwoVectors()
    {
        Vector2Int left = new(2, 3);
        Vector2Int right = new(4, 5);

        Vector2Int result = left * right;
        await Assert.That(result.X).IsEqualTo(8);
        await Assert.That(result.Y).IsEqualTo(15);
    }

    [Test]
    public async Task ShouldMultiplyByScalar()
    {
        const int scalar = 4;
        Vector2Int vector = new(2, 3);

        Vector2Int result = vector * scalar;
        await Assert.That(result.X).IsEqualTo(8);
        await Assert.That(result.Y).IsEqualTo(12);
    }

    [Test]
    public async Task ShouldMultiplyByScalarReversed()
    {
        const int scalar = 4;
        Vector2Int vector = new(2, 3);

        Vector2Int result = scalar * vector;
        await Assert.That(result.X).IsEqualTo(8);
        await Assert.That(result.Y).IsEqualTo(12);
    }

    [Test]
    public async Task ShouldDivideByScalar()
    {
        const int scalar = 2;
        Vector2Int vector = new(8, 12);

        Vector2Int result = vector / scalar;
        await Assert.That(result.X).IsEqualTo(4);
        await Assert.That(result.Y).IsEqualTo(6);
    }

    [Test]
    public async Task DivideShouldThrowsDivideByZeroExceptionWhenScalarIsZero()
    {
        const int scalar = 0;
        Vector2Int vector = new(8, 12);

        await Assert.That(() => vector / scalar).Throws<DivideByZeroException>();
    }

    [Test]
    public async Task ShouldNegateVector()
    {
        Vector2Int vector = new(3, -4);

        Vector2Int result = -vector;
        await Assert.That(result.X).IsEqualTo(-3);
        await Assert.That(result.Y).IsEqualTo(4);
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2Int left = new(3, 4);
        Vector2Int right = new(3, 4);

        bool result = left == right;
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2Int left = new(3, 4);
        Vector2Int right = new(3, 5);

        bool result = left == right;
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenVectorsAreNotEquals()
    {
        Vector2Int left = new(3, 4);
        Vector2Int right = new(3, 5);

        bool result = left != right;
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenVectorsAreEquals()
    {
        Vector2Int left = new(3, 4);
        Vector2Int right = new(3, 4);

        bool result = left != right;
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2Int left = new(3, 4);
        Vector2Int right = new(3, 4);

        bool result = left.Equals(right);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2Int left = new(3, 4);
        Vector2Int right = new(4, 4);

        bool result = left.Equals(right);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenVectorsAreEquals()
    {
        Vector2Int left = new(3, 4);
        Vector2Int right = new(3, 4);

        bool result = left.Equals((object)right);
        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenVectorsAreNotEquals()
    {
        Vector2Int left = new(3, 4);
        Vector2Int right = new(4, 4);

        bool result = left.Equals((object)right);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptVector2IntShouldReturnFalse()
    {
        Vector2Int left = new(3, 4);
        Size right = new(10f, 10f);

        bool result = left.Equals(right);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Vector2Int left = new(3, 4);

        bool result = left.Equals(null);
        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Vector2Int vector = new(3, 4);

        string result = vector.ToString();
        await Assert.That(result).IsEqualTo("(3, 4)");
    }

    [Test]
    public async Task ToVector2ShouldConvertTheVectorToVector2()
    {
        Vector2Int vectorInt = new(3, 4);

        Vector2 result = vectorInt.ToVector2();
        await Assert.That(result.X).IsEqualTo(3f);
        await Assert.That(result.Y).IsEqualTo(4f);
    }

    [Test]
    public async Task ShouldDeconstructVector2IntIntoItsComponents()
    {
        (int x, int y) = new Vector2Int(3, 4);

        await Assert.That(x).IsEqualTo(3);
        await Assert.That(y).IsEqualTo(4);
    }
}

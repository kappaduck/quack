// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class PointTests
{
    [Test]
    public async Task OriginShouldReturnTheOriginPoint()
    {
        Point origin = Point.Origin;

        await origin.X.Should().BeZero();
        await origin.Y.Should().BeZero();
    }

    [Test]
    public async Task ToShouldReturnTheDisplacementVector()
    {
        Point from = new(3, 4);
        Point target = new(5, 4);

        Vector2I displacement = from.To(target);

        await displacement.X.Should().BeEqualTo(2);
        await displacement.Y.Should().BeEqualTo(0);
    }

    [Test]
    public async Task DistanceShouldReturnTheDistanceBetweenTwoPoints()
    {
        Point left = new(1, 2);
        Point right = new(4, 6);

        float distance = left.Distance(right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task StaticDistanceShouldReturnTheDistanceBetweenTwoPoints()
    {
        Point left = new(1, 2);
        Point right = new(4, 6);

        float distance = Point.Distance(left, right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task LerpShouldInterpolateBetweenTwoPoints()
    {
        const float t = 0.5f;
        Point from = new(1, 2);
        Point to = new(3, 4);

        PointF result = Point.Lerp(from, to, t);

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task ShouldDeconstructPointIntoItsComponents()
    {
        (int x, int y) = new Point(3, 4);

        await x.Should().BeEqualTo(3);
        await y.Should().BeEqualTo(4);
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenPointsAreEquals()
    {
        Point left = new(3, 4);
        Point right = new(3, 4);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenPointsAreNotEquals()
    {
        Point left = new(3, 4);
        Point right = new(3, 5);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenPointsAreNotEquals()
    {
        Point left = new(3, 4);
        Point right = new(3, 5);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenPointsAreEquals()
    {
        Point left = new(3, 4);
        Point right = new(3, 4);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenPointsAreEquals()
    {
        Point left = new(3, 4);
        Point right = new(3, 4);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenPointsAreNotEquals()
    {
        Point left = new(3, 4);
        Point right = new(4, 4);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenPointsAreEquals()
    {
        Point left = new(3, 4);
        object right = new Point(3, 4);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenPointsAreNotEquals()
    {
        Point left = new(3, 4);
        object right = new Point(4, 4);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptPointFShouldReturnFalse()
    {
        Point left = new(3, 4);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Point left = new(3, 4);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Point point = new(3, 4);

        string result = point.ToString();
        await result.Should().BeEqualTo("(3, 4)");
    }

    [Test]
    public async Task ToVector2ShouldConvertPointToPointF()
    {
        Point point = new(3, 4);

        PointF converted = point.ToPointF();

        await converted.X.Should().BeEqualTo(point.X);
        await converted.Y.Should().BeEqualTo(point.Y);
    }

    [Test]
    public async Task ToVector2ShouldConvertPointToVector()
    {
        Point point = new(3, 4);

        Vector2 vector = point.ToVector2();

        await vector.X.Should().BeEqualTo(point.X);
        await vector.Y.Should().BeEqualTo(point.Y);
    }

    [Test]
    public async Task ToVector2iShouldConvertPointToVector()
    {
        Point point = new(3, 4);

        Vector2I vector = point.ToVector2i();

        await vector.X.Should().BeEqualTo(point.X);
        await vector.Y.Should().BeEqualTo(point.Y);
    }

    [Test]
    public async Task OperatorAddShouldTranslateAPointByDisplacementVector2i()
    {
        Point left = new(1, 2);
        Vector2I right = new(3, 4);

        Point result = left + right;

        await result.X.Should().BeEqualTo(4);
        await result.Y.Should().BeEqualTo(6);
    }

    [Test]
    public async Task OperatorAddShouldTranslateAPointByDisplacementVector2()
    {
        Point left = new(1, 2);
        Vector2 right = new(3f, 4f);

        PointF result = left + right;

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task OperatorSubtractShouldTranslateAPointBackwardsByDisplacementVector2i()
    {
        Point left = new(5, 7);
        Vector2I right = new(3, 4);

        Point result = left - right;

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }

    [Test]
    public async Task OperatorSubtractShouldTranslateAPointBackwardsByDisplacementVector2()
    {
        Point left = new(5, 7);
        Vector2 right = new(3f, 4f);

        PointF result = left - right;

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task OperatorSubtractShouldComputeTheDisplacementBetweenTwoPoints()
    {
        Point left = new(5, 7);
        Point right = new(3, 4);

        Vector2I result = left - right;

        await result.X.Should().BeEqualTo(2);
        await result.Y.Should().BeEqualTo(3);
    }
}

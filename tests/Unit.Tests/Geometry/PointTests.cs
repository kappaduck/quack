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

        Vector2 displacement = from.To(target);

        await displacement.X.Should().BeEqualTo(2f);
        await displacement.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task DistanceShouldReturnTheDistanceBetweenTwoPoints()
    {
        Point left = new(1f, 2f);
        Point right = new(4f, 6f);

        float distance = left.Distance(right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task FloorShouldConvertPointByFlooringEachComponent()
    {
        Point point = new(7.64f, -7.6f);

        PointI result = point.Floor();
        await result.X.Should().BeEqualTo(7);
        await result.Y.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task RoundShouldConvertPointByRoundingEachComponent()
    {
        Point point = new(7.64f, -7.6f);

        PointI result = point.Round();
        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task TruncateShouldConvertPointByTruncatingEachComponent()
    {
        Point point = new(7.64f, -7.6f);

        PointI result = point.Truncate();
        await result.X.Should().BeEqualTo(7);
        await result.Y.Should().BeEqualTo(-7);
    }

    [Test]
    public async Task StaticDistanceShouldReturnTheDistanceBetweenTwoPoints()
    {
        Point left = new(1f, 2f);
        Point right = new(4f, 6f);

        float distance = Point.Distance(left, right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task LerpShouldInterpolateBetweenTwoPoints()
    {
        const float t = 0.5f;
        Point from = new(1f, 2f);
        Point to = new(3f, 4f);

        Point result = Point.Lerp(from, to, t);

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task LerpUnclampedShouldInterpolateBetweenTwoPointsWithoutClamping()
    {
        const float t = 1.5f;
        Point from = new(1f, 2f);
        Point to = new(3f, 4f);

        Point result = Point.LerpUnclamped(from, to, t);

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(5f);
    }

    [Test]
    public async Task ShouldDeconstructPointIntoItsComponents()
    {
        (float x, float y) = new Point(3f, 4f);

        await x.Should().BeEqualTo(3f);
        await y.Should().BeEqualTo(4f);
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenPointsAreEquals()
    {
        Point left = new(3f, 4f);
        Point right = new(3f, 4f);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenPointsAreNotEquals()
    {
        Point left = new(3f, 4f);
        Point right = new(3f, 5f);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenPointsAreNotEquals()
    {
        Point left = new(3f, 4f);
        Point right = new(3f, 5f);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenPointsAreEquals()
    {
        Point left = new(3f, 4f);
        Point right = new(3f, 4f);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenPointsAreEquals()
    {
        Point left = new(3f, 4f);
        Point right = new(3f, 4f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenPointsAreNotEquals()
    {
        Point left = new(3f, 4f);
        Point right = new(4f, 4f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenPointsAreEquals()
    {
        Point left = new(3f, 4f);
        object right = new Point(3f, 4f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenPointsAreNotEquals()
    {
        Point left = new(3f, 4f);
        object right = new Point(4f, 4f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptPointfShouldReturnFalse()
    {
        Point left = new(3f, 4f);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Point left = new(3f, 4f);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Point point = new(3f, 4f);

        string result = point.ToString();
        await result.Should().BeEqualTo("(3, 4)");
    }

    [Test]
    public async Task ToVector2ShouldConvertPointToVector()
    {
        Point point = new(3f, 4f);

        Vector2 vector = point.ToVector2();

        await vector.X.Should().BeEqualTo(point.X);
        await vector.Y.Should().BeEqualTo(point.Y);
    }

    [Test]
    public async Task OperatorAddShouldTranslateAPointByDisplacementVector2()
    {
        Point left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        Point result = left + right;

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task OperatorAddShouldTranslateAPointByDisplacementVector2i()
    {
        Point left = new(1f, 2f);
        Vector2I right = new(3, 4);

        Point result = left + right;

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task OperatorSubstractShouldTranslateAPointBackwardsByDisplacementVector2()
    {
        Point left = new(5f, 7f);
        Vector2 right = new(3f, 4f);

        Point result = left - right;

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task OperatorSubstractShouldTranslateAPointBackwardsByDisplacementVector2i()
    {
        Point left = new(5f, 7f);
        Vector2I right = new(3, 4);

        Point result = left - right;

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task OperatorSubstractShouldCopmputeTheDisplacementBetweenTwoPoints()
    {
        Point left = new(5f, 7f);
        Point right = new(3f, 4f);

        Vector2 result = left - right;

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }
}

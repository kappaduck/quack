// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class PointfTests
{
    [Test]
    public async Task OriginShouldReturnTheOriginPoint()
    {
        Pointf origin = Pointf.Origin;

        await origin.X.Should().BeEqualTo(0f);
        await origin.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task ToShouldReturnTheDisplacementVector()
    {
        Pointf from = new(3, 4);
        Pointf target = new(5, 4);

        Vector2 displacement = from.To(target);

        await displacement.X.Should().BeEqualTo(2f);
        await displacement.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task DistanceShouldReturnTheDistanceBetweenTwoPoints()
    {
        Pointf left = new(1f, 2f);
        Pointf right = new(4f, 6f);

        float distance = left.Distance(right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task FloorShouldConvertPointByFlooringEachComponent()
    {
        Pointf point = new(7.64f, -7.6f);

        Point result = point.Floor();
        await result.X.Should().BeEqualTo(7);
        await result.Y.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task RoundShouldConvertPointByRoundingEachComponent()
    {
        Pointf point = new(7.64f, -7.6f);

        Point result = point.Round();
        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task TruncateShouldConvertPointByTruncatingEachComponent()
    {
        Pointf point = new(7.64f, -7.6f);

        Point result = point.Truncate();
        await result.X.Should().BeEqualTo(7);
        await result.Y.Should().BeEqualTo(-7);
    }

    [Test]
    public async Task StaticDistanceShouldReturnTheDistanceBetweenTwoPoints()
    {
        Pointf left = new(1f, 2f);
        Pointf right = new(4f, 6f);

        float distance = Pointf.Distance(left, right);
        await distance.Should().BeCloseTo(5f, 0.0001f);
    }

    [Test]
    public async Task LerpShouldInterpolateBetweenTwoPoints()
    {
        const float t = 0.5f;
        Pointf from = new(1f, 2f);
        Pointf to = new(3f, 4f);

        Pointf result = Pointf.Lerp(from, to, t);

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task LerpUnclampedShouldInterpolateBetweenTwoPointsWithoutClamping()
    {
        const float t = 1.5f;
        Pointf from = new(1f, 2f);
        Pointf to = new(3f, 4f);

        Pointf result = Pointf.LerpUnclamped(from, to, t);

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(5f);
    }

    [Test]
    public async Task ShouldDeconstructPointIntoItsComponents()
    {
        (float x, float y) = new Pointf(3f, 4f);

        await x.Should().BeEqualTo(3f);
        await y.Should().BeEqualTo(4f);
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenPointsAreEquals()
    {
        Pointf left = new(3f, 4f);
        Pointf right = new(3f, 4f);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenPointsAreNotEquals()
    {
        Pointf left = new(3f, 4f);
        Pointf right = new(3f, 5f);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenPointsAreNotEquals()
    {
        Pointf left = new(3f, 4f);
        Pointf right = new(3f, 5f);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenPointsAreEquals()
    {
        Pointf left = new(3f, 4f);
        Pointf right = new(3f, 4f);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenPointsAreEquals()
    {
        Pointf left = new(3f, 4f);
        Pointf right = new(3f, 4f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenPointsAreNotEquals()
    {
        Pointf left = new(3f, 4f);
        Pointf right = new(4f, 4f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenPointsAreEquals()
    {
        Pointf left = new(3f, 4f);
        Pointf right = new(3f, 4f);

        bool result = left.Equals((object)right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenPointsAreNotEquals()
    {
        Pointf left = new(3f, 4f);
        Pointf right = new(4f, 4f);

        bool result = left.Equals((object)right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptPointfShouldReturnFalse()
    {
        Pointf left = new(3f, 4f);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Pointf left = new(3f, 4f);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Pointf point = new(3f, 4f);

        string result = point.ToString();
        await result.Should().BeEqualTo("(3, 4)");
    }

    [Test]
    public async Task ToVector2ShouldConvertPointToVector()
    {
        Pointf point = new(3f, 4f);

        Vector2 vector = point.ToVector2();

        await vector.X.Should().BeEqualTo(point.X);
        await vector.Y.Should().BeEqualTo(point.Y);
    }

    [Test]
    public async Task OperatorAddShouldTranslateAPointByDisplacementVector2()
    {
        Pointf left = new(1f, 2f);
        Vector2 right = new(3f, 4f);

        Pointf result = left + right;

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task OperatorAddShouldTranslateAPointByDisplacementVector2i()
    {
        Pointf left = new(1f, 2f);
        Vector2i right = new(3, 4);

        Pointf result = left + right;

        await result.X.Should().BeEqualTo(4f);
        await result.Y.Should().BeEqualTo(6f);
    }

    [Test]
    public async Task OperatorSubstractShouldTranslateAPointBackwardsByDisplacementVector2()
    {
        Pointf left = new(5f, 7f);
        Vector2 right = new(3f, 4f);

        Pointf result = left - right;

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task OperatorSubstractShouldTranslateAPointBackwardsByDisplacementVector2i()
    {
        Pointf left = new(5f, 7f);
        Vector2i right = new(3, 4);

        Pointf result = left - right;

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task OperatorSubstractShouldCopmputeTheDisplacementBetweenTwoPoints()
    {
        Pointf left = new(5f, 7f);
        Pointf right = new(3f, 4f);

        Vector2 result = left - right;

        await result.X.Should().BeEqualTo(2f);
        await result.Y.Should().BeEqualTo(3f);
    }
}

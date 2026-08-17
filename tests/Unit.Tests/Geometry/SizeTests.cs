// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class SizeTests
{
    [Test]
    public async Task AreaShouldCalculateTheArea()
    {
        Size size = new(1920f, 1080f);

        float area = size.Area;
        await area.Should().BeEqualTo(2073600f);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenSizefIsEmpty()
    {
        Size size = new();
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenSizeIsNotEmpty()
    {
        Size size = new(1920f, 1080f);
        await size.IsEmpty.Should().BeFalse();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        Size size = new(0f, 1080f);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        Size size = new(1920f, 0f);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task ZeroShouldReturnAnEmptySize()
    {
        Size zero = Size.Zero;

        await zero.Width.Should().BeZero();
        await zero.Height.Should().BeZero();
    }

    [Test]
    public async Task ShouldDeconstructSizeIntoItsComponents()
    {
        (float x, float y) = new Size(1920f, 1080f);

        await x.Should().BeEqualTo(1920f);
        await y.Should().BeEqualTo(1080f);
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Size size = new(1920f, 1080f);

        string result = size.ToString();
        await result.Should().BeEqualTo("(1920, 1080)");
    }

    [Test]
    public async Task FloorShouldConvertSizeByFlooringEachComponent()
    {
        Size size = new(7.64f, -7.6f);

        SizeI result = size.Floor();

        await result.Width.Should().BeEqualTo(7);
        await result.Height.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task RoundShouldConvertSizeByRoundingEachComponent()
    {
        Size size = new(7.64f, -7.6f);

        SizeI result = size.Round();

        await result.Width.Should().BeEqualTo(8);
        await result.Height.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task TruncateShouldConvertVector2ByTruncatingEachComponent()
    {
        Size size = new(7.64f, -7.6f);

        SizeI result = size.Truncate();

        await result.Width.Should().BeEqualTo(7);
        await result.Height.Should().BeEqualTo(-7);
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenSizesAreEquals()
    {
        Size left = new(1920f, 1080f);
        Size right = new(1920f, 1080f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        Size left = new(1920f, 1080f);
        Size right = new(3440f, 1440f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenSizesAreEquals()
    {
        Size left = new(1920f, 1080f);
        object right = new Size(1920f, 1080f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenSizesAreNotEquals()
    {
        Size left = new(1920f, 1080f);
        object right = new Size(3440f, 1440f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptSizeShouldReturnFalse()
    {
        Size left = new(1920f, 1080f);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Size left = new(1920f, 1080f);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenSizesAreEquals()
    {
        Size left = new(1920f, 1080f);
        Size right = new(1920f, 1080f);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        Size left = new(1920f, 1080f);
        Size right = new(3440f, 1440f);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenSizesAreNotEquals()
    {
        Size left = new(1920f, 1080f);
        Size right = new(3440f, 1440f);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenSizesAreEquals()
    {
        Size left = new(1920f, 1080f);
        Size right = new(1920f, 1080f);

        bool result = left != right;
        await result.Should().BeFalse();
    }
}

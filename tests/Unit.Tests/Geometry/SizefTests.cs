// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class SizefTests
{
    [Test]
    public async Task AreaShouldCalculateTheArea()
    {
        Sizef size = new(1920f, 1080f);

        float area = size.Area;
        await area.Should().BeEqualTo(2073600f);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenSizefIsEmpty()
    {
        Sizef size = new();
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenSizeIsNotEmpty()
    {
        Sizef size = new(1920f, 1080f);
        await size.IsEmpty.Should().BeFalse();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        Sizef size = new(0f, 1080f);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        Sizef size = new(1920f, 0f);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task ZeroShouldReturnAnEmptySize()
    {
        Sizef zero = Sizef.Zero;

        await zero.Width.Should().BeEqualTo(0f);
        await zero.Height.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task ShouldDeconstructSizeIntoItsComponents()
    {
        (float x, float y) = new Sizef(1920f, 1080f);

        await x.Should().BeEqualTo(1920f);
        await y.Should().BeEqualTo(1080f);
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Sizef size = new(1920f, 1080f);

        string result = size.ToString();
        await result.Should().BeEqualTo("(1920, 1080)");
    }

    [Test]
    public async Task FloorShouldConvertSizeByFlooringEachComponent()
    {
        Sizef size = new(7.64f, -7.6f);

        Size result = size.Floor();

        await result.Width.Should().BeEqualTo(7);
        await result.Height.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task RoundShouldConvertSizeByRoundingEachComponent()
    {
        Sizef size = new(7.64f, -7.6f);

        Size result = size.Round();

        await result.Width.Should().BeEqualTo(8);
        await result.Height.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task TruncateShouldConvertVector2ByTruncatingEachComponent()
    {
        Sizef size = new(7.64f, -7.6f);

        Size result = size.Truncate();

        await result.Width.Should().BeEqualTo(7);
        await result.Height.Should().BeEqualTo(-7);
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenSizesAreEquals()
    {
        Sizef left = new(1920f, 1080f);
        Sizef right = new(1920f, 1080f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        Sizef left = new(1920f, 1080f);
        Sizef right = new(3440f, 1440f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenSizesAreEquals()
    {
        Sizef left = new(1920f, 1080f);
        object right = new Sizef(1920f, 1080f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenSizesAreNotEquals()
    {
        Sizef left = new(1920f, 1080f);
        object right = new Sizef(3440f, 1440f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptSizeShouldReturnFalse()
    {
        Sizef left = new(1920f, 1080f);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Sizef left = new(1920f, 1080f);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenSizesAreEquals()
    {
        Sizef left = new(1920f, 1080f);
        Sizef right = new(1920f, 1080f);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        Sizef left = new(1920f, 1080f);
        Sizef right = new(3440f, 1440f);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenSizesAreNotEquals()
    {
        Sizef left = new(1920f, 1080f);
        Sizef right = new(3440f, 1440f);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenSizesAreEquals()
    {
        Sizef left = new(1920f, 1080f);
        Sizef right = new(1920f, 1080f);

        bool result = left != right;
        await result.Should().BeFalse();
    }
}

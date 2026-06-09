// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class SizeFTests
{
    [Test]
    public async Task AreaShouldCalculateTheArea()
    {
        SizeF size = new(1920f, 1080f);

        float area = size.Area;
        await area.Should().BeEqualTo(2073600f);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenSizefIsEmpty()
    {
        SizeF size = new();
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenSizeIsNotEmpty()
    {
        SizeF size = new(1920f, 1080f);
        await size.IsEmpty.Should().BeFalse();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        SizeF size = new(0f, 1080f);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        SizeF size = new(1920f, 0f);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task ZeroShouldReturnAnEmptySize()
    {
        SizeF zero = SizeF.Zero;

        await zero.Width.Should().BeZero();
        await zero.Height.Should().BeZero();
    }

    [Test]
    public async Task ShouldDeconstructSizeIntoItsComponents()
    {
        (float x, float y) = new SizeF(1920f, 1080f);

        await x.Should().BeEqualTo(1920f);
        await y.Should().BeEqualTo(1080f);
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        SizeF size = new(1920f, 1080f);

        string result = size.ToString();
        await result.Should().BeEqualTo("(1920, 1080)");
    }

    [Test]
    public async Task FloorShouldConvertSizeByFlooringEachComponent()
    {
        SizeF size = new(7.64f, -7.6f);

        Size result = size.Floor();

        await result.Width.Should().BeEqualTo(7);
        await result.Height.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task RoundShouldConvertSizeByRoundingEachComponent()
    {
        SizeF size = new(7.64f, -7.6f);

        Size result = size.Round();

        await result.Width.Should().BeEqualTo(8);
        await result.Height.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task TruncateShouldConvertVector2ByTruncatingEachComponent()
    {
        SizeF size = new(7.64f, -7.6f);

        Size result = size.Truncate();

        await result.Width.Should().BeEqualTo(7);
        await result.Height.Should().BeEqualTo(-7);
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenSizesAreEquals()
    {
        SizeF left = new(1920f, 1080f);
        SizeF right = new(1920f, 1080f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        SizeF left = new(1920f, 1080f);
        SizeF right = new(3440f, 1440f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenSizesAreEquals()
    {
        SizeF left = new(1920f, 1080f);
        object right = new SizeF(1920f, 1080f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenSizesAreNotEquals()
    {
        SizeF left = new(1920f, 1080f);
        object right = new SizeF(3440f, 1440f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptSizeShouldReturnFalse()
    {
        SizeF left = new(1920f, 1080f);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        SizeF left = new(1920f, 1080f);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenSizesAreEquals()
    {
        SizeF left = new(1920f, 1080f);
        SizeF right = new(1920f, 1080f);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        SizeF left = new(1920f, 1080f);
        SizeF right = new(3440f, 1440f);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenSizesAreNotEquals()
    {
        SizeF left = new(1920f, 1080f);
        SizeF right = new(3440f, 1440f);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenSizesAreEquals()
    {
        SizeF left = new(1920f, 1080f);
        SizeF right = new(1920f, 1080f);

        bool result = left != right;
        await result.Should().BeFalse();
    }
}

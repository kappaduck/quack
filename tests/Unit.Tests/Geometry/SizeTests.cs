// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class SizeTests
{
    [Test]
    public async Task AreaShouldCalculateTheArea()
    {
        Size size = new(1920, 1080);

        int area = size.Area;
        await area.Should().BeEqualTo(2073600);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenSizeIsEmpty()
    {
        Size size = new();
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenSizeIsNotEmpty()
    {
        Size size = new(1920, 1080);
        await size.IsEmpty.Should().BeFalse();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        Size size = new(0, 1080);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        Size size = new(1920, 0);
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
        (int x, int y) = new Size(1920, 1080);

        await x.Should().BeEqualTo(1920);
        await y.Should().BeEqualTo(1080);
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        Size size = new(1920, 1080);

        string result = size.ToString();
        await result.Should().BeEqualTo("(1920, 1080)");
    }

    [Test]
    public async Task ToSizefShouldConvertToSizef()
    {
        Size size = new(1920, 1080);

        SizeF result = size.ToSizef();

        await result.Width.Should().BeEqualTo(size.Width);
        await result.Height.Should().BeEqualTo(size.Height);
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenSizesAreEquals()
    {
        Size left = new(1920, 1080);
        Size right = new(1920, 1080);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        Size left = new(1920, 1080);
        Size right = new(3440, 1440);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenSizesAreEquals()
    {
        Size left = new(1920, 1080);
        object right = new Size(1920, 1080);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenSizesAreNotEquals()
    {
        Size left = new(1920, 1080);
        object right = new Size(3440, 1440);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptSizeShouldReturnFalse()
    {
        Size left = new(1920, 1080);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Size left = new(1920, 1080);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenSizesAreEquals()
    {
        Size left = new(1920, 1080);
        Size right = new(1920, 1080);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        Size left = new(1920, 1080);
        Size right = new(3440, 1440);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenSizesAreNotEquals()
    {
        Size left = new(1920, 1080);
        Size right = new(3440, 1440);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenSizesAreEquals()
    {
        Size left = new(1920, 1080);
        Size right = new(1920, 1080);

        bool result = left != right;
        await result.Should().BeFalse();
    }
}

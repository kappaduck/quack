// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class SizeITests
{
    [Test]
    public async Task AreaShouldCalculateTheArea()
    {
        SizeI size = new(1920, 1080);

        int area = size.Area;
        await area.Should().BeEqualTo(2073600);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenSizeIsEmpty()
    {
        SizeI size = new();
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenSizeIsNotEmpty()
    {
        SizeI size = new(1920, 1080);
        await size.IsEmpty.Should().BeFalse();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        SizeI size = new(0, 1080);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        SizeI size = new(1920, 0);
        await size.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task ZeroShouldReturnAnEmptySize()
    {
        SizeI zero = SizeI.Zero;

        await zero.Width.Should().BeZero();
        await zero.Height.Should().BeZero();
    }

    [Test]
    public async Task ShouldDeconstructSizeIntoItsComponents()
    {
        (int x, int y) = new SizeI(1920, 1080);

        await x.Should().BeEqualTo(1920);
        await y.Should().BeEqualTo(1080);
    }

    [Test]
    public async Task ToStringShouldReturnTheCorrectFormat()
    {
        SizeI size = new(1920, 1080);

        string result = size.ToString();
        await result.Should().BeEqualTo("(1920, 1080)");
    }

    [Test]
    public async Task ToSizeFShouldConvertToSizeF()
    {
        SizeI size = new(1920, 1080);

        SizeF result = size.ToSizeF();

        await result.Width.Should().BeEqualTo(size.Width);
        await result.Height.Should().BeEqualTo(size.Height);
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenSizesAreEquals()
    {
        SizeI left = new(1920, 1080);
        SizeI right = new(1920, 1080);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        SizeI left = new(1920, 1080);
        SizeI right = new(3440, 1440);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenSizesAreEquals()
    {
        SizeI left = new(1920, 1080);
        object right = new SizeI(1920, 1080);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnFalseWhenSizesAreNotEquals()
    {
        SizeI left = new(1920, 1080);
        object right = new SizeI(3440, 1440);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptSizeShouldReturnFalse()
    {
        SizeI left = new(1920, 1080);
        const float right = 3f;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        SizeI left = new(1920, 1080);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenSizesAreEquals()
    {
        SizeI left = new(1920, 1080);
        SizeI right = new(1920, 1080);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenSizesAreNotEquals()
    {
        SizeI left = new(1920, 1080);
        SizeI right = new(3440, 1440);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenSizesAreNotEquals()
    {
        SizeI left = new(1920, 1080);
        SizeI right = new(3440, 1440);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenSizesAreEquals()
    {
        SizeI left = new(1920, 1080);
        SizeI right = new(1920, 1080);

        bool result = left != right;
        await result.Should().BeFalse();
    }
}

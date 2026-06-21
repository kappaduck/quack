// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Pixels;

namespace Unit.Tests.Video.Pixels;

internal sealed class ColorFTests
{
    [Test]
    public async Task ConstructorWithThreeChannelsShouldSetAlphaToOpaque()
    {
        ColorF color = new(0.2f, 0.4f, 0.6f);

        await color.R.Should().BeEqualTo(0.2f);
        await color.G.Should().BeEqualTo(0.4f);
        await color.B.Should().BeEqualTo(0.6f);
        await color.A.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task TransparentShouldBeAllZero()
    {
        ColorF transparent = ColorF.Transparent;

        await transparent.R.Should().BeZero();
        await transparent.G.Should().BeZero();
        await transparent.B.Should().BeZero();
        await transparent.A.Should().BeZero();
    }

    [Test]
    public async Task BlackShouldReturnTheColorBlack()
    {
        ColorF black = ColorF.Black;

        await black.R.Should().BeZero();
        await black.G.Should().BeZero();
        await black.B.Should().BeZero();
        await black.A.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task WhiteShouldReturnTheColorWhite()
    {
        ColorF white = ColorF.White;

        await white.R.Should().BeEqualTo(1f);
        await white.G.Should().BeEqualTo(1f);
        await white.B.Should().BeEqualTo(1f);
        await white.A.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task LerpShouldInterpolatesBetweenTwoColors()
    {
        ColorF from = new(0f, 0f, 0f, 0f);
        ColorF to = new(1f, 0.5f, 0.25f, 1f);

        ColorF color = ColorF.Lerp(from, to, 0.5f);

        await color.R.Should().BeCloseTo(0.5f, 0.0001f);
        await color.G.Should().BeCloseTo(0.25f, 0.0001f);
        await color.B.Should().BeCloseTo(0.125f, 0.0001f);
        await color.A.Should().BeCloseTo(0.5f, 0.0001f);
    }

    [Test]
    public async Task LerpShouldClampFactorToZeroWhenAmountIsLessThanZero()
    {
        ColorF from = new(0.1f, 0.2f, 0.3f, 0.4f);
        ColorF to = new(0.9f, 0.8f, 0.7f, 0.6f);

        ColorF color = ColorF.Lerp(from, to, -0.5f);
        await color.Should().BeEqualTo(from);
    }

    [Test]
    public async Task LerpShouldClampFactorToOneWhenAMountIsGreaterThanOne()
    {
        ColorF from = new(0.1f, 0.2f, 0.3f, 0.4f);
        ColorF to = new(0.9f, 0.8f, 0.7f, 0.6f);

        ColorF color = ColorF.Lerp(from, to, 1.5f);
        await color.Should().BeEqualTo(to);
    }

    [Test]
    public async Task DeconstructShouldAssignChannelsToOutParameters()
    {
        (float r, float g, float b, float a) = new ColorF(0.2f, 0.4f, 0.6f, 0.8f);

        await r.Should().BeEqualTo(0.2f);
        await g.Should().BeEqualTo(0.4f);
        await b.Should().BeEqualTo(0.6f);
        await a.Should().BeEqualTo(0.8f);
    }

    [Test]
    public async Task ToColorShouldScaleChannelsToBytes()
    {
        ColorF color = new(1f, 0f, 0.6f, 1f);

        Color result = color.ToColor();
        await result.Should().BeEqualTo(new Color(255, 0, 153, 255));
    }

    [Test]
    public async Task ToColorShouldClampChannelsOutsideUnitRange()
    {
        ColorF color = new(2f, -1f, 0.6f, 5f);

        Color result = color.ToColor();
        await result.Should().BeEqualTo(new Color(255, 0, 153, 255));
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenColorsAreEqual()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);
        ColorF right = new(0.2f, 0.4f, 0.6f, 0.8f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenColorsAreNotEqual()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);
        ColorF right = new(0.2f, 0.4f, 0.6f, 1f);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenColorsAreEqual()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);
        object right = new ColorF(0.2f, 0.4f, 0.6f, 0.8f);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptColorShouldReturnFalse()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);
        const int right = 3;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenColorsAreEqual()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);
        ColorF right = new(0.2f, 0.4f, 0.6f, 0.8f);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenColorsAreNotEqual()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);
        ColorF right = new(0.2f, 0.4f, 0.6f, 1f);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenColorsAreNotEqual()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);
        ColorF right = new(0.2f, 0.4f, 0.6f, 1f);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenColorsAreEqual()
    {
        ColorF left = new(0.2f, 0.4f, 0.6f, 0.8f);
        ColorF right = new(0.2f, 0.4f, 0.6f, 0.8f);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheTupleFormat()
    {
        ColorF color = new(1f, 0f, 0f, 1f);

        string value = color.ToString();
        await value.Should().BeEqualTo("(1, 0, 0, 1)");
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Pixels;

namespace Unit.Tests.Video.Pixels;

internal sealed class ColorTests
{
    [Test]
    public async Task ConstructorWithThreeChannelsShouldSetAlphaToOpaque()
    {
        Color color = new(10, 20, 30);

        await color.R.Should().BeEqualTo((byte)10);
        await color.G.Should().BeEqualTo((byte)20);
        await color.B.Should().BeEqualTo((byte)30);
        await color.A.Should().BeEqualTo((byte)255);
    }

    [Test]
    public async Task TransparentShouldBeAllZero()
    {
        Color transparent = Color.Transparent;

        await transparent.R.Should().BeZero();
        await transparent.G.Should().BeZero();
        await transparent.B.Should().BeZero();
        await transparent.A.Should().BeZero();
    }

    [Test]
    public async Task BlackShouldReturnTheColorBlack()
    {
        Color black = Color.Black;

        await black.R.Should().BeZero();
        await black.G.Should().BeZero();
        await black.B.Should().BeZero();
        await black.A.Should().BeEqualTo((byte)255);
    }

    [Test]
    public async Task WhiteShouldReturnTheColorWhite()
    {
        Color white = Color.White;

        await white.R.Should().BeEqualTo((byte)255);
        await white.G.Should().BeEqualTo((byte)255);
        await white.B.Should().BeEqualTo((byte)255);
        await white.A.Should().BeEqualTo((byte)255);
    }

    [Test]
    public async Task FromHexUsingPackedValueShouldUnpackChannels()
    {
        Color color = Color.FromHex(0xAABBCCDD);

        await color.R.Should().BeEqualTo((byte)170);
        await color.G.Should().BeEqualTo((byte)187);
        await color.B.Should().BeEqualTo((byte)204);
        await color.A.Should().BeEqualTo((byte)221);
    }

    [Test]
    [Arguments("#AABBCCDD")]
    [Arguments("#ABCD")]
    [Arguments("AABBCCDD")]
    [Arguments("ABCD")]
    public async Task FromHexUsingRGBAShouldConvertToColor(string hex)
    {
        Color color = Color.FromHex(hex);

        await color.R.Should().BeEqualTo((byte)170);
        await color.G.Should().BeEqualTo((byte)187);
        await color.B.Should().BeEqualTo((byte)204);
        await color.A.Should().BeEqualTo((byte)221);
    }

    [Test]
    [Arguments("#AABBCC")]
    [Arguments("#ABC")]
    [Arguments("AABBCC")]
    [Arguments("ABC")]
    public async Task FromHexUsingRGBShouldConvertToColor(string hex)
    {
        Color color = Color.FromHex(hex);

        await color.R.Should().BeEqualTo((byte)170);
        await color.G.Should().BeEqualTo((byte)187);
        await color.B.Should().BeEqualTo((byte)204);
    }

    [Test]
    [Arguments("")]
    [Arguments("$AABBCC")]
    [Arguments("#QQABBCC")]
    [Arguments("$QQBBCC")]
    [Arguments("$ABC")]
    [Arguments("#QBC")]
    [Arguments("$QBC")]
    [Arguments("color")]
    public async Task FromHexShouldThrowFormatExceptionWhenFormatIsNotValid(string hex)
    {
        await Assert.That(() => _ = Color.FromHex(hex))
                    .ThrowsExactly<FormatException>();
    }

    [Test]
    [Arguments("#AABBCCDD")]
    [Arguments(" #AABBCCDD ")]
    [Arguments("#ABCD")]
    [Arguments("AABBCCDD")]
    [Arguments("ABCD")]
    public async Task TryFromHexUsingRGBAShouldConvertToColor(string hex)
    {
        bool result = Color.TryFromHex(hex, out Color color);

        await result.Should().BeTrue();
        await color.R.Should().BeEqualTo((byte)170);
        await color.G.Should().BeEqualTo((byte)187);
        await color.B.Should().BeEqualTo((byte)204);
        await color.A.Should().BeEqualTo((byte)221);
    }

    [Test]
    [Arguments("#AABBCC")]
    [Arguments(" #AABBCC ")]
    [Arguments("#ABC")]
    [Arguments("AABBCC")]
    [Arguments("ABC")]
    public async Task TryFromHexUsingRGBShouldConvertToColor(string hex)
    {
        bool result = Color.TryFromHex(hex, out Color color);

        await result.Should().BeTrue();
        await color.R.Should().BeEqualTo((byte)170);
        await color.G.Should().BeEqualTo((byte)187);
        await color.B.Should().BeEqualTo((byte)204);
    }

    [Test]
    [Arguments("")]
    [Arguments("$AABBCC")]
    [Arguments("#QQABBCC")]
    [Arguments("$QQBBCC")]
    [Arguments("$ABC")]
    [Arguments("#QBC")]
    [Arguments("$QBC")]
    [Arguments("color")]
    public async Task TryFromHexShouldReturnFalseAndNoColorWhenFormatIsNotValid(string hex)
    {
        bool result = Color.TryFromHex(hex, out Color color);

        await result.Should().BeFalse();
        await color.R.Should().BeZero();
        await color.G.Should().BeZero();
        await color.B.Should().BeZero();
        await color.A.Should().BeZero();
    }

    [Test]
    public async Task LerpShouldInterpolatesBetweenTwoColors()
    {
        Color from = new(0, 0, 0, 0);
        Color to = new(100, 200, 50, 250);

        Color color = Color.Lerp(from, to, 0.5f);

        await color.Should().BeEqualTo(new Color(50, 100, 25, 125));
    }

    [Test]
    public async Task LerpShouldClampFactorToZeroWhenAmountIsLessThanZero()
    {
        Color color = Color.Lerp(Colors.Green, Colors.Red, -0.5f);

        await color.Should().BeEqualTo(Colors.Green);
    }

    [Test]
    public async Task LerpShouldClampFactorToOneWhenAMountIsGreaterThanOne()
    {
        Color color = Color.Lerp(Colors.Green, Colors.Red, 1.5f);

        await color.Should().BeEqualTo(Colors.Red);
    }

    [Test]
    public async Task DeconstructShouldAssignChannelsToOutParameters()
    {
        (byte r, byte g, byte b, byte a) = new Color(10, 20, 30, 40);

        await r.Should().BeEqualTo((byte)10);
        await g.Should().BeEqualTo((byte)20);
        await b.Should().BeEqualTo((byte)30);
        await a.Should().BeEqualTo((byte)40);
    }

    [Test]
    public async Task ToHexShouldPackChannels()
    {
        Color color = new(0x12, 0x34, 0x56, 0x78);

        uint hex = color.ToHex();
        await hex.Should().BeEqualTo(0x12345678u);
    }

    [Test]
    public async Task ToColorFShouldNormalizeChannels()
    {
        Color color = new(255, 0, 51, 255);

        ColorF result = color.ToColorF();

        await result.R.Should().BeCloseTo(1f, 0.0001f);
        await result.G.Should().BeCloseTo(0f, 0.0001f);
        await result.B.Should().BeCloseTo(0.2f, 0.0001f);
        await result.A.Should().BeCloseTo(1f, 0.0001f);
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenColorsAreEqual()
    {
        Color left = new(10, 20, 30, 40);
        Color right = new(10, 20, 30, 40);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenColorsAreNotEqual()
    {
        Color left = new(10, 20, 30, 40);
        Color right = new(10, 20, 30, 255);

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenColorsAreEqual()
    {
        Color left = new(10, 20, 30, 40);
        object right = new Color(10, 20, 30, 40);

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptColorShouldReturnFalse()
    {
        Color left = new(10, 20, 30, 40);
        const int right = 3;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        Color left = new(10, 20, 30, 40);

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenColorsAreEqual()
    {
        Color left = new(10, 20, 30, 40);
        Color right = new(10, 20, 30, 40);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenColorsAreNotEqual()
    {
        Color left = new(10, 20, 30, 40);
        Color right = new(10, 20, 30, 255);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenColorsAreNotEqual()
    {
        Color left = new(10, 20, 30, 40);
        Color right = new(10, 20, 30, 255);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenColorsAreEqual()
    {
        Color left = new(10, 20, 30, 40);
        Color right = new(10, 20, 30, 40);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnTheTupleFormat()
    {
        Color color = new(255, 0, 51, 255);

        string value = color.ToString();
        await value.Should().BeEqualTo("(255, 0, 51, 255)");
    }

    [Test]
    public async Task ToStringWithUppercaseHexFormatShouldReturnUppercaseHex()
    {
        Color color = new(0xAB, 0xCD, 0xEF, 0x09);

        string value = color.ToString("X", null);
        await value.Should().BeEqualTo("#ABCDEF09");
    }

    [Test]
    public async Task ToStringWithLowercaseHexFormatShouldReturnLowercaseHex()
    {
        Color color = new(0xAB, 0xCD, 0xEF, 0x09);

        string value = color.ToString("x", null);
        await value.Should().BeEqualTo("#abcdef09");
    }

    [Test]
    public async Task ToStringWithUnknownFormatShouldReturnTheTupleFormat()
    {
        Color color = new(255, 0, 51, 255);

        string value = color.ToString("G", null);
        await value.Should().BeEqualTo("(255, 0, 51, 255)");
    }

    [Test]
    public async Task TryFormatWithUppercaseHexFormatShouldReturnUppercaseHex()
    {
        Color color = new(0xAB, 0xCD, 0xEF, 0x09);

        string value = $"{color:X}";
        await value.Should().BeEqualTo("#ABCDEF09");
    }

    [Test]
    public async Task TryFormatWithLowercaseHexFormatShouldReturnLowercaseHex()
    {
        Color color = new(0xAB, 0xCD, 0xEF, 0x09);

        string value = $"{color:x}";
        await value.Should().BeEqualTo("#abcdef09");
    }

    [Test]
    public async Task TryFormatWithUnknownFormatShouldReturnTheTupleFormat()
    {
        Color color = new(255, 0, 51, 255);

        string value = $"{color:G}";
        await value.Should().BeEqualTo("(255, 0, 51, 255)");
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Graphics.Drawing;

namespace Unit.Tests.Graphics.Drawing;

internal sealed class BlendModeTests
{
    [Test]
    public async Task DefaultShouldBeNone()
    {
        BlendMode mode = default;
        await mode.Should().BeEqualTo(BlendMode.None);
    }

    [Test]
    public async Task ToStringShouldReturnThePresetName()
    {
        await BlendMode.None.ToString().Should().BeEqualTo("None");
        await BlendMode.Blend.ToString().Should().BeEqualTo("Blend");
        await BlendMode.BlendPremultiplied.ToString().Should().BeEqualTo("BlendPremultiplied");
        await BlendMode.Add.ToString().Should().BeEqualTo("Add");
        await BlendMode.AddPremultiplied.ToString().Should().BeEqualTo("AddPremultiplied");
        await BlendMode.Mod.ToString().Should().BeEqualTo("Mod");
        await BlendMode.Mul.ToString().Should().BeEqualTo("Mul");
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenModesAreEqual()
    {
        BlendMode left = BlendMode.Add;
        BlendMode right = BlendMode.Add;

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseWhenModesAreNotEqual()
    {
        BlendMode left = BlendMode.Add;
        BlendMode right = BlendMode.Mod;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithObjectShouldReturnTrueWhenModesAreEqual()
    {
        BlendMode left = BlendMode.Add;
        object right = BlendMode.Add;

        bool result = left.Equals(right);
        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsWithAnyTypeExceptBlendModeShouldReturnFalse()
    {
        BlendMode left = BlendMode.Add;
        const int right = 3;

        bool result = left.Equals(right);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task EqualsWithNullShouldReturnFalse()
    {
        BlendMode left = BlendMode.Add;

        bool result = left.Equals(null);
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnTrueWhenModesAreEqual()
    {
        BlendMode left = BlendMode.Add;
        BlendMode right = BlendMode.Add;

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualsShouldReturnFalseWhenModesAreNotEqual()
    {
        BlendMode left = BlendMode.Add;
        BlendMode right = BlendMode.Mod;

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnTrueWhenModesAreNotEqual()
    {
        BlendMode left = BlendMode.Add;
        BlendMode right = BlendMode.Mod;

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorNotEqualsShouldReturnFalseWhenModesAreEqual()
    {
        BlendMode left = BlendMode.Add;
        BlendMode right = BlendMode.Add;

        bool result = left != right;
        await result.Should().BeFalse();
    }
}

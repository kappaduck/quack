// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Windows;

namespace Unit.Tests.Windows;

internal sealed class AspectRatioTests
{
    [Test]
    [Arguments(0f, 0f)]
    [Arguments(1.5f, 1.5f)]
    [Arguments(1f, 2f)]
    [Arguments(0f, 2f)]
    [Arguments(2f, 0f)]
    public async Task ConstructorWithValidRangeStoresValues(float minimum, float maximum)
    {
        AspectRatio ratio = new(minimum, maximum);

        await ratio.Minimum.Should().BeEqualTo(minimum);
        await ratio.Maximum.Should().BeEqualTo(maximum);
    }

    [Test]
    public async Task SingleRatioConstructorAssignsBothBounds()
    {
        AspectRatio ratio = new(1.6f);

        await ratio.Minimum.Should().BeEqualTo(1.6f);
        await ratio.Maximum.Should().BeEqualTo(1.6f);
    }

    [Test]
    [Arguments(-1f, 2f)]
    [Arguments(1f, -2f)]
    [Arguments(-0.5f, -0.5f)]
    [Arguments(float.NaN, 2f)]
    [Arguments(1f, float.NaN)]
    [Arguments(float.PositiveInfinity, 2f)]
    [Arguments(1f, float.PositiveInfinity)]
    public async Task ConstructorWithNegativeOrNonFiniteThrows(float minimum, float maximum)
    {
        await Assert.That(() => new AspectRatio(minimum, maximum))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    [Arguments(3f, 2f)]
    [Arguments(2.5f, 1f)]
    public async Task ConstructorWithMinimumGreaterThanMaximumThrows(float minimum, float maximum)
    {
        await Assert.That(() => new AspectRatio(minimum, maximum))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    [Arguments(-3f)]
    [Arguments(float.NaN)]
    public async Task SingleRatioConstructorWithInvalidRatioThrows(float ratio)
    {
        await Assert.That(() => new AspectRatio(ratio))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task DeconstructReturnsBothBounds()
    {
        (float minimum, float maximum) = new AspectRatio(1f, 2f);

        await Assert.That(minimum).IsEqualTo(1f);
        await Assert.That(maximum).IsEqualTo(2f);
    }
}

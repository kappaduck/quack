// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video;

namespace Integration.Tests.Video;

internal sealed class PaletteTests
{
    [Test]
    public async Task ConstructorWithCountShouldCreatePaletteOfOpaqueWhite()
    {
        using Palette palette = new(4);

        Color[] colors = [.. palette.Colors];

        await palette.Count.Should().BeEqualTo(4);
        await colors.Should().All(c => c == Color.White);
    }

    [Test]
    public async Task ConstructorWithColorsShouldFillThePalette()
    {
        using Palette palette = new([Colors.Red, Colors.Teal]);

        await palette.Count.Should().BeEqualTo(2);
        await palette[0].Should().BeEqualTo(Colors.Red);
        await palette[1].Should().BeEqualTo(Colors.Teal);
    }

    [Test]
    [Arguments(0)]
    [Arguments(-1)]
    public async Task ConstructorWithNonPositiveCountShouldThrow(int count)
    {
        await Assert.That(() => new Palette(count))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ConstructWithEmptyColorsShouldThrow()
    {
        await Assert.That(() => new Palette([]))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task CountShouldReturnTheNumberOfEntries()
    {
        using Palette palette = new(16);

        await palette.Count.Should().BeEqualTo(16);
    }

    [Test]
    public async Task ColorsShouldReturnEveryEntry()
    {
        Color first = new(10, 20, 30, 40);
        Color second = new(50, 60, 70, 80);
        Color third = new(90, 100, 110, 120);

        using Palette palette = new([first, second, third]);

        Color[] colors = palette.Colors.ToArray();

        await colors.Length.Should().BeEqualTo(3);
        await colors[0].Should().BeEqualTo(first);
        await colors[1].Should().BeEqualTo(second);
        await colors[2].Should().BeEqualTo(third);
    }

    [Test]
    public async Task SetColorsShouldOverwriteFromTheGivenIndex()
    {
        using Palette palette = new(4);
        palette.SetColors([Colors.Red, Colors.Green], 1);

        await palette[0].Should().BeEqualTo(Color.White);
        await palette[1].Should().BeEqualTo(Colors.Red);
        await palette[2].Should().BeEqualTo(Colors.Green);
        await palette[3].Should().BeEqualTo(Color.White);
    }

    [Test]
    public async Task SetColorsShouldDefaultToTheFirstEntry()
    {
        Color first = new(1, 2, 3, 4);
        Color second = new(5, 6, 7, 8);

        using Palette palette = new(2);
        palette.SetColors([first, second]);

        await palette[0].Should().BeEqualTo(first);
        await palette[1].Should().BeEqualTo(second);
    }

    [Test]
    public async Task SetColorsWithNegativeFirstColorShouldThrow()
    {
        using Palette palette = new(2);

        await Assert.That(() => palette.SetColors([Color.Black], -1))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task SetColorsPastTheEndShouldThrow()
    {
        using Palette palette = new(2);

        await Assert.That(() => palette.SetColors([Color.Black, Color.Black], 1))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task IndexerSetterShouldUpdateASingleEntry()
    {
        Color red = new(255, 0, 0, 255);

        using Palette palette = new(2);
        palette[1] = red;

        await palette[0].Should().BeEqualTo(Color.White);
        await palette[1].Should().BeEqualTo(red);
    }

    [Test]
    [Arguments(-1)]
    [Arguments(2)]
    public async Task IndexerGetterOutOfRangeShouldThrow(int index)
    {
        using Palette palette = new(2);

        await Assert.That(() => palette[index])
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    [Arguments(-1)]
    [Arguments(2)]
    public async Task IndexerSetterOutOfRangeShouldThrow(int index)
    {
        using Palette palette = new(2);

        await Assert.That(() => palette[index] = Color.Black)
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task DisposeShouldBeIdempotent()
    {
        Palette palette = new(2);
        palette.Dispose();

        await Assert.That(palette.Dispose).ThrowsNothing();
    }

    [Test]
    public async Task CountAfterDisposeShouldThrow()
    {
        Palette palette = new(2);
        palette.Dispose();

        await Assert.That(() => palette.Count)
                    .ThrowsExactly<ObjectDisposedException>();
    }

    [Test]
    public async Task ColorsAfterDisposeShouldThrow()
    {
        Palette palette = new(2);
        palette.Dispose();

        await Assert.That(() => palette.Colors.ToArray())
                    .ThrowsExactly<ObjectDisposedException>();
    }

    [Test]
    public async Task IndexerAfterDisposeShouldThrow()
    {
        Palette palette = new(2);
        palette.Dispose();

        await Assert.That(() => palette[0])
                    .ThrowsExactly<ObjectDisposedException>();
    }

    [Test]
    public async Task SetColorsAfterDisposeShouldThrow()
    {
        Palette palette = new(2);
        palette.Dispose();

        await Assert.That(() => palette.SetColors([Color.Black]))
                    .ThrowsExactly<ObjectDisposedException>();
    }
}

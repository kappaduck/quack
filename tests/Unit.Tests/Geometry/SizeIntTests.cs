// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

public sealed class SizeIntTests
{
    [Test]
    public async Task DeconstructShouldDeconstructIntoWidthAndHeight()
    {
        (int width, int height) = new SizeInt(1920, 1080);

        await Assert.That(width).IsEqualTo(1920);
        await Assert.That(height).IsEqualTo(1080);
    }

    [Test]
    public async Task ToStringShouldReturnFormattedString()
    {
        SizeInt size = new(800, 600);
        await Assert.That(size.ToString()).IsEqualTo("(800, 600)");
    }
}

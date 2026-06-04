// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class Vector2ExtensionsTests
{
    [Test]
    public async Task FloorShouldConvertVector2ByFlooringEachComponentToVector2i()
    {
        Vector2 vector = new(7.64f, -7.6f);

        Vector2i result = vector.Floor();
        await result.X.Should().BeEqualTo(7);
        await result.Y.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task RoundShouldConvertVector2ByRoundingEachComponentToVector2i()
    {
        Vector2 vector = new(7.64f, -7.6f);

        Vector2i result = vector.Round();
        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task TruncateShouldConvertVector2ByTruncatingEachComponentToVector2i()
    {
        Vector2 vector = new(7.64f, -7.6f);

        Vector2i result = vector.Truncate();
        await result.X.Should().BeEqualTo(7);
        await result.Y.Should().BeEqualTo(-7);
    }
}

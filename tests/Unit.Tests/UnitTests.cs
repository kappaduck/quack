// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace Unit.Tests;

internal sealed class UnitTests
{
    [Test]
    public async Task ValueShouldBeTrue()
    {
        const bool success = true;
        await success.Should().BeTrue();
    }
}

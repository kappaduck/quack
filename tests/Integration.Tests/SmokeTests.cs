// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace Integration.Tests;

internal sealed class SmokeTests
{
    [Test]
    public async Task TrueShouldBeTrue()
    {
        await true.Should().BeTrue();
    }
}

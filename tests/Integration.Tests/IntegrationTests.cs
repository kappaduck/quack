// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace Integration.Tests;

internal sealed class IntegrationTests
{
    [Test]
    public async Task ValueShouldBeTrue()
    {
        const bool success = true;
        await success.Should().BeTrue();
    }
}

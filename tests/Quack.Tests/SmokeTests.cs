// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace Quack.Tests;

public sealed class SmokeTests
{
    [Test]
    public async Task SmokeTest()
    {
        const int a = 21;
        const int b = 21;

        int result = Add(a, b);

        await Assert.That(result).IsEqualTo(42);
    }

    private int Add(int a, int b) => a + b;
}

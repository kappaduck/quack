// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace Unit.Tests.Interop.Handles;

public sealed class NonOwningSafeHandleZeroInvalidTests
{
    [Test]
    public async Task IsInvalidShouldReturnTrueWhenHandleIsZero()
    {
        using NonOwningSafeHandleZeroInvalid handle = new TestHandle(nint.Zero);
        await Assert.That(handle.IsInvalid).IsTrue();
    }

    [Test]
    [Arguments(-42)]
    [Arguments(42)]
    public async Task IsInvalidShouldReturnFalseWhenHandleIsNotZero(nint value)
    {
        using NonOwningSafeHandleZeroInvalid handle = new TestHandle(value);
        await Assert.That(handle.IsInvalid).IsFalse();
    }
}

file sealed class TestHandle(nint handle) : NonOwningSafeHandleZeroInvalid(handle);

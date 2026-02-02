// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace Unit.Tests.Interop.Handles;

public sealed class WindowHandleTests
{
    [Test]
    public async Task WindowHandleShouldBeANonOwningSafeHandleZeroInvalid()
    {
        using WindowHandle handle = new(nint.Zero);
        await Assert.That(handle).IsTypeOf<NonOwningSafeHandleZeroInvalid>();
    }

    [Test]
    public async Task IsInvalidShouldReturnTrueWhenHandleIsZero()
    {
        using WindowHandle handle = new(nint.Zero);
        await Assert.That(handle.IsInvalid).IsTrue();
    }

    [Test]
    [Arguments(-42)]
    [Arguments(42)]
    public async Task IsInvalidShouldReturnFalseWhenHandleIsNotZero(nint value)
    {
        using WindowHandle handle = new(value);
        await Assert.That(handle.IsInvalid).IsFalse();
    }

    [Test]
    public async Task ZeroPropertyShouldReturnWindowHandleWithZeroAsHandle()
    {
        WindowHandle handle = WindowHandle.Zero;
        await Assert.That(handle.IsInvalid).IsTrue();
    }
}

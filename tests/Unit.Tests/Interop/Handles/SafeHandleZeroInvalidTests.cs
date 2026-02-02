// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace Unit.Tests.Interop.Handles;

public sealed class SafeHandleZeroInvalidTests
{
    [Test]
    public async Task IsInvalidShouldReturnTrueWhenHandleIsZero()
    {
        using SafeHandleZeroInvalid handle = new TestHandle(nint.Zero, true);
        await Assert.That(handle.IsInvalid).IsTrue();
    }

    [Test]
    [Arguments(-42)]
    [Arguments(42)]
    public async Task IsInvalidShouldReturnFalseWhenHandleIsNotZero(nint value)
    {
        using SafeHandleZeroInvalid handle = new TestHandle(value, true);
        await Assert.That(handle.IsInvalid).IsFalse();
    }
}

file sealed class TestHandle(nint handle, bool ownsHandle) : SafeHandleZeroInvalid(handle, ownsHandle)
{
    protected override bool ReleaseHandle() => true;
}

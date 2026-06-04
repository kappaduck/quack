// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;

namespace Unit.Tests.Exceptions;

internal sealed class ThrowHelperTests
{
    [Test]
    public async Task ThrowShouldThrowQuackException()
    {
        await Assert.That(() => ThrowHelper.Throw("message", "test"))
                    .ThrowsExactly<QuackException>()
                    .WithMessage("message (test)");
    }

    [Test]
    public async Task ThrowIfShouldNotThrowWhenConditionIsFalse()
    {
        await Assert.That(() => ThrowHelper.ThrowIf(false, "message"))
                    .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfShouldThrowWhenConditionIsTrue()
    {
        await Assert.That(() => ThrowHelper.ThrowIf(true, "message", "test"))
                    .ThrowsExactly<QuackException>()
                    .WithMessage("message (test)");
    }

    [Test]
    public async Task ThrowInteropShouldThrowQuackInteropException()
    {
        await Assert.That(() => ThrowHelper.ThrowInterop("module", "message", "test"))
                    .ThrowsExactly<QuackException>()
                    .WithMessage("[module] message (test)");
    }
}

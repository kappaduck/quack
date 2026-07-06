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
                    .ThrowsExactly<QuackInteropException>()
                    .WithMessage("[module] message (test)");
    }

    [Test]
    public async Task ThrowInvalidOperationShouldThrowInvalidOperationException()
    {
        await Assert.That(() => ThrowHelper.ThrowInvalidOperation("message"))
                    .ThrowsExactly<InvalidOperationException>()
                    .WithMessage("message");
    }

    [Test]
    public async Task ThrowOperationCanceledShouldThrowOperationCanceledException()
    {
        await Assert.That(() => ThrowHelper.ThrowOperationCanceled("message"))
                    .ThrowsExactly<OperationCanceledException>()
                    .WithMessage("message");
    }

    [Test]
    public async Task ThrowFormatShouldThrowFormatException()
    {
        await Assert.That(() => ThrowHelper.ThrowFormat("message"))
                    .ThrowsExactly<FormatException>()
                    .WithMessage("message");
    }

    [Test]
    public async Task ThrowFileNotFoundShouldThrowFormatException()
    {
        await Assert.That(() => ThrowHelper.ThrowFileNotFound("message", "path"))
                    .ThrowsExactly<FileNotFoundException>()
                    .WithMessage("message")
                    .And.HasProperty(c => c.FileName, "path");
    }
}

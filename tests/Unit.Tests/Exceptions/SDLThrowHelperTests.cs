// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;

namespace Unit.Tests.Exceptions;

internal sealed class SDLThrowHelperTests
{
    [Test]
    public async Task ThrowIfShouldNotThrowWhenConditionIsFalse()
    {
        await Assert.That(() => SDLThrowHelper.ThrowIf(false))
                    .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfShouldThrowWhenConditionIsTrue()
    {
        await Assert.That(() => SDLThrowHelper.ThrowIf(true, "test"))
                    .ThrowsExactly<QuackInteropException>()
                    .WithMessageMatching("[SDL3] * (test)");
    }

    [Test]
    public async Task ThrowIfFailedShouldNotThrowWhenConditionIsTrue()
    {
        await Assert.That(() => SDLThrowHelper.ThrowIfFailed(true))
                    .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfFailedShouldThrowWhenConditionIsFalse()
    {
        await Assert.That(() => SDLThrowHelper.ThrowIfFailed(false, "test"))
                    .ThrowsExactly<QuackInteropException>()
                    .WithMessageMatching("[SDL3] * (test)");
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    public async Task ThrowIfNegativeShouldNotThrowWhenValueIsPositive(int value)
    {
        await Assert.That(() => SDLThrowHelper.ThrowIfNegative(value))
                    .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNegativeShouldThrowWhenValueIsNegative()
    {
        await Assert.That(() => SDLThrowHelper.ThrowIfNegative(-1, "test"))
                    .ThrowsExactly<QuackInteropException>()
                    .WithMessageMatching("[SDL3] * (test)");
    }

    [Test]
    public async Task ThrowIfNullClassShouldNotThrowWhenValueIsNotNull()
    {
        const string value = "value";

        await Assert.That(() => SDLThrowHelper.ThrowIfNull(value))
                    .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfNullClassShouldThrowWhenValueIsNull()
    {
        const string? value = null;

        await Assert.That(() => SDLThrowHelper.ThrowIfNull(value, "test"))
                    .ThrowsExactly<QuackInteropException>()
                    .WithMessageMatching("[SDL3] * (test)");
    }

    [Test]
    [Arguments(-1)]
    [Arguments(1)]
    public async Task ThrowIfZeroShouldNotThrowWhenValueIsNotZero(int value)
    {
        await Assert.That(() => SDLThrowHelper.ThrowIfZero(value))
                    .ThrowsNothing();
    }

    [Test]
    public async Task ThrowIfZeroShouldThrowWhenValueIsZero()
    {
        await Assert.That(() => SDLThrowHelper.ThrowIfZero(0, "test"))
                    .ThrowsExactly<QuackInteropException>()
                    .WithMessageMatching("[SDL3] * (test)");
    }
}

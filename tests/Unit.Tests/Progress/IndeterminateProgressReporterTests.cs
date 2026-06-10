// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Progress;

namespace Unit.Tests.Progress;

internal sealed class IndeterminateProgressReporterTests
{
    private readonly IndeterminateProgressReporter _reporter = new();

    [Test]
    public async Task CancelShouldThrowOperationCanceledException()
    {
        await Assert.That(_reporter.Cancel)
                    .ThrowsExactly<OperationCanceledException>();
    }
}

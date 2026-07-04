// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.UI.Progress.Reporters;

namespace Unit.Tests.UI.Progress.Reporters;

internal sealed class AsyncIndeterminateProgressReporterTests : IDisposable
{
    private readonly AsyncIndeterminateProgressReporter _reporter = new();

    public void Dispose() => _reporter.Dispose();

    [Test]
    public async Task CancelShouldRequestToCancelTheToken()
    {
        _reporter.Cancel();
        await _reporter.CancellationToken.IsCancellationRequested.Should().BeTrue();
    }

    [Test]
    public async Task CancelAsyncShouldRequestToCancelTheToken()
    {
        await _reporter.CancelAsync();
        await _reporter.CancellationToken.IsCancellationRequested.Should().BeTrue();
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Progress;
using System.Diagnostics.CodeAnalysis;

namespace Unit.Tests.Progress;

internal sealed class AsyncIndeterminateProgressReporterTests : IDisposable
{
    private readonly AsyncIndeterminateProgressReporter _reporter = new();

    public void Dispose() => _reporter.Dispose();

    [Test]
    [SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "The test requires to test the method Cancel")]
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

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.UI.Progress;
using KappaDuck.Quack.UI.Progress.Reporters;

namespace Unit.Tests.UI.Progress.Reporters;

internal sealed class AsyncProgressReporterTests : IDisposable
{
    private readonly Mock<IProgressOperation> _operation = IProgressOperation.Mock();
    private readonly AsyncProgressReporter _reporter;

    public AsyncProgressReporterTests()
    {
        _reporter = new AsyncProgressReporter(_operation.Object, 100);
    }

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

    [Test]
    public void ReportShouldReportValueToOperation()
    {
        _reporter.Report(100);
        _operation.Report(1).WasCalled(Times.Once);
    }

    [Test]
    public async Task ReportShouldThrowExceptionWhenValueIsNegative()
    {
        await Assert.That(() => _reporter.Report(-100))
                    .ThrowsExactly<ArgumentOutOfRangeException>();

        _operation.Report(Any()).WasNeverCalled();
    }

    [Test]
    public void ReportShouldClampToTheTotalWhenValueIsGreaterThanTotal()
    {
        _reporter.Report(999);
        _operation.Report(1).WasCalled(Times.Once);
    }

    [Test]
    public async Task ReportShouldThrowOperationCanceledExceptionWhenIsCancelled()
    {
        await _reporter.CancelAsync();

        await Assert.That(() => _reporter.Report(100))
                    .ThrowsExactly<OperationCanceledException>();

        _operation.Report(Any()).WasNeverCalled();
    }

    [Test]
    public void IncrementShouldIncrementByStep()
    {
        _reporter.Increment(25);
        _operation.Report(0.25f).WasCalled(Times.Once);
    }

    [Test]
    public void IncrementShouldCumulateAndIncrementTheStepsWhenCallingMultipleTimes()
    {
        _reporter.Increment(25);
        _reporter.Increment(25);

        _operation.Report(0.5f).WasCalled(Times.Once);
    }

    [Test]
    public async Task IncrementShouldThrowExceptionWhenValueIsNegative()
    {
        await Assert.That(() => _reporter.Increment(-25))
                    .ThrowsExactly<ArgumentOutOfRangeException>();

        _operation.Report(Any()).WasNeverCalled();
    }

    [Test]
    public async Task IncrementShouldThrowOperationCanceledExceptionWhenIsCancelled()
    {
        await _reporter.CancelAsync();

        await Assert.That(() => _reporter.Increment(25))
                    .ThrowsExactly<OperationCanceledException>();

        _operation.Report(Any()).WasNeverCalled();
    }

    [Test]
    public void AdvanceShouldIncrementByOneStep()
    {
        _reporter.Advance();
        _operation.Report(0.01f).WasCalled(Times.Once);
    }

    [Test]
    public void AdvanceShouldCumulateAndAdvanceByOneStepWhenCallingMultipleTimes()
    {
        _reporter.Advance();
        _reporter.Advance();

        _operation.Report(0.02f).WasCalled(Times.Once);
    }

    [Test]
    public async Task AdvanceShouldThrowOperationCanceledExceptionWhenIsCancelled()
    {
        await _reporter.CancelAsync();

        await Assert.That(_reporter.Advance)
                    .ThrowsExactly<OperationCanceledException>();

        _operation.Report(Any()).WasNeverCalled();
    }
}

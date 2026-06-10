// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Progress;

namespace Unit.Tests.Progress;

internal sealed class ProgressReporterTests
{
    private readonly Mock<IProgressSink> _sink = IProgressSink.Mock();
    private readonly ProgressReporter _reporter;

    public ProgressReporterTests()
    {
        _reporter = new ProgressReporter(_sink.Object, 100);
    }

    [Test]
    public void CancelShouldCancelTheProgressSink()
    {
        _reporter.Cancel();
        _sink.Cancel().WasCalled(Times.Once);
    }

    [Test]
    public void CancelShouldNotCancelTheProgressSinkTwiceWhenIsAlreadyCancelled()
    {
        _reporter.Cancel();
        _reporter.Cancel();

        _sink.Cancel().WasCalled(Times.Once);
    }

    [Test]
    public void ReportShouldReportValueToProgressSink()
    {
        _reporter.Report(100);
        _sink.Report(1).WasCalled(Times.Once);
    }

    [Test]
    public async Task ReportShouldThrowExceptionWhenValueIsNegative()
    {
        await Assert.That(() => _reporter.Report(-100))
                    .ThrowsExactly<ArgumentOutOfRangeException>();

        _sink.Report(Any()).WasNeverCalled();
    }

    [Test]
    public void ReportShouldClampToTheTotalWhenValueIsGreaterThanTotal()
    {
        _reporter.Report(999);
        _sink.Report(1).WasCalled(Times.Once);
    }

    [Test]
    public void ReportShouldDoNothingWhenIsCancelled()
    {
        _reporter.Cancel();
        _reporter.Report(100);

        _sink.Report(Any()).WasNeverCalled();
    }

    [Test]
    public void IncrementShouldIncrementByStep()
    {
        _reporter.Increment(25);
        _sink.Report(0.25f).WasCalled(Times.Once);
    }

    [Test]
    public void IncrementShouldCumulateAndIncrementTheStepsWhenCallingMultipleTimes()
    {
        _reporter.Increment(25);
        _reporter.Increment(25);

        _sink.Report(0.5f).WasCalled(Times.Once);
    }

    [Test]
    public async Task IncrementShouldThrowExceptionWhenValueIsNegative()
    {
        await Assert.That(() => _reporter.Increment(-25))
                    .ThrowsExactly<ArgumentOutOfRangeException>();

        _sink.Report(Any()).WasNeverCalled();
    }

    [Test]
    public void IncrementShouldDoNothingWhenIsCancelled()
    {
        _reporter.Cancel();
        _reporter.Increment(25);

        _sink.Report(Any()).WasNeverCalled();
    }

    [Test]
    public void AdvanceShouldIncrementByOneStep()
    {
        _reporter.Advance();
        _sink.Report(0.01f).WasCalled(Times.Once);
    }

    [Test]
    public void AdvanceShouldCumulateAndAdvanceByOneStepWhenCallingMultipleTimes()
    {
        _reporter.Advance();
        _reporter.Advance();

        _sink.Report(0.02f).WasCalled(Times.Once);
    }

    [Test]
    public void AdvanceShouldDoNothingWhenIsCancelled()
    {
        _reporter.Cancel();
        _reporter.Advance();

        _sink.Report(Any()).WasNeverCalled();
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.UI.Progress;
using KappaDuck.Quack.UI.Progress.Reporters;

namespace Unit.Tests.UI.Progress.Reporters;

internal sealed class ProgressReporterTests
{
    private readonly Mock<IProgressOperation> _operation = IProgressOperation.Mock();
    private readonly ProgressReporter _reporter;

    public ProgressReporterTests()
    {
        _reporter = new ProgressReporter(_operation.Object, 100);
    }

    [Test]
    public void CancelShouldCancelTheOperation()
    {
        _reporter.Cancel();
        _operation.Cancel().WasCalled(Times.Once);
    }

    [Test]
    public void CancelShouldNotCancelTheOperationTwiceWhenIsAlreadyCancelled()
    {
        _reporter.Cancel();
        _reporter.Cancel();

        _operation.Cancel().WasCalled(Times.Once);
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
    public void ReportShouldDoNothingWhenIsCancelled()
    {
        _reporter.Cancel();
        _reporter.Report(100);

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
    public void IncrementShouldDoNothingWhenIsCancelled()
    {
        _reporter.Cancel();
        _reporter.Increment(25);

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
    public void AdvanceShouldDoNothingWhenIsCancelled()
    {
        _reporter.Cancel();
        _reporter.Advance();

        _operation.Report(Any()).WasNeverCalled();
    }
}

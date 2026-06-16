// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using Microsoft.Extensions.Time.Testing;

namespace Unit.Tests.Core;

internal sealed class GameClockTests
{
    private readonly FakeTimeProvider _provider = new();
    private readonly GameClock _clock;

    public GameClockTests()
    {
        _clock = new GameClock(_provider);
    }

    [Test]
    public async Task TickShouldReturnZeroDeltaWhenNoTimeHasElapsed()
    {
        GameTime time = _clock.Tick();

        await time.Delta.Should().BeZero();
    }

    [Test]
    public async Task TickShouldreturnElapsedTimeAsDelta()
    {
        _provider.Advance(TimeSpan.FromMilliseconds(16));
        GameTime time = _clock.Tick();

        await time.Delta.Should().BeEqualTo(TimeSpan.FromMilliseconds(16));
    }

    [Test]
    public async Task TickShouldMeasureDeltaBetweenConsecutiveTicks()
    {
        _provider.Advance(TimeSpan.FromMilliseconds(16));
        _clock.Tick();

        _provider.Advance(TimeSpan.FromMilliseconds(32));
        GameTime time = _clock.Tick();

        await time.Delta.Should().BeEqualTo(TimeSpan.FromMilliseconds(32));
    }

    [Test]
    public async Task TotalShouldAccumulateAcrossTicks()
    {
        _provider.Advance(TimeSpan.FromMilliseconds(16));
        _clock.Tick();

        _provider.Advance(TimeSpan.FromMilliseconds(16));
        GameTime time = _clock.Tick();

        await time.Total.Should().BeEqualTo(TimeSpan.FromMilliseconds(32));
    }

    [Test]
    public async Task TickShouldClampDeltaWhenExceedingMaxDelta()
    {
        _clock.MaxDelta = TimeSpan.FromMilliseconds(250);

        _provider.Advance(TimeSpan.FromSeconds(1));
        GameTime time = _clock.Tick();

        await time.Delta.Should().BeEqualTo(TimeSpan.FromMilliseconds(250));
    }

    [Test]
    public async Task TickShouldFlagRunningSlowlyWhenDeltaIsClamped()
    {
        _clock.MaxDelta = TimeSpan.FromMilliseconds(250);

        _provider.Advance(TimeSpan.FromSeconds(1));
        GameTime time = _clock.Tick();

        await time.IsRunningSlowly.Should().BeTrue();
    }

    [Test]
    public async Task TickShouldNotFlagRunningSlowlyWhenWithinMaxDelta()
    {
        _clock.MaxDelta = TimeSpan.FromMilliseconds(250);

        _provider.Advance(TimeSpan.FromMilliseconds(16));
        GameTime time = _clock.Tick();

        await time.IsRunningSlowly.Should().BeFalse();
    }

    [Test]
    public async Task TotalShouldUseRealElapsedTimeEvenWhenDeltaIsClamped()
    {
        _clock.MaxDelta = TimeSpan.FromMilliseconds(250);

        _provider.Advance(TimeSpan.FromSeconds(1));
        GameTime time = _clock.Tick();

        await time.Total.Should().BeEqualTo(TimeSpan.FromSeconds(1));
    }

    [Test]
    public async Task ResetShouldRestartTotalAndDelta()
    {
        _provider.Advance(TimeSpan.FromMilliseconds(100));
        _clock.Tick();

        _clock.Reset();

        _provider.Advance(TimeSpan.FromMilliseconds(16));
        GameTime result = _clock.Tick();

        await result.Total.Should().BeEqualTo(TimeSpan.FromMilliseconds(16));
        await result.Delta.Should().BeEqualTo(TimeSpan.FromMilliseconds(16));
    }

    [Test]
    public async Task DeltaSecondsShouldReflectDelta()
    {
        _provider.Advance(TimeSpan.FromMilliseconds(500));
        GameTime result = _clock.Tick();

        await result.DeltaSeconds.Should().BeCloseTo(0.5f, 0.0001f);
    }
}

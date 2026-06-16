// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Measures frame timing, producing a <see cref="GameTime"/> snapshot each tick.
/// </summary>
/// <remarks>
/// Create one clock per loop and call <see cref="Tick"/> once at the top of each frame. The managed
/// game loop owns a clock for you; create your own only when driving a classic loop.
/// </remarks>
public sealed class GameClock
{
    private readonly TimeProvider _provider;
    private long _startTimestamp;
    private long _lastTimestamp;

    /// <summary>
    /// Initializes a new clock driven by the <see cref="TimeProvider.System"/>.
    /// </summary>
    public GameClock() : this(TimeProvider.System)
    {
    }

    /// <summary>
    /// Initializes a new clock driven by a custom <paramref name="provider"/>.
    /// </summary>
    /// <remarks>
    /// Pass a custom provider for deterministic timing, e.g. replays or tests.
    /// </remarks>
    /// <param name="provider">The time provider</param>
    public GameClock(TimeProvider provider)
    {
        _provider = provider;
        Reset();
    }

    /// <summary>
    /// Gets or sets the maximum delta a single <see cref="Tick"/> may report.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use it to avoid a huge time step after a stall (dragging the window, a breakpoint, a long GC),
    /// which would otherwise make movement and physics jump. When a tick is clamped,
    /// <see cref="GameTime.IsRunningSlowly"/> is set.
    /// </para>
    /// <para>The default is <see langword="null"/>, which applies no clamp.</para>
    /// </remarks>
    public TimeSpan? MaxDelta { get; set; }

    /// <summary>
    /// Gets the total elapsed time since the clock started or was last reset.
    /// </summary>
    public TimeSpan Total => _provider.GetElapsedTime(_startTimestamp);

    /// <summary>
    /// Advances the clock by one frame and returns the timing snapshot.
    /// </summary>
    /// <returns>The timing values for the frame.</returns>
    public GameTime Tick()
    {
        long now = _provider.GetTimestamp();

        TimeSpan delta = _provider.GetElapsedTime(_lastTimestamp, now);
        _lastTimestamp = now;

        bool slow = false;
        if (MaxDelta.HasValue && delta > MaxDelta.Value)
        {
            delta = MaxDelta.Value;
            slow = true;
        }

        return new GameTime
        {
            Total = _provider.GetElapsedTime(_startTimestamp, now),
            Delta = delta,
            IsRunningSlowly = slow
        };
    }

    /// <summary>
    /// Resets the clock so the next tick measures from now and <see cref="Total"/> restarts.
    /// </summary>
    public void Reset()
    {
        _startTimestamp = _provider.GetTimestamp();
        _lastTimestamp = _startTimestamp;
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents a snapshot of timing values for a single frame.
/// </summary>
public readonly record struct GameTime
{
    /// <summary>
    /// Gets the total elapsed time since the clock started or was last reset.
    /// </summary>
    public TimeSpan Total { get; init; }

    /// <summary>
    /// Gets the elapsed time since the previous frame.
    /// </summary>
    public TimeSpan Delta { get; init; }

    /// <summary>
    /// Gets the delta time in seconds, convenient for scaling movement and physics.
    /// </summary>
    public float DeltaSeconds => (float)Delta.TotalSeconds;

    /// <summary>
    /// Gets a value indicating whether the loop is falling behind its target rate.
    /// </summary>
    /// <remarks>
    /// In a fixed time step loop it is <see langword="true"/> when updates cannot keep up with the
    /// target step. In a variable loop it is <see langword="true"/> when the frame's delta was
    /// clamped by <see cref="GameClock.MaxDelta"/>.
    /// </remarks>
    public bool IsRunningSlowly { get; init; }
}

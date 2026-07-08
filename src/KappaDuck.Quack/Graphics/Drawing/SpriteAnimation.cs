// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A reusable animation clip: a sequence of <see cref="SpriteSheet"/> frame indices, each shown for a duration, with
/// an optional loop.
/// </summary>
/// <remarks>
/// A clip is independent of any particular sprite; the same clip can be played by several <see cref="AnimatedSprite"/>
/// instances. Frame indices refer to the sheet the animation is played on.
/// </remarks>
public sealed class SpriteAnimation
{
    private readonly int[] _frames;
    private readonly TimeSpan[] _durations;

    /// <summary>
    /// Creates an animation whose frames are all shown for the same duration.
    /// </summary>
    /// <param name="frames">The sheet frame indices, in playback order.</param>
    /// <param name="frameDuration">How long each frame is shown.</param>
    /// <param name="loop"><see langword="true"/> to restart from the first frame after the last; otherwise <see langword="false"/>.</param>
    public SpriteAnimation(ReadOnlySpan<int> frames, TimeSpan frameDuration, bool loop = true)
    {
        _frames = frames.ToArray();
        _durations = new TimeSpan[_frames.Length];

        Array.Fill(_durations, frameDuration);

        Loop = loop;
        Duration = frameDuration * _frames.Length;
    }

    /// <summary>
    /// Creates an animation with a separate duration for each frame.
    /// </summary>
    /// <param name="frames">The sheet frame indices, in playback order.</param>
    /// <param name="durations">How long each corresponding frame is shown. Must have the same length as <paramref name="frames"/>.</param>
    /// <param name="loop"><see langword="true"/> to restart from the first frame after the last; otherwise <see langword="false"/>.</param>
    /// <exception cref="ArgumentException"><paramref name="frames"/> and <paramref name="durations"/> have different lengths.</exception>
    public SpriteAnimation(ReadOnlySpan<int> frames, ReadOnlySpan<TimeSpan> durations, bool loop = true)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(frames.Length, durations.Length);

        _frames = frames.ToArray();
        _durations = durations.ToArray();

        Loop = loop;
        Duration = Sum(durations);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the animation restarts after its last frame.
    /// </summary>
    public bool Loop { get; set; }

    /// <summary>
    /// Gets the number of frames in the animation.
    /// </summary>
    public int FrameCount => _frames.Length;

    /// <summary>
    /// Gets the total time for one pass through every frame.
    /// </summary>
    public TimeSpan Duration { get; }

    internal int FrameAt(int index) => _frames[index];

    internal TimeSpan DurationAt(int index) => _durations[index];

    private static TimeSpan Sum(ReadOnlySpan<TimeSpan> durations)
    {
        TimeSpan total = TimeSpan.Zero;

        foreach (TimeSpan duration in durations)
            total += duration;

        return total;
    }
}

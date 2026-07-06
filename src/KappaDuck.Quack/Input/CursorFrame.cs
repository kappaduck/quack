// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Input;

/// <summary>
/// Represents a single frame of an animated cursor.
/// </summary>
/// <remarks>
/// Every frame of an animation must share the same dimensions.
/// </remarks>
/// <param name="surface">The image used for this frame.</param>
/// <param name="duration">
/// How long the frame is shown before advancing to the next one, rounded down to the nearest millisecond. A
/// duration of <see cref="TimeSpan.Zero"/> or less means the frame never advances, ending the animation on that frame.
/// </param>
public readonly struct CursorFrame(Surface surface, TimeSpan duration)
{
    /// <summary>
    /// Gets the image used for this frame.
    /// </summary>
    public Surface Surface { get; } = surface;

    /// <summary>
    /// Gets how long the frame is shown before advancing to the next one.
    /// </summary>
    public TimeSpan Duration { get; } = duration;
}

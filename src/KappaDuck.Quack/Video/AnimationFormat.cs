// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video;

/// <summary>
/// Represents an animated image format.
/// </summary>
public enum AnimationFormat
{
    /// <summary>
    /// The ANI (Windows animated cursor) format.
    /// </summary>
    ANI = 0,

    /// <summary>
    /// The APNG (Animated PNG) format.
    /// </summary>
    APNG = 1,

    /// <summary>
    /// The AVIFS (AVIF image sequence) format.
    /// </summary>
    AVIFS = 2,

    /// <summary>
    /// The GIF format.
    /// </summary>
    GIF = 3,

    /// <summary>
    /// The WEBP format.
    /// </summary>
    WEBP = 4
}

internal static class AnimationFormatExtensions
{
    extension(AnimationFormat format)
    {
        public string Type => format switch
        {
            AnimationFormat.ANI => nameof(AnimationFormat.ANI),
            AnimationFormat.APNG => nameof(AnimationFormat.APNG),
            AnimationFormat.AVIFS => nameof(AnimationFormat.AVIFS),
            AnimationFormat.GIF => nameof(AnimationFormat.GIF),
            AnimationFormat.WEBP => nameof(AnimationFormat.WEBP),
            _ => throw new NotImplementedException(),
        };
    }
}

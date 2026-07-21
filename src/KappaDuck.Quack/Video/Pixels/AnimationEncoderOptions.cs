// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// Optional settings for <see cref="AnimationEncoder"/>.
/// </summary>
/// <remarks>
/// Every setting is optional; unset ones fall back to the format's own default. Settings that don't apply to the
/// chosen format (for example <see cref="AvifMaxThreads"/> when encoding a GIF) are ignored.
/// </remarks>
public sealed record AnimationEncoderOptions
{
    /// <summary>
    /// Gets or sets the encoding quality, from 0 to 100, for formats that support it (AVIF, WebP). Higher is better
    /// quality and larger output.
    /// </summary>
    public int? Quality { get; init; }

    /// <summary>
    /// Gets or sets the numerator of the fraction used to convert a frame's duration to seconds. Defaults to 1.
    /// </summary>
    public int? TimeBaseNumerator { get; set; }

    /// <summary>
    /// Gets or sets the denominator of the fraction used to convert a frame's duration to seconds. Defaults to 1000,
    /// meaning durations passed to <see cref="AnimationEncoder.AddFrame"/> are interpreted as milliseconds.
    /// </summary>
    public int? TimeBaseDenominator { get; set; }

    /// <summary>
    /// Gets or sets whether GIF encoding reuses a single shared color table across every frame instead of computing
    /// a fresh palette per frame.
    /// </summary>
    public bool? GifUseLookupTable { get; init; }

    /// <summary>
    /// Gets or sets how often AVIF encoding inserts a key frame.
    /// </summary>
    public int? AvifKeyFrameInterval { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of threads used for AVIF encoding.
    /// </summary>
    public int? AvifMaxThreads { get; init; }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Imaging.Animations;

/// <summary>
/// Describes the current state of an <see cref="AnimationDecoder"/>.
/// </summary>
public enum AnimationDecoderStatus
{
    /// <summary>
    /// The decoder is invalid, for example because it has already been closed.
    /// </summary>
    Invalid = -1,

    /// <summary>
    /// The decoder is active and more frames may still be available.
    /// </summary>
    Decoding = 0,

    /// <summary>
    /// Decoding failed. The most recent read did not produce a frame because of an error.
    /// </summary>
    Failed = 1,

    /// <summary>
    /// The end of the animation has been reached and every frame has been read.
    /// </summary>
    Complete = 2
}

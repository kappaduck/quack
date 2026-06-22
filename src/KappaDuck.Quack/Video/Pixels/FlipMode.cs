// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// The axes along which an image is flipped.
/// </summary>
[Flags]
public enum FlipMode
{
    /// <summary>
    /// No flip.
    /// </summary>
    None = 0,

    /// <summary>
    /// Flip horizontally, mirroring left and right.
    /// </summary>
    Horizontal = 1,

    /// <summary>
    /// Flip vertically, mirroring top and bottom.
    /// </summary>
    Vertical = 2
}

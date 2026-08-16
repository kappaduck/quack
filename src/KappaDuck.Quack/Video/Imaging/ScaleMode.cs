// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Imaging;

/// <summary>
/// The filtering used when an image is scaled.
/// </summary>
public enum ScaleMode
{
    /// <summary>
    /// Nearest pixel sampling, producing crisp, blocky results.
    /// </summary>
    Nearest = 0,

    /// <summary>
    /// Linear filtering, producing smooth, blended results.
    /// </summary>
    Linear = 1,

    /// <summary>
    /// Nearest pixel sampling tuned for pixel art.
    /// </summary>
    PixelArt = 2
}

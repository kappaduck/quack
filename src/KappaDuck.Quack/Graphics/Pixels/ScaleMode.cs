// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// The scaling mode to use when rendering a surface.
/// </summary>
[PublicAPI]
public enum ScaleMode
{
    /// <summary>
    /// No scaling is applied.
    /// </summary>
    Invalid = -1,

    /// <summary>
    /// Nearest pixel sampling.
    /// </summary>
    Nearest = 0,

    /// <summary>
    /// Linear filtering.
    /// </summary>
    Linear = 1
}

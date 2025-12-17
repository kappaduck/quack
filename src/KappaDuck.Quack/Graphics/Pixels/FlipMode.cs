// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Specifies the flip mode for a surface.
/// </summary>
[Flags]
[PublicAPI]
public enum FlipMode
{
    /// <summary>
    /// No flipping.
    /// </summary>
    None = 0,

    /// <summary>
    /// Flip the surface horizontally.
    /// </summary>
    Horizontal = 1,

    /// <summary>
    /// Flip the surface vertically.
    /// </summary>
    Vertical = 2
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// Describes how texture coordinates outside the [0, 1] range are interpreted when rendering geometry.
/// </summary>
public enum TextureAddressMode
{
    /// <summary>
    /// The addressing mode is invalid or could not be determined.
    /// </summary>
    Invalid = -1,

    /// <summary>
    /// Wrapping is enabled if texture coordinates are outside [0, 1]. This is the default.
    /// </summary>
    Auto = 0,

    /// <summary>
    /// Texture coordinates are clamped to the [0, 1] range.
    /// </summary>
    Clamp = 1,

    /// <summary>
    /// The texture is repeated (tiled) outside the [0, 1] range.
    /// </summary>
    Wrap = 2
}

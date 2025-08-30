// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// The access pattern allowed for a texture.
/// </summary>
public enum TextureAccess
{
    /// <summary>
    /// The texture is rarely changed, not lockable.
    /// </summary>
    Static = 0,

    /// <summary>
    /// The texture is frequently updated, lockable.
    /// </summary>
    Streaming = 1,

    /// <summary>
    /// The texture can be used as a render target.
    /// </summary>
    Target = 2
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// Describes how the pixels of a <see cref="Texture"/> may be accessed after it is created.
/// </summary>
public enum TextureAccess
{
    /// <summary>
    /// The texture changes rarely and cannot be locked. This is the default.
    /// </summary>
    Static = 0,

    /// <summary>
    /// The texture changes frequently and can be locked to write its pixels directly.
    /// </summary>
    Streaming = 1,

    /// <summary>
    /// The texture can be used as a render target that a <see cref="Renderer"/> draws into.
    /// </summary>
    Target = 2
}

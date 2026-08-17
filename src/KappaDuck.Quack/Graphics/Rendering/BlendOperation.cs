// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// The operation used to combine the weighted source and destination pixel components.
/// </summary>
/// <remarks>
/// Support varies by renderer backend. <see cref="Add"/> is supported everywhere; the other operations are
/// only available on some backends.
/// </remarks>
public enum BlendOperation
{
    /// <summary>
    /// Adds the source and destination, <c>source + destination</c>.
    /// </summary>
    Add = 0x1,

    /// <summary>
    /// Subtracts the destination from the source, <c>source - destination</c>.
    /// </summary>
    /// <remarks>
    /// Supported by D3D, OpenGL, OpenGLES, and Vulkan.
    /// </remarks>
    Subtract = 0x2,

    /// <summary>
    /// Subtracts the source from the destination, <c>destination - source</c>.
    /// </summary>
    /// <remarks>
    /// Supported by D3D, OpenGL, OpenGLES, and Vulkan.
    /// </remarks>
    ReverseSubtract = 0x3,

    /// <summary>
    /// Takes the smaller of the source and destination, <c>min(source, destination)</c>.
    /// </summary>
    /// <remarks>
    /// Supported by D3D, OpenGL, OpenGLES, and Vulkan.
    /// </remarks>
    Minimum = 0x4,

    /// <summary>
    /// Takes the larger of the source and destination, <c>max(source, destination)</c>.
    /// </summary>
    /// <remarks>
    /// Supported by D3D, OpenGL, OpenGLES, and Vulkan.
    /// </remarks>
    Maximum = 0x5
}

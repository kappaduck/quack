// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// The blend operation used when combining source and destination pixel components.
/// </summary>
public enum BlendOperation
{
    /// <summary>
    /// Defines the blend operation for adding colors in rendering.
    /// </summary>
    /// <remarks>
    /// Supported by all renderers.
    /// </remarks>
    Add = 0x1,

    /// <summary>
    /// Defines the blend operation for subtracting colors in rendering.
    /// </summary>
    /// <remarks>
    /// Supported by D3D, OpenGL, OpenGLES, and Vulkan.
    /// </remarks>
    Subtract = 0x2,

    /// <summary>
    /// Defines the blend operation for subtracting colors in reverse order in rendering.
    /// </summary>
    /// <remarks>
    /// Supported by D3D, OpenGL, OpenGLES, and Vulkan.
    /// </remarks>
    ReverseSubtract = 0x3,

    /// <summary>
    /// Defines the blend operation for taking the minimum of colors in rendering.
    /// </summary>
    /// <remarks>
    /// Supported by D3D, OpenGL, OpenGLES, and Vulkan.
    /// </remarks>
    Minimum = 0x4,

    /// <summary>
    /// Defines the blend operation for taking the maximum of colors in rendering.
    /// </summary>
    /// <remarks>
    /// Supported by D3D, OpenGL, OpenGLES, and Vulkan.
    /// </remarks>
    Maximum = 0x5
}

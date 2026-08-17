// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// Options to configure the <see cref="Renderer"/>.
/// </summary>
public sealed record RendererOptions
{
    /// <summary>
    /// Gets the name of the rendering driver to use, or <see langword="null"/> to let the engine choose the most suitable one.
    /// </summary>
    /// <remarks>
    /// If you want a specific renderer, you can find from the available renderers using <see cref="RenderDrivers.All"/>.
    /// You can use a comma-separated list e.g. vulkan, opengl which the engine will try each name, in the order listed, until one succeeds or all of them fail.
    /// </remarks>
    public string? Driver { get; init; }

    /// <summary>
    /// Gets the initial vertical synchronization mode.
    /// </summary>
    /// <remarks>
    /// <see cref="VSync.Disabled"/> to disables synchronization, 1 synchronizes with every vertical refresh, and larger values synchronize
    /// with every Nth refresh. <see cref="VSync.Adaptive"/>, where supported, to enables adaptive synchronization. The default is <see cref="VSync.Disabled"/>.
    /// </remarks>
    public VSync VSync { get; init; } = VSync.Disabled;

    /// <summary>
    /// Gets the colorspace used for output to the display.
    /// </summary>
    /// <remarks>
    /// Selecting <see cref="Colorspace.SrgbLinear"/> enables high-dynamic-range output on backends (direct3d11, direct3d12) that support it; drawing
    /// still uses the sRGB colorspace, but channel values may exceed 1.
    /// </remarks>
    public Colorspace Colorspace { get; init; } = Colorspace.Srgb;
}

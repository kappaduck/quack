// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// The set of parameters that control how a drawable is rendered onto an <see cref="IRenderTarget"/>: the transform
/// applied to its vertices, the blend mode, and the texture it is drawn with.
/// </summary>
/// <remarks>
/// Create a render state with an object initializer, for example <c>new RenderState { Texture = texture }</c>, or from
/// <see cref="Default"/>, then layer on additional transforms with a <c>with</c> expression. Avoid
/// <c>default(RenderState)</c>: it leaves the <see cref="Transform"/> unset rather than at
/// <see cref="Transform.Identity"/>.
/// </remarks>
public readonly record struct RenderState
{
    /// <summary>
    /// Creates a render state with an identity <see cref="Transform"/>, alpha blending and no texture.
    /// </summary>
    public RenderState()
    {
        Transform = Transform.Identity;
        BlendMode = BlendMode.Blend;
        Texture = null;
    }

    /// <summary>
    /// Gets the default render state: an identity transform, alpha blending and no texture.
    /// </summary>
    public static RenderState Default { get; } = new();

    /// <summary>
    /// Gets the transform applied to the vertices before they are drawn. Defaults to <see cref="Transform.Identity"/>.
    /// </summary>
    public Transform Transform { get; init; }

    /// <summary>
    /// Gets the blend mode used to combine the drawing with the target. Defaults to <see cref="BlendMode.Blend"/>.
    /// </summary>
    public BlendMode BlendMode { get; init; }

    /// <summary>
    /// Gets the texture the vertices are drawn with, or <see langword="null"/> to draw geometry without a texture.
    /// </summary>
    public Texture? Texture { get; init; }
}

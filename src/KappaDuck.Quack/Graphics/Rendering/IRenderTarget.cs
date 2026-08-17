// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Primitives;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// Represents a surface that can be drawn onto, such as a <see cref="Renderer"/>.
/// </summary>
/// <remarks>
/// Drawable are rendered with <see cref="Draw(IDrawable)"/>; they in turn lower themselves to the vertex overloads,
/// which apply the render state's transform, blend mode and texture.
/// </remarks>
public interface IRenderTarget
{
    /// <summary>
    /// Draws <paramref name="drawable"/> onto this target using the <see cref="RenderState.Default"/> state.
    /// </summary>
    /// <param name="drawable">The object to draw.</param>
    void Draw(IDrawable drawable);

    /// <summary>
    /// Draws <paramref name="drawable"/> onto this target using the given render state.
    /// </summary>
    /// <param name="drawable">The object to draw.</param>
    /// <param name="state">The render state to draw with.</param>
    void Draw(IDrawable drawable, RenderState state);

    /// <summary>
    /// Draws a sequence of vertices as triangles onto this target.
    /// </summary>
    /// <remarks>The render state's transform is applied to each vertex, and its texture, when set, textures the triangles.</remarks>
    /// <param name="vertices">The vertices to draw, taken three at a time as triangles.</param>
    /// <param name="state">The render state to draw with.</param>
    void Draw(ReadOnlySpan<Vertex> vertices, RenderState state);

    /// <summary>
    /// Draws indexed vertices as triangles onto this target.
    /// </summary>
    /// <remarks>The render state's transform is applied to each vertex, and its texture, when set, textures the triangles.</remarks>
    /// <param name="vertices">The vertices to draw.</param>
    /// <param name="indices">Indices into <paramref name="vertices"/>, taken three at a time as triangles.</param>
    /// <param name="state">The render state to draw with.</param>
    void Draw(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices, RenderState state);
}

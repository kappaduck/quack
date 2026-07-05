// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Graphics.Rendering;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// Represents an object that can draw itself onto an <see cref="IRenderTarget"/>, such as a sprite, shape or anything.
/// </summary>
public interface IDrawable
{
    /// <summary>
    /// Draws this object onto <paramref name="target"/>.
    /// </summary>
    /// <remarks>
    /// Implementations fold their own transform into <paramref name="state"/> (for example with
    /// <c>state = state with { Transform = state.Transform * localTransform }</c>), set the texture they need, then
    /// lower themselves to the target's vertex draws. Call <see cref="IRenderTarget.Draw(IDrawable)"/> rather than
    /// this method directly.
    /// </remarks>
    /// <param name="target">The target to draw onto.</param>
    /// <param name="state">The render state to draw with, carrying the accumulated transform, blend mode and texture.</param>
    void Draw(IRenderTarget target, RenderState state);
}

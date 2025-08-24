// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Rendering;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// Represents an object that can be drawn on a render target.
/// </summary>
public interface IDrawable
{
    /// <summary>
    /// Draws the object on the specified render target.
    /// </summary>
    /// <param name="target">The render target to draw on.</param>
    void Draw(IRenderTarget target);
}

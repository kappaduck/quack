// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Graphics.Rendering;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// Represents a 2D sprite that can be drawn on a render target by using a texture.
/// </summary>
/// <param name="texture">The texture to use for the sprite.</param>
public sealed class Sprite(Texture texture) : IDrawable
{
    private Rect _target = new(0, 0, texture.Width, texture.Height);
    private float _rotation;

    /// <summary>
    /// Gets or sets the center around which the sprite will be rotated.
    /// </summary>
    public Vector2 Center { get; set; } = new Vector2(texture.Width / 2f, texture.Height / 2f);

    /// <summary>
    /// Gets or sets the color modulation of the sprite.
    /// </summary>
    public Color Color
    {
        get
        {
            Color color = Texture.ColorModulation;
            return Color.FromArgb(Texture.AlphaModulation, color.R, color.G, color.B);
        }

        set
        {
            Texture.AlphaModulation = value.A;
            Texture.ColorModulation = Color.FromArgb(value.R, value.G, value.B);
        }
    }

    /// <summary>
    /// Gets or sets the flip mode of the sprite.
    /// </summary>
    public FlipMode Flip { get; set; } = FlipMode.None;

    /// <summary>
    /// Gets or sets the position of the sprite.
    /// </summary>
    public Vector2 Position
    {
        get => _target.Position;
        set => _target.Position = value;
    }

    /// <summary>
    /// Gets or sets the size of the sprite.
    /// </summary>
    public Vector2 Size
    {
        get => _target.Size;
        set => _target.Size = value;
    }

    /// <summary>
    /// Gets or sets the sub rectangle of the texture that will be displayed.
    /// </summary>
    public Rect Source { get; set; } = new(0, 0, texture.Width, texture.Height);

    /// <summary>
    /// Gets or sets the rotation of the sprite in degrees.
    /// </summary>
    public Angle Rotation
    {
        get => Angle.FromDegrees(_rotation);
        set => _rotation = value.Degrees;
    }

    /// <summary>
    /// Gets or sets the texture that the sprite will display.
    /// </summary>
    public Texture Texture { get; set; } = texture;

    /// <inheritdoc/>
    public void Draw(IRenderTarget target) => Texture.Render(Source, _target, _rotation, Center, Flip);

    /// <summary>
    /// Moves the sprite by the specified offset.
    /// </summary>
    /// <param name="offset">The offset by which to move the sprite.</param>
    public void Move(Vector2 offset) => Position += offset;
}

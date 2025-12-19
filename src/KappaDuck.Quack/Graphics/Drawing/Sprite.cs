// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Graphics.Rendering;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// <para>Represents a 2D sprite that can be drawn on a render target by using a texture.</para>
/// <para>
/// When creating a sprite, you can specify the texture to use, as well as its initial position, rotation, and scale. Important to note that
/// the sprite's transformation is applied in the order of translation, scaling, and then rotation.
/// </para>
/// </summary>
/// <param name="texture">The texture to use for the sprite.</param>
/// <param name="position">The initial position of the sprite.</param>
/// <param name="rotation">The initial rotation of the sprite.</param>
/// <param name="scale">The initial scale of the sprite.</param>
/// <param name="origin">The origin point of the sprite, if not specified the center of the texture will be used.</param>
public sealed class Sprite(Texture texture, Vector2 position = default, Angle rotation = default, Vector2? scale = null, Vector2? origin = null) : IDrawable
{
    private Rect _target = new(position.X, position.Y, texture.Width, texture.Height);
    private Transform _localTransform = Transform.Identity
            .WithOrigin(origin ?? new Vector2(texture.Width / 2f, texture.Height / 2f))
            .Translate(position)
            .Scale(scale ?? Vector2.One)
            .Rotate(rotation);

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
    /// Gets or sets the origin point of the sprite.
    /// </summary>
    /// <remarks>
    /// <para>The default origin point is the center of the sprite.</para>
    /// <para>The origin point is the point around which the sprite is rotated and scaled.</para>
    /// </remarks>
    public Vector2 Origin
    {
        get => _localTransform.Origin;
        set => _localTransform = _localTransform.WithOrigin(value);
    }

    /// <summary>
    /// Gets the position of the sprite.
    /// </summary>
    public Vector2 Position => _localTransform.Translation;

    /// <summary>
    /// Gets the rotation of the sprite.
    /// </summary>
    public Angle Rotation => Angle.FromDegrees(_localTransform.RotationInDegrees);

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
    /// Gets or sets the texture that the sprite will display.
    /// </summary>
    public Texture Texture { get; set; } = texture;

    /// <inheritdoc/>
    public void Draw(IRenderTarget target)
    {
        _target.Position = _localTransform.Translation;
        Texture.Render(Source, _target, _localTransform.RotationInDegrees, Origin, Flip);
    }

    /// <summary>
    /// Moves the sprite by the specified offset.
    /// </summary>
    /// <param name="offset">The offset by which to move the sprite.</param>
    public void Move(Vector2 offset) => _localTransform = _localTransform.Translate(offset);

    /// <summary>
    /// Moves the sprite forward in the direction it is facing.
    /// </summary>
    /// <param name="distance">The distance by which to move the sprite.</param>
    public void MoveForward(Vector2 distance)
    {
        float radians = _localTransform.RotationInDegrees * (MathF.PI / 180f);

        float offsetX = (distance.X * MathF.Cos(radians)) - (distance.Y * MathF.Sin(radians));
        float offsetY = (distance.X * MathF.Sin(radians)) + (distance.Y * MathF.Cos(radians));

        Move(new Vector2(offsetX, offsetY));
    }

    /// <summary>
    /// Rotates the sprite by the specified angle.
    /// </summary>
    /// <param name="angle">The angle by which to rotate the sprite.</param>
    public void Rotate(Angle angle) => _localTransform = _localTransform.Rotate(angle);
}

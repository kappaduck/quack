// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A drawable image, or a region of one, that can be positioned, rotated, scaled and tinted.
/// </summary>
/// <remarks>
/// A sprite is a lightweight view onto a <see cref="Texture"/>: many sprites can share one texture. Draw it with
/// <see cref="IRenderTarget.Draw(IDrawable)"/>.
/// </remarks>
public class Sprite : Transformable, IDrawable
{
    private static readonly int[] _indices = [0, 1, 2, 0, 2, 3];
    private readonly Vertex[] _vertices = new Vertex[4];

    private Texture _texture;
    private RectI _region;
    private Color _color = Colors.White;

    /// <summary>
    /// Creates a sprite that displays the whole of <paramref name="texture"/>.
    /// </summary>
    /// <param name="texture">The texture to display.</param>
    public Sprite(Texture texture) : this(texture, new RectI(0, 0, texture.Width, texture.Height))
    {
    }

    /// <summary>
    /// Creates a sprite that displays a region of <paramref name="texture"/>.
    /// </summary>
    /// <param name="texture">The texture to display.</param>
    /// <param name="region">The region of the texture to display, in texture pixels.</param>
    public Sprite(Texture texture, RectI region)
    {
        _texture = texture;
        _region = region;

        UpdateVertices();
    }

    /// <summary>
    /// Gets or sets the texture the sprite displays.
    /// </summary>
    /// <remarks>The current <see cref="Region"/> is kept; assign it again if the new texture has different dimensions.</remarks>
    public Texture Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            UpdateVertices();
        }
    }

    /// <summary>
    /// Gets or sets the region of the <see cref="Texture"/> the sprite displays, in texture pixels.
    /// </summary>
    public RectI Region
    {
        get => _region;
        set
        {
            _region = value;
            UpdateVertices();
        }
    }

    /// <summary>
    /// Gets or sets a color multiplied into the sprite, tinting it.
    /// </summary>
    /// <remarks>Defaults to <see cref="Colors.White"/>, which leaves the texture unchanged; the color's alpha sets the sprite's opacity.</remarks>
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            UpdateColors();
        }
    }

    /// <summary>
    /// Gets the sprite's bounding rectangle in its own local space, before its transform is applied.
    /// </summary>
    /// <remarks>Its size matches <see cref="Region"/>, with the top-left corner at (0, 0).</remarks>
    public Rect LocalBounds => new(0f, 0f, _region.Width, _region.Height);

    /// <summary>
    /// Gets the sprite's axis-aligned bounding rectangle in its parent's space, after its transform is applied.
    /// </summary>
    /// <remarks>When the sprite is rotated, this is the smallest upright rectangle that contains it.</remarks>
    public Rect GlobalBounds => Transform.TransformRect(LocalBounds);

    /// <inheritdoc/>
    public void Draw(IRenderTarget target, RenderState state)
    {
        state = state with
        {
            Transform = state.Transform * Transform,
            Texture = _texture
        };

        target.Draw(_vertices, _indices, state);
    }

    private void UpdateVertices()
    {
        float width = _region.Width;
        float height = _region.Height;

        _vertices[0].Position = new PointF(0f, 0f);
        _vertices[1].Position = new PointF(width, 0f);
        _vertices[2].Position = new PointF(width, height);
        _vertices[3].Position = new PointF(0f, height);

        float left = (float)_region.X / _texture.Width;
        float top = (float)_region.Y / _texture.Height;
        float right = (float)(_region.X + _region.Width) / _texture.Width;
        float bottom = (float)(_region.Y + _region.Height) / _texture.Height;

        _vertices[0].TextureCoordinate = new PointF(left, top);
        _vertices[1].TextureCoordinate = new PointF(right, top);
        _vertices[2].TextureCoordinate = new PointF(right, bottom);
        _vertices[3].TextureCoordinate = new PointF(left, bottom);

        UpdateColors();
    }

    private void UpdateColors()
    {
        ColorF color = _color.ToColorF();

        _vertices[0].Color = color;
        _vertices[1].Color = color;
        _vertices[2].Color = color;
        _vertices[3].Color = color;
    }
}

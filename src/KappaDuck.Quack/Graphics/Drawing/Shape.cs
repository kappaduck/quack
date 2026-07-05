// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// The base class for drawable shapes defined by a set of points such as rectangles, circles and polygons.
/// </summary>
/// <remarks>
/// A shape has a solid <see cref="FillColor"/> and an optional outline (<see cref="OutlineColor"/> and
/// <see cref="OutlineThickness"/>), and may be textured. Derive from it by implementing <see cref="PointCount"/> and
/// <see cref="GetPoint(int)"/>, and call <see cref="Update"/> whenever those would return different values. The points
/// must describe a convex polygon.
/// </remarks>
public abstract class Shape : Transformable, IDrawable
{
    private Vertex[] _shape = [];
    private int[] _indices = [];

    private Vertex[] _outline = [];
    private int[] _outlineIndices = [];

    private Color _color = Colors.White;
    private Color _outlineColor = Colors.White;
    private RectI _region;

    private Rect _insideBounds;
    private Rect _bounds;

    /// <summary>
    /// Gets or sets the color that fills the interior of the shape. Defaults to <see cref="Colors.White"/>.
    /// </summary>
    public Color FillColor
    {
        get => _color;
        set
        {
            _color = value;
            UpdateFillColors();
        }
    }

    /// <summary>
    /// Gets or sets the color of the shape's outline. Defaults to <see cref="Colors.White"/>.
    /// </summary>
    /// <remarks>The outline is only visible when <see cref="OutlineThickness"/> is non-zero.</remarks>
    public Color OutlineColor
    {
        get => _outlineColor;
        set
        {
            _outlineColor = value;
            UpdateOutlineColors();
        }
    }

    /// <summary>
    /// Gets or sets the thickness of the shape's outline, in local units. Defaults to 0 (no outline).
    /// </summary>
    /// <remarks>A positive thickness grows the outline outwards, a negative thickness grows it inwards.</remarks>
    public float OutlineThickness
    {
        get;
        set
        {
            field = value;
            Update();
        }
    }

    /// <summary>
    /// Gets or sets the texture drawn inside the shape, or <see langword="null"/> for a solid fill.
    /// </summary>
    /// <remarks>The <see cref="Region"/> is mapped across the shape's local bounds. The outline is never textured.</remarks>
    public Texture? Texture
    {
        get;
        set
        {
            field = value;
            UpdateTexCoords();
        }
    }

    /// <summary>
    /// Gets or sets the region of the <see cref="Texture"/> mapped across the shape, in texture pixels.
    /// </summary>
    public RectI Region
    {
        get => _region;
        set
        {
            _region = value;
            UpdateTexCoords();
        }
    }

    /// <summary>
    /// Gets the number of points that define the shape.
    /// </summary>
    public abstract int PointCount { get; }

    /// <summary>
    /// Gets the shape's bounding rectangle in its own local space, before its transform is applied.
    /// </summary>
    /// <remarks>The bounds include the outline.</remarks>
    public Rect LocalBounds => _bounds;

    /// <summary>
    /// Gets the shape's axis-aligned bounding rectangle in its parent's space, after its transform is applied.
    /// </summary>
    public Rect GlobalBounds => Transform.TransformRect(_bounds);

    /// <summary>
    /// Gets the position of a point of the shape, in local coordinates.
    /// </summary>
    /// <param name="index">The index of the point, from 0 to <see cref="PointCount"/> minus one.</param>
    /// <returns>The point at <paramref name="index"/>.</returns>
    public abstract PointF GetPoint(int index);

    /// <inheritdoc/>
    public void Draw(IRenderTarget target, RenderState state)
    {
        state = state with { Transform = state.Transform * Transform };

        if (_shape.Length > 0)
            target.Draw(_shape, _indices, state with { Texture = Texture });

        if (_outline.Length > 0)
            target.Draw(_outline, _outlineIndices, state with { Texture = null });
    }

    /// <summary>
    /// Rebuilds the shape's geometry from its points.
    /// </summary>
    /// <remarks>Call this from a derived class whenever <see cref="PointCount"/> or <see cref="GetPoint(int)"/> would return different values.</remarks>
    protected void Update()
    {
        if (PointCount < 3)
        {
            _shape = [];
            _indices = [];
            _outline = [];
            _outlineIndices = [];
            _insideBounds = default;
            _bounds = default;

            return;
        }

        _shape = new Vertex[PointCount + 1];

        for (int i = 0; i < PointCount; i++)
            _shape[i + 1].Position = GetPoint(i);

        _insideBounds = ComputeBounds(_shape, start: 1, PointCount);
        _shape[0].Position = new PointF(_insideBounds.X + (_insideBounds.Width / 2f), _insideBounds.Y + (_insideBounds.Height / 2f));

        _indices = new int[PointCount * 3];

        for (int i = 0; i < PointCount; i++)
        {
            _indices[(i * 3) + 0] = 0;
            _indices[(i * 3) + 1] = i + 1;
            _indices[(i * 3) + 2] = ((i + 1) % PointCount) + 1;
        }

        UpdateFillColors();
        UpdateTexCoords();
        UpdateOutline();

        _bounds = _outline.Length > 0 ? ComputeBounds(_outline, start: 0, _outline.Length) : _insideBounds;
    }

    private void UpdateFillColors()
    {
        ColorF color = _color.ToColorF();

        for (int i = 0; i < _shape.Length; i++)
            _shape[i].Color = color;
    }

    private void UpdateTexCoords()
    {
        if (Texture is null || _shape.Length == 0)
            return;

        float textureWidth = Texture.Width;
        float textureHeight = Texture.Height;

        for (int i = 0; i < _shape.Length; i++)
        {
            float ratioX = _insideBounds.Width > 0f ? (_shape[i].Position.X - _insideBounds.X) / _insideBounds.Width : 0f;
            float ratioY = _insideBounds.Height > 0f ? (_shape[i].Position.Y - _insideBounds.Y) / _insideBounds.Height : 0f;

            float u = (_region.X + (_region.Width * ratioX)) / textureWidth;
            float v = (_region.Y + (_region.Height * ratioY)) / textureHeight;

            _shape[i].TextureCoordinate = new PointF(u, v);
        }
    }

    private void UpdateOutline()
    {
        if (MathF.ApproximatelyZero(OutlineThickness))
        {
            _outline = [];
            _outlineIndices = [];

            return;
        }

        int count = PointCount;
        PointF center = _shape[0].Position;

        _outline = new Vertex[(count + 1) * 2];

        for (int i = 0; i < count; i++)
        {
            PointF p1 = _shape[i + 1].Position;
            PointF p0 = _shape[((i - 1 + count) % count) + 1].Position;
            PointF p2 = _shape[((i + 1) % count) + 1].Position;

            Vector2 n1 = Normal(p0, p1);
            Vector2 n2 = Normal(p1, p2);

            if (Vector2.Dot(n1, center - p1) > 0f)
                n1 = -n1;

            if (Vector2.Dot(n2, center - p1) > 0f)
                n2 = -n2;

            float factor = 1f + Vector2.Dot(n1, n2);
            Vector2 normal = !MathF.ApproximatelyZero(factor) ? (n1 + n2) / factor : n1;

            _outline[(i * 2) + 0].Position = p1;
            _outline[(i * 2) + 1].Position = p1 + (normal * OutlineThickness);
        }

        _outline[(count * 2) + 0].Position = _outline[0].Position;
        _outline[(count * 2) + 1].Position = _outline[1].Position;

        _outlineIndices = new int[(_outline.Length - 2) * 3];

        for (int i = 0; i < _outline.Length - 2; i++)
        {
            _outlineIndices[(i * 3) + 0] = i;
            _outlineIndices[(i * 3) + 1] = i + 1;
            _outlineIndices[(i * 3) + 2] = i + 2;
        }

        UpdateOutlineColors();
    }

    private void UpdateOutlineColors()
    {
        ColorF color = _outlineColor.ToColorF();

        for (int i = 0; i < _outline.Length; i++)
            _outline[i].Color = color;
    }

    private static Vector2 Normal(PointF from, PointF to)
    {
        Vector2 edge = to - from;
        Vector2 normal = new(-edge.Y, edge.X);

        float magnitude = normal.Magnitude;
        return !MathF.ApproximatelyZero(magnitude) ? normal / magnitude : normal;
    }

    private static Rect ComputeBounds(Vertex[] vertices, int start, int count)
    {
        if (count == 0)
            return default;

        PointF first = vertices[start].Position;
        float minX = first.X, minY = first.Y, maxX = first.X, maxY = first.Y;

        for (int i = start + 1; i < start + count; i++)
        {
            PointF point = vertices[i].Position;

            if (point.X < minX)
                minX = point.X;

            if (point.X > maxX)
                maxX = point.X;

            if (point.Y < minY)
                minY = point.Y;

            if (point.Y > maxY)
                maxY = point.Y;
        }

        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }
}

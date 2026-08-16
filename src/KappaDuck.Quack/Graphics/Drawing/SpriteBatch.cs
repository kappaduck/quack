// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// Collects many sprites and draws them in as few render calls as possible.
/// </summary>
/// <remarks>
/// <para>
/// Each sprite's transform, region and tint are baked into its vertices as it is added, and consecutive sprites that
/// share a texture are merged into a single draw call. Because one draw call uses a single texture, changing texture
/// starts a new call, so adding sprites grouped by texture yields the fewest calls. The order sprites are added in is
/// preserved, so overlapping sprites layer correctly.
/// </para>
/// <para>
/// Because it is drawn like any drawable, a render state passed when drawing (for example a camera transform) applies to the whole batch.
/// </para>
/// </remarks>
public sealed class SpriteBatch : IDrawable
{
    private const int VerticesPerSprite = 6;

    private readonly List<Run> _runs = [];
    private Vertex[] _vertices;
    private int _count;

    /// <summary>
    /// Creates an empty sprite batch.
    /// </summary>
    /// <param name="capacity">The number of sprites to reserve space for up front.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is negative.</exception>
    public SpriteBatch(int capacity = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
        _vertices = capacity > 0 ? new Vertex[capacity * VerticesPerSprite] : [];
    }

    /// <summary>
    /// Gets the number of sprites currently in the batch.
    /// </summary>
    public int Count => _count / VerticesPerSprite;

    /// <summary>
    /// Adds a sprite to the batch in its current state.
    /// </summary>
    /// <remarks>The sprite's transform, region and tint are captured now; later changes to the sprite do not affect what was added.</remarks>
    /// <param name="sprite">The sprite to add.</param>
    public void Add(Sprite sprite)
    {
        Texture texture = sprite.Texture;

        if (_runs.Count == 0 || _runs[^1].Texture != texture)
            _runs.Add(new Run(texture, _count, 0));

        Append(sprite);

        Run run = _runs[^1];
        _runs[^1] = run with { Count = run.Count + VerticesPerSprite };
    }

    /// <summary>
    /// Removes every sprite from the batch, keeping its capacity for reuse.
    /// </summary>
    public void Clear()
    {
        _count = 0;
        _runs.Clear();
    }

    /// <inheritdoc/>
    public void Draw(IRenderTarget target, RenderState state)
    {
        ReadOnlySpan<Vertex> vertices = _vertices.AsSpan(0, _count);

        foreach (Run run in _runs)
            target.Draw(vertices.Slice(run.Start, run.Count), state with { Texture = run.Texture });
    }

    private void Append(Sprite sprite)
    {
        Transform transform = sprite.Transform;
        RectI region = sprite.Region;
        Texture texture = sprite.Texture;
        ColorF color = sprite.Color.ToColorF();

        float width = region.Width;
        float height = region.Height;

        float left = (float)region.X / texture.Width;
        float top = (float)region.Y / texture.Height;
        float right = (float)(region.X + region.Width) / texture.Width;
        float bottom = (float)(region.Y + region.Height) / texture.Height;

        Vertex topLeft = new(transform.TransformPoint(new PointF(0f, 0f)), color, new PointF(left, top));
        Vertex topRight = new(transform.TransformPoint(new PointF(width, 0f)), color, new PointF(right, top));
        Vertex bottomRight = new(transform.TransformPoint(new PointF(width, height)), color, new PointF(right, bottom));
        Vertex bottomLeft = new(transform.TransformPoint(new PointF(0f, height)), color, new PointF(left, bottom));

        if (_count + VerticesPerSprite > _vertices.Length)
            Array.Resize(ref _vertices, int.Max(_count + VerticesPerSprite, _vertices.Length == 0 ? 4 * VerticesPerSprite : _vertices.Length * 2));

        _vertices[_count++] = topLeft;
        _vertices[_count++] = topRight;
        _vertices[_count++] = bottomRight;
        _vertices[_count++] = topLeft;
        _vertices[_count++] = bottomRight;
        _vertices[_count++] = bottomLeft;
    }

    private readonly record struct Run(Texture Texture, int Start, int Count);
}

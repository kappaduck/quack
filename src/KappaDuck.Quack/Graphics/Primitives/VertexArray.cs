// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Video.Pixels;
using System.Collections;
using System.Diagnostics;

namespace KappaDuck.Quack.Graphics.Primitives;

/// <summary>
/// A growable, drawable set of vertices assembled into triangles and drawn in a single call.
/// </summary>
/// <remarks>
/// Use it to batch many primitives — tile maps, particle systems, custom meshes — into one draw for performance.
/// Choose how the vertices are assembled with <see cref="PrimitiveType"/>, and set an optional <see cref="Texture"/>
/// to texture the whole batch. Unlike a shape, a vertex array has no transform of its own; its vertices are in the
/// coordinate space you give them, and any transform comes from the <see cref="RenderState"/> it is drawn with.
/// </remarks>
[DebuggerDisplay("Count = {Count}")]
[CollectionBuilder(typeof(VertexArray), nameof(Create))]
public sealed class VertexArray : IDrawable, IEnumerable<Vertex>
{
    private Vertex[] _vertices;
    private int[] _indices = [];
    private int _indexCount;
    private bool _indicesDirty = true;
    private PrimitiveType _primitiveType;

    /// <summary>
    /// Creates an empty vertex array.
    /// </summary>
    /// <param name="primitiveType">How the vertices are assembled into triangles.</param>
    /// <param name="capacity">The number of vertices to reserve space for up front.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is negative.</exception>
    public VertexArray(PrimitiveType primitiveType = PrimitiveType.Triangles, int capacity = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _primitiveType = primitiveType;
        _vertices = capacity > 0 ? new Vertex[capacity] : [];
    }

    internal VertexArray(PrimitiveType primitiveType, int capacity, ReadOnlySpan<Vertex> vertices) : this(primitiveType, capacity)
    {
        Count = vertices.Length;
        vertices.CopyTo(_vertices);
    }

    /// <summary>
    /// Gets or sets how the vertices are assembled into triangles.
    /// </summary>
    public PrimitiveType PrimitiveType
    {
        get => _primitiveType;
        set
        {
            _primitiveType = value;
            _indicesDirty = true;
        }
    }

    /// <summary>
    /// Gets or sets the texture applied to the whole batch, or <see langword="null"/> to draw colored geometry without a texture.
    /// </summary>
    public Texture? Texture { get; set; }

    /// <summary>
    /// Gets the number of vertices in the array.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Gets a reference to the vertex at the given index, which can be read or modified in place.
    /// </summary>
    /// <param name="index">The index of the vertex, from 0 to <see cref="Count"/> minus one.</param>
    /// <returns>A reference to the vertex.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is out of range.</exception>
    public ref Vertex this[int index]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);

            return ref _vertices[index];
        }
    }

    /// <summary>
    /// Returns the vertices as a span, for reading or bulk in-place modification.
    /// </summary>
    /// <remarks>
    /// The span is only valid until the next operation that changes the number of vertices.
    /// </remarks>
    /// <returns>A span over the current vertices.</returns>
    public Span<Vertex> AsSpan() => _vertices.AsSpan(0, Count);

    /// <summary>
    /// Appends a vertex to the end of the array.
    /// </summary>
    /// <param name="vertex">The vertex to append.</param>
    public void Add(Vertex vertex)
    {
        if (Count == _vertices.Length)
            Array.Resize(ref _vertices, _vertices.Length == 0 ? 4 : _vertices.Length * 2);

        _vertices[Count++] = vertex;
        _indicesDirty = true;
    }

    /// <summary>
    /// Appends a quad, as two triangles, covering a destination rectangle.
    /// </summary>
    /// <remarks>
    /// Intended for a <see cref="PrimitiveType.Triangles"/> array; each call adds six vertices.
    /// </remarks>
    /// <param name="destination">The rectangle the quad covers, in render coordinates.</param>
    /// <param name="color">The color of the quad.</param>
    public void AddQuad(Rect destination, Color color) => AddQuad(destination, new Rect(0f, 0f, 1f, 1f), color);

    /// <summary>
    /// Appends a textured quad, as two triangles, covering a destination rectangle.
    /// </summary>
    /// <remarks>
    /// Intended for a <see cref="PrimitiveType.Triangles"/> array; each call adds six vertices.
    /// </remarks>
    /// <param name="destination">The rectangle the quad covers, in render coordinates.</param>
    /// <param name="textureRegion">The region of the texture mapped onto the quad, in normalized coordinates where (0, 0) is the top-left and (1, 1) is the bottom-right.</param>
    /// <param name="color">The color multiplied into the quad.</param>
    public void AddQuad(Rect destination, Rect textureRegion, Color color)
    {
        ColorF tint = color.ToColorF();

        Vertex topLeft = new(destination.TopLeft, tint, textureRegion.TopLeft);
        Vertex topRight = new(destination.TopRight, tint, textureRegion.TopRight);
        Vertex bottomRight = new(destination.BottomRight, tint, textureRegion.BottomRight);
        Vertex bottomLeft = new(destination.BottomLeft, tint, textureRegion.BottomLeft);

        AddRange(topLeft, topRight, bottomRight, topLeft, bottomRight, bottomLeft);
    }

    /// <summary>
    /// Appends several vertices to the end of the array.
    /// </summary>
    /// <param name="vertices">The vertices to append.</param>
    public void AddRange(params ReadOnlySpan<Vertex> vertices)
    {
        int required = Count + vertices.Length;

        if (required > _vertices.Length)
            Array.Resize(ref _vertices, int.Max(required, _vertices.Length * 2));

        vertices.CopyTo(_vertices.AsSpan(Count));

        Count = required;
        _indicesDirty = true;
    }

    /// <summary>
    /// Creates a vertex array assembled as <see cref="PrimitiveType.Triangles"/> from the given vertices.
    /// </summary>
    /// <param name="vertices">The initial vertices.</param>
    /// <returns>A new vertex array containing the vertices.</returns>
    public static VertexArray Create(ReadOnlySpan<Vertex> vertices) => Create(PrimitiveType.Triangles, vertices);

    /// <summary>
    /// Creates a vertex array with the given primitive type from the given vertices.
    /// </summary>
    /// <param name="primitiveType">How the vertices are assembled into triangles.</param>
    /// <param name="vertices">The initial vertices.</param>
    /// <returns>A new vertex array containing the vertices.</returns>
    public static VertexArray Create(PrimitiveType primitiveType, ReadOnlySpan<Vertex> vertices)
        => Create(primitiveType, vertices.Length, vertices);

    /// <summary>
    /// Creates a vertex array with the given primitive type and reserved capacity from the given vertices.
    /// </summary>
    /// <param name="primitiveType">How the vertices are assembled into triangles.</param>
    /// <param name="capacity">The number of vertices to reserve space for up front.</param>
    /// <param name="vertices">The initial vertices.</param>
    /// <returns>A new vertex array containing the vertices.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is negative.</exception>
    public static VertexArray Create(PrimitiveType primitiveType, int capacity, ReadOnlySpan<Vertex> vertices)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
        return new(primitiveType, int.Max(capacity, vertices.Length), vertices);
    }

    /// <summary>
    /// Removes all vertices from the array while keeping its capacity.
    /// </summary>
    public void Clear()
    {
        Count = 0;
        _indicesDirty = true;
    }

    /// <inheritdoc/>
    public void Draw(IRenderTarget target, RenderState state)
    {
        if (Count < 3)
            return;

        ReadOnlySpan<Vertex> vertices = _vertices.AsSpan(0, Count);

        if (Texture is not null)
            state = state with { Texture = Texture };

        if (_primitiveType == PrimitiveType.Triangles)
        {
            target.Draw(vertices, state);
            return;
        }

        EnsureIndices();
        target.Draw(vertices, _indices.AsSpan(0, _indexCount), state);
    }

    /// <summary>
    /// Returns an enumerator that iterates over the vertices.
    /// </summary>
    /// <returns>An enumerator over the vertices.</returns>
    public IEnumerator<Vertex> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
            yield return _vertices[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Resizes the array, appending zeroed vertices or dropping trailing ones as needed.
    /// </summary>
    /// <param name="count">The new number of vertices.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
    public void Resize(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        if (count > _vertices.Length)
            Array.Resize(ref _vertices, count);

        if (count > Count)
            Array.Clear(_vertices, Count, count - Count);
        else if (count < Count)
            Array.Clear(_vertices, count, Count - count);

        Count = count;
        _indicesDirty = true;
    }

    private void EnsureIndices()
    {
        if (!_indicesDirty)
            return;

        int triangles = Count - 2;
        int required = triangles * 3;

        if (_indices.Length < required)
            _indices = new int[required];

        if (_primitiveType == PrimitiveType.TriangleStrip)
        {
            for (int i = 0; i < triangles; i++)
            {
                _indices[(i * 3) + 0] = i;
                _indices[(i * 3) + 1] = i + 1;
                _indices[(i * 3) + 2] = i + 2;
            }
        }
        else
        {
            for (int i = 0; i < triangles; i++)
            {
                _indices[(i * 3) + 0] = 0;
                _indices[(i * 3) + 1] = i + 1;
                _indices[(i * 3) + 2] = i + 2;
            }
        }

        _indexCount = required;
        _indicesDirty = false;
    }
}

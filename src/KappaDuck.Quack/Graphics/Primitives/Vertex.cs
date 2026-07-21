// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Graphics.Primitives;

/// <summary>
/// A single vertex used when drawing arbitrary geometry.
/// </summary>
/// <param name="position">The position of the vertex, in render coordinates.</param>
/// <param name="color">The color of the vertex.</param>
/// <param name="textureCoordinate">The normalized texture coordinate, where (0, 0) is the top-left and (1, 1) is the bottom-right of the texture. Ignored when drawing a geometry without a texture.</param>
[StructLayout(LayoutKind.Sequential)]
public struct Vertex(PointF position, ColorF color, PointF textureCoordinate) : IEquatable<Vertex>
{
    /// <summary>
    /// Creates a vertex from a position, an 8-bit-per-channel color and a texture coordinate.
    /// </summary>
    /// <param name="position">The position of the vertex, in render coordinates.</param>
    /// <param name="color">The color of the vertex, converted to floating-point channels.</param>
    /// <param name="textureCoordinate">
    /// The normalized texture coordinate, where (0, 0) is the top-left and (1, 1) is the bottom-right of the texture.
    /// Ignored when drawing a geometry without a texture.
    /// </param>
    public Vertex(PointF position, Color color, PointF textureCoordinate) : this(position, color.ToColorF(), textureCoordinate)
    {
    }

    /// <summary>
    /// Creates a vertex with a position and color and a zeroed texture coordinate.
    /// </summary>
    /// <param name="position">The position of the vertex, in render coordinates.</param>
    /// <param name="color">The color of the vertex.</param>
    public Vertex(PointF position, ColorF color) : this(position, color, default)
    {
    }

    /// <summary>
    /// Creates a vertex from a position and an 8-bit-per-channel color, with a zeroed texture coordinate.
    /// </summary>
    /// <param name="position">The position of the vertex, in render coordinates.</param>
    /// <param name="color">The color of the vertex, converted to floating-point channels.</param>
    public Vertex(PointF position, Color color) : this(position, color.ToColorF(), default)
    {
    }

    /// <summary>
    /// Creates a vertex with a color, at the origin and a zeroed texture coordinate.
    /// </summary>
    /// <param name="color">The color of the vertex.</param>
    public Vertex(ColorF color) : this(PointF.Origin, color, default)
    {
    }

    /// <summary>
    /// Creates a vertex with a color, at the origin and a zeroed texture coordinate.
    /// </summary>
    /// <param name="color">The color of the vertex, converted to floating-point channels.</param>
    public Vertex(Color color) : this(PointF.Origin, color.ToColorF(), default)
    {
    }

    /// <summary>
    /// Gets or sets the position of the vertex, in render coordinates.
    /// </summary>
    public PointF Position { get; set; } = position;

    /// <summary>
    /// Gets or sets the color of the vertex.
    /// </summary>
    public ColorF Color { get; set; } = color;

    /// <summary>
    /// Gets or sets the normalized texture coordinate, where (0, 0) is the top-left and (1, 1) is the bottom-right of the texture.
    /// </summary>
    /// <remarks>Ignored when drawing a geometry without a texture.</remarks>
    public PointF TextureCoordinate { get; set; } = textureCoordinate;

    /// <summary>
    /// Determines whether this vertex is equal to another vertex.
    /// </summary>
    /// <param name="other">The vertex to compare with the current vertex.</param>
    /// <returns><see langword="true"/> if the vertices are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vertex other)
        => Position.Equals(other.Position) && Color.Equals(other.Color) && TextureCoordinate.Equals(other.TextureCoordinate);

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Vertex other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(Position, Color, TextureCoordinate);

    /// <summary>
    /// Determines whether two vertices are equal.
    /// </summary>
    /// <param name="left">The left vertex.</param>
    /// <param name="right">The right vertex.</param>
    /// <returns><see langword="true"/> if the vertices are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vertex left, Vertex right) => left.Equals(right);

    /// <summary>
    /// Determines whether two vertices are not equal.
    /// </summary>
    /// <param name="left">The left vertex.</param>
    /// <param name="right">The right vertex.</param>
    /// <returns><see langword="true"/> if the vertices are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vertex left, Vertex right) => !(left == right);
}

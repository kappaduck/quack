// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Graphics.Primitives;

/// <summary>
/// A single vertex for textured or colored triangle drawing: a position, a color, and a
/// texture coordinate.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Vertex : IEquatable<Vertex>
{
    /// <summary>
    /// Gets the vertex position, in the target's coordinate space.
    /// </summary>
    public PointF Position { get; }

    /// <summary>
    /// Gets the vertex color, modulated with the texture when one is bound.
    /// </summary>
    public ColorF Color { get; }

    /// <summary>
    /// Gets the normalized texture coordinate, where <c>(0, 0)</c> is the top-left of the texture
    /// and <c>(1, 1)</c> the bottom-right. Ignored when no texture is bound.
    /// </summary>
    public PointF TexCoord { get; }

    /// <summary>
    /// Creates a vertex with a position, color, and texture coordinate.
    /// </summary>
    /// <param name="position">The position, in the target's coordinate space.</param>
    /// <param name="color">The color, modulated with the bound texture.</param>
    /// <param name="texCoord">The normalized texture coordinate.</param>
    public Vertex(PointF position, Color color, PointF texCoord)
    {
        Position = position;
        Color = color.ToColorF();
        TexCoord = texCoord;
    }

    /// <summary>
    /// Creates a vertex directly from a float color, without byte conversion.
    /// </summary>
    /// <param name="position">The position, in the target's coordinate space.</param>
    /// <param name="color">The color.</param>
    /// <param name="texCoord">The normalized texture coordinate.</param>
    public Vertex(PointF position, ColorF color, PointF texCoord)
    {
        Position = position;
        Color = color;
        TexCoord = texCoord;
    }

    /// <summary>
    /// Creates a solid-colored vertex with no meaningful texture coordinate.
    /// </summary>
    /// <param name="position">The position, in the target's coordinate space.</param>
    /// <param name="color">The color.</param>
    public Vertex(PointF position, Color color) : this(position, color, default)
    {
    }

    /// <inheritdoc/>
    public bool Equals(Vertex other) => Position == other.Position && Color == other.Color && TexCoord == other.TexCoord;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Vertex v && Equals(v);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Position, Color, TexCoord);

    /// <summary>
    /// Determines whether two vertices are equal.
    /// </summary>
    /// <param name="left">The left vertex.</param>
    /// <param name="right">The right vertex.</param>
    /// <returns><see langword="true"/> if the vertices are equals otherwise <see langword="false"/>.</returns>
    public static bool operator ==(Vertex left, Vertex right) => left.Equals(right);

    /// <summary>
    /// Determines whether two vertices differ.
    /// </summary>
    /// <param name="left">The left vertex.</param>
    /// <param name="right">The right vertex.</param>
    /// <returns><see langword="true"/> if the vertices are not equals otherwise <see langword="false"/>.</returns>
    public static bool operator !=(Vertex left, Vertex right) => !(left == right);
}

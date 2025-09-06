// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Native;
using System.Drawing;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Graphics.Primitives;

/// <summary>
/// Represents a vertex in 2D space.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Vertex
{
    /// <summary>
    /// Creates a vertex with the specified position and color.
    /// </summary>
    /// <param name="position">The position of the vertex.</param>
    /// <param name="color">The color of the vertex.</param>
    public Vertex(Vector2 position, Color color)
    {
        Position = position;
        Color = color;
    }

    /// <summary>
    /// Creates a vertex with the specified position and color.
    /// </summary>
    /// <param name="x">The x-coordinate of the vertex.</param>
    /// <param name="y">The y-coordinate of the vertex.</param>
    /// <param name="color">The color of the vertex.</param>
    public Vertex(float x, float y, Color color) : this(new Vector2(x, y), color)
    {
    }

    /// <summary>
    /// Creates a vertex with the specified position.
    /// </summary>
    /// <param name="x">The x-coordinate of the vertex.</param>
    /// <param name="y">The y-coordinate of the vertex.</param>
    public Vertex(float x, float y) : this(new Vector2(x, y), Color.White)
    {
    }

    /// <summary>
    /// Creates a vertex with the specified position.
    /// </summary>
    /// <param name="position">The position of the vertex.</param>
    public Vertex(Vector2 position) : this(position, Color.White)
    {
    }

    /// <summary>
    /// Creates a vertex with the specified color.
    /// </summary>
    /// <param name="color">The color of the vertex.</param>
    public Vertex(Color color) : this(new Vector2(0, 0), color)
    {
    }

    /// <summary>
    /// Gets or sets the position of the vertex.
    /// </summary>
    public Vector2 Position;

    private SDL_FColor _color;
    private Vector2 _texCoord;

    /// <summary>
    /// Gets or sets the color of the vertex.
    /// </summary>
    public Color Color
    {
        readonly get => _color.Color;
        set => _color = new SDL_FColor(value.R, value.G, value.B, value.A);
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Graphics.Primitives;

/// <summary>
/// Describes how a sequence of vertices is assembled into triangles.
/// </summary>
public enum PrimitiveType
{
    /// <summary>
    /// Every three vertices form one independent triangle.
    /// </summary>
    Triangles = 0,

    /// <summary>
    /// Each vertex after the first two forms a triangle with the previous two vertices, sharing an edge with the last triangle.
    /// </summary>
    TriangleStrip = 1,

    /// <summary>
    /// Each vertex after the first two forms a triangle with the first vertex and the previous one, like the slices of a pie.
    /// </summary>
    TriangleFan = 2
}

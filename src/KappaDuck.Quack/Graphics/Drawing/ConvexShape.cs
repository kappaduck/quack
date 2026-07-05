// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A convex polygon shape defined by an arbitrary set of points.
/// </summary>
/// <remarks>The points must form a convex polygon; concave shapes will not fill correctly.</remarks>
public sealed class ConvexShape : Shape
{
    private PointF[] _points;

    /// <summary>
    /// Creates a convex shape with the given number of points, all at the origin.
    /// </summary>
    /// <param name="pointCount">The number of points. Set each one with <see cref="SetPoint(int, PointF)"/>.</param>
    public ConvexShape(int pointCount)
    {
        _points = new PointF[pointCount];
        Update();
    }

    /// <summary>
    /// Creates a convex shape from the given points.
    /// </summary>
    /// <param name="points">The points of the convex polygon, in local coordinates.</param>
    public ConvexShape(ReadOnlySpan<PointF> points)
    {
        _points = points.ToArray();
        Update();
    }

    /// <inheritdoc/>
    public override int PointCount => _points.Length;

    /// <inheritdoc/>
    public override PointF GetPoint(int index) => _points[index];

    /// <summary>
    /// Sets the position of a single point of the shape.
    /// </summary>
    /// <param name="index">The index of the point, from 0 to <see cref="PointCount"/> minus one.</param>
    /// <param name="point">The new position of the point, in local coordinates.</param>
    public void SetPoint(int index, PointF point)
    {
        _points[index] = point;
        Update();
    }

    /// <summary>
    /// Replaces all points of the shape.
    /// </summary>
    /// <param name="points">The new points of the convex polygon, in local coordinates.</param>
    public void SetPoints(ReadOnlySpan<PointF> points)
    {
        _points = points.ToArray();
        Update();
    }
}

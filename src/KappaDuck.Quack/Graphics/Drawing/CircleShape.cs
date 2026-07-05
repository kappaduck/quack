// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A circle shape, approximated by a configurable number of points.
/// </summary>
public sealed class CircleShape : Shape
{
    private float _radius;
    private int _pointCount;

    /// <summary>
    /// Creates a circle of the given radius.
    /// </summary>
    /// <param name="radius">The radius of the circle, in local units.</param>
    /// <param name="pointCount">The number of points used to approximate the circle; more points give a smoother circle.</param>
    public CircleShape(float radius, int pointCount = 30)
    {
        _radius = radius;
        _pointCount = pointCount;
        Update();
    }

    /// <summary>
    /// Gets or sets the radius of the circle, in local units.
    /// </summary>
    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            Update();
        }
    }

    /// <inheritdoc/>
    public override int PointCount => _pointCount;

    /// <summary>
    /// Sets the number of points used to approximate the circle.
    /// </summary>
    /// <param name="count">The number of points; more points give a smoother circle.</param>
    public void SetPointCount(int count)
    {
        _pointCount = count;
        Update();
    }

    /// <inheritdoc/>
    public override PointF GetPoint(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _pointCount);

        Angle angle = Angle.FromDegrees((index * 360f / _pointCount) - 90f);

        float x = _radius + (_radius * angle.Cos);
        float y = _radius + (_radius * angle.Sin);

        return new PointF(x, y);
    }
}

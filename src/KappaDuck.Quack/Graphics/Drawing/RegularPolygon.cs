// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A regular polygon shape whose points are evenly spaced around a circle.
/// </summary>
/// <remarks>Use it for triangles, pentagons, hexagons and so on; a high side count approximates a circle.</remarks>
public sealed class RegularPolygon : Shape
{
    private float _radius;
    private int _sideCount;

    /// <summary>
    /// Creates a regular polygon.
    /// </summary>
    /// <param name="radius">The distance from the center to each point, in local units.</param>
    /// <param name="sideCount">The number of sides. Must be at least 3.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="sideCount"/> is less than 3.</exception>
    public RegularPolygon(float radius, int sideCount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(sideCount, 3);

        _radius = radius;
        _sideCount = sideCount;

        Update();
    }

    /// <summary>
    /// Gets or sets the distance from the center to each point, in local units.
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

    /// <summary>
    /// Gets or sets the number of sides. Must be at least 3.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">The value is less than 3.</exception>
    public int SideCount
    {
        get => _sideCount;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, 3);

            _sideCount = value;
            Update();
        }
    }

    /// <inheritdoc/>
    public override int PointCount => _sideCount;

    /// <inheritdoc/>
    public override Point GetPoint(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _sideCount);

        Angle angle = Angle.FromDegrees((index * 360f / _sideCount) - 90f);

        float x = _radius + (_radius * angle.Cos);
        float y = _radius + (_radius * angle.Sin);

        return new Point(x, y);
    }
}

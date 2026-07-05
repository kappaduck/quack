// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A rectangle shape defined by its width and height.
/// </summary>
public sealed class RectangleShape : Shape
{
    private SizeF _size;

    /// <summary>
    /// Creates a rectangle of the given size.
    /// </summary>
    /// <param name="size">The width and height of the rectangle, in local units.</param>
    public RectangleShape(SizeF size)
    {
        _size = size;
        Update();
    }

    /// <summary>
    /// Creates a rectangle of the given position and size.
    /// </summary>
    /// <param name="position">The position of the rectangle, in local units.</param>
    /// <param name="size">The width and height of the rectangle, in local units.</param>
    public RectangleShape(PointF position, SizeF size) : this(size)
        => Position = position;

    /// <summary>
    /// Creates a rectangle of the given rect.
    /// </summary>
    /// <param name="rect">The rectangle, in local units.</param>
    public RectangleShape(Rect rect) : this(rect.Position, rect.Size)
    {
    }

    /// <summary>
    /// Gets or sets the width and height of the rectangle, in local units.
    /// </summary>
    public SizeF Size
    {
        get => _size;
        set
        {
            _size = value;
            Update();
        }
    }

    /// <inheritdoc/>
    public override int PointCount { get; } = 4;

    /// <inheritdoc/>
    public override PointF GetPoint(int index) => index switch
    {
        0 => new PointF(0f, 0f),
        1 => new PointF(_size.Width, 0f),
        2 => new PointF(_size.Width, _size.Height),
        3 => new PointF(0f, _size.Height),
        _ => throw new ArgumentOutOfRangeException(nameof(index))
    };
}

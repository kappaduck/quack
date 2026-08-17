// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a rectangle defined by integer coordinates and dimensions.
/// </summary>
/// <param name="x">The x-coordinate of the top-left corner of the rectangle.</param>
/// <param name="y">The y-coordinate of the top-left corner of the rectangle.</param>
/// <param name="width">The width of the rectangle.</param>
/// <param name="height">The height of the rectangle.</param>
[StructLayout(LayoutKind.Sequential)]
public struct RectI(int x, int y, int width, int height) : IEquatable<RectI>, IEqualityOperators<RectI, RectI, bool>, ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary>
    /// Creates an empty rectangle at the origin (0, 0).
    /// </summary>
    public RectI() : this(0, 0, 0, 0)
    {
    }

    /// <summary>
    /// Creates a rectangle from a position and size.
    /// </summary>
    /// <param name="position">The position of the top-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public RectI(PointI position, Size size) : this(position.X, position.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the top-left corner.
    /// </summary>
    /// <remarks>
    /// Updating the x-coordinate will adjust <see cref="Right"/>.
    /// </remarks>
    public int X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the top-left corner.
    /// </summary>
    /// <remarks>
    /// Updating the y-coordinate will adjust <see cref="Bottom"/>.
    /// </remarks>
    public int Y { get; set; } = y;

    /// <summary>
    /// Gets or sets the width measured from the x-coordinate.
    /// </summary>
    /// <remarks>
    /// Updating the width will adjust <see cref="Right"/>.
    /// </remarks>
    public int Width { get; set; } = width;

    /// <summary>
    /// Gets or sets the height measured from the y-coordinate.
    /// </summary>
    /// <remarks>
    /// Updating the height will adjust <see cref="Bottom"/>.
    /// </remarks>
    public int Height { get; set; } = height;

    /// <summary>
    /// Gets the area of the rectangle.
    /// </summary>
    public readonly int Area => Width * Height;

    /// <summary>
    /// Gets or sets the position of the top-left corner.
    /// </summary>
    public PointI Position
    {
        readonly get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    /// <summary>
    /// Gets or sets the size of the rectangle.
    /// </summary>
    public Size Size
    {
        readonly get => new(Width, Height);
        set
        {
            Width = value.Width;
            Height = value.Height;
        }
    }

    /// <summary>
    /// Gets the x-coordinate of the left side of the rectangle.
    /// </summary>
    /// <remarks>It's an alias for <see cref="X"/>.</remarks>
    public readonly int Left => X;

    /// <summary>
    /// Gets the y-coordinate of the top side of the rectangle.
    /// </summary>
    /// <remarks>It's an alias for <see cref="Y"/>.</remarks>
    public readonly int Top => Y;

    /// <summary>
    /// Gets the x-coordinate of the right side of the rectangle.
    /// </summary>
    public readonly int Right => X + Width;

    /// <summary>
    /// Gets the y-coordinate of the bottom side of the rectangle.
    /// </summary>
    public readonly int Bottom => Y + Height;

    /// <summary>
    /// Gets the center point of the rectangle.
    /// </summary>
    /// <remarks>
    /// Returns a <see cref="Point"/> because the true center of an integer rectangle is not always integer-valued.
    /// </remarks>
    public readonly Point Center => new(X + (Width / 2f), Y + (Height / 2f));

    /// <summary>
    /// Gets the top-left corner of the rectangle.
    /// </summary>
    public readonly PointI TopLeft => new(Left, Top);

    /// <summary>
    /// Gets the top-right corner of the rectangle.
    /// </summary>
    public readonly PointI TopRight => new(Right, Top);

    /// <summary>
    /// Gets the bottom-left corner of the rectangle.
    /// </summary>
    public readonly PointI BottomLeft => new(Left, Bottom);

    /// <summary>
    /// Gets the bottom-right corner of the rectangle.
    /// </summary>
    public readonly PointI BottomRight => new(Right, Bottom);

    /// <summary>
    /// Gets a value indicating whether the rectangle is empty (either dimension is zero).
    /// </summary>
    public readonly bool IsEmpty => Width == 0 || Height == 0;

    /// <summary>
    /// Gets a rectangle with all components set to zero.
    /// </summary>
    public static RectI Zero { get; } = new(0, 0, 0, 0);

    /// <summary>
    /// Determines whether the point is contained within the rectangle.
    /// </summary>
    /// <param name="point">The point to check.</param>
    /// <returns><see langword="true"/> if the point is within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(PointI point)
        => !IsEmpty && point.X >= X && point.X <= Right && point.Y >= Y && point.Y <= Bottom;

    /// <summary>
    /// Determines whether the rectangle is fully contained within this rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to check.</param>
    /// <returns><see langword="true"/> if the rectangle is fully within this rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(RectI rect)
        => !IsEmpty && !rect.IsEmpty && rect.X >= X && rect.Right <= Right && rect.Y >= Y && rect.Bottom <= Bottom;

    /// <summary>
    /// Determines whether all points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if all points are within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool ContainsAll(ReadOnlySpan<PointI> points)
    {
        if (IsEmpty || points.IsEmpty)
            return false;

        foreach (PointI point in points)
        {
            if (!Contains(point))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Determines whether all points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if all points are within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool ContainsAll(IEnumerable<PointI> points)
    {
        if (IsEmpty)
            return false;

        return points switch
        {
            PointI[] array => ContainsAll(array),
            List<PointI> list => ContainsAll(CollectionsMarshal.AsSpan(list)),
            _ => points.All(Contains)
        };
    }

    /// <summary>
    /// Determines whether any points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if any point is within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool ContainsAny(ReadOnlySpan<PointI> points)
    {
        if (IsEmpty || points.IsEmpty)
            return false;

        foreach (PointI point in points)
        {
            if (Contains(point))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether any points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if any point is within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool ContainsAny(IEnumerable<PointI> points)
    {
        if (IsEmpty)
            return false;

        return points switch
        {
            PointI[] array => ContainsAny(array),
            List<PointI> list => ContainsAny(CollectionsMarshal.AsSpan(list)),
            _ => points.Any(Contains)
        };
    }

    /// <summary>
    /// Deconstructs the rectangle into its components.
    /// </summary>
    /// <param name="x">The x-coordinate of the top-left corner of the rectangle.</param>
    /// <param name="y">The y-coordinate of the top-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    public readonly void Deconstruct(out int x, out int y, out int width, out int height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    /// <summary>
    /// Grows the rectangle to include the specified point.
    /// </summary>
    /// <remarks>
    /// If the rectangle already contains the point, no changes are made.
    /// </remarks>
    /// <param name="point">The point to encapsulate.</param>
    public void Encapsulate(PointI point)
    {
        if (Contains(point))
            return;

        if (IsEmpty)
        {
            X = point.X;
            Y = point.Y;
            Width = 0;
            Height = 0;

            return;
        }

        int left = Math.Min(X, point.X);
        int top = Math.Min(Y, point.Y);
        int right = Math.Max(Right, point.X);
        int bottom = Math.Max(Bottom, point.Y);

        X = left;
        Y = top;
        Width = right - left;
        Height = bottom - top;
    }

    /// <summary>
    /// Grows the rectangle to include the specified rectangle.
    /// </summary>
    /// <remarks>
    /// If the rectangle already contains the specified rectangle, no changes are made.
    /// </remarks>
    /// <param name="rect">The rectangle to encapsulate.</param>
    public void Encapsulate(RectI rect)
    {
        if (Contains(rect) || rect.IsEmpty)
            return;

        if (IsEmpty)
        {
            this = rect;
            return;
        }

        int left = Math.Min(X, rect.X);
        int top = Math.Min(Y, rect.Y);
        int right = Math.Max(Right, rect.Right);
        int bottom = Math.Max(Bottom, rect.Bottom);

        X = left;
        Y = top;
        Width = right - left;
        Height = bottom - top;
    }

    /// <summary>
    /// Determines whether this rectangle overlaps with another rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to check for overlap.</param>
    /// <returns><see langword="true"/> if the rectangles overlap; otherwise, <see langword="false"/>.</returns>
    public readonly bool Overlaps(RectI rect)
    {
        if (IsEmpty || rect.IsEmpty)
            return false;

        return !(Left >= rect.Right || Right <= rect.Left || Top >= rect.Bottom || Bottom <= rect.Top);
    }

    /// <summary>
    /// Iterates through all points within the rectangle,
    /// starting from the top-left corner and proceeding row by row to the bottom-right corner.
    /// </summary>
    /// <remarks>
    /// Iterates points where:
    /// <code>
    /// rect.X &lt;= X &lt; rect.Right
    /// rect.Y &lt;= Y &lt; rect.Bottom
    /// </code>
    /// An empty rectangle will enumerate no points.
    /// </remarks>
    /// <returns>An array of all integer points within the rectangle.</returns>
    public readonly PointI[] ToPoints()
    {
        if (IsEmpty)
            return [];

        PointI[] points = new PointI[Area];
        int index = 0;

        PointEnumerator enumerator = new(this);

        while (enumerator.MoveNext())
            points[index++] = enumerator.Current;

        return points;
    }

    /// <summary>
    /// Calculates the intersecting rectangle of two rectangles.
    /// </summary>
    /// <param name="left">The first rectangle.</param>
    /// <param name="right">The second rectangle.</param>
    /// <returns>The intersecting rectangle, or <see cref="Zero"/> if there is no intersection.</returns>
    public static RectI Intersect(RectI left, RectI right)
    {
        if (!left.Overlaps(right))
            return Zero;

        int x = Math.Max(left.X, right.X);
        int y = Math.Max(left.Y, right.Y);
        int width = Math.Min(left.Right, right.Right) - x;
        int height = Math.Min(left.Bottom, right.Bottom) - y;

        return new RectI(x, y, width, height);
    }

    /// <summary>
    /// Calculates the smallest rectangle that contains both specified rectangles.
    /// </summary>
    /// <param name="left">The first rectangle.</param>
    /// <param name="right">The second rectangle.</param>
    /// <returns>The smallest rectangle that contains both rectangles.</returns>
    public static RectI Union(RectI left, RectI right)
    {
        if (left.IsEmpty && right.IsEmpty)
            return Zero;

        if (left.IsEmpty)
            return right;

        if (right.IsEmpty)
            return left;

        int x = Math.Min(left.X, right.X);
        int y = Math.Min(left.Y, right.Y);
        int width = Math.Max(left.Right, right.Right) - x;
        int height = Math.Max(left.Bottom, right.Bottom) - y;

        return new RectI(x, y, width, height);
    }

    /// <inheritdoc/>
    public readonly bool Equals(RectI other)
    {
        if (IsEmpty && other.IsEmpty)
            return true;

        return X == other.X
            && Y == other.Y
            && Width == other.Width
            && Height == other.Height;
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RectI other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <summary>
    /// Converts the rect to a <see cref="Rect"/>.
    /// </summary>
    /// <returns>The converted rect.</returns>
    public readonly Rect ToRect() => new(X, Y, Width, Height);

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({X}, {Y}, {Width}, {Height})", out charsWritten);

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"({X}, {Y}, {Width}, {Height})", out bytesWritten);

    /// <summary>
    /// Determines whether two rectangles are equal.
    /// </summary>
    /// <param name="left">The left rectangle.</param>
    /// <param name="right">The right rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(RectI left, RectI right) => left.Equals(right);

    /// <summary>
    /// Determines whether two rectangles are not equal.
    /// </summary>
    /// <param name="left">The left rectangle.</param>
    /// <param name="right">The right rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(RectI left, RectI right) => !(left == right);
}

file struct PointEnumerator
{
    private readonly int _startX;
    private readonly int _endX;
    private readonly int _endY;

    private PointI _current;

    internal PointEnumerator(RectI rect)
    {
        _startX = rect.X;
        _endX = rect.Right;
        _endY = rect.Bottom;

        _current = new(_startX - 1, rect.Y);
    }

    public readonly PointI Current => _current;

    public bool MoveNext()
    {
        if (++_current.X >= _endX)
        {
            _current.X = _startX;
            _current.Y++;
        }

        return _current.Y < _endY;
    }
}

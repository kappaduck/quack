// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a rectangle with integer coordinates.
/// </summary>
/// <param name="x">The x-coordinate of the top-left corner of the rectangle.</param>
/// <param name="y">The y-coordinate of the top-left corner of the rectangle.</param>
/// <param name="width">The width of the rectangle.</param>
/// <param name="height">The height of the rectangle.</param>
[StructLayout(LayoutKind.Sequential)]
public struct RectInt(int x, int y, int width, int height) : IEquatable<RectInt>, ISpanFormattable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RectInt"/> with all values set to zero.
    /// </summary>
    public RectInt() : this(0, 0, 0, 0)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RectInt"/> with the specified position and size.
    /// </summary>
    /// <param name="position">The position of the top-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public RectInt(Vector2Int position, SizeInt size) : this(position.X, position.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the top-left corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxX"/>.
    /// </remarks>
    public int X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxY"/>.
    /// </remarks>
    public int Y { get; set; } = y;

    /// <summary>
    /// Gets or sets the width of the rectangle, measured from the <see cref="X"/> position.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxX"/>.
    /// </remarks>
    public int Width { get; set; } = width;

    /// <summary>
    /// Gets or sets the height of the rectangle, measured from the <see cref="Y"/> position.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxY"/>.
    /// </remarks>
    public int Height { get; set; } = height;

    /// <summary>
    /// Gets the x-coordinate of the right side of the rectangle.
    /// </summary>
    public readonly int MaxX => X + Width;

    /// <summary>
    /// Gets the y-coordinate of the bottom side of the rectangle.
    /// </summary>
    public readonly int MaxY => Y + Height;

    /// <summary>
    /// Gets or sets the position of the center of the rectangle.
    /// </summary>
    public Vector2Int Center
    {
        readonly get => new(X + (Width / 2), Y + (Height / 2));
        set
        {
            X = value.X - (Width / 2);
            Y = value.Y - (Height / 2);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the rectangle is empty.
    /// </summary>
    public readonly bool IsEmpty => Width <= 0 || Height <= 0;

    /// <summary>
    /// Gets or sets the bottom-left corner of the rectangle.
    /// </summary>
    public Vector2Int BottomLeft
    {
        readonly get => new(X, Y);
        set
        {
            Width = MaxX - value.X;
            Height = MaxY - value.Y;
            X = value.X;
            Y = value.Y;
        }
    }

    /// <summary>
    /// Gets or sets the top-right corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will resize the rectangle, changing <see cref="Size"/> to preserve the bottom-left corner.
    /// </remarks>
    public Vector2Int TopRight
    {
        readonly get => new(MaxX, MaxY);
        set
        {
            Width = value.X - X;
            Height = value.Y - Y;
        }
    }

    /// <summary>
    /// Gets all points within the rectangle.
    /// </summary>
    /// <remarks>
    /// Points within the rectangle are not inclusive of the points on the upper limits of the rectangle.
    /// </remarks>
    public readonly IEnumerable<Vector2Int> Points => new PointEnumerable(this);

    /// <summary>
    /// Gets or sets the position of the rectangle.
    /// </summary>
    /// <remarks>
    /// This is same as <see cref="BottomLeft"/> except that setting it will move the rectangle rather than resize it.
    /// </remarks>
    public Vector2Int Position
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
    public SizeInt Size
    {
        readonly get => new(Width, Height);
        set
        {
            Width = value.Width;
            Height = value.Height;
        }
    }

    /// <summary>
    /// Gets an empty rectangle.
    /// </summary>
    public static RectInt Zero { get; } = new(0, 0, 0, 0);

    /// <summary>
    /// Determines whether the left rectangle is equal to the right rectangle.
    /// </summary>
    /// <param name="left">The left rectangle.</param>
    /// <param name="right">The right rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(RectInt left, RectInt right) => left.Equals(right);

    /// <summary>
    /// Determines whether the left rectangle is not equal to the right rectangle.
    /// </summary>
    /// <param name="left">The left rectangle.</param>
    /// <param name="right">The right rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(RectInt left, RectInt right) => !(left == right);

    /// <summary>
    /// Determines whether the specified point is contained within the rectangle.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns><see langword="true"/> if the point is contained within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(Vector2Int point)
        => point.X >= X && point.X <= MaxX && point.Y >= Y && point.Y <= MaxY;

    /// <summary>
    /// Determines whether any of the specified points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to test.</param>
    /// <returns><see langword="true"/> if any of the points are contained within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(ReadOnlySpan<Vector2Int> points)
    {
        if (IsEmpty || points.IsEmpty)
            return false;

        foreach (Vector2Int point in points)
        {
            if (Contains(point))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether any of the specified points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to test.</param>
    /// <returns><see langword="true"/> if any of the points are contained within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(IEnumerable<Vector2Int> points)
    {
        if (IsEmpty)
            return false;

        return points switch
        {
            Vector2Int[] array => Contains(array),
            List<Vector2Int> list => Contains(CollectionsMarshal.AsSpan(list)),
            _ => points.Any(Contains)
        };
    }

    /// <summary>
    /// Deconstructs the rectangle into its x, y, width, and height components.
    /// </summary>
    /// <param name="x">The x component.</param>
    /// <param name="y">The y component.</param>
    /// <param name="width">The width component.</param>
    /// <param name="height">The height component.</param>
    public readonly void Deconstruct(out int x, out int y, out int width, out int height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    /// <summary>
    /// Expands the rectangle to include the specified point.
    /// </summary>
    /// <param name="point">The point to encapsulate.</param>
    /// <remarks>
    /// If the rectangle already contains the point, no changes are made.
    /// </remarks>
    public void Encapsulate(Vector2Int point)
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

        int maxX = Math.Max(MaxX, point.X);
        int maxY = Math.Max(MaxY, point.Y);

        X = Math.Min(X, point.X);
        Y = Math.Min(Y, point.Y);
        Width = maxX - X;
        Height = maxY - Y;
    }

    /// <summary>
    /// Expands the rectangle to include the specified rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to encapsulate.</param>
    /// <remarks>
    /// If the rectangle already contains the specified rectangle, no changes are made.
    /// </remarks>
    public void Encapsulate(RectInt rect)
    {
        if (this == rect || rect.IsEmpty)
            return;

        if (IsEmpty)
        {
            this = rect;
            return;
        }

        int maxX = Math.Max(MaxX, rect.MaxX);
        int maxY = Math.Max(MaxY, rect.MaxY);

        X = Math.Min(X, rect.X);
        Y = Math.Min(Y, rect.Y);
        Width = maxX - X;
        Height = maxY - Y;
    }

    /// <summary>
    /// Computes the intersection of this rectangle with another rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to intersect with.</param>
    /// <returns>A new rectangle representing the intersection area. If there is no intersection, an empty rectangle is returned.</returns>
    public readonly RectInt Intersects(RectInt rect)
    {
        if (IsEmpty || rect.IsEmpty)
            return Zero;

        int maxX = Math.Max(X, rect.X);
        int maxY = Math.Max(Y, rect.Y);
        int minWidth = Math.Min(MaxX, rect.MaxX) - maxX;
        int minHeight = Math.Min(MaxY, rect.MaxY) - maxY;

        return new RectInt(maxX, maxY, minWidth, minHeight);
    }

    /// <summary>
    /// Determines whether this rectangle overlaps with another rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to test for overlap.</param>
    /// <returns><see langword="true"/> if the rectangles overlap; otherwise, <see langword="false"/>.</returns>
    public readonly bool Overlaps(RectInt rect)
    {
        if (IsEmpty || rect.IsEmpty)
            return false;

        return X < rect.MaxX
            && MaxX > rect.X
            && Y < rect.MaxY
            && MaxY > rect.Y;
    }

    /// <summary>
    /// Computes the union of this rectangle with another rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to unite with.</param>
    /// <returns>A new rectangle representing the smallest rectangle that contains both rectangles.</returns>
    public readonly RectInt Union(RectInt rect)
    {
        if (IsEmpty && rect.IsEmpty)
            return Zero;

        if (IsEmpty)
            return rect;

        if (rect.IsEmpty)
            return this;

        int minX = Math.Min(X, rect.X);
        int minY = Math.Min(Y, rect.Y);
        int maxWidth = Math.Max(MaxX, rect.MaxX) - minX;
        int maxHeight = Math.Max(MaxY, rect.MaxY) - minY;

        return new RectInt(minX, minY, maxWidth, maxHeight);
    }

    /// <inheritdoc/>
    public readonly bool Equals(RectInt other)
    {
        if (IsEmpty && other.IsEmpty)
            return true;

        return X == other.X
            && Y == other.Y
            && Width == other.Width
            && Height == other.Height;
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RectInt rect && Equals(rect);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <summary>
    /// The string representation of the rectangle in the format "(X, Y, Width, Height)".
    /// </summary>
    /// <returns>The string representation of the rectangle.</returns>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({X}, {Y}, {Width}, {Height})", out charsWritten);
}

[StructLayout(LayoutKind.Auto)]
file readonly struct PointEnumerable(RectInt rect) : IEnumerable<Vector2Int>
{
    public IEnumerator<Vector2Int> GetEnumerator() => new PointEnumerator(rect);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

[StructLayout(LayoutKind.Auto)]
file struct PointEnumerator : IEnumerator<Vector2Int>
{
    private readonly int _startX;
    private readonly int _endX;

    private readonly int _startY;
    private readonly int _endY;

    private Vector2Int _current;

    internal PointEnumerator(RectInt rect)
    {
        _startX = rect.X;
        _endX = rect.MaxX;

        _startY = rect.Y;
        _endY = rect.MaxY;

        _current = new Vector2Int(_startX - 1, _startY);
    }

    public readonly Vector2Int Current => _current;

    readonly object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (++_current.X >= _endX)
        {
            _current.X = _startX;
            _current.Y++;
        }

        return _current.Y < _endY;
    }

    public void Reset()
    {
        _current.X = _startX - 1;
        _current.Y = _startY;
    }

    public readonly void Dispose()
    {
    }
}

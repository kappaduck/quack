// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

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
    /// Initializes a new instance of the <see cref="RectInt"/> struct.
    /// </summary>
    public RectInt() : this(0, 0, 0, 0)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RectInt"/> struct.
    /// </summary>
    /// <param name="position">The position of the top-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public RectInt(Vector2Int position, Vector2Int size) : this(position.X, position.Y, size.X, size.Y)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the top-left corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxX"/>.
    /// </remarks>
    public int X = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxY"/>.
    /// </remarks>
    public int Y = y;

    /// <summary>
    /// Gets or sets the width of the rectangle, measured from the <see cref="X"/> position.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxX"/>.
    /// </remarks>
    public int Width = width;

    /// <summary>
    /// Gets or sets the height of the rectangle, measured from the <see cref="Y"/> position.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxY"/>.
    /// </remarks>
    public int Height = height;

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
    /// Gets the points of the rectangle.
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
    public Vector2Int Size
    {
        readonly get => new(Width, Height);
        set
        {
            Width = value.X;
            Height = value.Y;
        }
    }

    /// <summary>
    /// Gets a empty rectangle.
    /// </summary>
    public static RectInt Zero { get; }

    /// <summary>
    /// Compares two rectangles are equal.
    /// </summary>
    /// <param name="left">Left rectangle to compare.</param>
    /// <param name="right">Right rectangle to compare.</param>
    /// <returns><see langword="true"/> if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(RectInt left, RectInt right) => left.Equals(right);

    /// <summary>
    /// Compares two rectangles are not equal.
    /// </summary>
    /// <param name="left">Left rectangle to compare.</param>
    /// <param name="right">Right rectangle to compare.</param>
    /// <returns><see langword="true"/> if the rectangles are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(RectInt left, RectInt right) => !(left == right);

    /// <summary>
    /// Determines whether the rectangle contains a specified point.
    /// </summary>
    /// <param name="point">The point to check.</param>
    /// <returns><see langword="true"/> if the rectangle contains the point; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(Vector2Int point) => point.X >= X && point.X <= MaxX && point.Y >= Y && point.Y <= MaxY;

    /// <summary>
    /// Determines whether the rectangle contains a specified point.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if the rectangle contains any of the points; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(ReadOnlySpan<Vector2Int> points)
    {
        if (IsEmpty || points.IsEmpty)
            return false;

        for (int i = 0; i < points.Length; i++)
        {
            if (Contains(points[i]))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether the rectangle contains a specified point.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if the rectangle contains any of the points; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(IEnumerable<Vector2Int> points)
    {
        if (IsEmpty || points is null)
            return false;

        if (points is Vector2Int[] array)
            return Contains(array);

        if (points is List<Vector2Int> list)
            return Contains(CollectionsMarshal.AsSpan(list));

        return points.Any(Contains);
    }

    /// <summary>
    /// Expands the rectangle to contain the specified point.
    /// </summary>
    /// <param name="point">The point to encapsulate.</param>
    public void Encapsulate(Vector2Int point)
    {
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
    /// Expands the rectangle to include another rectangle.
    /// </summary>
    /// <param name="other">The rectangle to encapsulate.</param>
    public void Encapsulate(RectInt other)
    {
        if (IsEmpty)
        {
            this = other;
            return;
        }

        int maxX = Math.Max(MaxX, other.MaxX);
        int maxY = Math.Max(MaxY, other.MaxY);

        X = Math.Min(X, other.X);
        Y = Math.Min(Y, other.Y);
        Width = maxX - X;
        Height = maxY - Y;
    }

    /// <summary>
    /// Calculates the intersection of two rectangles.
    /// </summary>
    /// <remarks>
    /// If both rectangles are empty, the method returns an empty rectangle.
    /// </remarks>
    /// <param name="other">The rectangle to intersect.</param>
    /// <returns>The intersections of two rectangles or an empty rectangle if there is no intersection.</returns>
    public readonly RectInt Intersects(RectInt other)
    {
        if (IsEmpty || other.IsEmpty)
            return Zero;

        int maxX = Math.Max(X, other.X);
        int maxY = Math.Max(Y, other.Y);
        int minWidth = Math.Min(MaxX, other.MaxX) - maxX;
        int minHeight = Math.Min(MaxY, other.MaxY) - maxY;

        return new RectInt(maxX, maxY, minWidth, minHeight);
    }

    /// <summary>
    /// Determines whether this rectangle overlaps with another rectangle.
    /// </summary>
    /// <param name="other">The rectangle to check.</param>
    /// <returns><see langword="true"/> if the rectangles overlap; otherwise, <see langword="false"/>.</returns>
    public readonly bool Overlaps(RectInt other)
    {
        if (IsEmpty || other.IsEmpty)
            return false;

        return X < other.MaxX
            && MaxX > other.X
            && Y < other.MaxY
            && MaxY > other.Y;
    }

    /// <summary>
    /// Calculates the union of two rectangles.
    /// </summary>
    /// <remarks>
    /// If both rectangles are empty, it returns an empty rectangle.
    /// If one of the rectangles is empty, it returns the other rectangle.
    /// </remarks>
    /// <param name="other">The rectangle to union.</param>
    /// <returns>The union of two rectangles.</returns>
    public readonly RectInt Union(RectInt other)
    {
        if (IsEmpty && other.IsEmpty)
            return Zero;

        if (IsEmpty)
            return other;

        if (other.IsEmpty)
            return this;

        int minX = Math.Min(X, other.X);
        int minY = Math.Min(Y, other.Y);
        int maxWidth = Math.Max(MaxX, other.MaxX) - minX;
        int maxHeight = Math.Max(MaxY, other.MaxY) - minY;

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
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
        => obj is RectInt rectangle && Equals(rectangle);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <inheritdoc/>
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
    private readonly int _startY;
    private readonly int _endX;
    private readonly int _endY;

    private Vector2Int _current;

    internal PointEnumerator(RectInt rectangle)
    {
        _startX = rectangle.X;
        _startY = rectangle.Y;
        _endX = rectangle.MaxX;
        _endY = rectangle.MaxY;

        _current = new(_startX - 1, _startY);
    }

    public readonly Vector2Int Current => _current;

    readonly object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        _current.X++;

        if (_current.X >= _endX)
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

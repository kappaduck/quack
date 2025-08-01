// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a rectangle with floating-point coordinates.
/// </summary>
/// <param name="x">The x-coordinate of the top-left corner of the rectangle.</param>
/// <param name="y">The y-coordinate of the top-left corner of the rectangle.</param>
/// <param name="width">The width of the rectangle.</param>
/// <param name="height">The height of the rectangle.</param>
[StructLayout(LayoutKind.Sequential)]
public struct Rect(float x, float y, float width, float height) : IEquatable<Rect>, ISpanFormattable
{
    /// <summary>
    /// Initializes an empty instance of the <see cref="Rect"/>.
    /// </summary>
    public Rect() : this(0f, 0f, 0f, 0f)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rect"/> with the specified position and size.
    /// </summary>
    /// <param name="position">The position of the top-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public Rect(Vector2 position, Vector2 size) : this(position.X, position.Y, size.X, size.Y)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the top-left corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxX"/>.
    /// </remarks>
    public float X = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxY"/>.
    /// </remarks>
    public float Y = y;

    /// <summary>
    /// Gets or sets the width of the rectangle, measured from the <see cref="X"/> position.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxX"/>.
    /// </remarks>
    public float Width = width;

    /// <summary>
    /// Gets or sets the height of the rectangle, measured from the <see cref="Y"/> position.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxY"/>.
    /// </remarks>
    public float Height = height;

    /// <summary>
    /// Gets the x-coordinate of the right side of the rectangle.
    /// </summary>
    public readonly float MaxX => X + Width;

    /// <summary>
    /// Gets the y-coordinate of the bottom side of the rectangle.
    /// </summary>
    public readonly float MaxY => Y + Height;

    /// <summary>
    /// Gets or sets the position of the center of the rectangle.
    /// </summary>
    public Vector2 Center
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
    public readonly bool IsEmpty => MathF.IsNearlyZero(Width) || MathF.IsNearlyZero(Height);

    /// <summary>
    /// Gets or sets the bottom-left corner of the rectangle.
    /// </summary>
    public Vector2 BottomLeft
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
    public Vector2 TopRight
    {
        readonly get => new(MaxX, MaxY);
        set
        {
            Width = value.X - X;
            Height = value.Y - Y;
        }
    }

    /// <summary>
    /// Gets or sets the position of the rectangle.
    /// </summary>
    /// <remarks>
    /// This is same as <see cref="BottomLeft"/> except that setting it will move the rectangle rather than resize it.
    /// </remarks>
    public Vector2 Position
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
    public Vector2 Size
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
    public static Rect Zero { get; } = new(0f, 0f, 0f, 0f);

    /// <summary>
    /// Compares two rectangles are equal.
    /// </summary>
    /// <param name="left">Left rectangle to compare.</param>
    /// <param name="right">Right rectangle to compare.</param>
    /// <returns><see langword="true"/> if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Rect left, Rect right) => left.Equals(right);

    /// <summary>
    /// Compares two rectangles are not equal.
    /// </summary>
    /// <param name="left">Left rectangle to compare.</param>
    /// <param name="right">Right rectangle to compare.</param>
    /// <returns><see langword="true"/> if the rectangles are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Rect left, Rect right) => !(left == right);

    /// <summary>
    /// Determines whether the rectangle contains a specified point.
    /// </summary>
    /// <param name="point">The point to check.</param>
    /// <returns><see langword="true"/> if the rectangle contains the point; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(Vector2 point) => point.X >= X && point.X <= MaxX && point.Y >= Y && point.Y <= MaxY;

    /// <summary>
    /// Determines whether the rectangle contains a specified point.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if the rectangle contains any of the points; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(ReadOnlySpan<Vector2> points)
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
    public readonly bool Contains(IEnumerable<Vector2> points)
    {
        if (IsEmpty || points is null)
            return false;

        if (points is Vector2[] array)
            return Contains(array);

        if (points is List<Vector2> list)
            return Contains(CollectionsMarshal.AsSpan(list));

        return points.Any(Contains);
    }

    /// <summary>
    /// Expands the rectangle to contain the specified point.
    /// </summary>
    /// <param name="point">The point to encapsulate.</param>
    public void Encapsulate(Vector2 point)
    {
        if (IsEmpty)
        {
            X = point.X;
            Y = point.Y;
            Width = 0;
            Height = 0;

            return;
        }

        float maxX = MathF.Max(MaxX, point.X);
        float maxY = MathF.Max(MaxY, point.Y);

        X = MathF.Min(X, point.X);
        Y = MathF.Min(Y, point.Y);
        Width = maxX - X;
        Height = maxY - Y;
    }

    /// <summary>
    /// Expands the rectangle to include another rectangle.
    /// </summary>
    /// <param name="other">The rectangle to encapsulate.</param>
    public void Encapsulate(Rect other)
    {
        if (IsEmpty)
        {
            this = other;
            return;
        }

        float maxX = MathF.Max(MaxX, other.MaxX);
        float maxY = MathF.Max(MaxY, other.MaxY);

        X = MathF.Min(X, other.X);
        Y = MathF.Min(Y, other.Y);
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
    public readonly Rect Intersects(Rect other)
    {
        if (IsEmpty || other.IsEmpty)
            return Zero;

        float maxX = Math.Max(X, other.X);
        float maxY = Math.Max(Y, other.Y);
        float minWidth = Math.Min(MaxX, other.MaxX) - maxX;
        float minHeight = Math.Min(MaxY, other.MaxY) - maxY;

        return new Rect(maxX, maxY, minWidth, minHeight);
    }

    /// <summary>
    /// Determines whether this rectangle overlaps with another rectangle.
    /// </summary>
    /// <param name="other">The rectangle to check.</param>
    /// <returns><see langword="true"/> if the rectangles overlap; otherwise, <see langword="false"/>.</returns>
    public readonly bool Overlaps(Rect other)
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
    public readonly Rect Union(Rect other)
    {
        if (IsEmpty && other.IsEmpty)
            return Zero;

        if (IsEmpty)
            return other;

        if (other.IsEmpty)
            return this;

        float minX = Math.Min(X, other.X);
        float minY = Math.Min(Y, other.Y);
        float maxWidth = Math.Max(MaxX, other.MaxX) - minX;
        float maxHeight = Math.Max(MaxY, other.MaxY) - minY;

        return new Rect(minX, minY, maxWidth, maxHeight);
    }

    /// <inheritdoc/>
    public readonly bool Equals(Rect other)
    {
        if (IsEmpty && other.IsEmpty)
            return true;

        return MathF.IsNearlyZero(X - other.X)
            && MathF.IsNearlyZero(Y - other.Y)
            && MathF.IsNearlyZero(Width - other.Width)
            && MathF.IsNearlyZero(Height - other.Height);
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
        => obj is Rect rectangle && Equals(rectangle);

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

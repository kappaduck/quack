// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a floating point rectangle.
/// </summary>
/// <param name="x">The x-coordinate of the top-left corner of the rectangle.</param>
/// <param name="y">The y-coordinate of the top-left corner of the rectangle.</param>
/// <param name="width">The width of the rectangle.</param>
/// <param name="height">The height of the rectangle.</param>
[StructLayout(LayoutKind.Sequential)]
public struct Rect(float x, float y, float width, float height) : IEquatable<Rect>, ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary>
    /// Creates an empty rectangle at the origin (0, 0).
    /// </summary>
    public Rect() : this(0f, 0f, 0f, 0f)
    {
    }

    /// <summary>
    /// Creates a rectangle from a position and size.
    /// </summary>
    /// <param name="position">The position of the top-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public Rect(PointF position, SizeF size) : this(position.X, position.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the top-left corner.
    /// </summary>
    /// <remarks>
    /// Updating the x-coordinate will adjust <see cref="Right"/>.
    /// </remarks>
    public float X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the top-left corner.
    /// </summary>
    /// <remarks>
    /// Updating the y-coordinate will adjust <see cref="Bottom"/>.
    /// </remarks>
    public float Y { get; set; } = y;

    /// <summary>
    /// Gets or sets the width measured from the x-coordinate.
    /// </summary>
    /// <remarks>
    /// Updating the width will adjust <see cref="Right"/>.
    /// </remarks>
    public float Width { get; set; } = width;

    /// <summary>
    /// Gets or sets the height measured from the y-coordinate.
    /// </summary>
    /// <remarks>
    /// Updating the height will adjust <see cref="Bottom"/>.
    /// </remarks>
    public float Height { get; set; } = height;

    /// <summary>
    /// Gets the area of the rectangle.
    /// </summary>
    public readonly float Area => Width * Height;

    /// <summary>
    /// Gets or sets the position of the top-left corner.
    /// </summary>
    public PointF Position
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
    public SizeF Size
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
    public readonly float Left => X;

    /// <summary>
    /// Gets the y-coordinate of the top side of the rectangle.
    /// </summary>
    /// <remarks>It's an alias for <see cref="Y"/>.</remarks>
    public readonly float Top => Y;

    /// <summary>
    /// Gets the x-coordinate of the right side of the rectangle.
    /// </summary>
    public readonly float Right => X + Width;

    /// <summary>
    /// Gets the y-coordinate of the bottom side of the rectangle.
    /// </summary>
    public readonly float Bottom => Y + Height;

    /// <summary>
    /// Gets the center point of the rectangle.
    /// </summary>
    public readonly PointF Center => new(X + (Width / 2f), Y + (Height / 2f));

    /// <summary>
    /// Gets the top-left corner of the rectangle.
    /// </summary>
    public readonly PointF TopLeft => new(Left, Top);

    /// <summary>
    /// Gets the top-right corner of the rectangle.
    /// </summary>
    public readonly PointF TopRight => new(Right, Top);

    /// <summary>
    /// Gets the bottom-left corner of the rectangle.
    /// </summary>
    public readonly PointF BottomLeft => new(Left, Bottom);

    /// <summary>
    /// Gets the bottom-right corner of the rectangle.
    /// </summary>
    public readonly PointF BottomRight => new(Right, Bottom);

    /// <summary>
    /// Gets a value indicating whether the rectangle is empty (either dimension is approximately zero).
    /// </summary>
    public readonly bool IsEmpty => MathF.ApproximatelyZero(Width) || MathF.ApproximatelyZero(Height);

    /// <summary>
    /// Gets a rectangle with all components set to zero.
    /// </summary>
    public static Rect Zero { get; } = new(0f, 0f, 0f, 0f);

    /// <summary>
    /// Determines whether the point is contained within the rectangle.
    /// </summary>
    /// <param name="point">The point to check.</param>
    /// <returns><see langword="true"/> if the point is within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(PointF point)
        => !IsEmpty && point.X >= X && point.X <= Right && point.Y >= Y && point.Y <= Bottom;

    /// <summary>
    /// Determines whether the rectangle is fully contained within this rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to check.</param>
    /// <returns><see langword="true"/> if the rectangle is fully within this rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(Rect rect)
        => !IsEmpty && !rect.IsEmpty && rect.X >= X && rect.Right <= Right && rect.Y >= Y && rect.Bottom <= Bottom;

    /// <summary>
    /// Determines whether all points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if all points are within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool ContainsAll(ReadOnlySpan<PointF> points)
    {
        if (IsEmpty || points.IsEmpty)
            return false;

        foreach (PointF point in points)
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
    public readonly bool ContainsAll(IEnumerable<PointF> points)
    {
        if (IsEmpty)
            return false;

        return points switch
        {
            PointF[] array => ContainsAll(array),
            List<PointF> list => ContainsAll(CollectionsMarshal.AsSpan(list)),
            _ => points.All(Contains)
        };
    }

    /// <summary>
    /// Determines whether any points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to check.</param>
    /// <returns><see langword="true"/> if any point is within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool ContainsAny(ReadOnlySpan<PointF> points)
    {
        if (IsEmpty || points.IsEmpty)
            return false;

        foreach (PointF point in points)
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
    public readonly bool ContainsAny(IEnumerable<PointF> points)
    {
        if (IsEmpty)
            return false;

        return points switch
        {
            PointF[] array => ContainsAny(array),
            List<PointF> list => ContainsAny(CollectionsMarshal.AsSpan(list)),
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
    public readonly void Deconstruct(out float x, out float y, out float width, out float height)
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
    public void Encapsulate(PointF point)
    {
        if (Contains(point))
            return;

        if (IsEmpty)
        {
            X = point.X;
            Y = point.Y;
            Width = 0f;
            Height = 0f;

            return;
        }

        float left = MathF.Min(X, point.X);
        float top = MathF.Min(Y, point.Y);
        float right = MathF.Max(Right, point.X);
        float bottom = MathF.Max(Bottom, point.Y);

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
    public void Encapsulate(Rect rect)
    {
        if (Contains(rect) || rect.IsEmpty)
            return;

        if (IsEmpty)
        {
            this = rect;
            return;
        }

        float left = MathF.Min(X, rect.X);
        float top = MathF.Min(Y, rect.Y);
        float right = MathF.Max(Right, rect.Right);
        float bottom = MathF.Max(Bottom, rect.Bottom);

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
    public readonly bool Overlaps(Rect rect)
    {
        if (IsEmpty || rect.IsEmpty)
            return false;

        return !(Left >= rect.Right || Right <= rect.Left || Top >= rect.Bottom || Bottom <= rect.Top);
    }

    /// <summary>
    /// Calculates the intersecting rectangle of two rectangles.
    /// </summary>
    /// <param name="left">The first rectangle.</param>
    /// <param name="right">The second rectangle.</param>
    /// <returns>The intersecting rectangle, or <see cref="Zero"/> if there is no intersection.</returns>
    public static Rect Intersect(Rect left, Rect right)
    {
        if (!left.Overlaps(right))
            return Zero;

        float x = MathF.Max(left.X, right.X);
        float y = MathF.Max(left.Y, right.Y);
        float width = MathF.Min(left.Right, right.Right) - x;
        float height = MathF.Min(left.Bottom, right.Bottom) - y;

        return new Rect(x, y, width, height);
    }

    /// <summary>
    /// Calculates the smallest rectangle that contains both specified rectangles.
    /// </summary>
    /// <param name="left">The first rectangle.</param>
    /// <param name="right">The second rectangle.</param>
    /// <returns>The smallest rectangle that contains both rectangles.</returns>
    public static Rect Union(Rect left, Rect right)
    {
        if (left.IsEmpty && right.IsEmpty)
            return Zero;

        if (left.IsEmpty)
            return right;

        if (right.IsEmpty)
            return left;

        float x = MathF.Min(left.X, right.X);
        float y = MathF.Min(left.Y, right.Y);
        float width = MathF.Max(left.Right, right.Right) - x;
        float height = MathF.Max(left.Bottom, right.Bottom) - y;

        return new Rect(x, y, width, height);
    }

    /// <inheritdoc/>
    public readonly bool Equals(Rect other)
    {
        if (IsEmpty && other.IsEmpty)
            return true;

        return MathF.ApproximatelyEquals(X, other.X)
            && MathF.ApproximatelyEquals(Y, other.Y)
            && MathF.ApproximatelyEquals(Width, other.Width)
            && MathF.ApproximatelyEquals(Height, other.Height);
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Rect other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({X}, {Y}, {Width}, {Height})", out charsWritten);

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"({X}, {Y}, {Width}, {Height})", out bytesWritten);

    /// <summary>
    /// Returns the largest <see cref="Rect"/> whose components are less than or equal to those of this rectangle.
    /// </summary>
    public readonly RectI Floor()
        => new((int)MathF.Floor(X), (int)MathF.Floor(Y), (int)MathF.Floor(Width), (int)MathF.Floor(Height));

    /// <summary>
    /// Returns the <see cref="RectI"/> whose components are the nearest integer to those of this rectangle.
    /// </summary>
    public readonly RectI Round()
        => new((int)MathF.Round(X), (int)MathF.Round(Y), (int)MathF.Round(Width), (int)MathF.Round(Height));

    /// <summary>
    /// Returns the <see cref="RectI"/> whose components are truncated toward zero from those of this rectangle.
    /// </summary>
    public readonly RectI Truncate() => new((int)X, (int)Y, (int)Width, (int)Height);

    /// <summary>
    /// Determines whether two rectangles are equal.
    /// </summary>
    /// <param name="left">The left rectangle.</param>
    /// <param name="right">The right rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Rect left, Rect right) => left.Equals(right);

    /// <summary>
    /// Determines whether two rectangles are not equal.
    /// </summary>
    /// <param name="left">The left rectangle.</param>
    /// <param name="right">The right rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Rect left, Rect right) => !(left == right);
}

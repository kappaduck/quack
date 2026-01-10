// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;

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
    /// Creates an empty rectangle.
    /// </summary>
    public Rect() : this(0f, 0f, 0f, 0f)
    {
    }

    /// <summary>
    /// Creates a rectangle with position and size.
    /// </summary>
    /// <param name="position">The position of the top-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public Rect(Vector2 position, Size size) : this(position.X, position.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the top-left corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxX"/>.
    /// </remarks>
    public float X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the top-left corner of the rectangle.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxY"/>.
    /// </remarks>
    public float Y { get; set; } = y;

    /// <summary>
    /// Gets or sets the width of the rectangle, measured from the <see cref="X"/> position.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxX"/>.
    /// </remarks>
    public float Width { get; set; } = width;

    /// <summary>
    /// Gets or sets the height of the rectangle, measured from the <see cref="Y"/> position.
    /// </summary>
    /// <remarks>
    /// Setting this value will adjust <see cref="MaxY"/>.
    /// </remarks>
    public float Height { get; set; } = height;

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
    /// Gets an empty rectangle.
    /// </summary>
    public static Rect Zero { get; } = new(0f, 0f, 0f, 0f);

    /// <summary>
    /// Determines whether the left rectangle is equal to the right rectangle.
    /// </summary>
    /// <param name="left">The left rectangle.</param>
    /// <param name="right">The right rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Rect left, Rect right) => left.Equals(right);

    /// <summary>
    /// Determines whether the left rectangle is not equal to the right rectangle.
    /// </summary>
    /// <param name="left">The left rectangle.</param>
    /// <param name="right">The right rectangle.</param>
    /// <returns><see langword="true"/> if the rectangles are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Rect left, Rect right) => !(left == right);

    /// <summary>
    /// Determines whether the specified point is contained within the rectangle.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns><see langword="true"/> if the point is contained within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(Vector2 point) => !IsEmpty && point.X >= X && point.X <= MaxX && point.Y >= Y && point.Y <= MaxY;

    /// <summary>
    /// Determines whether any of the specified points are contained within the rectangle.
    /// </summary>
    /// <param name="points">The points to test.</param>
    /// <returns><see langword="true"/> if any of the points are contained within the rectangle; otherwise, <see langword="false"/>.</returns>
    public readonly bool Contains(ReadOnlySpan<Vector2> points)
    {
        if (IsEmpty || points.IsEmpty)
            return false;

        foreach (Vector2 point in points)
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
    public readonly bool Contains(IEnumerable<Vector2> points)
    {
        if (IsEmpty)
            return false;

        return points switch
        {
            Vector2[] array => Contains(array),
            List<Vector2> list => Contains(CollectionsMarshal.AsSpan(list)),
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
    public readonly void Deconstruct(out float x, out float y, out float width, out float height)
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
    public void Encapsulate(Vector2 point)
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

        float maxX = MathF.Max(MaxX, point.X);
        float maxY = MathF.Max(MaxY, point.Y);

        X = MathF.Min(X, point.X);
        Y = MathF.Min(Y, point.Y);
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
    public void Encapsulate(Rect rect)
    {
        if (this == rect || rect.IsEmpty)
            return;

        if (IsEmpty)
        {
            this = rect;
            return;
        }

        float maxX = MathF.Max(MaxX, rect.MaxX);
        float maxY = MathF.Max(MaxY, rect.MaxY);

        X = MathF.Min(X, rect.X);
        Y = MathF.Min(Y, rect.Y);
        Width = maxX - X;
        Height = maxY - Y;
    }

    /// <summary>
    /// Computes the intersection of this rectangle with another rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to intersect with.</param>
    /// <returns>A new rectangle representing the intersection area. If there is no intersection, an empty rectangle is returned.</returns>
    public readonly Rect Intersects(Rect rect)
    {
        if (IsEmpty || rect.IsEmpty)
            return Zero;

        float maxX = MathF.Max(X, rect.X);
        float maxY = MathF.Max(Y, rect.Y);
        float minWidth = MathF.Min(MaxX, rect.MaxX) - maxX;
        float minHeight = MathF.Min(MaxY, rect.MaxY) - maxY;

        return new Rect(maxX, maxY, minWidth, minHeight);
    }

    /// <summary>
    /// Determines whether this rectangle overlaps with another rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to test for overlap.</param>
    /// <returns><see langword="true"/> if the rectangles overlap; otherwise, <see langword="false"/>.</returns>
    public readonly bool Overlaps(Rect rect)
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
    public readonly Rect Union(Rect rect)
    {
        if (IsEmpty && rect.IsEmpty)
            return Zero;

        if (IsEmpty)
            return rect;

        if (rect.IsEmpty)
            return this;

        float minX = MathF.Min(X, rect.X);
        float minY = MathF.Min(Y, rect.Y);
        float maxWidth = MathF.Max(MaxX, rect.MaxX) - minX;
        float maxHeight = MathF.Max(MaxY, rect.MaxY) - minY;

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
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Rect rect && Equals(rect);

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

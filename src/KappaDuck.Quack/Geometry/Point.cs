// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a floating point two-dimensional point (world/game space location).
/// </summary>
/// <param name="x">The x-coordinate of the point.</param>
/// <param name="y">The y-coordinate of the point.</param>
[StructLayout(LayoutKind.Sequential)]
public struct Point(float x, float y) :
    IAdditionOperators<Point, Vector2, Point>,
    ISubtractionOperators<Point, Vector2, Point>,
    ISubtractionOperators<Point, Point, Vector2>,
    IEqualityOperators<Point, Point, bool>,
    IEquatable<Point>,
    ISpanFormattable,
    IUtf8SpanFormattable
{
    /// <summary>
    /// Creates a point at the origin (0, 0).
    /// </summary>
    public Point() : this(0f, 0f)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate.
    /// </summary>
    public float X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate.
    /// </summary>
    public float Y { get; set; } = y;

    /// <summary>
    /// Gets the origin point (0, 0).
    /// </summary>
    public static Point Origin { get; } = new(0f, 0f);

    /// <summary>
    /// Computes the displacement vector from this point to another point.
    /// </summary>
    /// <param name="target">The target point.</param>
    /// <returns>The displacement vector.</returns>
    public readonly Vector2 To(Point target) => target - this;

    /// <summary>
    /// Computes the distance between this point and another point.
    /// </summary>
    /// <param name="to">The other point.</param>
    /// <returns>The distance between the two points.</returns>
    public readonly float Distance(Point to) => Distance(this, to);

    /// <summary>
    /// Returns the largest <see cref="PointI"/> whose coordinates are less than or equal to those of this point.
    /// </summary>
    public readonly PointI Floor() => new((int)MathF.Floor(X), (int)MathF.Floor(Y));

    /// <summary>
    /// Returns the <see cref="PointI"/> whose coordinates are the nearest integer to those of this point.
    /// </summary>
    public readonly PointI Round() => new((int)MathF.Round(X), (int)MathF.Round(Y));

    /// <summary>
    /// Returns the <see cref="PointI"/> whose coordinates are truncated toward zero from those of this point.
    /// </summary>
    public readonly PointI Truncate() => new((int)X, (int)Y);

    /// <summary>
    /// Computes the distance between two points.
    /// </summary>
    /// <param name="from">The point to measure from.</param>
    /// <param name="to">The point to measure to.</param>
    /// <returns>The distance between the two points.</returns>
    public static float Distance(Point from, Point to) => from.To(to).Magnitude;

    /// <summary>
    /// Computes the linear interpolation between two points with a clamped interpolation factor.
    /// </summary>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The ending point.</param>
    /// <param name="interpolationFactor">The interpolation factor between 0 and 1.</param>
    /// <returns>The interpolated point.</returns>
    public static Point Lerp(Point from, Point to, float interpolationFactor)
    {
        interpolationFactor = Math.Clamp(interpolationFactor, 0f, 1f);

        float x = (to.X - from.X) * interpolationFactor;
        float y = (to.Y - from.Y) * interpolationFactor;

        return new Point(from.X + x, from.Y + y);
    }

    /// <summary>
    /// Computes the linear interpolation between two points with an unclamped interpolation factor.
    /// </summary>
    /// <param name="from">The starting point.</param>
    /// <param name="to">The ending point.</param>
    /// <param name="interpolationFactor">The interpolation factor.</param>
    /// <returns>The interpolated point.</returns>
    public static Point LerpUnclamped(Point from, Point to, float interpolationFactor)
    {
        float x = (to.X - from.X) * interpolationFactor;
        float y = (to.Y - from.Y) * interpolationFactor;

        return new Point(from.X + x, from.Y + y);
    }

    /// <summary>
    /// Deconstructs the point into its components.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    public readonly void Deconstruct(out float x, out float y) => (x, y) = (X, Y);

    /// <summary>
    /// Determines whether this point is equal to another point.
    /// </summary>
    /// <param name="other">The point to compare with the current point.</param>
    /// <returns><see langword="true"/> if the points are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Point other)
        => MathF.ApproximatelyEquals(X, other.X) && MathF.ApproximatelyEquals(Y, other.Y);

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Point other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <summary>
    /// Converts the point to a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>The converted vector.</returns>
    public readonly Vector2 ToVector2() => new(X, Y);

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({X}, {Y})", out charsWritten);

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"({X}, {Y})", out bytesWritten);

    /// <summary>
    /// Translates a point by a displacement vector.
    /// </summary>
    /// <param name="left">The point.</param>
    /// <param name="right">The displacement.</param>
    /// <returns>The translated point.</returns>
    public static Point operator +(Point left, Vector2 right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Translates a point by an integer displacement vector, yielding a <see cref="Point"/>.
    /// </summary>
    /// <remarks>
    /// Mixed-type operation: float location nudged by an integer displacement always produces a float result.
    /// </remarks>
    /// <param name="left">The point.</param>
    /// <param name="right">The integer displacement.</param>
    /// <returns>The translated point.</returns>
    public static Point operator +(Point left, Vector2I right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Translates a point backwards by a displacement vector.
    /// </summary>
    /// <param name="left">The point.</param>
    /// <param name="right">The displacement.</param>
    /// <returns>The translated point.</returns>
    public static Point operator -(Point left, Vector2 right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Translates a point backwards by an integer displacement vector, yielding a <see cref="Point"/>.
    /// </summary>
    /// <remarks>
    /// Mixed-type operation: float location nudged by an integer displacement always produces a float result.
    /// </remarks>
    /// <param name="left">The point.</param>
    /// <param name="right">The integer displacement.</param>
    /// <returns>The translated point.</returns>
    public static Point operator -(Point left, Vector2I right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Computes the displacement between two points.
    /// </summary>
    /// <param name="left">The end point.</param>
    /// <param name="right">The start point.</param>
    /// <returns>The displacement vector.</returns>
    public static Vector2 operator -(Point left, Point right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Determines whether two points are equal.
    /// </summary>
    /// <param name="left">The left point.</param>
    /// <param name="right">The right point.</param>
    /// <returns><see langword="true"/> if the points are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Point left, Point right) => left.Equals(right);

    /// <summary>
    /// Determines whether two points are not equal.
    /// </summary>
    /// <param name="left">The left point.</param>
    /// <param name="right">The right point.</param>
    /// <returns><see langword="true"/> if the points are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Point left, Point right) => !(left == right);
}

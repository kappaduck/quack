// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents an integer two-dimensional vector.
/// </summary>
/// <param name="x">The x-coordinate of the vector.</param>
/// <param name="y">The y-coordinate of the vector.</param>
public struct Vector2Int(int x, int y) :
    IAdditionOperators<Vector2Int, Vector2Int, Vector2Int>,
    ISubtractionOperators<Vector2Int, Vector2Int, Vector2Int>,
    IMultiplyOperators<Vector2Int, int, Vector2Int>,
    IDivisionOperators<Vector2Int, int, Vector2Int>,
    IUnaryNegationOperators<Vector2Int, Vector2Int>,
    IEquatable<Vector2Int>,
    ISpanFormattable,
    IUtf8SpanFormattable
{
    /// <summary>
    /// Creates a zero vector.
    /// </summary>
    public Vector2Int() : this(0, 0)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate.
    /// </summary>
    public int X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate.
    /// </summary>
    public int Y { get; set; } = y;

    /// <summary>
    /// Gets a value indicating whether is a zero vector.
    /// </summary>
    public readonly bool IsZero => X == 0 && Y == 0;

    /// <summary>
    /// Gets the magnitude (length) of the vector.
    /// </summary>
    /// <remarks>
    /// If you only need to compare vector lengths, consider using <see cref="MagnitudeSquared"/> instead.
    /// </remarks>
    public readonly float Magnitude => MathF.Sqrt(MagnitudeSquared);

    /// <summary>
    /// Gets the squared magnitude.
    /// </summary>
    /// <remarks>
    /// It's more efficient to use the squared magnitude when comparing vector lengths
    /// instead of using the actual magnitude, as it avoids the computational cost of a square root operation.
    /// </remarks>
    public readonly int MagnitudeSquared => (X * X) + (Y * Y);

    /// <summary>
    /// Gets the left perpendicular vector.
    /// </summary>
    public readonly Vector2Int LeftPerpendicular => new(-Y, X);

    /// <summary>
    /// Gets the right perpendicular vector.
    /// </summary>
    public readonly Vector2Int RightPerpendicular => new(Y, -X);

    /// <summary>
    /// Unit vector pointing downwards in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corer.
    /// </remarks>
    public static Vector2Int Down { get; } = new(0, 1);

    /// <summary>
    /// Units vector pointing to the left in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corer.
    /// </remarks>
    public static Vector2Int Left { get; } = new(-1, 0);

    /// <summary>
    /// Units vector pointing to the right in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corer.
    /// </remarks>
    public static Vector2Int Right { get; } = new(1, 0);

    /// <summary>
    /// Units vector pointing upwards in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corer.
    /// </remarks>
    public static Vector2Int Up { get; } = new(0, -1);

    /// <summary>
    /// Gets the zero vector (0, 0).
    /// </summary>
    public static Vector2Int Zero { get; } = new(0, 0);

    /// <summary>
    /// Deconstructs the vector into its components.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    public readonly void Deconstruct(out int x, out int y) => (x, y) = (X, Y);

    /// <summary>
    /// Determines whether this vector is equal to another vector.
    /// </summary>
    /// <param name="other">The vector to compare with the current vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2Int other)
    {
        return MathF.ApproximatelyEqual(X, other.X)
            && MathF.ApproximatelyEqual(Y, other.Y);
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Vector2Int other && Equals(other);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <summary>
    /// Converts the vector to a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>The converted vector.</returns>
    public readonly Vector2 ToVector2() => new(X, Y);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({X}, {Y})", out charsWritten);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"({X}, {Y})", out bytesWritten);

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2Int operator +(Vector2Int left, Vector2Int right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Subtracts one vector from another.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2Int operator -(Vector2Int left, Vector2Int right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Multiplies two vectors component-wise.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2Int operator *(Vector2Int left, Vector2Int right) => new(left.X * right.X, left.Y * right.Y);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to multiply by.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2Int operator *(Vector2Int left, int right) => new(left.X * right, left.Y * right);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The scalar to multiply by.</param>
    /// <param name="right">The vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2Int operator *(int left, Vector2Int right) => right * left;

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to divide by.</param>
    /// <returns>The resulting vector.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the vector is divided by zero.</exception>
    public static Vector2Int operator /(Vector2Int left, int right)
    {
        Math.ThrowIfDividedByZero(right);
        return new Vector2Int(left.X / right, left.Y / right);
    }

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    public static Vector2Int operator -(Vector2Int value) => new(-value.X, -value.Y);

    /// <summary>
    /// Determines whether two vectors are equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector2Int left, Vector2Int right) => left.Equals(right);

    /// <summary>
    /// Determines whether two vectors are not equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vector2Int left, Vector2Int right) => !(left == right);
}

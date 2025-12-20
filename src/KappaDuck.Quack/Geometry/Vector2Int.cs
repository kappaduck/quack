// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents an integer two-dimensional vector.
/// </summary>
/// <param name="x">The x-coordinate of the vector.</param>
/// <param name="y">The y-coordinate of the vector.</param>
[StructLayout(LayoutKind.Sequential)]
public struct Vector2Int(int x, int y) :
    IAdditionOperators<Vector2Int, Vector2Int, Vector2Int>,
    ISubtractionOperators<Vector2Int, Vector2Int, Vector2Int>,
    IMultiplyOperators<Vector2Int, int, Vector2Int>,
    IDivisionOperators<Vector2Int, int, Vector2Int>,
    IUnaryNegationOperators<Vector2Int, Vector2Int>,
    IEquatable<Vector2Int>,
    ISpanFormattable
{
    /// <summary>
    /// Gets or sets the x-coordinate of the vector.
    /// </summary>
    public int X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the vector.
    /// </summary>
    public int Y { get; set; } = y;

    /// <summary>
    /// Gets a value indicating whether the vector is zero.
    /// </summary>
    /// <remarks>
    /// It compares the squared length of the vector to a small tolerance.
    /// </remarks>
    public readonly bool IsZero => X == 0 && Y == 0;

    /// <summary>
    /// Gets the magnitude of the vector.
    /// </summary>
    /// <remarks>
    /// For performance reasons when comparing, it is recommended to use <see cref="MagnitudeSquared"/> instead of <see cref="Magnitude"/> to avoid the square root operation.
    /// </remarks>
    public readonly float Magnitude => MathF.Sqrt(MagnitudeSquared);

    /// <summary>
    /// Gets the squared magnitude of the vector.
    /// </summary>
    /// <remarks>
    /// It is more efficient to compare the squared magnitude of two vectors than the magnitude.
    /// </remarks>
    public readonly int MagnitudeSquared => (X * X) + (Y * Y);

    /// <summary>
    /// Gets the shorthand for writing (0, -1).
    /// </summary>
    public static Vector2Int Down { get; } = new(0, -1);

    /// <summary>
    /// Gets the shorthand for writing (-1, 0).
    /// </summary>
    public static Vector2Int Left { get; } = new(-1, 0);

    /// <summary>
    /// Gets the shorthand for writing (1, 1).
    /// </summary>
    public static Vector2Int One { get; } = new(1, 1);

    /// <summary>
    /// Gets the perpendicular vector.
    /// </summary>
    public readonly Vector2Int Perpendicular => new(-Y, X);

    /// <summary>
    /// Gets the shorthand for writing (1, 0).
    /// </summary>
    public static Vector2Int Right { get; } = new(1, 0);

    /// <summary>
    /// Gets the shorthand for writing (0, 1).
    /// </summary>
    public static Vector2Int Up { get; } = new(0, 1);

    /// <summary>
    /// Gets an origin vector.
    /// </summary>
    public static Vector2Int Zero { get; } = new(0, 0);

    /// <summary>
    /// Implicitly converts a <see cref="Vector2Int"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="vector">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Vector2(Vector2Int vector) => new(vector.X, vector.Y);

    /// <summary>
    /// Adds two vectors together.
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

    /// <summary>
    /// Converts the vector to a <see cref="Vector2"/>.
    /// </summary>
    /// <returns>The converted vector.</returns>
    public readonly Vector2 ToVector2() => new(X, Y);

    /// <summary>
    /// Determines whether this vector is equal to another vector.
    /// </summary>
    /// <param name="other">The other vector to compare with.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2Int other) => X == other.X && Y == other.Y;

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Vector2Int other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({X}, {Y})", out charsWritten);
}

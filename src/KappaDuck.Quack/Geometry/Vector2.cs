// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a floating-point two-dimensional vector.
/// </summary>
/// <param name="x">The x-coordinate of the vector.</param>
/// <param name="y">The y-coordinate of the vector.</param>
[StructLayout(LayoutKind.Sequential)]
public struct Vector2(float x, float y) :
    IAdditionOperators<Vector2, Vector2, Vector2>,
    ISubtractionOperators<Vector2, Vector2, Vector2>,
    IMultiplyOperators<Vector2, float, Vector2>,
    IDivisionOperators<Vector2, float, Vector2>,
    IUnaryNegationOperators<Vector2, Vector2>,
    IEquatable<Vector2>,
    ISpanFormattable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2"/>.
    /// </summary>
    public Vector2() : this(0f, 0f)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2"/> with polar coordinates.
    /// </summary>
    /// <param name="radius">The radius (magnitude) of the vector.</param>
    /// <param name="angle">The angle of the vector.</param>
    public Vector2(float radius, Angle angle) : this(radius * angle.Cos, radius * angle.Sin)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the vector.
    /// </summary>
    public float X = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the vector.
    /// </summary>
    public float Y = y;

    /// <summary>
    /// Gets a value indicating whether the vector is normalized.
    /// </summary>
    public readonly bool IsNormalized => MathF.IsNearlyZero(MagnitudeSquared - 1f);

    /// <summary>
    /// Gets a value indicating whether the vector is zero.
    /// </summary>
    /// <remarks>
    /// It compares the squared magnitude of the vector to a small tolerance.
    /// </remarks>
    public readonly bool IsZero => MagnitudeSquared <= (float.Epsilon * float.Epsilon);

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
    public readonly float MagnitudeSquared => (X * X) + (Y * Y);

    /// <summary>
    /// Gets the normalized vector.
    /// </summary>
    public readonly Vector2 Normalized
    {
        get
        {
            if (IsNormalized)
                return this;

            float magnitude = Magnitude;

            if (magnitude > float.Epsilon)
                return this / magnitude;

            return Zero;
        }
    }

    /// <summary>
    /// Gets the shorthand for writing (0, -1).
    /// </summary>
    public static Vector2 Down { get; } = new(0f, -1f);

    /// <summary>
    /// Gets the shorthand for writing (-1, 0).
    /// </summary>
    public static Vector2 Left { get; } = new(-1f, 0f);

    /// <summary>
    /// Gets the shorthand for writing (1, 1).
    /// </summary>
    public static Vector2 One { get; } = new(1f, 1f);

    /// <summary>
    /// Gets the perpendicular vector.
    /// </summary>
    public readonly Vector2 Perpendicular => new(-Y, X);

    /// <summary>
    /// Gets the shorthand for writing (1, 0).
    /// </summary>
    public static Vector2 Right { get; } = new(1f, 0f);

    /// <summary>
    /// Gets the shorthand for writing (0, 1).
    /// </summary>
    public static Vector2 Up { get; } = new(0f, 1f);

    /// <summary>
    /// Gets an origin vector.
    /// </summary>
    public static Vector2 Zero { get; } = new(0f, 0f);

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="left">Left vector.</param>
    /// <param name="right">Right vector.</param>
    /// <returns>The sum of the vectors.</returns>
    public static Vector2 operator +(Vector2 left, Vector2 right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    /// <param name="left">Left vector.</param>
    /// <param name="right">Right vector.</param>
    /// <returns>The difference of the vectors.</returns>
    public static Vector2 operator -(Vector2 left, Vector2 right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">Scalar to multiply by.</param>
    /// <returns>The product of the vector and the scalar.</returns>
    public static Vector2 operator *(Vector2 left, float right) => new(left.X * right, left.Y * right);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">Scalar to multiply.</param>
    /// <param name="right">The vector.</param>
    /// <returns>The product of the scalar and the vector.</returns>
    public static Vector2 operator *(float left, Vector2 right) => right * left;

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">Scalar to divide by.</param>
    /// <returns>The division of the vector and the scalar.</returns>
    public static Vector2 operator /(Vector2 left, float right)
    {
        Math.ThrowIfDividedByZero(right);

        return new(left.X / right, left.Y / right);
    }

    /// <summary>
    /// Negates a vector.
    /// </summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector2.</returns>
    public static Vector2 operator -(Vector2 value) => new(-value.X, -value.Y);

    /// <summary>
    /// Compares two vectors are equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector2 left, Vector2 right) => left.Equals(right);

    /// <summary>
    /// Compares two vectors are not equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vector2 left, Vector2 right) => !(left == right);

    /// <summary>
    /// Gets the angle between two vectors.
    /// </summary>
    /// <param name="from">The first vector.</param>
    /// <param name="to">The second vector.</param>
    /// <returns>The angle between the two vectors.</returns>
    public static Angle Angle(Vector2 from, Vector2 to)
    {
        float dot = Dot(from.Normalized, to.Normalized);

        dot = Math.Clamp(dot, -1f, 1f);

        return MathF.Acos(dot);
    }

    /// <summary>
    /// Clamps a vector to a specified length.
    /// </summary>
    /// <param name="vector">The vector to clamp.</param>
    /// <param name="maxLength">The maximum length to clamp the vector to.</param>
    /// <returns>The clamped vector.</returns>
    public static Vector2 Clamp(Vector2 vector, float maxLength)
    {
        return vector.MagnitudeSquared > maxLength * maxLength
            ? vector.Normalized * maxLength
            : vector;
    }

    /// <summary>
    /// Gets the distance between two vectors.
    /// </summary>
    /// <param name="left">The left vector to calculate the distance to.</param>
    /// <param name="right">The right vector to calculate the distance to.</param>
    /// <returns>The distance between the two vectors.</returns>
    public static float Distance(Vector2 left, Vector2 right) => (left - right).Magnitude;

    /// <summary>
    /// Produces a dot product of two vectors.
    /// </summary>
    /// <param name="left">Left vector to multiply.</param>
    /// <param name="right">Right vector to multiply.</param>
    /// <returns>A scalar that is the dot product of the two vectors.</returns>
    public static float Dot(Vector2 left, Vector2 right) => (left.X * right.X) + (left.Y * right.Y);

    /// <summary>
    /// Linearly interpolates between two vectors with a clamped interpolation factor.
    /// </summary>
    /// <param name="start">The start vector.</param>
    /// <param name="end">The end vector.</param>
    /// <param name="interpolationFactor">The interpolation factor. The value is clamped between 0 and 1.</param>
    /// <returns>A new vector interpolated between the start and end vectors.</returns>
    public static Vector2 Lerp(Vector2 start, Vector2 end, float interpolationFactor)
    {
        interpolationFactor = Math.Clamp(interpolationFactor, 0f, 1f);

        return start + ((end - start) * interpolationFactor);
    }

    /// <summary>
    /// Linearly interpolates between two vectors with an unclamped interpolation factor.
    /// </summary>
    /// <param name="start">The start vector.</param>
    /// <param name="end">The end vector.</param>
    /// <param name="interpolationFactor">The interpolation factor.</param>
    /// <returns>A new vector interpolated between the start and end vectors.</returns>
    public static Vector2 LerpUnclamped(Vector2 start, Vector2 end, float interpolationFactor)
        => start + ((end - start) * interpolationFactor);

    /// <summary>
    /// Gets the maximum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The maximum vector.</returns>
    public static Vector2 Max(Vector2 left, Vector2 right)
        => new(MathF.Max(left.X, right.X), MathF.Max(left.Y, right.Y));

    /// <summary>
    /// Gets the minimum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The minimum vector.</returns>
    public static Vector2 Min(Vector2 left, Vector2 right)
        => new(MathF.Min(left.X, right.X), MathF.Min(left.Y, right.Y));

    /// <summary>
    /// Moves a vector towards a target vector.
    /// </summary>
    /// <param name="current">The current vector.</param>
    /// <param name="target">The target vector.</param>
    /// <param name="maxDistanceDelta">The maximum distance to move the vector.</param>
    /// <returns>The new vector moved towards the target vector.</returns>
    public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
    {
        Vector2 vector = target - current;
        float magnitude = vector.Magnitude;

        if (magnitude <= maxDistanceDelta || MathF.IsNearlyZero(magnitude))
            return target;

        return current + (vector / magnitude * maxDistanceDelta);
    }

    /// <summary>
    /// Scales a vector by multiplying each component by the corresponding component of another vector.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector2 Scale(Vector2 left, Vector2 right) => new(left.X * right.X, left.Y * right.Y);

    /// <summary>
    /// Reflects the current vector across the given normal vector.
    /// </summary>
    /// <remarks>
    /// If the normal vector is not normalized, it will be normalized for the calculation.
    /// </remarks>
    /// <param name="vector">The vector to reflect.</param>
    /// <param name="normal">The normal vector to reflect across. Must be normalized.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector2 Reflect(Vector2 vector, Vector2 normal)
    {
        Vector2 normalized = normal.Normalized;
        return vector - (2 * Dot(vector, normalized) * normalized);
    }

    /// <summary>
    /// Rotates the vector by the given angle.
    /// </summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>The rotated vector.</returns>
    public static Vector2 Rotate(Vector2 vector, Angle angle)
        => new((vector.X * angle.Cos) - (vector.Y * angle.Sin), (vector.X * angle.Sin) + (vector.Y * angle.Cos));

    /// <summary>
    /// Gets the angle between two vectors.
    /// </summary>
    /// <param name="to">The second vector.</param>
    /// <returns>The angle between the two vectors.</returns>
    public readonly Angle Angle(Vector2 to) => Angle(this, to);

    /// <summary>
    /// Clamps the vector to a maximum length.
    /// </summary>
    /// <param name="maxLength">The maximum length to clamp to.</param>
    /// <returns>A clamped vector.</returns>
    public readonly Vector2 Clamp(float maxLength) => Clamp(this, maxLength);

    /// <summary>
    /// Gets the distance between two vectors.
    /// </summary>
    /// <param name="other">The other vector to calculate the distance to.</param>
    /// <returns>The distance between the two vectors.</returns>
    public readonly float Distance(Vector2 other) => Distance(this, other);

    /// <summary>
    /// Produces a dot product of two vectors.
    /// </summary>
    /// <param name="right">Right vector to multiply.</param>
    /// <returns>A scalar that is the dot product of the two vectors.</returns>
    public readonly float Dot(Vector2 right) => Dot(this, right);

    /// <summary>
    /// Linearly interpolates between the current vector and the target vector by the given interpolation factor.
    /// </summary>
    /// <param name="target">The target vector to interpolate towards.</param>
    /// <param name="interpolationFactor">The interpolation factor. The value is clamped between 0 and 1.</param>
    /// <returns>A new vector interpolated between the current vector and the target vector.</returns>
    public readonly Vector2 Lerp(Vector2 target, float interpolationFactor) => Lerp(this, target, interpolationFactor);

    /// <summary>
    /// Linearly interpolates between two vectors with an unclamped interpolation factor.
    /// </summary>
    /// <param name="target">The target vector.</param>
    /// <param name="interpolationFactor">The interpolation factor.</param>
    /// <returns>A new vector interpolated between the start and end vectors.</returns>
    public readonly Vector2 LerpUnclamped(Vector2 target, float interpolationFactor) => LerpUnclamped(this, target, interpolationFactor);

    /// <summary>
    /// Moves the current vector towards a target vector.
    /// </summary>
    /// <param name="target">The target vector to move towards.</param>
    /// <param name="maxDistanceDelta">The maximum distance to move the vector.</param>
    /// <returns>The new vector moved towards the target vector.</returns>
    public readonly Vector2 MoveTowards(Vector2 target, float maxDistanceDelta) => MoveTowards(this, target, maxDistanceDelta);

    /// <summary>
    /// Scales the vector by multiplying each component by the corresponding component of another vector.
    /// </summary>
    /// <param name="vector">The vector to scale by.</param>
    /// <returns>The scaled vector.</returns>
    public readonly Vector2 Scale(Vector2 vector) => Scale(this, vector);

    /// <summary>
    /// Reflects the current vector across the given normal vector.
    /// </summary>
    /// <remarks>
    /// If the normal vector is not normalized, it will be normalized for the calculation.
    /// </remarks>
    /// <param name="vector">The normal vector to reflect across. Must be normalized.</param>
    /// <returns>The reflected vector.</returns>
    public readonly Vector2 Reflect(Vector2 vector) => Reflect(this, vector);

    /// <summary>
    /// Rotates the vector by the given angle.
    /// </summary>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>The rotated vector.</returns>
    public readonly Vector2 Rotate(Angle angle) => Rotate(this, angle);

    /// <summary>
    /// Compares two vectors are equal.
    /// </summary>
    /// <param name="other">The vector to compare.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2 other)
    {
        return MathF.IsNearlyZero(X - other.X)
            && MathF.IsNearlyZero(Y - other.Y);
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
        => obj is Vector2 vector && Equals(vector);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    /// <summary>
    /// Returns a string representation of the vector in the format "(X, Y)".
    /// </summary>
    /// <returns>>A string representation of the vector.</returns>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({X}, {Y})", out charsWritten);
}

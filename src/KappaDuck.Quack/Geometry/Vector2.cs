// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using NumericVector2 = System.Numerics.Vector2;

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
    IMultiplyOperators<Vector2, Vector2, Vector2>,
    IMultiplyOperators<Vector2, float, Vector2>,
    IDivisionOperators<Vector2, float, Vector2>,
    IUnaryNegationOperators<Vector2, Vector2>,
    IEquatable<Vector2>,
    ISpanFormattable
{
    /// <summary>
    /// Creates an empty vector.
    /// </summary>
    public Vector2() : this(0f, 0f)
    {
    }

    /// <summary>
    /// Creates a vector from polar coordinates.
    /// </summary>
    /// <param name="radius">The radius (magnitude) of the vector.</param>
    /// <param name="angle">The angle of the vector.</param>
    public Vector2(float radius, Angle angle) : this(radius * angle.Cos, radius * angle.Sin)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the vector.
    /// </summary>
    public float X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the vector.
    /// </summary>
    public float Y { get; set; } = y;

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
    public readonly bool IsZero => MagnitudeSquared <= float.Epsilon * float.Epsilon;

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
    /// Gets the normalized (unit length) version of the vector.
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
    /// Gets the shorthand for writing (0, 1).
    /// </summary>
    public static Vector2 Down { get; } = new(0f, 1f);

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
    /// Gets the shorthand for writing (0, -1).
    /// </summary>
    public static Vector2 Up { get; } = new(0f, -1f);

    /// <summary>
    /// Gets an origin vector.
    /// </summary>
    public static Vector2 Zero { get; } = new(0f, 0f);

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2 operator +(Vector2 left, Vector2 right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Subtracts one vector from another.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2 operator -(Vector2 left, Vector2 right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Multiplies two vectors component-wise.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2 operator *(Vector2 left, Vector2 right) => new(left.X * right.X, left.Y * right.Y);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to multiply by.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2 operator *(Vector2 left, float right) => new(left.X * right, left.Y * right);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The scalar to multiply by.</param>
    /// <param name="right">The vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2 operator *(float left, Vector2 right) => right * left;

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to divide by.</param>
    /// <returns>The resulting vector.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the vector is divided by zero.</exception>
    public static Vector2 operator /(Vector2 left, float right)
    {
        Math.ThrowIfDividedByZero(right);
        return new Vector2(left.X / right, left.Y / right);
    }

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    public static Vector2 operator -(Vector2 value) => new(-value.X, -value.Y);

    /// <summary>
    /// Determines whether two vectors are equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector2 left, Vector2 right) => left.Equals(right);

    /// <summary>
    /// Determines whether two vectors are not equal.
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
    /// Clamps the vector to a maximum length.
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
    /// Computes the distance between two vectors.
    /// </summary>
    /// <param name="from">The first vector to measure from.</param>
    /// <param name="to">The second vector to measure to.</param>
    /// <returns>The distance between the two vectors.</returns>
    public static float Distance(Vector2 from, Vector2 to) => (to - from).Magnitude;

    /// <summary>
    /// Computes the dot product of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public static float Dot(Vector2 left, Vector2 right) => (left.X * right.X) + (left.Y * right.Y);

    /// <summary>
    /// Computes the linear interpolation between two vectors with a clamped interpolation factor.
    /// </summary>
    /// <param name="start">The starting vector.</param>
    /// <param name="end">The ending vector.</param>
    /// <param name="interpolationFactor">The interpolation factor between 0 and 1.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector2 Lerp(Vector2 start, Vector2 end, float interpolationFactor)
    {
        interpolationFactor = Math.Clamp(interpolationFactor, 0f, 1f);
        return start + ((end - start) * interpolationFactor);
    }

    /// <summary>
    /// Computes the linear interpolation between two vectors with an unclamped interpolation factor.
    /// </summary>
    /// <param name="start">The starting vector.</param>
    /// <param name="end">The ending vector.</param>
    /// <param name="interpolationFactor">The interpolation factor.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector2 LerpUnclamped(Vector2 start, Vector2 end, float interpolationFactor)
        => start + ((end - start) * interpolationFactor);

    /// <summary>
    /// Computes the component-wise maximum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The component-wise maximum vector.</returns>
    public static Vector2 Max(Vector2 left, Vector2 right)
        => new(MathF.Max(left.X, right.X), MathF.Max(left.Y, right.Y));

    /// <summary>
    /// Computes the component-wise minimum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The component-wise minimum vector.</returns>
    public static Vector2 Min(Vector2 left, Vector2 right)
        => new(MathF.Min(left.X, right.X), MathF.Min(left.Y, right.Y));

    /// <summary>
    /// Moves a vector towards a target vector by a maximum distance.
    /// </summary>
    /// <param name="current">The current vector.</param>
    /// <param name="target">The target vector.</param>
    /// <param name="maxDistanceDelta">The maximum distance to move.</param>
    /// <returns>The moved vector towards the target.</returns>
    public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
    {
        Vector2 toVector = target - current;
        float distance = toVector.Magnitude;

        if (distance <= maxDistanceDelta || MathF.IsNearlyZero(distance))
            return target;

        return current + (toVector / distance * maxDistanceDelta);
    }

    /// <summary>
    /// Scales a vector by another vector component-wise.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector2 Scale(Vector2 left, Vector2 right) => left * right;

    /// <summary>
    /// Reflects a vector off a surface with the given normal.
    /// </summary>
    /// <remarks>
    /// The normal vector is expected to be normalized.
    /// If it is not, the method will normalize it internally.
    /// </remarks>
    /// <param name="vector">The vector to reflect.</param>
    /// <param name="normal">The normal of the surface to reflect off.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector2 Reflect(Vector2 vector, Vector2 normal)
    {
        Vector2 normalized = normal.Normalized;
        return vector - (2f * Dot(vector, normalized) * normalized);
    }

    /// <summary>
    /// Rotates a vector by the specified angle.
    /// </summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle to rotate the vector by.</param>
    /// <returns>The rotated vector.</returns>
    public static Vector2 Rotate(Vector2 vector, Angle angle)
    {
        float cos = angle.Cos;
        float sin = angle.Sin;

        float x = (vector.X * cos) - (vector.Y * sin);
        float y = (vector.X * sin) + (vector.Y * cos);

        return new Vector2(x, y);
    }

    /// <summary>
    /// Computes the angle between this vector and another vector.
    /// </summary>
    /// <param name="to">The other vector.</param>
    /// <returns>The angle between the two vectors.</returns>
    public readonly Angle Angle(Vector2 to) => Angle(this, to);

    /// <summary>
    /// Clamps this vector to a maximum length.
    /// </summary>
    /// <param name="maxLength">The maximum length to clamp the vector to.</param>
    /// <returns>The clamped vector.</returns>
    public readonly Vector2 Clamp(float maxLength) => Clamp(this, maxLength);

    /// <summary>
    /// Deconstructs the size into its x and y components.
    /// </summary>
    /// <param name="x">The x component.</param>
    /// <param name="y">The y component.</param>
    public readonly void Deconstruct(out float x, out float y) => (x, y) = (X, Y);

    /// <summary>
    /// Computes the distance between this vector and another vector.
    /// </summary>
    /// <param name="to">The other vector.</param>
    /// <returns>The distance between the two vectors.</returns>
    public readonly float Distance(Vector2 to) => Distance(this, to);

    /// <summary>
    /// Computes the dot product of this vector and another vector.
    /// </summary>
    /// <param name="right">The other vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public readonly float Dot(Vector2 right) => Dot(this, right);

    /// <summary>
    /// Computes the linear interpolation between this vector and another vector with a clamped interpolation factor.
    /// </summary>
    /// <param name="end">The ending vector.</param>
    /// <param name="interpolationFactor">The interpolation factor between 0 and 1.</param>
    /// <returns>The interpolated vector.</returns>
    public readonly Vector2 Lerp(Vector2 end, float interpolationFactor) => Lerp(this, end, interpolationFactor);

    /// <summary>
    /// Computes the linear interpolation between this vector and another vector with an unclamped interpolation factor.
    /// </summary>
    /// <param name="end">The ending vector.</param>
    /// <param name="interpolationFactor">The interpolation factor.</param>
    /// <returns>The interpolated vector.</returns>
    public readonly Vector2 LerpUnclamped(Vector2 end, float interpolationFactor) => LerpUnclamped(this, end, interpolationFactor);

    /// <summary>
    /// Moves this vector towards a target vector by a maximum distance.
    /// </summary>
    /// <param name="target">The target vector.</param>
    /// <param name="maxDistanceDelta">The maximum distance to move.</param>
    /// <returns>The moved vector towards the target.</returns>
    public readonly Vector2 MoveTowards(Vector2 target, float maxDistanceDelta) => MoveTowards(this, target, maxDistanceDelta);

    /// <summary>
    /// Scales this vector by another vector component-wise.
    /// </summary>
    /// <param name="right">The other vector.</param>
    /// <returns>The scaled vector.</returns>
    public readonly Vector2 Scale(Vector2 right) => Scale(this, right);

    /// <summary>
    /// Reflects this vector off a surface with the given normal.
    /// </summary>
    /// <remarks>
    /// The normal vector is expected to be normalized.
    /// If it is not, the method will normalize it internally.
    /// </remarks>
    /// <param name="normal">The normal of the surface to reflect off.</param>
    /// <returns>The reflected vector.</returns>
    public readonly Vector2 Reflect(Vector2 normal) => Reflect(this, normal);

    /// <summary>
    /// Rotates this vector by the specified angle.
    /// </summary>
    /// <param name="angle">The angle to rotate the vector by.</param>
    /// <returns>The rotated vector.</returns>
    public readonly Vector2 Rotate(Angle angle) => Rotate(this, angle);

    /// <summary>
    /// Determines whether this vector is equal to another vector.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2 other)
    {
        return MathF.IsNearlyZero(X - other.X)
            && MathF.IsNearlyZero(Y - other.Y);
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Vector2 vector && Equals(vector);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    /// <summary>
    /// The string representation of the vector in the format (X, Y).
    /// </summary>
    /// <returns>The string representation of the vector.</returns>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({X}, {Y})", out charsWritten);

    internal readonly NumericVector2 ToNumerics() => new(X, Y);
}

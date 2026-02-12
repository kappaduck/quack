// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a floating point two-dimensional vector.
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
    ISpanFormattable,
    IUtf8SpanFormattable
{
    /// <summary>
    /// Creates a zero vector (0, 0).
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
    /// Gets or sets the x-coordinate.
    /// </summary>
    public float X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate.
    /// </summary>
    public float Y { get; set; } = y;

    /// <summary>
    /// Gets a value indicating whether is a normalized vector (magnitude of 1).
    /// </summary>
    public readonly bool IsNormalized => MathF.Abs(MagnitudeSquared - 1f) < MathExtensions.NormalizedEpsilon;

    /// <summary>
    /// Gets a value indicating whether is a zero vector.
    /// </summary>
    public readonly bool IsZero => MagnitudeSquared < MathExtensions.GeometryEpsilonSquared;

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
    public readonly float MagnitudeSquared => (X * X) + (Y * Y);

    /// <summary>
    /// Gets the normalized vector, which has a magnitude of 1.
    /// </summary>
    /// <remarks>
    /// If the vector is already normalized, it returns itself.
    /// </remarks>
    public readonly Vector2 Normalized
    {
        get
        {
            if (IsNormalized)
                return this;

            if (MagnitudeSquared > MathExtensions.GeometryEpsilonSquared)
                return this / Magnitude;

            return Zero;
        }
    }

    /// <summary>
    /// Gets the left perpendicular vector.
    /// </summary>
    public readonly Vector2 LeftPerpendicular => new(-Y, X);

    /// <summary>
    /// Gets the right perpendicular vector.
    /// </summary>
    public readonly Vector2 RightPerpendicular => new(Y, -X);

    /// <summary>
    /// Unit vector pointing downwards in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corer.
    /// </remarks>
    public static Vector2 Down { get; } = new(0f, 1f);

    /// <summary>
    /// Units vector pointing to the left in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corer.
    /// </remarks>
    public static Vector2 Left { get; } = new(-1f, 0f);

    /// <summary>
    /// Units vector pointing to the right in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corer.
    /// </remarks>
    public static Vector2 Right { get; } = new(1f, 0f);

    /// <summary>
    /// Units vector pointing upwards in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corer.
    /// </remarks>
    public static Vector2 Up { get; } = new(0f, -1f);

    /// <summary>
    /// Gets the zero vector (0, 0).
    /// </summary>
    public static Vector2 Zero { get; } = new(0f, 0f);

    /// <summary>
    /// Computes the angle between this vector and another vector.
    /// </summary>
    /// <param name="to">The other vector.</param>
    /// <returns>The angle between the two vectors.</returns>
    public readonly Angle Angle(Vector2 to) => Between(this, to);

    /// <summary>
    /// Clamps to a maximum length.
    /// </summary>
    /// <param name="maxLength">The maximum length to clamp the vector to.</param>
    /// <returns>The clamped vector.</returns>
    public readonly Vector2 Clamp(float maxLength) => Clamp(this, maxLength);

    /// <summary>
    /// Computes the cross product of this vector and another vector.
    /// </summary>
    /// <remarks>
    /// In screen space (top-left origin), a positive value means <paramref name="other"/>
    /// is clockwise from this vector, and a negative value means it is counter-clockwise.
    /// </remarks>
    /// <param name="other">The other vector.</param>
    /// <returns>The cross product of the two vectors.</returns>
    public readonly float Cross(Vector2 other) => Cross(this, other);

    /// <summary>
    /// Deconstructs the vector into its components.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
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
    /// <param name="other">The other vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public readonly float Dot(Vector2 other) => Dot(this, other);

    /// <summary>
    /// Moves this vector towards a target vector by a maximum distance.
    /// </summary>
    /// <param name="target">The target vector.</param>
    /// <param name="maxDistanceDelta">The maximum distance to move.</param>
    /// <returns>The moved vector towards the target.</returns>
    public readonly Vector2 MoveTowards(Vector2 target, float maxDistanceDelta) => MoveTowards(this, target, maxDistanceDelta);

    /// <summary>
    /// Reflects this vector off a surface defined by a normal vector.
    /// </summary>
    /// <remarks>
    /// The normal vector should be normalized (unit length) for accurate results.
    /// If the normal vector is not normalized, it will be normalized internally.
    /// </remarks>
    /// <param name="normal">The normal vector of the surface.</param>
    /// <returns>The reflected vector.</returns>
    public readonly Vector2 Reflect(Vector2 normal) => Reflect(this, normal);

    /// <summary>
    /// Rotates this vector by a given angle.
    /// </summary>
    /// <param name="angle">The angle to rotate by.</param>
    /// <returns>The rotated vector.</returns>
    public readonly Vector2 Rotate(Angle angle) => Rotate(this, angle);

    /// <summary>
    /// Scales this vector by another vector.
    /// </summary>
    /// <param name="scale">The scale vector.</param>
    /// <returns>The scaled vector.</returns>
    public readonly Vector2 Scale(Vector2 scale) => Scale(this, scale);

    /// <summary>
    /// Computes the angle between two vectors.
    /// </summary>
    /// <param name="from">The first vector.</param>
    /// <param name="to">The second vector.</param>
    /// <returns>The angle between the two vectors.</returns>
    public static Angle Between(Vector2 from, Vector2 to)
    {
        float dot = Dot(from.Normalized, to.Normalized);
        dot = Math.Clamp(dot, -1f, 1f);

        return Geometry.Angle.FromRadians(MathF.Acos(dot));
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
    /// Computes the cross product of two vectors.
    /// </summary>
    /// <remarks>
    /// In screen space (top-left origin), a positive value means <paramref name="right"/>
    /// is clockwise from <paramref name="left"/>, and a negative value means it is counter-clockwise.
    /// </remarks>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The cross product of the two vectors.</returns>
    public static float Cross(Vector2 left, Vector2 right) => (left.X * right.Y) - (left.Y * right.X);

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
    /// <param name="from">The starting vector.</param>
    /// <param name="to">The ending vector.</param>
    /// <param name="interpolationFactor">The interpolation factor between 0 and 1.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector2 Lerp(Vector2 from, Vector2 to, float interpolationFactor)
    {
        interpolationFactor = Math.Clamp(interpolationFactor, 0f, 1f);
        return from + ((to - from) * interpolationFactor);
    }

    /// <summary>
    /// Computes the linear interpolation between two vectors with an unclamped interpolation factor.
    /// </summary>
    /// <param name="from">The starting vector.</param>
    /// <param name="to">The ending vector.</param>
    /// <param name="interpolationFactor">The interpolation factor.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector2 LerpUnclamped(Vector2 from, Vector2 to, float interpolationFactor)
        => from + ((to - from) * interpolationFactor);

    /// <summary>
    /// Determines the component-wise maximum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The component-wise maximum vector.</returns>
    public static Vector2 Max(Vector2 left, Vector2 right)
        => new(MathF.Max(left.X, right.X), MathF.Max(left.Y, right.Y));

    /// <summary>
    /// Determines the component-wise minimum of two vectors.
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
        Vector2 direction = target - current;
        float distance = direction.Magnitude;

        if (distance <= maxDistanceDelta || MathF.ApproximatelyZero(distance))
            return target;

        return current + (direction / distance * maxDistanceDelta);
    }

    /// <summary>
    /// Reflects a vector off a surface defined by a normal vector.
    /// </summary>
    /// <remarks>
    /// The normal vector should be normalized (unit length) for accurate results.
    /// If the normal vector is not normalized, it will be normalized internally.
    /// </remarks>
    /// <param name="vector">The vector to reflect.</param>
    /// <param name="normal">The normal vector of the surface.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector2 Reflect(Vector2 vector, Vector2 normal)
    {
        Vector2 normalized = normal.Normalized;
        return vector - (2f * Dot(vector, normalized) * normalized);
    }

    /// <summary>
    /// Rotates a vector by a given angle.
    /// </summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <param name="angle">The angle to rotate by.</param>
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
    /// Scales a vector by another vector.
    /// </summary>
    /// <param name="vector">The vector to scale.</param>
    /// <param name="scale">The scale vector.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector2 Scale(Vector2 vector, Vector2 scale) => vector * scale;

    /// <summary>
    /// Determines whether this vector is equal to another vector.
    /// </summary>
    /// <param name="other">The vector to compare with the current vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2 other)
    {
        return MathF.ApproximatelyEquals(X, other.X)
            && MathF.ApproximatelyEquals(Y, other.Y);
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Vector2 other && Equals(other);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

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
}

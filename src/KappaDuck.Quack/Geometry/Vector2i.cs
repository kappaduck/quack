// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents an integer two-dimensional vector.
/// </summary>
/// <param name="x">The x-coordinate of the vector.</param>
/// <param name="y">The y-coordinate of the vector.</param>

[StructLayout(LayoutKind.Auto)]
public struct Vector2i(int x, int y) :
    IAdditionOperators<Vector2i, Vector2i, Vector2i>,
    ISubtractionOperators<Vector2i, Vector2i, Vector2i>,
    IMultiplyOperators<Vector2i, Vector2i, Vector2i>,
    IMultiplyOperators<Vector2i, int, Vector2i>,
    IDivisionOperators<Vector2i, int, Vector2i>,
    IUnaryNegationOperators<Vector2i, Vector2i>,
    IEquatable<Vector2i>,
    ISpanFormattable,
    IUtf8SpanFormattable
{
    /// <summary>
    /// Creates a zero vector (0, 0).
    /// </summary>
    public Vector2i() : this(0, 0)
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
    /// Gets the squared magnitude.
    /// </summary>
    /// <remarks>
    /// It's more efficient to use the squared magnitude when comparing vector lengths
    /// instead of using the actual magnitude, as it avoids the computational cost of a square root operation.
    /// </remarks>
    public readonly int MagnitudeSquared => (X * X) + (Y * Y);

    /// <summary>
    /// Gets the magnitude (length) of the vector.
    /// </summary>
    /// <remarks>
    /// If you only need to compare vector lengths, consider using <see cref="MagnitudeSquared"/> instead.
    /// </remarks>
    public readonly float Magnitude => MathF.Sqrt(MagnitudeSquared);

    /// <summary>
    /// Gets the normalized vector as a <see cref="Vector2"/>.
    /// </summary>
    /// <remarks>
    /// Integer normalization is always lossy, so this returns a <see cref="Vector2"/> (float).
    /// </remarks>
    public readonly Vector2 Normalized => ToVector2().Normalized;

    /// <summary>
    /// Gets the left perpendicular vector.
    /// </summary>
    public readonly Vector2i LeftPerpendicular => new(-Y, X);

    /// <summary>
    /// Gets the right perpendicular vector.
    /// </summary>
    public readonly Vector2i RightPerpendicular => new(Y, -X);

    /// <summary>
    /// Unit vector pointing downwards in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corner.
    /// </remarks>
    public static Vector2i Down { get; } = new(0, 1);

    /// <summary>
    /// Unit vector pointing to the left in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corner.
    /// </remarks>
    public static Vector2i Left { get; } = new(-1, 0);

    /// <summary>
    /// Unit vector pointing to the right in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corner.
    /// </remarks>
    public static Vector2i Right { get; } = new(1, 0);

    /// <summary>
    /// Unit vector pointing upwards in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corner.
    /// </remarks>
    public static Vector2i Up { get; } = new(0, -1);

    /// <summary>
    /// Gets the zero vector (0, 0).
    /// </summary>
    public static Vector2i Zero { get; } = new(0, 0);

    /// <summary>
    /// Clamps to a maximum length.
    /// </summary>
    /// <param name="maxLength">The maximum length to clamp the vector to.</param>
    /// <returns>The clamped vector.</returns>
    public readonly Vector2i Clamp(int maxLength) => Clamp(this, maxLength);

    /// <summary>
    /// Computes the cross product of this vector and another vector.
    /// </summary>
    /// <remarks>
    /// In screen space (top-left origin), a positive value means <paramref name="other"/>
    /// is clockwise from this vector, and a negative value means it is counter-clockwise.
    /// </remarks>
    /// <param name="other">The other vector.</param>
    /// <returns>The cross product of the two vectors.</returns>
    public readonly int Cross(Vector2i other) => Cross(this, other);

    /// <summary>
    /// Deconstructs the vector into its components.
    /// </summary>
    /// <param name="x">The x-coordinate.</param>
    /// <param name="y">The y-coordinate.</param>
    public readonly void Deconstruct(out int x, out int y) => (x, y) = (X, Y);

    /// <summary>
    /// Computes the distance between this vector and another vector.
    /// </summary>
    /// <param name="to">The other vector.</param>
    /// <returns>The distance between the two vectors as a <see cref="float"/>.</returns>
    public readonly float Distance(Vector2i to) => Distance(this, to);

    /// <summary>
    /// Computes the dot product of this vector and another vector.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public readonly int Dot(Vector2i other) => Dot(this, other);

    /// <summary>
    /// Moves this vector towards a target vector by a maximum number of steps.
    /// </summary>
    /// <param name="target">The target vector.</param>
    /// <param name="maxDistanceDelta">The maximum number of steps to move.</param>
    /// <returns>The moved vector towards the target.</returns>
    public readonly Vector2i MoveTowards(Vector2i target, int maxDistanceDelta) => MoveTowards(this, target, maxDistanceDelta);

    /// <summary>
    /// Clamps the vector to a maximum length.
    /// </summary>
    /// <param name="vector">The vector to clamp.</param>
    /// <param name="maxLength">The maximum length to clamp the vector to.</param>
    /// <returns>The clamped vector.</returns>
    public static Vector2i Clamp(Vector2i vector, int maxLength)
    {
        return vector.MagnitudeSquared > maxLength * maxLength
            ? (vector.Normalized * maxLength).Truncate()
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
    public static int Cross(Vector2i left, Vector2i right) => (left.X * right.Y) - (left.Y * right.X);

    /// <summary>
    /// Computes the distance between two vectors.
    /// </summary>
    /// <param name="from">The first vector to measure from.</param>
    /// <param name="to">The second vector to measure to.</param>
    /// <returns>The distance between the two vectors as a <see cref="float"/>.</returns>
    public static float Distance(Vector2i from, Vector2i to) => (to - from).Magnitude;

    /// <summary>
    /// Computes the dot product of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public static int Dot(Vector2i left, Vector2i right) => (left.X * right.X) + (left.Y * right.Y);

    /// <summary>
    /// Determines the component-wise maximum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The component-wise maximum vector.</returns>
    public static Vector2i Max(Vector2i left, Vector2i right)
        => new(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y));

    /// <summary>
    /// Determines the component-wise minimum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The component-wise minimum vector.</returns>
    public static Vector2i Min(Vector2i left, Vector2i right)
        => new(Math.Min(left.X, right.X), Math.Min(left.Y, right.Y));

    /// <summary>
    /// Moves a vector towards a target vector by a maximum number of steps.
    /// </summary>
    /// <remarks>
    /// Unlike the float equivalent, movement is done in integer steps along the displacement direction.
    /// </remarks>
    /// <param name="current">The current vector.</param>
    /// <param name="target">The target vector.</param>
    /// <param name="maxDistanceDelta">The maximum number of steps to move.</param>
    /// <returns>The moved vector towards the target.</returns>
    public static Vector2i MoveTowards(Vector2i current, Vector2i target, int maxDistanceDelta)
    {
        Vector2i direction = target - current;
        int distanceSquared = direction.MagnitudeSquared;

        if (distanceSquared == 0 || distanceSquared <= maxDistanceDelta * maxDistanceDelta)
            return target;

        float distance = MathF.Sqrt(distanceSquared);
        return current + (direction.ToVector2() / distance * maxDistanceDelta).Truncate();
    }

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i Add(Vector2i left, Vector2i right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Subtracts one vector from another.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i Subtract(Vector2i left, Vector2i right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Multiplies two vectors component-wise.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i Multiply(Vector2i left, Vector2i right) => new(left.X * right.X, left.Y * right.Y);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to multiply by.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i Multiply(Vector2i left, int right) => new(left.X * right, left.Y * right);

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to divide by.</param>
    /// <returns>The resulting vector.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the vector is divided by zero.</exception>
    public static Vector2i Divide(Vector2i left, int right)
    {
        Math.ThrowIfDividedByZero(right);
        return new Vector2i(left.X / right, left.Y / right);
    }

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    public static Vector2i Negate(Vector2i value) => new(-value.X, -value.Y);

    /// <summary>
    /// Determines whether this vector is equal to another vector.
    /// </summary>
    /// <param name="other">The vector to compare with the current vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2i other) => X == other.X && Y == other.Y;

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Vector2i other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <summary>
    /// Converts the vector to a <see cref="Vector2"/>.
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
    /// Implicitly converts a <see cref="Vector2i"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    public static implicit operator Vector2(Vector2i value) => new(value.X, value.Y);

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i operator +(Vector2i left, Vector2i right) => Add(left, right);

    /// <summary>
    /// Subtracts one vector from another.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i operator -(Vector2i left, Vector2i right) => Subtract(left, right);

    /// <summary>
    /// Multiplies two vectors component-wise.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i operator *(Vector2i left, Vector2i right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to multiply by.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i operator *(Vector2i left, int right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The scalar to multiply by.</param>
    /// <param name="right">The vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2i operator *(int left, Vector2i right) => Multiply(right, left);

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to divide by.</param>
    /// <returns>The resulting vector using integer division (truncates toward zero).</returns>
    /// <exception cref="DivideByZeroException">Thrown when the vector is divided by zero.</exception>
    public static Vector2i operator /(Vector2i left, int right) => Divide(left, right);

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    public static Vector2i operator -(Vector2i value) => Negate(value);

    /// <summary>
    /// Determines whether two vectors are equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector2i left, Vector2i right) => left.Equals(right);

    /// <summary>
    /// Determines whether two vectors are not equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vector2i left, Vector2i right) => !(left == right);
}

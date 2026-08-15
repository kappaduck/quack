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
public struct Vector2I(int x, int y) :
    IAdditionOperators<Vector2I, Vector2I, Vector2I>,
    ISubtractionOperators<Vector2I, Vector2I, Vector2I>,
    IMultiplyOperators<Vector2I, Vector2I, Vector2I>,
    IMultiplyOperators<Vector2I, int, Vector2I>,
    IDivisionOperators<Vector2I, int, Vector2I>,
    IUnaryNegationOperators<Vector2I, Vector2I>,
    IEquatable<Vector2I>,
    ISpanFormattable,
    IUtf8SpanFormattable
{
    /// <summary>
    /// Creates a zero vector (0, 0).
    /// </summary>
    public Vector2I() : this(0, 0)
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
    /// Integer normalization is always loss, so this returns a <see cref="Vector2"/> (float).
    /// </remarks>
    public readonly Vector2 Normalized => ToVector2().Normalized;

    /// <summary>
    /// Gets the left perpendicular vector.
    /// </summary>
    public readonly Vector2I LeftPerpendicular => new(-Y, X);

    /// <summary>
    /// Gets the right perpendicular vector.
    /// </summary>
    public readonly Vector2I RightPerpendicular => new(Y, -X);

    /// <summary>
    /// Unit vector pointing downwards in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corner.
    /// </remarks>
    public static Vector2I Down { get; } = new(0, 1);

    /// <summary>
    /// Unit vector pointing to the left in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corner.
    /// </remarks>
    public static Vector2I Left { get; } = new(-1, 0);

    /// <summary>
    /// Unit vector pointing to the right in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corner.
    /// </remarks>
    public static Vector2I Right { get; } = new(1, 0);

    /// <summary>
    /// Unit vector pointing upwards in a 2D coordinate system.
    /// </summary>
    /// <remarks>
    /// The origin is at the top-left corner.
    /// </remarks>
    public static Vector2I Up { get; } = new(0, -1);

    /// <summary>
    /// Gets the zero vector (0, 0).
    /// </summary>
    public static Vector2I Zero { get; } = new(0, 0);

    /// <summary>
    /// Clamps to a maximum length.
    /// </summary>
    /// <param name="maxLength">The maximum length to clamp the vector to.</param>
    /// <returns>The clamped vector.</returns>
    public readonly Vector2I Clamp(int maxLength) => Clamp(this, maxLength);

    /// <summary>
    /// Computes the cross product of this vector and another vector.
    /// </summary>
    /// <remarks>
    /// In screen space (top-left origin), a positive value means <paramref name="other"/>
    /// is clockwise from this vector, and a negative value means it is counter-clockwise.
    /// </remarks>
    /// <param name="other">The other vector.</param>
    /// <returns>The cross product of the two vectors.</returns>
    public readonly int Cross(Vector2I other) => Cross(this, other);

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
    public readonly float Distance(Vector2I to) => Distance(this, to);

    /// <summary>
    /// Computes the dot product of this vector and another vector.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public readonly int Dot(Vector2I other) => Dot(this, other);

    /// <summary>
    /// Moves this vector towards a target vector by a maximum number of steps.
    /// </summary>
    /// <param name="target">The target vector.</param>
    /// <param name="maxDistanceDelta">The maximum number of steps to move.</param>
    /// <returns>The moved vector towards the target.</returns>
    public readonly Vector2I MoveTowards(Vector2I target, int maxDistanceDelta) => MoveTowards(this, target, maxDistanceDelta);

    /// <summary>
    /// Clamps the vector to a maximum length.
    /// </summary>
    /// <param name="vector">The vector to clamp.</param>
    /// <param name="maxLength">The maximum length to clamp the vector to.</param>
    /// <returns>The clamped vector.</returns>
    public static Vector2I Clamp(Vector2I vector, int maxLength)
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
    public static int Cross(Vector2I left, Vector2I right) => (left.X * right.Y) - (left.Y * right.X);

    /// <summary>
    /// Computes the distance between two vectors.
    /// </summary>
    /// <param name="from">The first vector to measure from.</param>
    /// <param name="to">The second vector to measure to.</param>
    /// <returns>The distance between the two vectors as a <see cref="float"/>.</returns>
    public static float Distance(Vector2I from, Vector2I to) => (to - from).Magnitude;

    /// <summary>
    /// Computes the dot product of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public static int Dot(Vector2I left, Vector2I right) => (left.X * right.X) + (left.Y * right.Y);

    /// <summary>
    /// Determines the component-wise maximum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The component-wise maximum vector.</returns>
    public static Vector2I Max(Vector2I left, Vector2I right)
        => new(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y));

    /// <summary>
    /// Determines the component-wise minimum of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The component-wise minimum vector.</returns>
    public static Vector2I Min(Vector2I left, Vector2I right)
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
    public static Vector2I MoveTowards(Vector2I current, Vector2I target, int maxDistanceDelta)
    {
        Vector2I direction = target - current;
        int distanceSquared = direction.MagnitudeSquared;

        if (distanceSquared == 0 || distanceSquared <= maxDistanceDelta * maxDistanceDelta)
            return target;

        float distance = MathF.Sqrt(distanceSquared);
        return current + (direction.ToVector2() / distance * maxDistanceDelta).Truncate();
    }

    /// <summary>
    /// Determines whether this vector is equal to another vector.
    /// </summary>
    /// <param name="other">The vector to compare with the current vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Vector2I other) => X == other.X && Y == other.Y;

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Vector2I other && Equals(other);

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
    /// Adds two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2I operator +(Vector2I left, Vector2I right) => new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Subtracts one vector from another.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2I operator -(Vector2I left, Vector2I right) => new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Multiplies two vectors component-wise.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2I operator *(Vector2I left, Vector2I right) => new(left.X * right.X, left.Y * right.Y);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to multiply by.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2I operator *(Vector2I left, int right) => new(left.X * right, left.Y * right);

    /// <summary>
    /// Multiplies a vector by a scalar.
    /// </summary>
    /// <param name="left">The scalar to multiply by.</param>
    /// <param name="right">The vector.</param>
    /// <returns>The resulting vector.</returns>
    public static Vector2I operator *(int left, Vector2I right) => right * left;

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="left">The vector.</param>
    /// <param name="right">The scalar to divide by.</param>
    /// <returns>The resulting vector using integer division (truncates toward zero).</returns>
    /// <exception cref="DivideByZeroException">Thrown when the vector is divided by zero.</exception>
    public static Vector2I operator /(Vector2I left, int right)
    {
        Math.ThrowIfDividedByZero(right);
        return new Vector2I(left.X / right, left.Y / right);
    }

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    public static Vector2I operator -(Vector2I value) => new(-value.X, -value.Y);

    /// <summary>
    /// Determines whether two vectors are equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Vector2I left, Vector2I right) => left.Equals(right);

    /// <summary>
    /// Determines whether two vectors are not equal.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns><see langword="true"/> if the vectors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Vector2I left, Vector2I right) => !(left == right);
}

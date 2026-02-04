// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;

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
    IUnaryNegationOperators<Vector2, Vector2>
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
    public readonly bool IsNormalized => MathF.ApproximatelyEqual(MagnitudeSquared, 1f);

    /// <summary>
    /// Gets a value indicating whether is a zero vector.
    /// </summary>
    public readonly bool IsZero => MagnitudeSquared <= float.Epsilon * float.Epsilon;

    /// <summary>
    /// Gets the magnitude (length) of the vector.
    /// </summary>
    /// <remarks>
    /// If you only need to compare vector lengths, consider using <see cref="MagnitudeSquared"/> instead
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
}

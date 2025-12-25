// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents an angle measurement in geometry.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly struct Angle :
    IAdditionOperators<Angle, Angle, Angle>,
    ISubtractionOperators<Angle, Angle, Angle>,
    IMultiplyOperators<Angle, float, Angle>,
    IDivisionOperators<Angle, float, Angle>,
    IUnaryNegationOperators<Angle, Angle>,
    IComparable,
    IComparable<Angle>,
    IEquatable<Angle>,
    ISpanFormattable
{
    private Angle(float radians) => Radians = radians;

    /// <summary>
    /// Gets the angle in radians.
    /// </summary>
    public float Radians { get; }

    /// <summary>
    /// Gets the angle in degrees.
    /// </summary>
    public float Degrees => (float)(Radians * (180f / Math.PI));

    /// <summary>
    /// Gets the computed sine of the angle.
    /// </summary>
    public float Sin => MathF.Sin(Radians);

    /// <summary>
    /// Gets the computed cosine of the angle.
    /// </summary>
    public float Cos => MathF.Cos(Radians);

    /// <summary>
    /// Gets the computed tangent of the angle.
    /// </summary>
    public float Tan => MathF.Tan(Radians);

    /// <summary>
    /// Represents an angle of zero radians (0 degrees).
    /// </summary>
    public static Angle Zero { get; } = new(0f);

    /// <summary>
    /// Implicitly converts a float to an angle in radians.
    /// </summary>
    /// <param name="radians">The angle in radians.</param>
    public static implicit operator Angle(float radians) => new(radians);

    /// <summary>
    /// Explicitly converts an angle to a float representing the angle in radians.
    /// </summary>
    /// <param name="angle">The angle.</param>
    public static explicit operator float(Angle angle) => angle.Radians;

    /// <summary>
    /// Adds two angles together.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle operator +(Angle left, Angle right) => new(left.Radians + right.Radians);

    /// <summary>
    /// Subtracts one angle from another.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle operator -(Angle left, Angle right) => new(left.Radians - right.Radians);

    /// <summary>
    /// Multiplies an angle by a scalar.
    /// </summary>
    /// <param name="left">The angle.</param>
    /// <param name="right">The scalar to multiply by.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle operator *(Angle left, float right) => new(left.Radians * right);

    /// <summary>
    /// Multiplies an angle by a scalar.
    /// </summary>
    /// <param name="left">The scalar to multiply by.</param>
    /// <param name="right">The angle.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle operator *(float left, Angle right) => right * left;

    /// <summary>
    /// Divides an angle by a scalar.
    /// </summary>
    /// <param name="left">The angle.</param>
    /// <param name="right">The scalar to divide by.</param>
    /// <returns>The resulting angle.</returns>
    /// <exception cref="DivideByZeroException">The angle is divided by zero.</exception>
    public static Angle operator /(Angle left, float right)
    {
        Math.ThrowIfDividedByZero(right);

        return new Angle(left.Radians / right);
    }

    /// <summary>
    /// Negates the angle.
    /// </summary>
    /// <param name="value">The angle to negate.</param>
    /// <returns>The negated angle.</returns>
    public static Angle operator -(Angle value) => new(-value.Radians);

    /// <summary>
    /// Determines whether two angles are equal.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><see langword="true"/> if the angles are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Angle left, Angle right) => left.Equals(right);

    /// <summary>
    /// Determines whether two angles are not equal.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><see langword="true"/> if the angles are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Angle left, Angle right) => !(left == right);

    /// <summary>
    /// Determines whether the left angle is less than the right angle.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><see langword="true"/> if the left angle is lesser than the right angle; otherwise, <see langword="false"/>.</returns>
    public static bool operator <(Angle left, Angle right) => left.Radians < right.Radians;

    /// <summary>
    /// Determines whether the left angle is greater than the right angle.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><see langword="true"/> if the left angle is greater than the right angle; otherwise, <see langword="false"/>.</returns>
    public static bool operator >(Angle left, Angle right) => left.Radians > right.Radians;

    /// <summary>
    /// Determines whether the left angle is less than or equal to the right angle.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><see langword="true"/> if the left angle is lesser than or equal to the right angle; otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(Angle left, Angle right) => left.Radians <= right.Radians;

    /// <summary>
    /// Determines whether the left angle is greater than or equal to the right angle.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><see langword="true"/> if the left angle is greater than or equal to the right angle; otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(Angle left, Angle right) => left.Radians >= right.Radians;

    /// <summary>
    /// Creates an angle from degrees.
    /// </summary>
    /// <param name="degrees">The angle in degrees.</param>
    /// <returns>The angle.</returns>
    public static Angle FromDegrees(float degrees) => new((float)(degrees * Math.PI / 180f));

    /// <summary>
    /// Creates an angle from radians.
    /// </summary>
    /// <param name="radians">The angle in radians.</param>
    /// <returns>The angle.</returns>
    public static Angle FromRadians(float radians) => new(radians);

    /// <summary>
    /// Normalizes the angle to be within the specified range.
    /// </summary>
    /// <remarks>
    /// By default, the angle is normalized to be within the range of 0 to 360 degrees.
    /// </remarks>
    /// <param name="min">The minimum value of the range.</param>
    /// <param name="max">The maximum value of the range.</param>
    /// <returns>The normalized angle.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public Angle Normalize(float min = 0f, float max = 360f)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);

        float range = max - min;
        return FromDegrees(((((Degrees - min) % range) + range) % range) + min);
    }

    /// <inheritdoc/>
    public int CompareTo(Angle other) => Radians.CompareTo(other.Radians);

    /// <inheritdoc/>
    public int CompareTo(object? obj) => obj switch
    {
        null => 1,
        Angle x => CompareTo(x),
        _ => throw new ArgumentException("Object must be of type Angle", nameof(obj))
    };

    /// <summary>
    /// Determines whether the angle is equal to another angle.
    /// </summary>
    /// <param name="other">The other angle.</param>
    /// <returns><see langword="true"/> if the angles are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Angle other) => MathF.IsNearlyZero(Radians - other.Radians);

    /// <summary>
    /// Determines whether the angle is equal to another object.
    /// </summary>
    /// <param name="obj">The other object.</param>
    /// <returns><see langword="true"/> if the object is an angle and is equal to this angle; otherwise, <see langword="false"/>.</returns>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Angle angle && Equals(angle);

    /// <inheritdoc/>
    public override int GetHashCode() => Radians.GetHashCode();

    /// <summary>
    /// The string representation of the angle in degrees.
    /// </summary>
    /// <returns>The string representation of the angle.</returns>
    public override string ToString() => $"{this}";

    /// <inheritdoc/>
    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"{Degrees}°", out charsWritten);
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents an angle in a 2D mathematical context.
/// Used for directional computation, rotations and trigonometric operations.
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
    ISpanFormattable,
    IUtf8SpanFormattable
{
    private Angle(float radians) => Radians = radians;

    /// <summary>
    /// Gets the angle in radians
    /// </summary>
    public float Radians { get; }

    /// <summary>
    /// Gets the angle in degrees.
    /// </summary>
    public float Degrees => (float)double.RadiansToDegrees(Radians);

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
    /// Gets an angle of zero radians (0 degrees).
    /// </summary>
    public static Angle Zero { get; } = new(0f);

    /// <inheritdoc/>
    public int CompareTo(Angle other) => Radians.CompareTo(other.Radians);

    /// <inheritdoc/>
    public int CompareTo(object? obj) => obj switch
    {
        null => 1,
        Angle angle => CompareTo(angle),
        _ => throw new ArgumentException("Object is not an Angle.", nameof(obj))
    };

    /// <inheritdoc/>
    public bool Equals(Angle other) => MathF.ApproximatelyEquals(Radians, other.Radians);

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Angle angle && Equals(angle);

    /// <inheritdoc/>
    public override int GetHashCode() => Radians.GetHashCode();

    /// <summary>
    /// The string representation of the angle in degrees.
    /// </summary>
    /// <returns>The string representation of the angle.</returns>
    public override string ToString() => $"{this}";

    /// <summary>
    /// The string representation of the angle with formatting.
    /// </summary>
    /// <remarks>
    /// Use <c>"R"</c> or <c>"r"</c> as the format string to get the angle in radians.
    /// Otherwise, the angle is represented in degrees.
    /// </remarks>
    /// <param name="format">The format string.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>The string representation of the angle.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format is "R" or "r")
            return $"{Radians} rad";

        return ToString();
    }

    /// <summary>
    /// Add two angles.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle Add(Angle left, Angle right) => new(left.Radians + right.Radians);

    /// <summary>
    /// Subtracts one angle from another.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle Subtract(Angle left, Angle right) => new(left.Radians - right.Radians);

    /// <summary>
    /// Multiplies an angle by a scalar.
    /// </summary>
    /// <param name="left">The angle.</param>
    /// <param name="right">The scalar to multiply by.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle Multiply(Angle left, float right) => new(left.Radians * right);

    /// <summary>
    /// Divides an angle by a scalar.
    /// </summary>
    /// <param name="left">The angle.</param>
    /// <param name="right">The scalar to divide by.</param>
    /// <returns>The resulting angle.</returns>
    /// <exception cref="DivideByZeroException">Thrown when attempting to divide by zero.</exception>
    public static Angle Divide(Angle left, float right)
    {
        Math.ThrowIfDividedByZero(right);
        return new(left.Radians / right);
    }

    /// <summary>
    /// Negates an angle.
    /// </summary>
    /// <param name="value">The angle to negate.</param>
    /// <returns>The negated angle.</returns>
    public static Angle Negate(Angle value) => new(-value.Radians);

    /// <summary>
    /// Creates an angle from degrees.
    /// </summary>
    /// <param name="degrees">The angle in degrees.</param>
    /// <returns>The angle.</returns>
    public static Angle FromDegrees(float degrees) => new(float.CreateTruncating(double.DegreesToRadians(degrees)));

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
    [ExcludeFromCodeCoverage]
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite(provider, $"{Degrees}°", out charsWritten);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"{Degrees}°", out bytesWritten);

    /// <summary>
    /// Add two angles.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle operator +(Angle left, Angle right) => Add(left, right);

    /// <summary>
    /// Subtracts one angle from another.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle operator -(Angle left, Angle right) => Subtract(left, right);

    /// <summary>
    /// Multiplies an angle by a scalar.
    /// </summary>
    /// <param name="left">The angle.</param>
    /// <param name="right">The scalar to multiply by.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle operator *(Angle left, float right) => Multiply(left, right);

    /// <summary>
    /// Multiplies an angle by a scalar.
    /// </summary>
    /// <param name="left">The scalar to multiply by.</param>
    /// <param name="right">The angle.</param>
    /// <returns>The resulting angle.</returns>
    public static Angle operator *(float left, Angle right) => Multiply(right, left);

    /// <summary>
    /// Divides an angle by a scalar.
    /// </summary>
    /// <param name="left">The angle.</param>
    /// <param name="right">The scalar to divide by.</param>
    /// <returns>The resulting angle.</returns>
    /// <exception cref="DivideByZeroException">Thrown when attempting to divide by zero.</exception>
    public static Angle operator /(Angle left, float right) => Divide(left, right);

    /// <summary>
    /// Negates an angle.
    /// </summary>
    /// <param name="value">The angle to negate.</param>
    /// <returns>The negated angle.</returns>
    public static Angle operator -(Angle value) => Negate(value);

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
    /// <returns><see langword="true"/> if the left angle is less than the right angle; otherwise, <see langword="false"/>.</returns>
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
    /// <returns><see langword="true"/> if the left angle is less than or equal to the right angle; otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(Angle left, Angle right) => left.Radians <= right.Radians;

    /// <summary>
    /// Determines whether the left angle is greater than or equal to the right angle.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><see langword="true"/> if the left angle is greater than or equal to the right angle; otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(Angle left, Angle right) => left.Radians >= right.Radians;
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a floating point size defined by a width and height.
/// </summary>
/// <param name="width">The width component of the size.</param>
/// <param name="height">The height component of the size.</param>
public struct Size(float width, float height) : IEquatable<Size>, IEqualityOperators<Size, Size, bool>, ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary>
    /// Creates a zero size (0, 0).
    /// </summary>
    public Size() : this(0f, 0f)
    {
    }

    /// <summary>
    /// Gets or sets the width component.
    /// </summary>
    public float Width { get; set; } = width;

    /// <summary>
    /// Gets or sets the height component.
    /// </summary>
    public float Height { get; set; } = height;

    /// <summary>
    /// Gets the area represented by this size.
    /// </summary>
    public readonly float Area => Width * Height;

    /// <summary>
    /// Gets a value indicating whether the size is empty (either dimension is approximately zero).
    /// </summary>
    public readonly bool IsEmpty => MathF.ApproximatelyZero(Width) || MathF.ApproximatelyZero(Height);

    /// <summary>
    /// Gets a size with all components set to zero.
    /// </summary>
    public static Size Zero { get; } = new(0f, 0f);

    /// <summary>
    /// Returns the largest <see cref="SizeI"/> whose components are less than or equal to those of this size.
    /// </summary>
    public readonly SizeI Floor() => new((int)MathF.Floor(Width), (int)MathF.Floor(Height));

    /// <summary>
    /// Returns the <see cref="SizeI"/> whose components are the nearest integer to those of this size.
    /// </summary>
    public readonly SizeI Round() => new((int)MathF.Round(Width), (int)MathF.Round(Height));

    /// <summary>
    /// Returns the <see cref="SizeI"/> whose components are truncated toward zero from those of this size.
    /// </summary>
    public readonly SizeI Truncate() => new((int)Width, (int)Height);

    /// <summary>
    /// Deconstructs the size into its width and height components.
    /// </summary>
    /// <param name="width">The width component.</param>
    /// <param name="height">The height component.</param>
    public readonly void Deconstruct(out float width, out float height) => (width, height) = (Width, Height);

    /// <summary>
    /// Determines whether this size is equal to another size.
    /// </summary>
    /// <param name="other">The size to compare with the current size.</param>
    /// <returns><see langword="true"/> if the sizes are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Size other)
        => MathF.ApproximatelyEquals(Width, other.Width) && MathF.ApproximatelyEquals(Height, other.Height);

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Size other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(Width, Height);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({Width}, {Height})", out charsWritten);

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"({Width}, {Height})", out bytesWritten);

    /// <summary>
    /// Explicitly converts a <see cref="Size"/> to a <see cref="SizeI"/> by truncating components.
    /// </summary>
    /// <remarks>
    /// Use <see cref="Floor"/>, <see cref="Round"/>, or <see cref="Truncate"/> for explicit rounding control.
    /// </remarks>
    /// <param name="value">The size to convert.</param>
    public static explicit operator SizeI(Size value) => value.Truncate();

    /// <summary>
    /// Determines whether two sizes are equal.
    /// </summary>
    /// <param name="left">The left size.</param>
    /// <param name="right">The right size.</param>
    /// <returns><see langword="true"/> if the sizes are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Size left, Size right) => left.Equals(right);

    /// <summary>
    /// Determines whether two sizes are not equal.
    /// </summary>
    /// <param name="left">The left size.</param>
    /// <param name="right">The right size.</param>
    /// <returns><see langword="true"/> if the sizes are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Size left, Size right) => !(left == right);
}

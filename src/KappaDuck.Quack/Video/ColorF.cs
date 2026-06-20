// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Video;

/// <summary>
/// Represents an RGBA color with floating-point channels, normally in the range 0-1.
/// </summary>
/// <remarks>
/// Channels are not clamped, so values outside the 0-1 range are allowed to support
/// high-dynamic-range and wide-gamut workflows. Use <see cref="Color"/> for the common
/// 8-bit-per-channel representation.
/// </remarks>
/// <param name="r">The red channel (normally 0-1).</param>
/// <param name="g">The green channel (normally 0-1).</param>
/// <param name="b">The blue channel (normally 0-1).</param>
/// <param name="a">The alpha channel (normally 0-1), where 0 is fully transparent and 1 is fully opaque.</param>
[StructLayout(LayoutKind.Sequential)]
public struct ColorF(float r, float g, float b, float a) :
    IEqualityOperators<ColorF, ColorF, bool>,
    IEquatable<ColorF>,
    ISpanFormattable,
    IUtf8SpanFormattable
{
    /// <summary>
    /// Creates a fully transparent black color (0, 0, 0, 0).
    /// </summary>
    public ColorF() : this(0f, 0f, 0f, 0f)
    {
    }

    /// <summary>
    /// Creates an opaque color from red, green and blue channels with the alpha channel set to 1.
    /// </summary>
    /// <param name="r">The red channel (normally 0-1).</param>
    /// <param name="g">The green channel (normally 0-1).</param>
    /// <param name="b">The blue channel (normally 0-1).</param>
    public ColorF(float r, float g, float b) : this(r, g, b, 1f)
    {
    }

    /// <summary>
    /// Gets or sets the red channel (normally 0-1).
    /// </summary>
    public float R { get; set; } = r;

    /// <summary>
    /// Gets or sets the green channel (normally 0-1).
    /// </summary>
    public float G { get; set; } = g;

    /// <summary>
    /// Gets or sets the blue channel (normally 0-1).
    /// </summary>
    public float B { get; set; } = b;

    /// <summary>
    /// Gets or sets the alpha channel (normally 0-1), where 0 is fully transparent and 1 is fully opaque.
    /// </summary>
    public float A { get; set; } = a;

    /// <summary>
    /// Gets a fully transparent black color (0, 0, 0, 0).
    /// </summary>
    public static ColorF Transparent { get; } = new(0f, 0f, 0f, 0f);

    /// <summary>
    /// Gets an opaque black color (0, 0, 0, 1).
    /// </summary>
    public static ColorF Black { get; } = new(0f, 0f, 0f);

    /// <summary>
    /// Gets an opaque white color (1, 1, 1, 1).
    /// </summary>
    public static ColorF White { get; } = new(1f, 1f, 1f);

    /// <summary>
    /// Linearly interpolates between two colors.
    /// </summary>
    /// <param name="from">The starting color.</param>
    /// <param name="to">The ending color.</param>
    /// <param name="amount">The interpolation factor, clamped between 0 and 1.</param>
    /// <returns>The interpolated color.</returns>
    public static ColorF Lerp(ColorF from, ColorF to, float amount)
    {
        amount = Math.Clamp(amount, 0f, 1f);

        float r = from.R + ((to.R - from.R) * amount);
        float g = from.G + ((to.G - from.G) * amount);
        float b = from.B + ((to.B - from.B) * amount);
        float a = from.A + ((to.A - from.A) * amount);

        return new ColorF(r, g, b, a);
    }

    /// <summary>
    /// Deconstructs the color into its channels.
    /// </summary>
    /// <param name="r">The red channel.</param>
    /// <param name="g">The green channel.</param>
    /// <param name="b">The blue channel.</param>
    /// <param name="a">The alpha channel.</param>
    public readonly void Deconstruct(out float r, out float g, out float b, out float a)
        => (r, g, b, a) = (R, G, B, A);

    /// <summary>
    /// Determines whether this color is equal to another color.
    /// </summary>
    /// <param name="other">The color to compare with the current color.</param>
    /// <returns><see langword="true"/> if the colors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(ColorF other) => MathF.ApproximatelyEquals(R, other.R)
                                                 && MathF.ApproximatelyEquals(G, other.G)
                                                 && MathF.ApproximatelyEquals(B, other.B)
                                                 && MathF.ApproximatelyEquals(A, other.A);

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is ColorF other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(R, G, B, A);

    /// <summary>
    /// Converts the color to a <see cref="Color"/> by clamping each channel to the range 0-1 and scaling to 0-255.
    /// </summary>
    /// <returns>The converted color.</returns>
    public readonly Color ToColor()
    {
        byte red = (byte)float.Round(Math.Clamp(R, 0f, 1f) * 255f);
        byte green = (byte)float.Round(Math.Clamp(G, 0f, 1f) * 255f);
        byte blue = (byte)float.Round(Math.Clamp(B, 0f, 1f) * 255f);
        byte alpha = (byte)float.Round(Math.Clamp(A, 0f, 1f) * 255f);

        return new Color(red, green, blue, alpha);
    }

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({R}, {G}, {B}, {A})", out charsWritten);

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"({R}, {G}, {B}, {A})", out bytesWritten);

    /// <summary>
    /// Determines whether two colors are equal.
    /// </summary>
    /// <param name="left">The left color.</param>
    /// <param name="right">The right color.</param>
    /// <returns><see langword="true"/> if the colors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(ColorF left, ColorF right) => left.Equals(right);

    /// <summary>
    /// Determines whether two colors are not equal.
    /// </summary>
    /// <param name="left">The left color.</param>
    /// <param name="right">The right color.</param>
    /// <returns><see langword="true"/> if the colors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(ColorF left, ColorF right) => !(left == right);
}

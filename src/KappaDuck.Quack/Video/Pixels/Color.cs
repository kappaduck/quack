// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using System.Numerics;
using System.Text.Unicode;

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// Represents a 32-bit RGBA color, with one byte (0-255) per channel.
/// </summary>
/// <remarks>
/// Use <see cref="ColorF"/> when you need high-precision or wide-gamut colors expressed
/// as floating-point channels.
/// </remarks>
/// <param name="r">The red channel (0-255).</param>
/// <param name="g">The green channel (0-255).</param>
/// <param name="b">The blue channel (0-255).</param>
/// <param name="a">The alpha channel (0-255), where 0 is fully transparent and 255 is fully opaque.</param>
[StructLayout(LayoutKind.Sequential)]
public struct Color(byte r, byte g, byte b, byte a) :
    IEqualityOperators<Color, Color, bool>,
    IEquatable<Color>,
    ISpanFormattable,
    IUtf8SpanFormattable
{
    /// <summary>
    /// Creates a fully transparent black color (0, 0, 0, 0).
    /// </summary>
    public Color() : this(0, 0, 0, 0)
    {
    }

    /// <summary>
    /// Creates an opaque color from red, green and blue channels with the alpha channel set to 255.
    /// </summary>
    /// <param name="r">The red channel (0-255).</param>
    /// <param name="g">The green channel (0-255).</param>
    /// <param name="b">The blue channel (0-255).</param>
    public Color(byte r, byte g, byte b) : this(r, g, b, byte.MaxValue)
    {
    }

    /// <summary>
    /// Gets or sets the red channel (0-255).
    /// </summary>
    public byte R { get; set; } = r;

    /// <summary>
    /// Gets or sets the green channel (0-255).
    /// </summary>
    public byte G { get; set; } = g;

    /// <summary>
    /// Gets or sets the blue channel (0-255).
    /// </summary>
    public byte B { get; set; } = b;

    /// <summary>
    /// Gets or sets the alpha channel (0-255), where 0 is fully transparent and 255 is fully opaque.
    /// </summary>
    public byte A { get; set; } = a;

    /// <summary>
    /// Gets a fully transparent black color (0, 0, 0, 0).
    /// </summary>
    public static Color Transparent { get; } = new(0, 0, 0, 0);

    /// <summary>
    /// Gets an opaque black color (0, 0, 0, 255).
    /// </summary>
    public static Color Black { get; } = new(0, 0, 0);

    /// <summary>
    /// Gets an opaque white color (255, 255, 255, 255).
    /// </summary>
    public static Color White { get; } = new(255, 255, 255);

    /// <summary>
    /// Deconstructs the color into its channels.
    /// </summary>
    /// <param name="r">The red channel.</param>
    /// <param name="g">The green channel.</param>
    /// <param name="b">The blue channel.</param>
    /// <param name="a">The alpha channel.</param>
    public readonly void Deconstruct(out byte r, out byte g, out byte b, out byte a) => (r, g, b, a) = (R, G, B, A);

    /// <summary>
    /// Creates a color from a packed 32-bit value laid out as <c>0xRRGGBBAA</c>.
    /// </summary>
    /// <param name="value">The packed value where the most significant byte is the red channel and the least significant byte is the alpha channel.</param>
    /// <returns>The unpacked color.</returns>
    public static Color FromHex(uint value) => new((byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value);

    /// <summary>
    /// Creates a color from a hexadecimal string such as <c>#RRGGBBAA</c>.
    /// </summary>
    /// <remarks>
    /// The leading <c>#</c> is optional. The following layouts are accepted, where each letter is a hex digit:
    /// <c>RGB</c>, <c>RGBA</c>, <c>RRGGBB</c> and <c>RRGGBBAA</c>. Short layouts expand each digit
    /// (for example <c>#abc</c> becomes <c>#aabbcc</c>), and the alpha channel defaults to fully opaque when omitted.
    /// </remarks>
    /// <param name="hex">The hexadecimal string to parse.</param>
    /// <returns>The parsed color.</returns>
    /// <exception cref="FormatException"><paramref name="hex"/> is not a valid hexadecimal color.</exception>
    public static Color FromHex(ReadOnlySpan<char> hex)
    {
        if (!TryFromHex(hex, out Color color))
            ThrowHelper.ThrowFormat($"'{hex}' is not a valid hexadecimal color.");

        return color;
    }

    /// <summary>
    /// Tries to create a color from a hexadecimal string such as <c>#RRGGBBAA</c>.
    /// </summary>
    /// <remarks>
    /// The leading <c>#</c> is optional. The following layouts are accepted, where each letter is a hex digit:
    /// <c>RGB</c>, <c>RGBA</c>, <c>RRGGBB</c> and <c>RRGGBBAA</c>. Short layouts expand each digit
    /// (for example <c>#abc</c> becomes <c>#aabbcc</c>), and the alpha channel defaults to fully opaque when omitted.
    /// </remarks>
    /// <param name="hex">The hexadecimal string to parse.</param>
    /// <param name="color">When this method returns, contains the parsed color if parsing succeeded; otherwise, the default color.</param>
    /// <returns><see langword="true"/> if <paramref name="hex"/> was parsed successfully; otherwise, <see langword="false"/>.</returns>
    public static bool TryFromHex(ReadOnlySpan<char> hex, out Color color)
    {
        color = default;

        hex = hex.Trim();

        if (!hex.IsEmpty && hex[0] == '#')
            hex = hex[1..];

        switch (hex.Length)
        {
            case 3 when TryParseDigit(hex[0], out int r) && TryParseDigit(hex[1], out int g) && TryParseDigit(hex[2], out int b):
                color = new Color(Expand(r), Expand(g), Expand(b));
                return true;

            case 4 when TryParseDigit(hex[0], out int r) && TryParseDigit(hex[1], out int g) && TryParseDigit(hex[2], out int b) && TryParseDigit(hex[3], out int a):
                color = new Color(Expand(r), Expand(g), Expand(b), Expand(a));
                return true;

            case 6 when TryParseByte(hex[..2], out byte r) && TryParseByte(hex[2..4], out byte g) && TryParseByte(hex[4..6], out byte b):
                color = new Color(r, g, b);
                return true;

            case 8 when TryParseByte(hex[..2], out byte r) && TryParseByte(hex[2..4], out byte g) && TryParseByte(hex[4..6], out byte b) && TryParseByte(hex[6..8], out byte a):
                color = new Color(r, g, b, a);
                return true;

            default:
                return false;
        }

        static byte Expand(int digit) => (byte)((digit * 16) + digit);
    }

    /// <summary>
    /// Linearly interpolates between two colors.
    /// </summary>
    /// <param name="from">The starting color.</param>
    /// <param name="to">The ending color.</param>
    /// <param name="factor">The interpolation factor, clamped between 0 and 1.</param>
    /// <returns>The interpolated color.</returns>
    public static Color Lerp(Color from, Color to, float factor)
    {
        factor = Math.Clamp(factor, 0f, 1f);

        byte r = (byte)float.Round(from.R + ((to.R - from.R) * factor));
        byte g = (byte)float.Round(from.G + ((to.G - from.G) * factor));
        byte b = (byte)float.Round(from.B + ((to.B - from.B) * factor));
        byte a = (byte)float.Round(from.A + ((to.A - from.A) * factor));

        return new Color(r, g, b, a);
    }

    /// <summary>
    /// Determines whether this color is equal to another color.
    /// </summary>
    /// <param name="other">The color to compare with the current color.</param>
    /// <returns><see langword="true"/> if the colors are equal; otherwise, <see langword="false"/>.</returns>
    public readonly bool Equals(Color other) => R == other.R && G == other.G && B == other.B && A == other.A;

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Color other && Equals(other);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(R, G, B, A);

    /// <summary>
    /// Packs the color into a 32-bit value laid out as <c>0xRRGGBBAA</c>.
    /// </summary>
    /// <returns>The packed value.</returns>
    public readonly uint ToHex()
        => ((uint)R << 24) | ((uint)G << 16) | ((uint)B << 8) | A;

    /// <summary>
    /// Converts the color to a <see cref="ColorF"/> by normalizing each channel to the range 0-1.
    /// </summary>
    /// <returns>The converted color.</returns>
    public readonly ColorF ToColorF() => new(R / 255f, G / 255f, B / 255f, A / 255f);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <summary>
    /// The string representation of the color with formatting.
    /// </summary>
    /// <remarks>
    /// Use <c>"X"</c> or <c>"x"</c> as the format string to get the color as a <c>#RRGGBBAA</c> hex string
    /// (uppercase or lowercase). Otherwise, the color is represented as <c>(R, G, B, A)</c>.
    /// </remarks>
    /// <param name="format">The format string.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>The string representation of the color.</returns>
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format is "X")
            return $"#{R:X2}{G:X2}{B:X2}{A:X2}";

        if (format is "x")
            return $"#{R:x2}{G:x2}{B:x2}{A:x2}";

        return ToString();
    }

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (format is "X")
            return destination.TryWrite($"#{R:X2}{G:X2}{B:X2}{A:X2}", out charsWritten);

        if (format is "x")
            return destination.TryWrite($"#{R:x2}{G:x2}{B:x2}{A:x2}", out charsWritten);

        return destination.TryWrite($"({R}, {G}, {B}, {A})", out charsWritten);
    }

    /// <inheritdoc/>
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (format is "X")
            Utf8.TryWrite(utf8Destination, provider, $"#{R:X2}{G:X2}{B:X2}{A:X2}", out bytesWritten);

        if (format is "x")
            Utf8.TryWrite(utf8Destination, provider, $"#{R:x2}{G:x2}{B:x2}{A:x2}", out bytesWritten);

        return Utf8.TryWrite(utf8Destination, provider, $"({R}, {G}, {B}, {A})", out bytesWritten);
    }

    /// <summary>
    /// Determines whether two colors are equal.
    /// </summary>
    /// <param name="left">The left color.</param>
    /// <param name="right">The right color.</param>
    /// <returns><see langword="true"/> if the colors are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Color left, Color right) => left.Equals(right);

    /// <summary>
    /// Determines whether two colors are not equal.
    /// </summary>
    /// <param name="left">The left color.</param>
    /// <param name="right">The right color.</param>
    /// <returns><see langword="true"/> if the colors are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Color left, Color right) => !(left == right);

    private static bool TryParseDigit(char c, out int value)
    {
        if (c is >= '0' and <= '9')
        {
            value = c - '0';
            return true;
        }

        if (c is >= 'a' and <= 'f')
        {
            value = c - 'a' + 10;
            return true;
        }

        if (c is >= 'A' and <= 'F')
        {
            value = c - 'A' + 10;
            return true;
        }

        value = 0;
        return false;
    }

    private static bool TryParseByte(ReadOnlySpan<char> span, out byte value)
    {
        if (TryParseDigit(span[0], out int high) && TryParseDigit(span[1], out int low))
        {
            value = (byte)((high * 16) + low);
            return true;
        }

        value = 0;
        return false;
    }
}

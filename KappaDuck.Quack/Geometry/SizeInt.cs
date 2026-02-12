// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Text.Unicode;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a size in a two-dimensional coordinate system.
/// </summary>
/// <param name="width">The width component of the size.</param>
/// <param name="height">The height component of the size.</param>
public struct SizeInt(int width, int height) : ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary>
    /// Gets or sets the width component.
    /// </summary>
    public int Width { get; set; } = width;

    /// <summary>
    /// Gets or sets the height component.
    /// </summary>
    public int Height { get; set; } = height;

    /// <summary>
    /// Deconstructs the size into its width and height components.
    /// </summary>
    /// <param name="width">The width component.</param>
    /// <param name="height">The height component.</param>
    public readonly void Deconstruct(out int width, out int height) => (width, height) = (Width, Height);

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({Width}, {Height})", out charsWritten);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"({Width}, {Height})", out bytesWritten);
}

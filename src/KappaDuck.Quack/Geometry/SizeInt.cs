// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a two-dimensional size.
/// </summary>
/// <param name="width">The width component of the size.</param>
/// <param name="height">The height component of the size.</param>
[StructLayout(LayoutKind.Sequential)]
public struct SizeInt(int width, int height) : ISpanFormattable
{
    /// <summary>
    /// Gets or sets the width component of the size.
    /// </summary>
    public int Width { get; set; } = width;

    /// <summary>
    /// Gets or sets the height component of the size.
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
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"({Width}, {Height})", out charsWritten);
}

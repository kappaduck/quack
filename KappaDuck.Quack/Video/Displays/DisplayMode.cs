// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Pixels;
using System.Text.Unicode;

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Represents a display mode (a video mode supported by a display).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct DisplayMode : ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary>
    /// Gets the unique identifier for the display mode.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets the pixel format of the display mode.
    /// </summary>
    public PixelFormat Format { get; }

    /// <summary>
    /// Gets the width of the display mode in pixels.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the display mode in pixels.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the scale converting size to pixels (e.g. a 1920x1080 mode with 2.0 scale would have 3840x2160 pixels).
    /// </summary>
    public float PixelDensity { get; }

    /// <summary>
    /// Gets the refresh rate of the display mode in hertz or 0 for unspecified.
    /// </summary>
    public float RefreshRate { get; }

    /// <summary>
    /// Gets the precise refresh rate numerator or 0 for unspecified.
    /// </summary>
    public int Numerator { get; }

    /// <summary>
    /// Gets the precise refresh rate denominator or 0 for unspecified.
    /// </summary>
    public int Denominator { get; }

    private readonly nint _internal;

    /// <inheritdoc/>
    public override readonly string ToString() => $"{this}";

    /// <inheritdoc/>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => destination.TryWrite($"{Width}x{Height} @ {RefreshRate}Hz", out charsWritten);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public readonly bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => Utf8.TryWrite(utf8Destination, provider, $"{Width}x{Height} @ {RefreshRate}Hz", out bytesWritten);
}

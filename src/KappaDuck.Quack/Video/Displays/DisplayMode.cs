// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Pixels;

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Represents a display mode, which includes resolution, refresh rate, and pixel format.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct DisplayMode
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
}

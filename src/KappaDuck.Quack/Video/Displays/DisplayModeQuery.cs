// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Queries for matching display modes.
/// </summary>
public record DisplayModeQuery
{
    /// <summary>
    /// The width of the display mode in pixels.
    /// </summary>
    public int Width { get; init; }

    /// <summary>
    /// The height of the display mode in pixels.
    /// </summary>
    public int Height { get; init; }

    /// <summary>
    /// The refresh rate of the display mode in hertz, or null for the desktop's refresh rate.
    /// </summary>
    public float? RefreshRate { get; init; }

    /// <summary>
    /// <see langword="true"/> to include high density display modes; otherwise, <see langword="false"/>.
    /// </summary>
    public bool HighDensity { get; init; }
}

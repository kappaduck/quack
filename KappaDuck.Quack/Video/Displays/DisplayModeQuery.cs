// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Represents a query to find the closest display mode matching the specified parameters.
/// </summary>
public record DisplayModeQuery
{
    /// <summary>
    /// Gets or initializes the width of the display mode.
    /// </summary>
    public int Width { get; init; }

    /// <summary>
    /// Gets or initializes the height of the display mode.
    /// </summary>
    public int Height { get; init; }

    /// <summary>
    /// Gets or initializes the refresh rate of the display mode in hertz or <see langword="null"/> to use desktop mode's refresh rate.
    /// </summary>
    public float? RefreshRate { get; init; }

    /// <summary>
    /// Gets or initializes a value indicating whether the display mode should be high density (e.g. a 1920x1080 mode with high density would have 3840x2160 pixels).
    /// </summary>
    public bool HighDensity { get; init; }
}

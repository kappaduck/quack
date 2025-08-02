// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Represents the orientation of a display.
/// </summary>
public enum DisplayOrientation
{
    /// <summary>
    /// Unknown orientation.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The display is in landscape mode, with the right side up, relative to portrait mode.
    /// </summary>
    Landscape = 1,

    /// <summary>
    /// The display is in landscape mode, with the left side up, relative to portrait mode.
    /// </summary>
    LandscapeFlipped = 2,

    /// <summary>
    /// The display is in portrait mode.
    /// </summary>
    Portrait = 3,

    /// <summary>
    /// The display is in portrait mode, upside down.
    /// </summary>
    PortraitFlipped = 4
}

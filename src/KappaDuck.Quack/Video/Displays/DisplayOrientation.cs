// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// The orientation of a display.
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
    /// The display is in landscape mode, but flipped upside down, relative to portrait mode.
    /// </summary>
    LandscapeFlipped = 2,

    /// <summary>
    /// The display is in portrait mode.
    /// </summary>
    Portrait = 3,

    /// <summary>
    /// The display is in portrait mode, but flipped upside down.
    /// </summary>
    PortraitFlipped = 4
}

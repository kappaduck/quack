// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Types of logical presentations for display content.
/// </summary>
public enum LogicalPresentation
{
    /// <summary>
    /// The logical presentation is disabled.
    /// </summary>
    Disabled = 0,

    /// <summary>
    /// The rendered content is stretched to fill the entire display area.
    /// </summary>
    Stretch = 1,

    /// <summary>
    /// The rendered content is fitted within the display area while maintaining its aspect ratio, potentially adding black bars (letterboxing) to fill any remaining space.
    /// </summary>
    Letterbox = 2,

    /// <summary>
    /// The rendered content is fit to the smallest dimension of the display area, potentially cropping parts of the content that exceed the display bounds.
    /// </summary>
    OverScan = 3,

    /// <summary>
    /// The rendered content is scaled up by an integer factor to fit within the display area, maintaining the original aspect ratio without any distortion.
    /// </summary>
    IntegerScale = 4
}

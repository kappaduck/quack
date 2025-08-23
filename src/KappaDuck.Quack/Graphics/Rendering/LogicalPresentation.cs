// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// How the logical size is mapped to the output.
/// </summary>
public enum LogicalPresentation
{
    /// <summary>
    /// There is no logical size in effect.
    /// </summary>
    Disabled = 0,

    /// <summary>
    /// The rendered content is stretched to the output resolution.
    /// </summary>
    Stretch = 1,

    /// <summary>
    /// The rendered content is fit to the largest dimension and the other dimension is letterboxed with black bars.
    /// </summary>
    Letterbox = 2,

    /// <summary>
    /// The rendered content is fit to the smallest dimension and the other dimension extends beyond the output bounds.
    /// </summary>
    OverScan = 3,

    /// <summary>
    /// The rendered content is scaled up by integer multiples to fit the output resolution.
    /// </summary>
    IntegerScale = 4
}

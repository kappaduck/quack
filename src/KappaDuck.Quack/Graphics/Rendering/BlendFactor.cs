// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// The normalized factor that a pixel component is multiplied by before a <see cref="BlendOperation"/> is applied.
/// </summary>
/// <remarks>
/// Factors are listed as the multipliers applied to the red, green, blue and alpha components, in that order.
/// </remarks>
public enum BlendFactor
{
    /// <summary>
    /// The factor <c>(0, 0, 0, 0)</c>.
    /// </summary>
    Zero = 0x1,

    /// <summary>
    /// The factor <c>(1, 1, 1, 1)</c>.
    /// </summary>
    One = 0x2,

    /// <summary>
    /// The source color <c>(srcR, srcG, srcB, srcA)</c>.
    /// </summary>
    SourceColor = 0x3,

    /// <summary>
    /// One minus the source color <c>(1 - srcR, 1 - srcG, 1 - srcB, 1 - srcA)</c>.
    /// </summary>
    OneMinusSourceColor = 0x4,

    /// <summary>
    /// The source alpha <c>(srcA, srcA, srcA, srcA)</c>.
    /// </summary>
    SourceAlpha = 0x5,

    /// <summary>
    /// One minus the source alpha <c>(1 - srcA, 1 - srcA, 1 - srcA, 1 - srcA)</c>.
    /// </summary>
    OneMinusSourceAlpha = 0x6,

    /// <summary>
    /// The destination color <c>(dstR, dstG, dstB, dstA)</c>.
    /// </summary>
    DestinationColor = 0x7,

    /// <summary>
    /// One minus the destination color <c>(1 - dstR, 1 - dstG, 1 - dstB, 1 - dstA)</c>.
    /// </summary>
    OneMinusDestinationColor = 0x8,

    /// <summary>
    /// The destination alpha <c>(dstA, dstA, dstA, dstA)</c>.
    /// </summary>
    DestinationAlpha = 0x9,

    /// <summary>
    /// One minus the destination alpha <c>(1 - dstA, 1 - dstA, 1 - dstA, 1 - dstA)</c>.
    /// </summary>
    OneMinusDestinationAlpha = 0xA
}

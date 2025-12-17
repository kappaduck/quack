// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// The normalized factor used to multiply pixel components.
/// </summary>
[PublicAPI]
public enum BlendFactor : uint
{
    /// <summary>
    /// 0, 0, 0, 0.
    /// </summary>
    Zero = 0x1,

    /// <summary>
    /// 1, 1, 1, 1.
    /// </summary>
    One = 0x2,

    /// <summary>
    /// Source color: srcR, srcG, srcB, srcA.
    /// </summary>
    SourceColor = 0x3,

    /// <summary>
    /// One minus source color: 1-srcR, 1-srcG, 1-srcB, 1-srcA.
    /// </summary>
    OneMinusSourceColor = 0x4,

    /// <summary>
    /// Source alpha: srcA, srcA, srcA, srcA.
    /// </summary>
    SourceAlpha = 0x5,

    /// <summary>
    /// One minus source alpha: 1-srcA, 1-srcA, 1-srcA, 1-srcA.
    /// </summary>
    OneMinusSourceAlpha = 0x6,

    /// <summary>
    /// Destination color: dstR, dstG, dstB, dstA.
    /// </summary>
    DestinationColor = 0x7,

    /// <summary>
    /// One minus destination color: 1-dstR, 1-dstG, 1-dstB, 1-dstA.
    /// </summary>
    OneMinusDestinationColor = 0x8,

    /// <summary>
    /// Destination alpha: dstA, dstA, dstA, dstA.
    /// </summary>
    DestinationAlpha = 0x9,

    /// <summary>
    /// One minus destination alpha: 1-dstA, 1-dstA, 1-dstA, 1-dstA.
    /// </summary>
    OneMinusDestinationAlpha = 0xA
}

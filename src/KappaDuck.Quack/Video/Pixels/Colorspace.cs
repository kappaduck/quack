// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// A representative set of colorspace describing how color values should be interpreted.
/// </summary>
public enum Colorspace : uint
{
    /// <summary>
    /// An unknown or unspecified colorspace.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The linear sRGB colorspace; the default for floating-point surfaces.
    /// </summary>
    SrgbLinear = 0x12000500,

    /// <summary>
    /// The gamma-corrected sRGB colorspace; the default for 8-bit RGB surfaces.
    /// </summary>
    Srgb = 0x120005A0,

    /// <summary>
    /// The default colorspace for RGB surfaces when none is specified. Equivalent to <see cref="Srgb"/>.
    /// </summary>
    RgbDefault = Srgb,

    /// <summary>
    /// The HDR10 colorspace; the default for 10-bit surfaces.
    /// </summary>
    Hdr10 = 0x12002600,

    /// <summary>
    /// The limited-range BT.709 YCbCr colorspace.
    /// </summary>
    Bt709Limited = 0x21100421,

    /// <summary>
    /// The limited-range BT.601 YCbCr colorspace.
    /// </summary>
    Bt601Limited = 0x211018C6,

    /// <summary>
    /// The default colorspace for YUV surfaces when none is specified. Equivalent to <see cref="Bt601Limited"/>.
    /// </summary>
    YuvDefault = Bt601Limited,

    /// <summary>
    /// The limited-range BT.2020 YCbCr colorspace.
    /// </summary>
    Bt2020Limited = 0x21102609,

    /// <summary>
    /// The full-range BT.709 YCbCr colorspace used by JPEG.
    /// </summary>
    Jpeg = 0x220004C6,

    /// <summary>
    /// The full-range BT.709 YCbCr colorspace.
    /// </summary>
    Bt709Full = 0x22100421,

    /// <summary>
    /// The full-range BT.601 YCbCr colorspace.
    /// </summary>
    Bt601Full = 0x221018C6,

    /// <summary>
    /// The full-range BT.2020 YCbCr colorspace.
    /// </summary>
    Bt2020Full = 0x22102609
}

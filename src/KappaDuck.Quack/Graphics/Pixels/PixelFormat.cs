// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Represents the format of a pixel.
/// </summary>
public enum PixelFormat
{
    /// <summary>
    /// Unknown pixel format.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// 1LSB pixel format.
    /// </summary>
    Index1LSB = 0x11100100,

    /// <summary>
    /// 1MSB pixel format.
    /// </summary>
    Index1MSB = 0x11200100,

    /// <summary>
    /// 4LSB pixel format.
    /// </summary>
    Index4LSB = 0x12100400,

    /// <summary>
    /// 4MSB pixel format.
    /// </summary>
    Index4MSB = 0x12200400,

    /// <summary>
    /// 8-bit pixel format.
    /// </summary>
    Index8 = 0x13000801,

    /// <summary>
    /// RGB332 pixel format.
    /// </summary>
    RGB332 = 0x14110801,

    /// <summary>
    /// RGB444 pixel format.
    /// </summary>
    XRGB4444 = 0x15120c02,

    /// <summary>
    /// XRGB1555 pixel format.
    /// </summary>
    XRGB1555 = 0x15130f02,

    /// <summary>
    /// RGB565 pixel format.
    /// </summary>
    RGB565 = 0x15151002,

    /// <summary>
    /// ARGB4444 pixel format.
    /// </summary>
    ARGB4444 = 0x15321002,

    /// <summary>
    /// ARGB1555 pixel format.
    /// </summary>
    ARGB1555 = 0x15331002,

    /// <summary>
    /// RGBA4444 pixel format.
    /// </summary>
    RGBA4444 = 0x15421002,

    /// <summary>
    /// RGBA5551 pixel format.
    /// </summary>
    RGBA5551 = 0x15441002,

    /// <summary>
    /// BGR444 pixel format.
    /// </summary>
    XBGR4444 = 0x15520c02,

    /// <summary>
    /// XBGR1555 pixel format.
    /// </summary>
    XBGR1555 = 0x15530f02,

    /// <summary>
    /// BGR565 pixel format.
    /// </summary>
    BGR565 = 0x15551002,

    /// <summary>
    /// ABGR4444 pixel format.
    /// </summary>
    ABGR4444 = 0x15721002,

    /// <summary>
    /// ABGR1555 pixel format.
    /// </summary>
    ABGR1555 = 0x15731002,

    /// <summary>
    /// BGRA4444 pixel format.
    /// </summary>
    BGRA4444 = 0x15821002,

    /// <summary>
    /// BGRA5551 pixel format.
    /// </summary>
    BGRA5551 = 0x15841002,

    /// <summary>
    /// XRGB8888 pixel format.
    /// </summary>
    XRGB8888 = 0x16161804,

    /// <summary>
    /// Alias for <see cref="XRGB8888"/>.
    /// </summary>
    /// <remarks>
    /// This is based on SDL with little-endian byte order.
    /// </remarks>
    BGRX32 = XRGB8888,

    /// <summary>
    /// XRGB2101010 pixel format.
    /// </summary>
    XRGB2101010 = 0x16172004,

    /// <summary>
    /// RGBX8888 pixel format.
    /// </summary>
    RGBX8888 = 0x16261804,

    /// <summary>
    /// Alias for <see cref="RGBX8888"/>.
    /// </summary>
    /// <remarks>
    /// This is based on SDL with little-endian byte order.
    /// </remarks>
    XBGR32 = RGBX8888,

    /// <summary>
    /// ARGB8888 pixel format.
    /// </summary>
    ARGB8888 = 0x16362004,

    /// <summary>
    /// Alias for <see cref="ARGB8888"/>.
    /// </summary>
    /// <remarks>
    /// This is based on SDL with little-endian byte order.
    /// </remarks>
    BGRA32 = ARGB8888,

    /// <summary>
    /// ARGB2101010 pixel format.
    /// </summary>
    ARGB2101010 = 0x16372004,

    /// <summary>
    /// RGBA8888 pixel format.
    /// </summary>
    RGBA8888 = 0x16462004,

    /// <summary>
    /// Alias for <see cref="RGBA8888"/>.
    /// </summary>
    /// <remarks>
    /// This is based on SDL with little-endian byte order.
    /// </remarks>
    ABGR32 = RGBA8888,

    /// <summary>
    /// Alias for XBGR8888 pixel format.
    /// </summary>
    /// <remarks>
    /// This is based on SDL with little-endian byte order.
    /// </remarks>
    RGBX32 = 0x16561804,

    /// <summary>
    /// XBGR2101010 pixel format.
    /// </summary>
    XBGR2101010 = 0x16572004,

    /// <summary>
    /// Alias for BGRX8888 pixel format.
    /// </summary>
    /// <remarks>
    /// This is based on SDL with little-endian byte order.
    /// </remarks>
    XRGB32 = 0x16661804,

    /// <summary>
    /// Alias for ABGR8888 pixel format.
    /// </summary>
    /// <remarks>
    /// This is based on SDL with little-endian byte order.
    /// </remarks>
    RGBA32 = 0x16762004,

    /// <summary>
    /// ABGR2101010 pixel format.
    /// </summary>
    ABGR2101010 = 0x16772004,

    /// <summary>
    /// Alias for BGRA8888 pixel format.
    /// </summary>
    /// <remarks>
    /// This is based on SDL with little-endian byte order.
    /// </remarks>
    ARGB32 = 0x16862004,

    /// <summary>
    /// RGB24 pixel format.
    /// </summary>
    RGB24 = 0x17101803,

    /// <summary>
    /// BGR24 pixel format.
    /// </summary>
    BGR24 = 0x17401803,

    /// <summary>
    /// RGB48 pixel format.
    /// </summary>
    RGB48 = 0x18103006,

    /// <summary>
    /// RGBA64 pixel format.
    /// </summary>
    RGBA64 = 0x18204008,

    /// <summary>
    /// ARGB64 pixel format.
    /// </summary>
    ARGB64 = 0x18304008,

    /// <summary>
    /// BGR48 pixel format.
    /// </summary>
    BGR48 = 0x18403006,

    /// <summary>
    /// BGRA64 pixel format.
    /// </summary>
    BGRA64 = 0x18504008,

    /// <summary>
    /// ABGR64 pixel format.
    /// </summary>
    ABGR64 = 0x18604008,

    /// <summary>
    /// RGB48Float pixel format.
    /// </summary>
    RGB48Float = 0x1a103006,

    /// <summary>
    /// RGBA64Float pixel format.
    /// </summary>
    RGBA64Float = 0x1a204008,

    /// <summary>
    /// ARGB64Float pixel format.
    /// </summary>
    ARGB64Float = 0x1a304008,

    /// <summary>
    /// BGR48Float pixel format.
    /// </summary>
    BGR48Float = 0x1a403006,

    /// <summary>
    /// BGRA64Float pixel format.
    /// </summary>
    BGRA64Float = 0x1a504008,

    /// <summary>
    /// ABGR64Float pixel format.
    /// </summary>
    ABGR64Float = 0x1a604008,

    /// <summary>
    /// RGB96Float pixel format.
    /// </summary>
    RGB96Float = 0x1b10600c,

    /// <summary>
    /// RGBA128Float pixel format.
    /// </summary>
    RGBA128Float = 0x1b208010,

    /// <summary>
    /// ARGB128Float pixel format.
    /// </summary>
    ARGB128Float = 0x1b308010,

    /// <summary>
    /// BGR96Float pixel format.
    /// </summary>
    BGR96Float = 0x1b40600c,

    /// <summary>
    /// BGRA128Float pixel format.
    /// </summary>
    BGRA128Float = 0x1b508010,

    /// <summary>
    /// ABGR128Float pixel format.
    /// </summary>
    ABGR128Float = 0x1b608010,

    /// <summary>
    /// 2LSB pixel format.
    /// </summary>
    Index2LSB = 0x1c100200,

    /// <summary>
    /// 2MSB pixel format.
    /// </summary>
    Index2MSB = 0x1c200200,

    /// <summary>
    /// External OES pixel format.
    /// </summary>
    ExternalOes = 0x2053454f,

    /// <summary>
    /// P010 pixel format.
    /// </summary>
    P010 = 0x30313050,

    /// <summary>
    /// NV21 pixel format.
    /// </summary>
    NV21 = 0x3132564e,

    /// <summary>
    /// NV12 pixel format.
    /// </summary>
    NV12 = 0x3231564e,

    /// <summary>
    /// YV12 pixel format.
    /// </summary>
    YV12 = 0x32315659,

    /// <summary>
    /// YUY2 pixel format.
    /// </summary>
    YUY2 = 0x32595559,

    /// <summary>
    /// YVYU pixel format.
    /// </summary>
    YVYU = 0x55595659,

    /// <summary>
    /// IYUV pixel format.
    /// </summary>
    IYUV = 0x56555949,

    /// <summary>
    /// UYVY pixel format.
    /// </summary>
    UYVY = 0x59565955
}

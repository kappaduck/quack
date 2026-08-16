// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Imaging;

/// <summary>
/// Identifies the in-memory layout of a pixel.
/// </summary>
/// <remarks>
/// Two naming conventions are used. Names with per-component bit counts, such as
/// <see cref="Argb8888"/> or <see cref="Xrgb1555"/>, describe channels packed into a single
/// integer in the platform's native byte order, listed from the most significant bits. Names with
/// a component list and a single bit count, such as <see cref="Rgb24"/> or <see cref="Abgr64"/>,
/// describe the exact order of the bytes in memory regardless of platform. Indexed formats encode a
/// palette index instead of color channels.
/// </remarks>
public enum PixelFormat : uint
{
    /// <summary>
    /// An unknown or unsupported pixel format.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// 1-bit palette index. Pixels are packed several per byte, the leftmost pixel in the least significant bits.
    /// </summary>
    Index1Lsb = 0x11100100,

    /// <summary>
    /// 1-bit palette index. Pixels are packed several per byte, the leftmost pixel in the most significant bits.
    /// </summary>
    Index1Msb = 0x11200100,

    /// <summary>
    /// 4-bit palette index. Two pixels per byte, the leftmost pixel in the least significant bits.
    /// </summary>
    Index4Lsb = 0x12100400,

    /// <summary>
    /// 4-bit palette index. Two pixels per byte, the leftmost pixel in the most significant bits.
    /// </summary>
    Index4Msb = 0x12200400,

    /// <summary>
    /// 8-bit palette index, one pixel per byte.
    /// </summary>
    Index8 = 0x13000801,

    /// <summary>
    /// Packed 8-bit format with 3 bits for red, 3 bits for green and 2 bits for blue.
    /// </summary>
    Rgb332 = 0x14110801,

    /// <summary>
    /// Packed 16-bit format with 4 unused bits followed by 4 bits each for red, green and blue.
    /// </summary>
    Xrgb4444 = 0x15120c02,

    /// <summary>
    /// Packed 16-bit format with 1 unused bit followed by 5 bits each for red, green and blue.
    /// </summary>
    Xrgb1555 = 0x15130f02,

    /// <summary>
    /// Packed 16-bit format with 5 bits for red, 6 bits for green and 5 bits for blue.
    /// </summary>
    Rgb565 = 0x15151002,

    /// <summary>
    /// Packed 16-bit format with 4 bits each for alpha, red, green and blue, in that order from the most significant bits.
    /// </summary>
    Argb4444 = 0x15321002,

    /// <summary>
    /// Packed 16-bit format with 1 bit for alpha and 5 bits each for red, green and blue.
    /// </summary>
    Argb1555 = 0x15331002,

    /// <summary>
    /// Packed 16-bit format with 4 bits each for red, green, blue and alpha, in that order from the most significant bits.
    /// </summary>
    Rgba4444 = 0x15421002,

    /// <summary>
    /// Packed 16-bit format with 5 bits each for red, green and blue and 1 bit for alpha.
    /// </summary>
    Rgba5551 = 0x15441002,

    /// <summary>
    /// Packed 16-bit format with 4 unused bits followed by 4 bits each for blue, green and red.
    /// </summary>
    Xbgr4444 = 0x15520c02,

    /// <summary>
    /// Packed 16-bit format with 1 unused bit followed by 5 bits each for blue, green and red.
    /// </summary>
    Xbgr1555 = 0x15530f02,

    /// <summary>
    /// Packed 16-bit format with 5 bits for blue, 6 bits for green and 5 bits for red.
    /// </summary>
    Bgr565 = 0x15551002,

    /// <summary>
    /// Packed 16-bit format with 4 bits each for alpha, blue, green and red, in that order from the most significant bits.
    /// </summary>
    Abgr4444 = 0x15721002,

    /// <summary>
    /// Packed 16-bit format with 1 bit for alpha and 5 bits each for blue, green and red.
    /// </summary>
    Abgr1555 = 0x15731002,

    /// <summary>
    /// Packed 16-bit format with 4 bits each for blue, green, red and alpha, in that order from the most significant bits.
    /// </summary>
    Bgra4444 = 0x15821002,

    /// <summary>
    /// Packed 16-bit format with 5 bits each for blue, green and red and 1 bit for alpha.
    /// </summary>
    Bgra5551 = 0x15841002,

    /// <summary>
    /// Packed 32-bit format with 8 unused bits followed by 8 bits each for red, green and blue.
    /// </summary>
    Xrgb8888 = 0x16161804,

    /// <summary>
    /// Packed 32-bit format with 2 unused bits followed by 10 bits each for red, green and blue.
    /// </summary>
    Xrgb2101010 = 0x16172004,

    /// <summary>
    /// Packed 32-bit format with 8 bits each for red, green and blue followed by 8 unused bits.
    /// </summary>
    Rgbx8888 = 0x16261804,

    /// <summary>
    /// Packed 32-bit format with 8 bits each for alpha, red, green and blue, in that order from the most significant bits.
    /// </summary>
    Argb8888 = 0x16362004,

    /// <summary>
    /// Packed 32-bit format with 2 bits for alpha and 10 bits each for red, green and blue.
    /// </summary>
    Argb2101010 = 0x16372004,

    /// <summary>
    /// Packed 32-bit format with 8 bits each for red, green, blue and alpha, in that order from the most significant bits.
    /// </summary>
    Rgba8888 = 0x16462004,

    /// <summary>
    /// Packed 32-bit format with 8 unused bits followed by 8 bits each for blue, green and red.
    /// </summary>
    Xbgr8888 = 0x16561804,

    /// <summary>
    /// Packed 32-bit format with 2 unused bits followed by 10 bits each for blue, green and red.
    /// </summary>
    Xbgr2101010 = 0x16572004,

    /// <summary>
    /// Packed 32-bit format with 8 bits each for blue, green and red followed by 8 unused bits.
    /// </summary>
    Bgrx8888 = 0x16661804,

    /// <summary>
    /// Packed 32-bit format with 8 bits each for alpha, blue, green and red, in that order from the most significant bits.
    /// </summary>
    Abgr8888 = 0x16762004,

    /// <summary>
    /// Packed 32-bit format with 2 bits for alpha and 10 bits each for blue, green and red.
    /// </summary>
    Abgr2101010 = 0x16772004,

    /// <summary>
    /// Packed 32-bit format with 8 bits each for blue, green, red and alpha, in that order from the most significant bits.
    /// </summary>
    Bgra8888 = 0x16862004,

    /// <summary>
    /// Byte array of red, green and blue, in that order. 3 bytes per pixel.
    /// </summary>
    Rgb24 = 0x17101803,

    /// <summary>
    /// Byte array of blue, green and red, in that order. 3 bytes per pixel.
    /// </summary>
    Bgr24 = 0x17401803,

    /// <summary>
    /// Byte array of 16-bit red, green and blue, in that order. 6 bytes per pixel.
    /// </summary>
    Rgb48 = 0x18103006,

    /// <summary>
    /// Byte array of 16-bit red, green, blue and alpha, in that order. 8 bytes per pixel.
    /// </summary>
    Rgba64 = 0x18204008,

    /// <summary>
    /// Byte array of 16-bit alpha, red, green and blue, in that order. 8 bytes per pixel.
    /// </summary>
    Argb64 = 0x18304008,

    /// <summary>
    /// Byte array of 16-bit blue, green and red, in that order. 6 bytes per pixel.
    /// </summary>
    Bgr48 = 0x18403006,

    /// <summary>
    /// Byte array of 16-bit blue, green, red and alpha, in that order. 8 bytes per pixel.
    /// </summary>
    Bgra64 = 0x18504008,

    /// <summary>
    /// Byte array of 16-bit alpha, blue, green and red, in that order. 8 bytes per pixel.
    /// </summary>
    Abgr64 = 0x18604008,

    /// <summary>
    /// Byte array of half-precision (16-bit) floating-point red, green and blue, in that order. 6 bytes per pixel.
    /// </summary>
    Rgb48Float = 0x1a103006,

    /// <summary>
    /// Byte array of half-precision (16-bit) floating-point red, green, blue and alpha, in that order. 8 bytes per pixel.
    /// </summary>
    Rgba64Float = 0x1a204008,

    /// <summary>
    /// Byte array of half-precision (16-bit) floating-point alpha, red, green and blue, in that order. 8 bytes per pixel.
    /// </summary>
    Argb64Float = 0x1a304008,

    /// <summary>
    /// Byte array of half-precision (16-bit) floating-point blue, green and red, in that order. 6 bytes per pixel.
    /// </summary>
    Bgr48Float = 0x1a403006,

    /// <summary>
    /// Byte array of half-precision (16-bit) floating-point blue, green, red and alpha, in that order. 8 bytes per pixel.
    /// </summary>
    Bgra64Float = 0x1a504008,

    /// <summary>
    /// Byte array of half-precision (16-bit) floating-point alpha, blue, green and red, in that order. 8 bytes per pixel.
    /// </summary>
    Abgr64Float = 0x1a604008,

    /// <summary>
    /// Byte array of single-precision (32-bit) floating-point red, green and blue, in that order. 12 bytes per pixel.
    /// </summary>
    Rgb96Float = 0x1b10600c,

    /// <summary>
    /// Byte array of single-precision (32-bit) floating-point red, green, blue and alpha, in that order. 16 bytes per pixel.
    /// </summary>
    Rgba128Float = 0x1b208010,

    /// <summary>
    /// Byte array of single-precision (32-bit) floating-point alpha, red, green and blue, in that order. 16 bytes per pixel.
    /// </summary>
    Argb128Float = 0x1b308010,

    /// <summary>
    /// Byte array of single-precision (32-bit) floating-point blue, green and red, in that order. 12 bytes per pixel.
    /// </summary>
    Bgr96Float = 0x1b40600c,

    /// <summary>
    /// Byte array of single-precision (32-bit) floating-point blue, green, red and alpha, in that order. 16 bytes per pixel.
    /// </summary>
    Bgra128Float = 0x1b508010,

    /// <summary>
    /// Byte array of single-precision (32-bit) floating-point alpha, blue, green and red, in that order. 16 bytes per pixel.
    /// </summary>
    Abgr128Float = 0x1b608010,

    /// <summary>
    /// 2-bit palette index. Pixels are packed several per byte, the leftmost pixel in the least significant bits.
    /// </summary>
    Index2Lsb = 0x1c100200,

    /// <summary>
    /// 2-bit palette index. Pixels are packed several per byte, the leftmost pixel in the most significant bits.
    /// </summary>
    Index2Msb = 0x1c200200,

    /// <summary>
    /// Android OES external texture format.
    /// </summary>
    ExternalOes = 0x2053454f,

    /// <summary>
    /// Planar YUV format with 10-bit channels: a Y plane followed by an interleaved U/V plane (2 planes).
    /// </summary>
    P010 = 0x30313050,

    /// <summary>
    /// Planar YUV format: a full Y plane followed by an interleaved V/U plane (2 planes).
    /// </summary>
    Nv21 = 0x3132564e,

    /// <summary>
    /// Planar YUV format: a full Y plane followed by an interleaved U/V plane (2 planes).
    /// </summary>
    Nv12 = 0x3231564e,

    /// <summary>
    /// Planar YUV format: a full Y plane followed by V and U planes (3 planes).
    /// </summary>
    Yv12 = 0x32315659,

    /// <summary>
    /// Packed YUV format ordered Y0, U0, Y1, V0 (one plane).
    /// </summary>
    Yuy2 = 0x32595559,

    /// <summary>
    /// Motion JPEG.
    /// </summary>
    Mjpg = 0x47504a4d,

    /// <summary>
    /// Packed YUV format ordered Y0, V0, Y1, U0 (one plane).
    /// </summary>
    Yvyu = 0x55595659,

    /// <summary>
    /// Planar YUV format: a full Y plane followed by U and V planes (3 planes).
    /// </summary>
    Iyuv = 0x56555949,

    /// <summary>
    /// Packed YUV format ordered U0, Y0, V0, Y1 (one plane).
    /// </summary>
    Uyvy = 0x59565955
}

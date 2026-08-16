// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Imaging;

namespace Unit.Tests.Video.Imaging;

internal sealed class PixelFormatTests
{
    [Test]
    [Arguments(PixelFormat.Unknown, 0)]
    [Arguments(PixelFormat.Index1Lsb, 1)]
    [Arguments(PixelFormat.Index2Lsb, 2)]
    [Arguments(PixelFormat.Index4Lsb, 4)]
    [Arguments(PixelFormat.Index8, 8)]
    [Arguments(PixelFormat.Rgb332, 8)]
    [Arguments(PixelFormat.Xrgb4444, 12)]
    [Arguments(PixelFormat.Rgb565, 16)]
    [Arguments(PixelFormat.Rgb24, 24)]
    [Arguments(PixelFormat.Argb8888, 32)]
    [Arguments(PixelFormat.Rgb48, 48)]
    [Arguments(PixelFormat.Rgba64, 64)]
    [Arguments(PixelFormat.Rgba128Float, 128)]
    [Arguments(PixelFormat.Yuy2, 0)]
    [Arguments(PixelFormat.Nv12, 0)]
    [Arguments(PixelFormat.Mjpg, 0)]
    public async Task BitsPerPixelShouldReturnTheBitDepth(PixelFormat format, int expected)
    {
        int bitsPerPixel = format.BitsPerPixel;
        await bitsPerPixel.Should().BeEqualTo(expected);
    }

    [Test]
    [Arguments(PixelFormat.Unknown, 0)]
    [Arguments(PixelFormat.Index1Lsb, 0)]
    [Arguments(PixelFormat.Index2Lsb, 0)]
    [Arguments(PixelFormat.Index4Lsb, 0)]
    [Arguments(PixelFormat.Index8, 1)]
    [Arguments(PixelFormat.Rgb332, 1)]
    [Arguments(PixelFormat.Xrgb4444, 2)]
    [Arguments(PixelFormat.Rgb565, 2)]
    [Arguments(PixelFormat.Rgb24, 3)]
    [Arguments(PixelFormat.Argb8888, 4)]
    [Arguments(PixelFormat.Rgb48, 6)]
    [Arguments(PixelFormat.Rgba64, 8)]
    [Arguments(PixelFormat.Rgba128Float, 16)]
    [Arguments(PixelFormat.Yuy2, 2)]
    [Arguments(PixelFormat.Uyvy, 2)]
    [Arguments(PixelFormat.Yvyu, 2)]
    [Arguments(PixelFormat.P010, 2)]
    [Arguments(PixelFormat.Nv12, 1)]
    [Arguments(PixelFormat.Yv12, 1)]
    [Arguments(PixelFormat.Mjpg, 1)]
    [Arguments(PixelFormat.ExternalOes, 1)]
    public async Task BytesPerPixelShouldReturnTheByteSize(PixelFormat format, int expected)
    {
        int bytesPerPixel = format.BytesPerPixel;
        await bytesPerPixel.Should().BeEqualTo(expected);
    }

    [Test]
    [Arguments(PixelFormat.Index1Lsb, true)]
    [Arguments(PixelFormat.Index1Msb, true)]
    [Arguments(PixelFormat.Index2Lsb, true)]
    [Arguments(PixelFormat.Index4Lsb, true)]
    [Arguments(PixelFormat.Index8, true)]
    [Arguments(PixelFormat.Argb8888, false)]
    [Arguments(PixelFormat.Rgb24, false)]
    [Arguments(PixelFormat.Rgb565, false)]
    [Arguments(PixelFormat.Yuy2, false)]
    [Arguments(PixelFormat.Nv12, false)]
    [Arguments(PixelFormat.Unknown, false)]
    public async Task IsIndexedShouldReturnWhetherTheFormatIsIndexed(PixelFormat format, bool expected)
    {
        bool indexed = format.IsIndexed;
        await indexed.Should().BeEqualTo(expected);
    }
}

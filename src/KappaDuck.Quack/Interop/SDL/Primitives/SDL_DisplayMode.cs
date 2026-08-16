// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_DisplayMode
{
    internal SDL_DisplayMode(uint displayId, PixelFormat format, int width, int height, float pixelDensity, float refreshRate, int refreshRateNumerator, int refreshRateDenominator)
    {
        DisplayId = displayId;
        Format = format;
        Width = width;
        Height = height;
        PixelDensity = pixelDensity;
        RefreshRate = refreshRate;
        RefreshRateNumerator = refreshRateNumerator;
        RefreshRateDenominator = refreshRateDenominator;
    }

    internal uint DisplayId { get; }

    internal PixelFormat Format { get; }

    internal int Width { get; }

    internal int Height { get; }

    internal float PixelDensity { get; }

    internal float RefreshRate { get; }

    internal int RefreshRateNumerator { get; }

    internal int RefreshRateDenominator { get; }

    private readonly nint _internal;
}

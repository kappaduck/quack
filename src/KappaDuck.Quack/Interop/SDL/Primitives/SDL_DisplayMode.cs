// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_DisplayMode
{
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

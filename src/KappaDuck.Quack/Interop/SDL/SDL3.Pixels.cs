// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreatePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Palette* CreatePalette(int count);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DestroyPalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void DestroyPalette(SDL_Palette* palette);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetMasksForPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetMasksForPixelFormat(PixelFormat format, int* bitsPerPixel, uint* redMask, uint* greenMask, uint* blueMask, uint* alphaMask);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPixelFormatDetails")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_PixelFormatDetails* GetPixelFormatDetails(PixelFormat format);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPixelFormatForMasks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial PixelFormat GetPixelFormatForMasks(int bitsPerPixel, uint redMask, uint greenMask, uint blueMask, uint alphaMask);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetRGB(uint pixel, SDL_PixelFormatDetails* details, SDL_Palette* palette, byte* r, byte* g, byte* b);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetRGBA(uint pixel, SDL_PixelFormatDetails* details, SDL_Palette* palette, byte* r, byte* g, byte* b, byte* a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_MapRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint MapRGB(SDL_PixelFormatDetails* details, SDL_Palette* palette, byte r, byte g, byte b);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_MapRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint MapRGBA(SDL_PixelFormatDetails* details, SDL_Palette* palette, byte r, byte g, byte b, byte a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetPaletteColors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetPaletteColors(SDL_Palette* palette, ReadOnlySpan<Color> colors, int start, int length);
}

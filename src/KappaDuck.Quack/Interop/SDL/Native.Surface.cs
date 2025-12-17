// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL;

internal static unsafe partial class Native
{
    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial SDLPalette* SDL_CreatePalette(int length);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial SDLPalette* SDL_CreateSurfacePalette(SDLSurface* surface);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial void SDL_DestroyPalette(SDLPalette* palette);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool SDL_GetMasksForPixelFormat(PixelFormat format, out int bitsPerPixel, out uint redMask, out uint greenMask, out uint blueMask, out uint alphaMask);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial PixelFormatDetails* SDL_GetPixelFormatDetails(PixelFormat format);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    internal static partial string SDL_GetPixelFormatName(PixelFormat format);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial PixelFormat SDL_GetPixelFormatForMasks(int bitsPerPixel, uint redMask, uint greenMask, uint blueMask, uint alphaMask);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial void SDL_GetRGB(uint pixel, PixelFormatDetails* details, SDLPalette* palette, byte* r, byte* g, byte* b);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial void SDL_GetRGBA(uint pixel, PixelFormatDetails* details, SDLPalette* palette, byte* r, byte* g, byte* b, byte* a);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial uint SDL_MapRGB(PixelFormatDetails* details, SDLPalette* palette, byte r, byte g, byte b);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial uint SDL_MapRGBA(PixelFormatDetails* details, SDLPalette* palette, byte r, byte g, byte b, byte a);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool SDL_SetPaletteColors(SDLPalette* palette, ReadOnlySpan<SDLColor> colors, int startIndex, int length);
}

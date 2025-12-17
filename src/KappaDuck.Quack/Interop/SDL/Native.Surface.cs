// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

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
    internal static partial bool SDL_SetPaletteColors(SDLPalette* palette, ReadOnlySpan<SDLColor> colors, int startIndex, int length);
}

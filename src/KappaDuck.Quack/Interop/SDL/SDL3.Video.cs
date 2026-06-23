// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetClosestFullscreenDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetClosestFullscreenDisplayMode(uint displayId, int width, int height, float refreshRate, [MarshalAs(UnmanagedType.I1)] bool includeHighDensityModes, out SDL_DisplayMode closest);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_DisplayMode* GetCurrentDisplayMode(uint displayId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial DisplayOrientation GetCurrentDisplayOrientation(uint displayId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentVideoDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string? GetCurrentVideoDriver();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDesktopDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_DisplayMode* GetDesktopDisplayMode(uint displayId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetDisplayBounds(uint displayId, out RectI bounds);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayContentScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial float GetDisplayContentScale(uint displayId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayForPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetDisplayForPoint(in Point point);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayForRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetDisplayForRect(in RectI rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string? GetDisplayName(uint displayId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetDisplayProperties(uint displayId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplays")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerArrayMarshaller<,>), CountElementName = "length")]
    internal static partial Span<uint> GetDisplays(out int length);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayUsableBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetDisplayUsableBounds(uint displayId, out RectI bounds);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetFullscreenDisplayModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_DisplayMode** GetFullscreenDisplayModes(uint displayId, out int count);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNaturalDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial DisplayOrientation GetNaturalDisplayOrientation(uint displayId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumRenderDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetNumRenderDrivers();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumVideoDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetNumVideoDrivers();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPrimaryDisplay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetPrimaryDisplay();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string GetRenderDriver(int index);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetVideoDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string GetVideoDriver(int index);
}

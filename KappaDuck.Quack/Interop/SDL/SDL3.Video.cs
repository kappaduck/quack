// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Video.Displays;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal static partial class Video
    {
        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetClosestFullscreenDisplayMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool GetClosestFullscreenDisplayMode(uint display, int width, int height, float refreshRate, [MarshalAs(UnmanagedType.U1)] bool includeHighDensityMode, out DisplayMode displayMode);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentDisplayMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static unsafe partial DisplayMode* GetCurrentDisplayMode(uint display);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentDisplayOrientation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial DisplayOrientation GetCurrentDisplayOrientation(uint display);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentVideoDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string? GetCurrentVideoDriver();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDesktopDisplayMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static unsafe partial DisplayMode* GetDesktopDisplayMode(uint display);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplays"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(CallerArrayMarshaller<,>), CountElementName = "length")]
        internal static partial Span<uint> GetDisplays(out int length);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayBounds"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool GetDisplayBounds(uint display, out RectInt bounds);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayContentScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial float GetDisplayContentScale(uint display);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayForPoint"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static unsafe partial uint GetDisplayForPoint(Vector2Int* point);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayForRect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static unsafe partial uint GetDisplayForRect(RectInt* rectangle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string GetDisplayName(uint display);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial uint GetDisplayProperties(uint display);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayUsableBounds"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool GetDisplayUsableBounds(uint display, out RectInt bounds);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetFullscreenDisplayModes"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static unsafe partial DisplayMode** GetFullscreenDisplayModes(uint display, out int count);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNaturalDisplayOrientation"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial DisplayOrientation GetNaturalDisplayOrientation(uint display);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumRenderDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial int GetNumRenderDrivers();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumVideoDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial int GetNumVideoDrivers();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPrimaryDisplay"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial uint GetPrimaryDisplay();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string GetRenderDriver(int index);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetVideoDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string GetVideoDriver(int index);
    }
}

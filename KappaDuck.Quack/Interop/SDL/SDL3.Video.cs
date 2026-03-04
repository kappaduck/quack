// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal static partial class Video
    {
        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentVideoDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string? GetCurrentVideoDriver();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumRenderDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial int GetNumRenderDrivers();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumVideoDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial int GetNumVideoDrivers();

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

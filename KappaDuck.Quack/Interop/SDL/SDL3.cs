// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL;

[ExcludeFromCodeCoverage]
internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ClearError"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial void ClearError();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetError"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string? GetError();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial int Version();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_InitSubSystem"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool InitSubSystem(Subsystem subsystems);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_QuitSubSystem"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial void QuitSubSystem(Subsystem subsystems);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_Quit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial void Quit();
}

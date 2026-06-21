// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentVideoDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string? GetCurrentVideoDriver();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumRenderDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetNumRenderDrivers();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumVideoDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetNumVideoDrivers();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string GetRenderDriver(int index);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetVideoDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string GetVideoDriver(int index);
}

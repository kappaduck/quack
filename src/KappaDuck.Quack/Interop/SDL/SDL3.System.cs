// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.System;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DisableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool DisableScreenSaver();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_EnableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool EnableScreenSaver();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPowerInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial PowerState GetPowerInfo(out int seconds, out int percent);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPreferredLocales")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Locale** GetPreferredLocales(out int count);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSystemRAM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetSystemRAM();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSystemTheme")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial Theme GetSystemTheme();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_OpenURL", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool OpenURL(string url);
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.Win32.Primitives;
using KappaDuck.Quack.Interop.X11.Primitives;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [SupportedOSPlatform(nameof(OSPlatform.Linux))]
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetX11EventHook")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetX11EventHook(delegate* unmanaged[Cdecl]<void*, XEvent*, byte> callback, void* userData);

    [SupportedOSPlatform(nameof(OSPlatform.Windows))]
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowsMessageHook")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetWindowsMessageHook(delegate* unmanaged[Cdecl]<void*, MSG*, byte> callback, void* userData);
}

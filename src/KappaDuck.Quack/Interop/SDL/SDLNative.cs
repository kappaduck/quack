// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDLNative
{
    internal const string Library = "SDL3";
    internal const string ImageLibrary = "SDL3_image";
    internal const string TTFLibrary = "SDL3_ttf";

    internal static unsafe void Free<T>(T* pointer) where T : unmanaged => Free((nint)pointer);

    internal static unsafe void Free<T>(T** pointer) where T : unmanaged => Free((nint)pointer);

    internal static void Free(nint pointer) => SDL_free(pointer);

    [SupportedOSPlatform("windows")]
    [LibraryImport(Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SDL_SetWindowsMessageHook(Win32Native.MessageCallback callback, nint userData = default);

    [LibraryImport(Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_free(nint pointer);
}

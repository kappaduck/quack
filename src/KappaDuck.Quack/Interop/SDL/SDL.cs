// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using MessageCallback = KappaDuck.Quack.Interop.Win32.Win32.MessageCallback;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal const string Core = "SDL3";
    internal const string Image = "SDL3_image";
    internal const string TTF = "SDL3_ttf";

    [SupportedOSPlatform("windows")]
    [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SDL_SetWindowsMessageHook(MessageCallback callback, nint userData = default);
}

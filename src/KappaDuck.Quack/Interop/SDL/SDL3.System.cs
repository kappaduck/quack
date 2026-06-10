// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.System;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPowerInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial PowerState GetPowerInfo(out int seconds, out int percent);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_OpenURL", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool OpenURL(string url);
}

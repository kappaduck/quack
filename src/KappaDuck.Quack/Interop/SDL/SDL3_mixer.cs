// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3_mixer
{
    [LibraryImport(nameof(SDL3_mixer), EntryPoint = "MIX_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool Init();

    [LibraryImport(nameof(SDL3_mixer), EntryPoint = "MIX_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void Quit();

    [LibraryImport(nameof(SDL3_mixer), EntryPoint = "MIX_Version")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int Version();
}

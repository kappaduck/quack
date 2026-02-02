// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL;

[ExcludeFromCodeCoverage]
internal static partial class SDL3_ttf
{
    [LibraryImport(nameof(SDL3_ttf), EntryPoint = "TTF_Init"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool Init();

    [LibraryImport(nameof(SDL3_ttf), EntryPoint = "TTF_Quit"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial void Quit();

    [LibraryImport(nameof(SDL3_ttf), EntryPoint = "TTF_Version"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial int Version();
}

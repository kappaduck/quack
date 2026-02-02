// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL;

[ExcludeFromCodeCoverage]
internal static partial class SDL3_image
{
    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_Version"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    internal static partial int Version();
}

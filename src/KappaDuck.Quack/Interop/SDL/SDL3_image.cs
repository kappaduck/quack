// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3_image
{
    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_Version")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int Version();
}

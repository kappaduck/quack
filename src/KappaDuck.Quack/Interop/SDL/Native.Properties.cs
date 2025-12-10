// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class Native
{
    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool SDL_SetAppMetadataProperty(string name, string value);
}

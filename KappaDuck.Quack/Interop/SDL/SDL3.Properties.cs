// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal static partial class Properties
    {
        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetAppMetadataProperty", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetAppMetadataProperty(string name, string value);
    }
}

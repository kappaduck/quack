// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal sealed partial class SDL_Cursor() : SafeHandleZeroInvalid(ownsHandle: true)
{
    protected override bool ReleaseHandle()
    {
        SDL_DestroyCursor(handle);
        return true;
    }

    [LibraryImport(nameof(SDL3)), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    private static partial void SDL_DestroyCursor(nint cursor);
}

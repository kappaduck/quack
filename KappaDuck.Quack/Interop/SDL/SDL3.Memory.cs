// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal static partial class Memory
    {
        internal static unsafe void Free<T>(T* ptr) where T : unmanaged
            => SDL_free((nint)ptr);

        internal static unsafe void Free<T>(T** ptr) where T : unmanaged
            => SDL_free((nint)ptr);

        [LibraryImport(nameof(SDL3)), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        private static partial void SDL_free(nint ptr);
    }
}

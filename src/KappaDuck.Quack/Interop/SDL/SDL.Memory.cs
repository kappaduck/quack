// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static partial class Memory
    {
        internal static unsafe void Free<T>(T* pointer) where T : unmanaged => Free((nint)pointer);

        internal static unsafe void Free<T>(T** pointer) where T : unmanaged => Free((nint)pointer);

        internal static void Free(nint pointer) => SDL_free(pointer);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        private static partial void SDL_free(nint pointer);
    }
}

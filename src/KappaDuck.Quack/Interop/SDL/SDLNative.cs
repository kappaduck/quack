// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDLNative
{
    internal const string Library = "SDL3";
    internal const string ImageLibrary = "SDL3_image";
    internal const string TTFLibrary = "SDL3_ttf";

    internal static unsafe void Free<T>(T* pointer) where T : unmanaged => Free((nint)pointer);

    internal static unsafe void Free<T>(T** pointer) where T : unmanaged => Free((nint)pointer);

    internal static void Free(nint pointer) => SDL_free(pointer);

    [LibraryImport(Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_free(nint pointer);
}

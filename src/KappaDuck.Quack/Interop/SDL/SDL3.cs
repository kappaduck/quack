// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_InitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool InitSubSystem(Subsystem subsystem);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_QuitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void QuitSubSystem(Subsystem subsystem);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void Quit();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int Version();
}

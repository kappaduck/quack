// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ShowFileDialogWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ShowFileDialogWithProperties(SDL_FileDialogType type, delegate* unmanaged[Cdecl]<nint, byte**, int, void> callback, nint data, uint properties);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ShowMessageBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ShowMessageBox(in SDL_MessageBoxData data, out int buttonId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ShowSimpleMessageBox", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ShowSimpleMessageBox(SDL_MessageBoxState flags, string title, string message, SDL_Window* window);
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.System;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal static partial class System
    {
        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial SDL_Cursor CreateCursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, int width, int height, int hotSpotX, int hotSpotY);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateSystemCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial SDL_Cursor CreateSystemCursor(CursorType type);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DisableScreenSaver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool DisableScreenSaver();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_EnableScreenSaver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool EnableScreenSaver();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDefaultCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial SDL_Cursor GetDefaultCursor();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPowerInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial PowerStatus GetPowerInfo(out int seconds, out int percent);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSystemTheme"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial Theme GetSystemTheme();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HideCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool HideCursor();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_OpenURL", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool OpenURL(string url);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetCursor(SDL_Cursor cursor);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ShowCursor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool ShowCursor();
    }
}

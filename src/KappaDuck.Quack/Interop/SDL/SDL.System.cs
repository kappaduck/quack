// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL.Handles;
using KappaDuck.Quack.Interop.SDL.Native;
using KappaDuck.Quack.System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static KappaDuck.Quack.System.PowerStatus;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static partial class System
    {
        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_CursorHandle SDL_CreateCursor(ReadOnlySpan<byte> pixels, ReadOnlySpan<byte> mask, int width, int height, int hotSpotX, int hotSpotY);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static unsafe partial SDL_CursorHandle SDL_CreateColorCursor(SDL_Surface* surface, int x, int y);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_CursorHandle SDL_CreateSystemCursor(Cursor.Type type);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_DisableScreenSaver();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_EnableScreenSaver();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_CursorHandle SDL_GetDefaultCursor();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial PowerState SDL_GetPowerInfo(out int seconds, out int percent);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_HideCursor();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SystemTheme SDL_GetSystemTheme();

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_OpenURL(string url);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_ScreenSaverEnabled();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetCursor(SDL_CursorHandle cursor);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_ShowCursor();
    }
}

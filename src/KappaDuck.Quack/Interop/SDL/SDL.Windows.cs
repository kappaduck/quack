// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Interop.SDL.Handles;
using KappaDuck.Quack.Interop.SDL.Native;
using KappaDuck.Quack.Video.Displays;
using KappaDuck.Quack.Windows;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static partial class Windows
    {
        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_WindowHandle SDL_CreateWindowWithProperties(uint properties);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_FlashWindow(SDL_WindowHandle window, FlashOperation operation);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_GetDisplayForWindow(SDL_WindowHandle window);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_GetWindowBordersSize(SDL_WindowHandle window, out int top, out int left, out int bottom, out int right);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial float SDL_GetWindowDisplayScale(SDL_WindowHandle window);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_GetWindowID(SDL_WindowHandle handle);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial float SDL_GetWindowPixelDensity(SDL_WindowHandle window);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial PixelFormat SDL_GetWindowPixelFormat(SDL_WindowHandle window);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetWindowPosition(SDL_WindowHandle window, out int x, out int y);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_GetWindowProperties(SDL_WindowHandle window);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetWindowSafeArea(SDL_WindowHandle window, out RectInt area);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_HideWindow(SDL_WindowHandle handle);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_MaximizeWindow(SDL_WindowHandle handle);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_MinimizeWindow(SDL_WindowHandle handle);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_RaiseWindow(SDL_WindowHandle handle);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_RestoreWindow(SDL_WindowHandle handle);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_ScreenKeyboardShown(SDL_WindowHandle window);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowAlwaysOnTop(SDL_WindowHandle handle, [MarshalAs(UnmanagedType.U1)] bool onTop);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowAspectRatio(SDL_WindowHandle window, float min, float max);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowBordered(SDL_WindowHandle handle, [MarshalAs(UnmanagedType.U1)] bool bordered);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowFocusable(SDL_WindowHandle handle, [MarshalAs(UnmanagedType.U1)] bool focusable);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowFullscreen(SDL_WindowHandle handle, [MarshalAs(UnmanagedType.U1)] bool fullscreen);

        internal static unsafe bool SDL_SetWindowFullscreenMode(SDL_WindowHandle handle, DisplayMode? value)
        {
            if (value is null)
                return SDL_SetWindowFullscreenMode(handle, mode: null);

            DisplayMode mode = value.Value;
            return SDL_SetWindowFullscreenMode(handle, &mode);
        }

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        private static unsafe partial bool SDL_SetWindowFullscreenMode(SDL_WindowHandle window, DisplayMode* mode);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static unsafe partial bool SDL_SetWindowIcon(SDL_WindowHandle window, SDL_Surface* icon);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowKeyboardGrab(SDL_WindowHandle handle, [MarshalAs(UnmanagedType.U1)] bool grabbed);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowMaximumSize(SDL_WindowHandle window, int width, int height);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowMinimumSize(SDL_WindowHandle window, int width, int height);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowMouseGrab(SDL_WindowHandle handle, [MarshalAs(UnmanagedType.U1)] bool grabbed);

        internal static unsafe bool SDL_SetWindowMouseRect(SDL_WindowHandle handle, RectInt? value)
        {
            if (value is null)
                return SDL_SetWindowMouseRect(handle, rectangle: null);

            RectInt rect = value.Value;
            return SDL_SetWindowMouseRect(handle, &rect);
        }

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        private static unsafe partial bool SDL_SetWindowMouseRect(SDL_WindowHandle window, RectInt* rectangle);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowOpacity(SDL_WindowHandle window, float opacity);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowPosition(SDL_WindowHandle window, int x, int y);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowResizable(SDL_WindowHandle handle, [MarshalAs(UnmanagedType.U1)] bool resizable);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowSize(SDL_WindowHandle window, int width, int height);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetWindowTitle(SDL_WindowHandle window, string title);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_ShowWindow(SDL_WindowHandle window);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_ShowWindowSystemMenu(SDL_WindowHandle window, int x, int y);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SyncWindow(SDL_WindowHandle window);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_WarpMouseInWindow(SDL_WindowHandle window, float x, float y);
    }
}

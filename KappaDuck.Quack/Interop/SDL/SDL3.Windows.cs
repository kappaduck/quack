// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Displays;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal static partial class Windows
    {
        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateWindowWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial SDL_Window CreateWindowWithProperties(uint properties);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FlashWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool FlashWindow(SDL_Window window, FlashOperation operation);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayForWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial uint GetDisplayForWindow(SDL_Window window);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowBordersSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial void GetWindowBordersSize(SDL_Window window, out int top, out int left, out int bottom, out int right);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowDisplayScale"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial float GetWindowDisplayScale(SDL_Window window);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowFlags"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial ulong GetWindowFlags(SDL_Window handle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial uint GetWindowID(SDL_Window handle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowPixelDensity"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial float GetWindowPixelDensity(SDL_Window window);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowPixelFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial PixelFormat GetWindowPixelFormat(SDL_Window window);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool GetWindowPosition(SDL_Window window, out int x, out int y);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial uint GetWindowProperties(SDL_Window window);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowSafeArea"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool GetWindowSafeArea(SDL_Window window, out RectInt area);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HideWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool HideWindow(SDL_Window handle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_MaximizeWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool MaximizeWindow(SDL_Window handle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_MinimizeWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool MinimizeWindow(SDL_Window handle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RaiseWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool RaiseWindow(SDL_Window handle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RestoreWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool RestoreWindow(SDL_Window handle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ScreenKeyboardShown"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool ScreenKeyboardShown(SDL_Window window);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowAlwaysOnTop"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowAlwaysOnTop(SDL_Window handle, [MarshalAs(UnmanagedType.U1)] bool onTop);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowAspectRatio"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowAspectRatio(SDL_Window window, float min, float max);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowBordered"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowBordered(SDL_Window handle, [MarshalAs(UnmanagedType.U1)] bool bordered);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowFocusable"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowFocusable(SDL_Window handle, [MarshalAs(UnmanagedType.U1)] bool focusable);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowFullscreen"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowFullscreen(SDL_Window handle, [MarshalAs(UnmanagedType.U1)] bool fullscreen);

        internal static unsafe bool SetWindowFullscreenMode(SDL_Window handle, DisplayMode? value)
        {
            if (value is null)
                return SDL_SetWindowFullscreenMode(handle, mode: null);

            DisplayMode mode = value.Value;
            return SDL_SetWindowFullscreenMode(handle, &mode);
        }

        [LibraryImport(nameof(SDL3)), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static unsafe partial bool SDL_SetWindowFullscreenMode(SDL_Window window, DisplayMode* mode);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowKeyboardGrab"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowKeyboardGrab(SDL_Window handle, [MarshalAs(UnmanagedType.U1)] bool grabbed);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowMaximumSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowMaximumSize(SDL_Window window, int width, int height);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowMinimumSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowMinimumSize(SDL_Window window, int width, int height);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowMouseGrab"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowMouseGrab(SDL_Window handle, [MarshalAs(UnmanagedType.U1)] bool grabbed);

        internal static unsafe bool SetWindowMouseRect(SDL_Window handle, RectInt? value)
        {
            if (value is null)
                return SDL_SetWindowMouseRect(handle, rectangle: null);

            RectInt rect = value.Value;
            return SDL_SetWindowMouseRect(handle, &rect);
        }

        [LibraryImport(nameof(SDL3)), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static unsafe partial bool SDL_SetWindowMouseRect(SDL_Window window, RectInt* rectangle);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowOpacity"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowOpacity(SDL_Window window, float opacity);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowPosition(SDL_Window window, int x, int y);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowRelativeMouseMode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowRelativeMouseMode(SDL_Window window, [MarshalAs(UnmanagedType.U1)] bool enabled);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowResizable"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowResizable(SDL_Window handle, [MarshalAs(UnmanagedType.U1)] bool resizable);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowSize"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowSize(SDL_Window window, int width, int height);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowTitle", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetWindowTitle(SDL_Window window, string title);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ShowWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool ShowWindow(SDL_Window window);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ShowWindowSystemMenu"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool ShowWindowSystemMenu(SDL_Window window, int x, int y);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SyncWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SyncWindow(SDL_Window window);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WarpMouseInWindow"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial void WarpMouseInWindow(SDL_Window window, float x, float y);
    }
}

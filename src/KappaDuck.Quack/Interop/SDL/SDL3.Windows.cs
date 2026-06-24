// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Pixels;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ClearComposition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ClearComposition(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreatePopupWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Window* CreatePopupWindow(SDL_Window* parent, int offsetX, int offsetY, int width, int height, ulong flags);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateWindowWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Window* CreateWindowWithProperties(uint properties);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void DestroyWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DestroyWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool DestroyWindowSurface(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FlashWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool FlashWindow(SDL_Window* window, FlashOperation operation);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDisplayForWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetDisplayForWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetGrabbedWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Window* GetGrabbedWindow();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyboardFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Window* GetKeyboardFocus();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetMouseFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Window* GetMouseFocus();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowAspectRatio(SDL_Window* window, out float minAspect, out float maxAspect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowBordersSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowBordersSize(SDL_Window* window, out int top, out int left, out int bottom, out int right);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowDisplayScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial float GetWindowDisplayScale(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial Window.State GetWindowFlags(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Window* GetWindowFromID(uint id);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowFullscreenMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_DisplayMode* GetWindowFullscreenMode(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetWindowID(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowKeyboardGrab(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowMaximumSize(SDL_Window* window, out int width, out int height);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowMinimumSize(SDL_Window* window, out int width, out int height);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowMouseGrab(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial RectI* GetWindowMouseRect(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial float GetWindowOpacity(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Window* GetWindowParent(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowPixelDensity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial float GetWindowPixelDensity(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial PixelFormat GetWindowPixelFormat(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowPosition(SDL_Window* window, out int x, out int y);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetWindowProperties(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowRelativeMouseMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowRelativeMouseMode(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowSafeArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowSafeArea(SDL_Window* window, out RectI rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowSize(SDL_Window* window, out int width, out int height);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowSizeInPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetWindowSizeInPixels(SDL_Window* window, out int width, out int height);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string GetWindowTitle(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HideWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool HideWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_MaximizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool MaximizeWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_MinimizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool MinimizeWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RaiseWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RaiseWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RestoreWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RestoreWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ScreenKeyboardShown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ScreenKeyboardShown(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRelativeMouseTransform")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRelativeMouseTransform(delegate* unmanaged[Cdecl]<void*, ulong, SDL_Window*, uint, float*, float*, void> callback, void* userdata);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowAlwaysOnTop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowAlwaysOnTop(SDL_Window* window, [MarshalAs(UnmanagedType.I1)] bool onTop);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowAspectRatio(SDL_Window* window, float minAspect, float maxAspect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowBordered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowBordered(SDL_Window* window, [MarshalAs(UnmanagedType.I1)] bool bordered);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowFocusable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowFocusable(SDL_Window* window, [MarshalAs(UnmanagedType.I1)] bool focusable);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowFullscreen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowFullscreen(SDL_Window* window, [MarshalAs(UnmanagedType.I1)] bool fullscreen);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowFullscreenMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowFullscreenMode(SDL_Window* window, SDL_DisplayMode* mode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowIcon(SDL_Window* window, SDL_Surface* icon);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowKeyboardGrab(SDL_Window* window, [MarshalAs(UnmanagedType.I1)] bool grabbed);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowMaximumSize(SDL_Window* window, int maxWidth, int maxHeight);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowMinimumSize(SDL_Window* window, int minWidth, int minHeight);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowMouseGrab(SDL_Window* window, [MarshalAs(UnmanagedType.I1)] bool grabbed);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowMouseRect(SDL_Window* window, RectI* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowOpacity(SDL_Window* window, float opacity);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowPosition(SDL_Window* window, int x, int y);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowRelativeMouseMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowRelativeMouseMode(SDL_Window* window, [MarshalAs(UnmanagedType.I1)] bool enabled);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowResizable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowResizable(SDL_Window* window, [MarshalAs(UnmanagedType.I1)] bool resizable);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowSize(SDL_Window* window, int width, int height);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetWindowTitle", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetWindowTitle(SDL_Window* window, string title);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ShowWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ShowWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ShowWindowSystemMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ShowWindowSystemMenu(SDL_Window* window, int x, int y);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SyncWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SyncWindow(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WarpMouseInWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void WarpMouseInWindow(SDL_Window* window, float x, float y);
}

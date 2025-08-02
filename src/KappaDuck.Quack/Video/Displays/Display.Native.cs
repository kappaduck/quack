// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Marshallers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Native methods for <see cref="Display"/>.
/// </summary>
public sealed partial class Display
{
    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_GetClosestFullscreenDisplayMode(uint display, int width, int height, float refreshRate, [MarshalAs(UnmanagedType.U1)] bool includeHighDensityMode, out DisplayMode displayMode);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial DisplayMode* SDL_GetCurrentDisplayMode(uint display);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint display);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial DisplayMode* SDL_GetDesktopDisplayMode(uint display);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedArrayMarshaller<,>), CountElementName = "length")]
    private static partial Span<uint> SDL_GetDisplays(out int length);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_GetDisplayBounds(uint display, out RectInt bounds);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetDisplayContentScale(uint display);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial uint SDL_GetDisplayForPoint(Vector2Int* point);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial uint SDL_GetDisplayForRect(RectInt* rectangle);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    private static partial string SDL_GetDisplayName(uint display);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetDisplayProperties(uint display);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_GetDisplayUsableBounds(uint display, out RectInt bounds);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial DisplayMode** SDL_GetFullscreenDisplayModes(uint display, out int count);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(uint display);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPrimaryDisplay();
}

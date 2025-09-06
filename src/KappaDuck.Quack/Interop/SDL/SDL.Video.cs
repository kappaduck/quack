// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.Marshalling;
using KappaDuck.Quack.Video.Displays;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static partial class Video
    {
        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetClosestFullscreenDisplayMode(uint display, int width, int height, float refreshRate, [MarshalAs(UnmanagedType.U1)] bool includeHighDensityMode, out DisplayMode displayMode);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static unsafe partial DisplayMode* SDL_GetCurrentDisplayMode(uint display);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(uint display);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static unsafe partial DisplayMode* SDL_GetDesktopDisplayMode(uint display);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(CallerOwnedArrayMarshaller<,>), CountElementName = "length")]
        internal static partial Span<uint> SDL_GetDisplays(out int length);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetDisplayBounds(uint display, out RectInt bounds);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial float SDL_GetDisplayContentScale(uint display);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static unsafe partial uint SDL_GetDisplayForPoint(Vector2Int* point);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static unsafe partial uint SDL_GetDisplayForRect(RectInt* rectangle);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetDisplayName(uint display);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_GetDisplayProperties(uint display);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetDisplayUsableBounds(uint display, out RectInt bounds);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static unsafe partial DisplayMode** SDL_GetFullscreenDisplayModes(uint display, out int count);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(uint display);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_GetPrimaryDisplay();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetCurrentVideoDriver();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial int SDL_GetNumRenderDrivers();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial int SDL_GetNumVideoDrivers();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetRenderDriver(int index);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetVideoDriver(int index);
    }
}

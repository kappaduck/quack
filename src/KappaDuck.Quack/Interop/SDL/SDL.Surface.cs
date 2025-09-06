// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Interop.Marshalling;
using KappaDuck.Quack.Interop.SDL.Native;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static unsafe partial class Surface
    {
        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_ClearSurface(SDL_Surface* surface, float r, float g, float b, float a);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_Surface* SDL_ConvertSurface(SDL_Surface* surface, PixelFormat format);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_Surface* SDL_CreateSurface(int width, int height, PixelFormat format);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_DestroySurface(SDL_Surface* surface);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_Surface* SDL_DuplicateSurface(SDL_Surface* surface);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_FillSurfaceRect(SDL_Surface* surface, RectInt* rect, uint color);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_FillSurfaceRects(SDL_Surface* surface, ReadOnlySpan<RectInt> rects, int count, uint color);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_FlipSurface(SDL_Surface* surface, FlipMode mode);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_Palette* SDL_GetSurfacePalette(SDL_Surface* surface);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_Surface* SDL_ScaleSurface(SDL_Surface* surface, int width, int height, ScaleMode mode);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetSurfacePalette(SDL_Surface* surface, SDL_Palette* palette);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SurfaceHasColorKey(SDL_Surface* surface);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SurfaceHasRLE(SDL_Surface* surface);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetMasksForPixelFormat(PixelFormat format, out int bitsPerPixel, out uint redMask, out uint greenMask, out uint blueMask, out uint alphaMask);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial PixelFormatDetails* SDL_GetPixelFormatDetails(PixelFormat format);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetPixelFormatName(PixelFormat format);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial PixelFormat SDL_GetPixelFormatForMasks(int bitsPerPixel, uint redMask, uint greenMask, uint blueMask, uint alphaMask);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_GetRGB(uint pixel, PixelFormatDetails* details, SDL_Palette* palette, byte* r, byte* g, byte* b);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_GetRGBA(uint pixel, PixelFormatDetails* details, SDL_Palette* palette, byte* r, byte* g, byte* b, byte* a);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_MapRGB(PixelFormatDetails* details, SDL_Palette* palette, byte r, byte g, byte b);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_MapRGBA(PixelFormatDetails* details, SDL_Palette* palette, byte r, byte g, byte b, byte a);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_Palette* SDL_CreatePalette(int length);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_Palette* SDL_CreateSurfacePalette(SDL_Surface* surface);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_DestroyPalette(SDL_Palette* palette);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetPaletteColors(SDL_Palette* palette, ReadOnlySpan<SDL_Color> colors, int startIndex, int length);

        [LibraryImport(Image, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_Surface* IMG_Load(string filePath);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial BlendMode SDL_ComposeCustomBlendMode(BlendFactor source, BlendFactor destination, BlendOperation operation, BlendFactor sourceAlpha, BlendFactor destinationAlpha, BlendOperation alphaOperation);
    }
}

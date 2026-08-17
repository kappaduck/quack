// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_AddSurfaceAlternateImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool AddSurfaceAlternateImage(SDL_Surface* surface, SDL_Surface* image);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_BlitSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool BlitSurface(SDL_Surface* source, RectI* sourceRect, SDL_Surface* destination, RectI* destinationRect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_BlitSurface9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool BlitSurface9Grid(SDL_Surface* source, RectI* sourceRect, int leftWidth, int rightWidth, int topHeight, int bottomHeight, float scale, ScaleMode mode, SDL_Surface* destination, RectI* destinationRect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_BlitSurfaceScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool BlitSurfaceScaled(SDL_Surface* source, RectI* sourceRect, SDL_Surface* destination, RectI* destinationRect, ScaleMode mode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_BlitSurfaceTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool BlitSurfaceTiled(SDL_Surface* source, RectI* sourceRect, SDL_Surface* destination, RectI* destinationRect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_BlitSurfaceTiledWithScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool BlitSurfaceTiledWithScale(SDL_Surface* source, RectI* sourceRect, float scale, ScaleMode mode, SDL_Surface* destination, RectI* destinationRect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_BlitSurfaceUnchecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool BlitSurfaceUnchecked(SDL_Surface* source, RectI* sourceRect, SDL_Surface* destination, RectI* destinationRect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_BlitSurfaceUncheckedScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool BlitSurfaceUncheckedScaled(SDL_Surface* source, RectI* sourceRect, SDL_Surface* destination, RectI* destinationRect, ScaleMode mode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ClearSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ClearSurface(SDL_Surface* surface, float r, float g, float b, float a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ConvertPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ConvertPixels(int width, int height, PixelFormat sourceFormat, ReadOnlySpan<byte> source, int sourcePitch, PixelFormat destinationFormat, Span<byte> destination, int destinationPitch);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ConvertPixelsAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ConvertPixelsAndColorspace(int width, int height, PixelFormat sourceFormat, Colorspace sourceColorSpace, uint sourceProperties, ReadOnlySpan<byte> source, int sourcePitch, PixelFormat destinationFormat, Colorspace destinationColorSpace, uint destinationProperties, Span<byte> destination, int destinationPitch);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ConvertSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* ConvertSurface(SDL_Surface* surface, PixelFormat format);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ConvertSurfaceAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* ConvertSurfaceAndColorspace(SDL_Surface* surface, PixelFormat format, SDL_Palette* palette, Colorspace colorspace, uint properties);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* CreateSurface(int width, int height, PixelFormat format);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateSurfaceFrom")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* CreateSurfaceFrom(int width, int height, PixelFormat format, void* pixels, int pitch);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Palette* CreateSurfacePalette(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DestroySurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void DestroySurface(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DuplicateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* DuplicateSurface(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FillSurfaceRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool FillSurfaceRect(SDL_Surface* destination, RectI* rect, uint color);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FillSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool FillSurfaceRects(SDL_Surface* destination, ReadOnlySpan<RectI> rects, int count, uint color);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FlipSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool FlipSurface(SDL_Surface* surface, FlipMode flip);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetSurfaceAlphaMod(SDL_Surface* surface, byte* alpha);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetSurfaceBlendMode(SDL_Surface* surface, BlendMode* blendMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetSurfaceColorMod(SDL_Surface* surface, byte* r, byte* g, byte* b);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial Colorspace GetSurfaceColorspace(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSurfaceImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface** GetSurfaceImages(SDL_Surface* surface, int* count);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Palette* GetSurfacePalette(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetSurfaceProperties(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_LockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool LockSurface(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_MapSurfaceRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint MapSurfaceRGB(SDL_Surface* surface, byte r, byte g, byte b);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_MapSurfaceRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint MapSurfaceRGBA(SDL_Surface* surface, byte r, byte g, byte b, byte a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PremultiplyAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool PremultiplyAlpha(int width, int height, PixelFormat sourceFormat, ReadOnlySpan<byte> source, int sourcePitch, PixelFormat destinationFormat, Span<byte> destination, int destinationPitch, [MarshalAs(UnmanagedType.I1)] bool linear);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PremultiplySurfaceAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool PremultiplySurfaceAlpha(SDL_Surface* surface, [MarshalAs(UnmanagedType.I1)] bool linear);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ReadSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ReadSurfacePixel(SDL_Surface* surface, int x, int y, byte* r, byte* g, byte* b, byte* a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ReadSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ReadSurfacePixelFloat(SDL_Surface* surface, int x, int y, float* r, float* g, float* b, float* a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RemoveSurfaceAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void RemoveSurfaceAlternateImages(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RotateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* RotateSurface(SDL_Surface* surface, float angle);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ScaleSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* ScaleSurface(SDL_Surface* surface, int width, int height, ScaleMode scale);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetSurfaceAlphaMod")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetSurfaceAlphaMod(SDL_Surface* surface, byte alpha);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetSurfaceBlendMode")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetSurfaceBlendMode(SDL_Surface* surface, BlendMode blendMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetSurfaceClipRect")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetSurfaceClipRect(SDL_Surface* surface, RectI* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetSurfaceColorKey")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetSurfaceColorKey(SDL_Surface* surface, [MarshalAs(UnmanagedType.I1)] bool enabled, uint key);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetSurfaceColorMod")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetSurfaceColorMod(SDL_Surface* surface, byte r, byte g, byte b);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetSurfaceColorspace")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetSurfaceColorspace(SDL_Surface* surface, Colorspace colorspace);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetSurfacePalette")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetSurfacePalette(SDL_Surface* surface, SDL_Palette* palette);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetSurfaceRLE")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetSurfaceRLE(SDL_Surface* surface, [MarshalAs(UnmanagedType.I1)] bool enabled);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_StretchSurface")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool StretchSurface(SDL_Surface* source, RectI* sourceRect, SDL_Surface* destination, RectI* destinationRect, ScaleMode mode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SurfaceHasRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SurfaceHasRLE(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_UnlockSurface")]
    internal static partial void UnlockSurface(SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WriteSurfacePixel")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool WriteSurfacePixel(SDL_Surface* surface, int x, int y, byte r, byte g, byte b, byte a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WriteSurfacePixelFloat")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool WriteSurfacePixelFloat(SDL_Surface* surface, int x, int y, float r, float g, float b, float a);
}

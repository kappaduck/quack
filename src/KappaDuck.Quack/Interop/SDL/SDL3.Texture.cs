// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Imaging;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Texture* CreateTexture(SDL_Renderer* renderer, PixelFormat format, TextureAccess access, int width, int height);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateTextureFromSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Texture* CreateTextureFromSurface(SDL_Renderer* renderer, SDL_Surface* surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DestroyTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void DestroyTexture(SDL_Texture* texture);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetTextureAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetTextureAlphaMod(SDL_Texture* texture, byte* alpha);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetTextureBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetTextureBlendMode(SDL_Texture* texture, BlendMode* blendMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetTextureColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetTextureColorMod(SDL_Texture* texture, byte* r, byte* g, byte* b);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetTexturePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Palette* GetTexturePalette(SDL_Texture* texture);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetTextureProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetTextureProperties(SDL_Texture* texture);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetTextureScaleMode(SDL_Texture* texture, ScaleMode* scaleMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_LockTextureToSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool LockTextureToSurface(SDL_Texture* texture, RectI* rect, SDL_Surface** surface);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderTexture(SDL_Renderer* renderer, SDL_Texture* texture, Rect* source, Rect* destination);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderTexture9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderTexture9Grid(SDL_Renderer* renderer, SDL_Texture* texture, Rect* source, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, Rect* destination);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderTexture9GridTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderTexture9GridTiled(SDL_Renderer* renderer, SDL_Texture* texture, Rect* source, float leftWidth, float rightWidth, float topHeight, float bottomHeight, float scale, Rect* destination, float tileScale);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderTextureAffine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderTextureAffine(SDL_Renderer* renderer, SDL_Texture* texture, Rect* source, PointF* origin, PointF* right, PointF* down);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderTextureRotated")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderTextureRotated(SDL_Renderer* renderer, SDL_Texture* texture, Rect* source, Rect* destination, double angle, PointF* center, FlipMode flip);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderTextureTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderTextureTiled(SDL_Renderer* renderer, SDL_Texture* texture, Rect* source, float scale, Rect* destination);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderTarget")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderTarget(SDL_Renderer* renderer, SDL_Texture* texture);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetTextureAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetTextureAlphaMod(SDL_Texture* texture, byte alpha);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetTextureBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetTextureBlendMode(SDL_Texture* texture, BlendMode blendMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetTextureColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetTextureColorMod(SDL_Texture* texture, byte r, byte g, byte b);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetTexturePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetTexturePalette(SDL_Texture* texture, SDL_Palette* palette);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetTextureScaleMode(SDL_Texture* texture, ScaleMode scaleMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_UnlockTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void UnlockTexture(SDL_Texture* texture);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_UpdateNVTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool UpdateNVTexture(SDL_Texture* texture, RectI* rect, ReadOnlySpan<byte> yPlane, int yPitch, ReadOnlySpan<byte> uvPlane, int uvPitch);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_UpdateTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool UpdateTexture(SDL_Texture* texture, RectI* rect, ReadOnlySpan<byte> pixels, int pitch);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_UpdateYUVTexture")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool UpdateYUVTexture(SDL_Texture* texture, RectI* rect, ReadOnlySpan<byte> yPlane, int yPitch, ReadOnlySpan<byte> uPlane, int uPitch, ReadOnlySpan<byte> vPlane, int vPitch);
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ConvertEventToRenderCoordinates")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool ConvertEventToRenderCoordinates(SDL_Renderer* renderer, SDL_Event* e);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateRenderer", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Renderer* CreateRenderer(SDL_Window* window, string? name);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateRendererWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Renderer* CreateRendererWithProperties(uint properties);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DestroyRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void DestroyRenderer(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FlushRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool FlushRenderer(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetCurrentRenderOutputSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetCurrentRenderOutputSize(SDL_Renderer* renderer, int* width, int* height);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetDefaultTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetDefaultTextureScaleMode(SDL_Renderer* renderer, ScaleMode* scaleMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderClipRect(SDL_Renderer* renderer, RectI* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderColorScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderColorScale(SDL_Renderer* renderer, float* scale);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderDrawBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderDrawBlendMode(SDL_Renderer* renderer, BlendMode* blendMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderDrawColor(SDL_Renderer* renderer, byte* r, byte* g, byte* b, byte* a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderDrawColorFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderDrawColorFloat(SDL_Renderer* renderer, float* r, float* g, float* b, float* a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Renderer* GetRenderer(SDL_Window* window);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRendererName", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string? GetRendererName(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRendererProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint GetRendererProperties(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderLogicalPresentation(SDL_Renderer* renderer, int* width, int* height, LogicalPresentation* mode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderLogicalPresentationRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderLogicalPresentationRect(SDL_Renderer* renderer, Rect* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderOutputSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderOutputSize(SDL_Renderer* renderer, int* width, int* height);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderSafeArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderSafeArea(SDL_Renderer* renderer, RectI* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderScale(SDL_Renderer* renderer, float* scaleX, float* scaleY);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderTextureAddressMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderTextureAddressMode(SDL_Renderer* renderer, TextureAddressMode* uMode, TextureAddressMode* vMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderViewport(SDL_Renderer* renderer, RectI* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetRenderVSync(SDL_Renderer* renderer, int* vsync);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRenderWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Window* GetRenderWindow(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderClear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderClear(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderClipEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderClipEnabled(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderCoordinatesFromWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderCoordinatesFromWindow(SDL_Renderer* renderer, float windowX, float windowY, float* x, float* y);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderCoordinatesToWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderCoordinatesToWindow(SDL_Renderer* renderer, float x, float y, float* windowX, float* windowY);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderDebugText", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderDebugText(SDL_Renderer* renderer, float x, float y, string text);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderFillRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderFillRect(SDL_Renderer* renderer, Rect* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderFillRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderFillRects(SDL_Renderer* renderer, ReadOnlySpan<Rect> rects, int count);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderGeometry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderGeometry(SDL_Renderer* renderer, SDL_Texture* texture, ReadOnlySpan<Vertex> vertices, int numVertices, ReadOnlySpan<int> indices, int numIndices);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderGeometry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool UnsafeRenderGeometry(SDL_Renderer* renderer, SDL_Texture* texture, ReadOnlySpan<Vertex> vertices, int numVertices, int* indices, int numIndices);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderGeometryRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderGeometryRaw(SDL_Renderer* renderer, SDL_Texture* texture, float* xy, int xyStride, ColorF* color, int colorStride, float* uv, int uvStride, int numVertices, void* indices, int numIndices, int sizeIndices);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderLine")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderLine(SDL_Renderer* renderer, float x1, float y1, float x2, float y2);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderLines")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderLines(SDL_Renderer* renderer, ReadOnlySpan<PointF> points, int count);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderPoint(SDL_Renderer* renderer, float x, float y);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderPoints(SDL_Renderer* renderer, ReadOnlySpan<PointF> points, int count);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderPresent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderPresent(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderReadPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* RenderReadPixels(SDL_Renderer* renderer, RectI* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderRect(SDL_Renderer* renderer, Rect* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderRects(SDL_Renderer* renderer, ReadOnlySpan<Rect> rects, int count);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RenderViewportSet")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool RenderViewportSet(SDL_Renderer* renderer);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetDefaultTextureScaleMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetDefaultTextureScaleMode(SDL_Renderer* renderer, ScaleMode scaleMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderClipRect(SDL_Renderer* renderer, RectI* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderColorScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderColorScale(SDL_Renderer* renderer, float scale);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderDrawBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderDrawBlendMode(SDL_Renderer* renderer, BlendMode blendMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderDrawColor(SDL_Renderer* renderer, byte r, byte g, byte b, byte a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderDrawColorFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderDrawColorFloat(SDL_Renderer* renderer, float r, float g, float b, float a);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderLogicalPresentation(SDL_Renderer* renderer, int width, int height, LogicalPresentation mode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderScale(SDL_Renderer* renderer, float scaleX, float scaleY);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderTextureAddressMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderTextureAddressMode(SDL_Renderer* renderer, TextureAddressMode uMode, TextureAddressMode vMode);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderViewport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderViewport(SDL_Renderer* renderer, RectI* rect);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetRenderVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetRenderVSync(SDL_Renderer* renderer, int vsync);
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Interop.SDL.Handles;
using KappaDuck.Quack.Interop.SDL.Native;
using KappaDuck.Quack.Video.Displays;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static partial class Renderer
    {
        [LibraryImport(Image, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_TextureHandle IMG_LoadTexture(SDL_RendererHandle renderer, string file);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_ConvertEventToRenderCoordinates(SDL_RendererHandle renderer, ref Event e);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_RendererHandle SDL_CreateRenderer(SDL_WindowHandle window, string? name = null);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial SDL_TextureHandle SDL_CreateTexture(SDL_RendererHandle renderer, PixelFormat format, TextureAccess access, int width, int height);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static unsafe partial SDL_TextureHandle SDL_CreateTextureFromSurface(SDL_RendererHandle renderer, SDL_Surface* surface);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetCurrentRenderOutputSize(SDL_RendererHandle renderer, out int w, out int h);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_RenderDebugText(SDL_RendererHandle renderer, float x, float y, string text);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetRenderLogicalPresentation(SDL_RendererHandle renderer, out int width, out int height, out LogicalPresentation mode);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetRenderLogicalPresentationRect(SDL_RendererHandle renderer, out Rect rect);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetRenderOutputSize(SDL_RendererHandle renderer, out int w, out int h);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetRenderSafeArea(SDL_RendererHandle renderer, out RectInt rect);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_RenderClear(SDL_RendererHandle renderer);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_RenderCoordinatesFromWindow(SDL_RendererHandle renderer, float windowX, float windowY, out float x, out float y);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_RenderCoordinatesToWindow(SDL_RendererHandle renderer, float x, float y, out float windowX, out float windowY);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_RenderGeometry(SDL_RendererHandle renderer, nint texture, ReadOnlySpan<Vertex> vertices, int numVertices, ReadOnlySpan<int> indices, int numIndices);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_RenderPresent(SDL_RendererHandle renderer);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static unsafe partial void SDL_RenderTextureRotated(SDL_RendererHandle renderer, SDL_TextureHandle texture, Rect* source, Rect* destination, double angle, Vector2* center, FlipMode flip);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_SetRenderDrawColor(SDL_RendererHandle renderer, byte r, byte g, byte b, byte a);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetRenderLogicalPresentation(SDL_RendererHandle renderer, int width, int height, LogicalPresentation mode);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetRenderVSync(SDL_RendererHandle renderer, int vsync);
    }
}

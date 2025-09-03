// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Graphics.Pixels;

public sealed unsafe partial class Surface
{
    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_ClearSurface(SurfaceHandle* surface, float r, float g, float b, float a);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SurfaceHandle* SDL_ConvertSurface(SurfaceHandle* surface, PixelFormat format);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SurfaceHandle* SDL_CreateSurface(int width, int height, PixelFormat format);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroySurface(SurfaceHandle* surface);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SurfaceHandle* SDL_DuplicateSurface(SurfaceHandle* surface);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_FillSurfaceRect(SurfaceHandle* surface, RectInt* rect, uint color);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_FillSurfaceRects(SurfaceHandle* surface, ReadOnlySpan<RectInt> rects, int count, uint color);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_FlipSurface(SurfaceHandle* surface, FlipMode mode);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette.PaletteHandle* SDL_GetSurfacePalette(SurfaceHandle* surface);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SurfaceHandle* SDL_ScaleSurface(SurfaceHandle* surface, int width, int height, ScaleMode mode);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SetSurfacePalette(SurfaceHandle* surface, Palette.PaletteHandle* palette);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SurfaceHasColorKey(SurfaceHandle* surface);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SurfaceHasRLE(SurfaceHandle* surface);

    [StructLayout(LayoutKind.Sequential)]
    internal struct SurfaceHandle
    {
        public readonly SurfaceState State;

        public readonly PixelFormat Format;

        public readonly int Width;

        public readonly int Height;

        public readonly int Pitch;

        public void* Pixels;

        private readonly int _refCount;
        private readonly nint _reserved;
    }

    [Flags]
    internal enum SurfaceState : uint
    {
        None = 0x00000000u,

        /// <summary>
        /// Surface uses preallocated pixel memory.
        /// </summary>
        PreAllocated = 0x00000001u,

        /// <summary>
        /// Surface needs to be locked to access pixels.
        /// </summary>
        LockNeeded = 0x00000002u,

        /// <summary>
        /// Surface is currently locked.
        /// </summary>
        Locked = 0x00000004u,

        /// <summary>
        /// Surface uses pixels that are allocated with SIMD alignment.
        /// </summary>
        SimdAligned = 0x00000008u,
    }
}

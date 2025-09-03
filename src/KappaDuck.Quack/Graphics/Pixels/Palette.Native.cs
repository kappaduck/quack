// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static KappaDuck.Quack.Graphics.Pixels.Surface;

namespace KappaDuck.Quack.Graphics.Pixels;

public sealed unsafe partial class Palette
{
    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PaletteHandle* SDL_CreatePalette(int length);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PaletteHandle* SDL_CreateSurfacePalette(SurfaceHandle* surface);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyPalette(PaletteHandle* palette);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SetPaletteColors(PaletteHandle* palette, ReadOnlySpan<NativeColor> colors, int startIndex, int length);

    [StructLayout(LayoutKind.Sequential)]
    internal struct PaletteHandle
    {
        public readonly int Length;

        public NativeColor* Colors;

        private readonly uint _version;
        private readonly int _refCount;
    }
}

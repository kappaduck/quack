// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Interop.SDL.Handles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static partial class Texture
    {
        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetTextureAlphaMod(SDL_TextureHandle texture, out byte alpha);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetTextureBlendMode(SDL_TextureHandle texture, out BlendMode mode);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetTextureColorMod(SDL_TextureHandle texture, out byte r, out byte g, out byte b);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetTextureScaleMode(SDL_TextureHandle texture, out ScaleMode scale);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_GetTextureProperties(SDL_TextureHandle texture);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetTextureAlphaMod(SDL_TextureHandle texture, byte alpha);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetTextureBlendMode(SDL_TextureHandle texture, BlendMode mode);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetTextureColorMod(SDL_TextureHandle texture, byte r, byte g, byte b);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetTextureScaleMode(SDL_TextureHandle texture, ScaleMode scale);
    }
}

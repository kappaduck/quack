// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3_image
{
    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_LoadTexture", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Texture* LoadTexture(SDL_Renderer* renderer, string file);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_LoadTexture_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Texture* LoadTexture(SDL_Renderer* renderer, SDL_IOStream* source, [MarshalAs(UnmanagedType.I1)] bool closeIO);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_Version")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int Version();
}

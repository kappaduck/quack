// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3_image
{
    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_AddAnimationEncoderFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool AddAnimationEncoderFrame(IMG_AnimationEncoder* encoder, SDL_Surface* frame, ulong duration);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_CloseAnimationEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool CloseAnimationEncoder(IMG_AnimationEncoder* encoder);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_CreateAnimatedCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Cursor* CreateAnimatedCursor(IMG_Animation* animation, int hotX, int hotY);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_CreateAnimationEncoderWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IMG_AnimationEncoder* CreateAnimationEncoderWithProperties(uint properties);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_FreeAnimation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FreeAnimation(IMG_Animation* animation);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_Load", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* FromFile(string file);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_LoadTexture", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Texture* FromFile(SDL_Renderer* renderer, string file);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_Load_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Surface* FromStream(SDL_IOStream* stream, [MarshalAs(UnmanagedType.I1)] bool closeIO);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_LoadTexture_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SDL_Texture* FromStream(SDL_Renderer* renderer, SDL_IOStream* stream, [MarshalAs(UnmanagedType.I1)] bool closeIO);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_LoadAnimation", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IMG_Animation* LoadAnimation(string file);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_LoadAnimation_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IMG_Animation* LoadAnimation(SDL_IOStream* stream, [MarshalAs(UnmanagedType.I1)] bool closeIO);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_Save", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool Save(SDL_Surface* surface, string file);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveTyped_IO", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool Save(SDL_Surface* surface, SDL_IOStream* stream, string type);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveAnimation", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SaveAnimation(IMG_Animation* animation, string file);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveAnimationTyped_IO", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SaveAnimation(IMG_Animation* animation, SDL_IOStream* stream, [MarshalAs(UnmanagedType.I1)] bool closeIO, string type);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveAVIF", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SaveAVIF(SDL_Surface* surface, string file, int quality);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveAVIF_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SaveAVIF(SDL_Surface* surface, SDL_IOStream* stream, int quality);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveJPG", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SaveJPG(SDL_Surface* surface, string file, int quality);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveJPG_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SaveJPG(SDL_Surface* surface, SDL_IOStream* stream, int quality);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveWEBP", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SaveWEBP(SDL_Surface* surface, string file, float quality);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_SaveWEBP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SaveWEBP(SDL_Surface* surface, SDL_IOStream* stream, float quality);

    [LibraryImport(nameof(SDL3_image), EntryPoint = "IMG_Version")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int Version();
}

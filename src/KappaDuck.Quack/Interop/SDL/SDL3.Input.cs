// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyboardNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string? GetKeyboardNameById(uint id);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyboards")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerArrayMarshaller<,>), CountElementName = "length")]
    internal static partial Span<uint> GetKeyboards(out int length);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool HasKeyboard();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasScreenKeyboardSupport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool HasScreenKeyboardSupport();
}

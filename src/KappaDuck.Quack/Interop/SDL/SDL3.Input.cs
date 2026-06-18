// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;
using KappaDuck.Quack.Interop.SDL.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyFromScancode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial Key GetKeyFromScancode(Scancode code, Keymod keymod, [MarshalAs(UnmanagedType.I1)] bool keyEvents);

    [LibraryImport(nameof(SDL3), StringMarshalling = StringMarshalling.Utf8, EntryPoint = "SDL_GetKeyFromName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial Key GetKeyFromName(string name);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string GetKeyName(Key key);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyboardNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string? GetKeyboardNameById(uint id);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyboards")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerArrayMarshaller<,>), CountElementName = "length")]
    internal static partial Span<uint> GetKeyboards(out int length);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetScancodeFromKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial Scancode GetScancodeFromKey(Key key, Keymod* keymod);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetScancodeFromName", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial Scancode GetScancodeFromName(string name);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetScancodeName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string GetScancodeName(Scancode code);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool HasKeyboard();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasScreenKeyboardSupport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool HasScreenKeyboardSupport();

    /// <summary>
    /// The string is not copied, so it must be valid for the lifetime of the application.
    /// The source generator will free the string so we need to pass a <see cref="Span{T}"/> of byte instead to keep in memory.
    /// </summary>
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetScancodeName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetScancodeName(Scancode code, Span<byte> name);
}

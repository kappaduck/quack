// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Input.Keyboard;
using KappaDuck.Quack.Input.Mouse;
using KappaDuck.Quack.Interop.SDL.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal static partial class Input
    {
        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CaptureMouse"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool CaptureMouse([MarshalAs(UnmanagedType.U1)] bool enabled);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyFromScancode"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial Keycode GetKeyFromScancode(Scancode code, Modifier modifier, [MarshalAs(UnmanagedType.U1)] bool keyEvents);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string GetKeyName(Keycode code);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyboardNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string GetKeyboardNameById(uint keyboard);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyboards"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(CallerArrayMarshaller<,>), CountElementName = "length")]
        internal static partial Span<uint> GetKeyboards(out int length);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetKeyboardState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLArrayMarshaller<,>), CountElementName = "length")]
        internal static partial Span<byte> GetKeyboardState(out int length);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetModState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial Modifier GetModState();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetScancodeFromKey"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static unsafe partial Scancode GetScancodeFromKey(Keycode code, Modifier* modifier);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetScancodeFromName", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial Scancode GetScancodeFromName(string name);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetScancodeName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string GetScancodeName(Scancode code);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasKeyboard"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool HasKeyboard();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasScreenKeyboardSupport"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool HasScreenKeyboardSupport();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ResetKeyboard"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial void ResetKeyboard();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetModState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial void SetModState(Modifier modifier);

        /// <summary>
        /// The string is not copied, so it must be valid for the lifetime of the application.
        /// The source generator will free the string so we need to pass a <see cref="Span{T}"/> of byte instead to keep in memory.
        /// </summary>
        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetScancodeName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetScancodeName(Scancode code, Span<byte> name);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetGlobalMouseState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial MouseButtonState GetGlobalMouseState(out float x, out float y);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetMice"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(CallerArrayMarshaller<,>), CountElementName = "length")]
        internal static partial Span<uint> GetMice(out int length);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetMouseState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial MouseButtonState GetMouseState(out float x, out float y);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetRelativeMouseState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial MouseButtonState GetRelativeMouseState(out float x, out float y);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetMouseNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string GetMouseNameById(uint id);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasMouse"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool HasMouse();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WarpMouseGlobal"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool WarpMouseGlobal(float x, float y);
    }
}

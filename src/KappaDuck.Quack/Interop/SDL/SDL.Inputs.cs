// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Marshalling;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using static KappaDuck.Quack.Inputs.Keyboard;
using static KappaDuck.Quack.Inputs.Mouse;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static partial class Inputs
    {
        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial Keycode SDL_GetKeyFromScancode(Scancode code, Modifier modifier, [MarshalAs(UnmanagedType.U1)] bool keyEvents);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetKeyName(Keycode code);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetKeyboardNameForID(uint keyboard);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(CallerOwnedArrayMarshaller<,>), CountElementName = "length")]
        internal static partial Span<uint> SDL_GetKeyboards(out int length);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedArrayMarshaller<,>), CountElementName = "length")]
        internal static partial Span<byte> SDL_GetKeyboardState(out int length);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial Modifier SDL_GetModState();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial Scancode SDL_GetScancodeFromKey(Keycode code, out Modifier modifier);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial Scancode SDL_GetScancodeFromName(string name);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetScancodeName(Scancode code);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_HasKeyboard();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_HasScreenKeyboardSupport();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_ResetKeyboard();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_SetModState(Modifier modifier);

        /// <summary>
        /// The string is not copied, so it must be valid for the lifetime of the application.
        /// The source generator will free the string so we need to pass a <see cref="Span{T}"/> of byte instead to keep in memory.
        /// </summary>
        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetScancodeName(Scancode code, Span<byte> name);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial ButtonState SDL_GetGlobalMouseState(out float x, out float y);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(CallerOwnedArrayMarshaller<,>), CountElementName = "length")]
        internal static partial Span<uint> SDL_GetMice(out int length);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial ButtonState SDL_GetMouseState(out float x, out float y);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial ButtonState SDL_GetRelativeMouseState(out float x, out float y);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetMouseNameForID(uint mouse);

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_HasMouse();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_WarpMouseGlobal(float x, float y);
    }
}

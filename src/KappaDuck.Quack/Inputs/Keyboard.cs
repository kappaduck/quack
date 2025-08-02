// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Marshallers;
using KappaDuck.Quack.Windows;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace KappaDuck.Quack.Inputs;

public sealed partial class Keyboard
{
    internal Keyboard(uint id)
    {
        Id = id;
        Name = SDL_GetKeyboardNameForID(id);
    }

    /// <summary>
    /// Gets a value indicating whether a keyboard is currently connected.
    /// </summary>
    public static bool HasKeyboard => SDL_HasKeyboard();

    /// <summary>
    /// Gets a value indicating whether the screen keyboard is supported.
    /// </summary>
    public static bool HasScreenKeyboardSupport => SDL_HasScreenKeyboardSupport();

    /// <summary>
    /// Gets the instance id of the mouse.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets or sets the current modifier state.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Setting the modifier state allows you to impose modifier keys states on your application.
    /// Simply pass your desired modifier states as a bitwise OR'd combination of <see cref="Modifier"/> flags.
    /// </para>
    /// <para>
    /// This does not change the keyboard state, only the key modifier flags that SDL reports.
    /// </para>
    /// </remarks>
    public static Modifier ModifierState
    {
        get => SDL_GetModState();
        set => SDL_SetModState(value);
    }

    /// <summary>
    /// Gets the name of the keyboard.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Get the key code corresponding to the given scancode according to the current keyboard layout.
    /// </summary>
    /// <param name="code">The scancode of the key.</param>
    /// <param name="modifier">The modifier state to use when translating the scancode to a keycode.</param>
    /// <returns>The key code that corresponds to the given <see cref="Scancode"/> or <see cref="Keycode.Unknown"/> if the scancode doesn't correspond to a key.</returns>
    public static Keycode GetKeyFromScancode(Scancode code, Modifier? modifier = null)
        => SDL_GetKeyFromScancode(code, modifier ?? Modifier.None, keyEvents: false);

    /// <summary>
    /// Get the human-readable name of a key code.
    /// </summary>
    /// <remarks>
    /// Letters will be presented in their uppercase form, if applicable.
    /// </remarks>
    /// <param name="code">The key code.</param>
    /// <returns>The human-readable name of the key code or <see cref="string.Empty"/> if the key code doesn't have a name.</returns>
    public static string GetKeyName(Keycode code) => SDL_GetKeyName(code);

    /// <summary>
    /// Get a list of currently connected keyboards.
    /// </summary>
    /// <remarks>
    /// This will include any device or virtual driver that includes keyboard functionality,
    /// including some mice, KVM switches, motherboard power buttons, etc. You should wait for input from a device
    /// before you consider it actively in use.
    /// </remarks>
    /// <returns>The list of connected keyboards.</returns>
    public static Keyboard[] GetKeyboards()
    {
        ReadOnlySpan<uint> ids = SDL_GetKeyboards(out _);

        if (ids.IsEmpty)
            return [];

        Keyboard[] keyboards = new Keyboard[ids.Length];

        for (int i = 0; i < ids.Length; i++)
            keyboards[i] = new Keyboard(ids[i]);

        return keyboards;
    }

    /// <summary>
    /// Gets the scancode of a key from a human-readable name.
    /// </summary>
    /// <param name="name">The human-readable scancode name.</param>
    /// <returns>The scancode or <see cref="Scancode.Unknown"/> if the name wasn't recognized.</returns>
    public static Scancode GetScancode(string name) => SDL_GetScancodeFromName(name);

    /// <summary>
    /// Get the scancode corresponding to the given key code according to the current keyboard layout.
    /// </summary>
    /// <param name="keycode">The key code.</param>
    /// <param name="modifier">The modifier state that would be used when the scancode generates this key.</param>
    /// <returns>The scancode that corresponds to the given <see cref="Keycode"/>.</returns>
    public static Scancode GetScancode(Keycode keycode, out Modifier modifier)
        => SDL_GetScancodeFromKey(keycode, out modifier);

    /// <summary>
    /// Gets the human-readable name of the scancode.
    /// </summary>
    /// <remarks>
    /// The name is by design not stable across platforms, e.g. the name for <see cref="Scancode.LeftGui"/>
    /// is "Left GUI" on Linux, but "Left Windows" on Windows. Some scancodes like <see cref="Scancode.NonUsBackslash"/> don't have
    /// any name at all.
    /// </remarks>
    /// <param name="scancode">The scancode.</param>
    /// <returns>The human-readable name or <see cref="string.Empty"/> if the scancode doesn't have a name.</returns>
    public static string GetScancodeName(Scancode scancode) => SDL_GetScancodeName(scancode);

    /// <summary>
    /// Check if a key is pressed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Uses <see cref="Window.Poll(out Event)"/>, <see cref="EventManager.Poll(out Event)"/> or <see cref="EventManager.Pump"/>
    /// to update the keyboard state.
    /// </para>
    /// <para>
    /// It gives you the current state of the keyboard after all events have been processed, so if a key is pressed and
    /// released before you process events, then the key will not appear as pressed.
    /// </para>
    /// </remarks>
    /// <param name="code">The scancode of the key.</param>
    /// <returns><see langword="true"/> if the key is pressed; otherwise, <see langword="false"/>.</returns>
    public static bool IsPressed(Scancode code)
    {
        ReadOnlySpan<byte> keys = SDL_GetKeyboardState(out _);

        return keys[(int)code] == 1;
    }

    /// <summary>
    /// Clear the state of the keyboard.
    /// </summary>
    /// <remarks>
    /// It will generate <see cref="EventType.KeyUp"/> events for all pressed keys.
    /// </remarks>
    public static void Reset() => SDL_ResetKeyboard();

    /// <summary>
    /// Sets the human-readable name of the scancode.
    /// </summary>
    /// <param name="scancode">The scancode.</param>
    /// <param name="name">The human-readable name.</param>
    /// <exception cref="QuackNativeException">An error occurred while setting the scancode name.</exception>
    public static void SetScancodeName(Scancode scancode, string name)
        => QuackNativeException.ThrowIfFailed(SDL_SetScancodeName(scancode, Encoding.UTF8.GetBytes(name)));

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode SDL_GetKeyFromScancode(Scancode code, Modifier modifier, [MarshalAs(UnmanagedType.U1)] bool keyEvents);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    private static partial string SDL_GetKeyName(Keycode code);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    private static partial string SDL_GetKeyboardNameForID(uint keyboard);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CallerOwnedArrayMarshaller<,>), CountElementName = "length")]
    private static partial Span<uint> SDL_GetKeyboards(out int length);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedArrayMarshaller<,>), CountElementName = "length")]
    private static partial Span<byte> SDL_GetKeyboardState(out int length);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Modifier SDL_GetModState();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromKey(Keycode code, out Modifier modifier);

    [LibraryImport(SDLNative.Library, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode SDL_GetScancodeFromName(string name);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    private static partial string SDL_GetScancodeName(Scancode code);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_HasKeyboard();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_HasScreenKeyboardSupport();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ResetKeyboard();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetModState(Modifier modifier);

    /// <summary>
    /// The string is not copied, so it must be valid for the lifetime of the application.
    /// The source generator will free the string so we need to pass a <see cref="Span{T}"/> of byte instead to keep in memory.
    /// </summary>
    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SetScancodeName(Scancode code, Span<byte> name);
}

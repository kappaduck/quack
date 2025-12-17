// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Windows;
using System.Text;

namespace KappaDuck.Quack.Inputs;

/// <summary>
/// Represents a keyboard device.
/// </summary>
[PublicAPI]
public sealed partial class Keyboard
{
    internal Keyboard(uint id)
    {
        Id = id;
        Name = Native.SDL_GetKeyboardNameForID(id);
    }

    /// <summary>
    /// Gets a value indicating whether a keyboard is connected.
    /// </summary>
    public static bool HasKeyboard => Native.SDL_HasKeyboard();

    /// <summary>
    /// Gets a value indicating whether a virtual keyboard is supported.
    /// </summary>
    public static bool HasVirtualKeyboard => Native.SDL_HasScreenKeyboardSupport();

    /// <summary>
    /// Gets the unique keyboard identifier.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets or sets the modifier state.
    /// </summary>
    /// <remarks>
    /// Modifying the modifier state allows you to impose modifier keys on your application.
    /// This does not affect the keyboard state, only the modifier state.
    /// </remarks>
    public static Modifier Modifiers
    {
        get => Native.SDL_GetModState();
        set => Native.SDL_SetModState(value);
    }

    /// <summary>
    /// Gets the name of the keyboard.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the keycode corresponding to the given scancode according to the current keyboard layout.
    /// </summary>
    /// <param name="code">The scancode.</param>
    /// <param name="modifier">The modifier to apply when translating the scancode to a keycode.</param>
    /// <returns>The corresponding keycore to the given scancode or <see cref="Keycode.Unknown"/> if the scancode does not have a corresponding keycode.</returns>
    public static Keycode GetKeyFromScancode(Scancode code, Modifier modifier = Modifier.None)
        => Native.SDL_GetKeyFromScancode(code, modifier, keyEvents: false);

    /// <summary>
    /// Gets the name of a keycode.
    /// </summary>
    /// <remarks>
    /// Letters will be presented in their uppercase form, if applicable.
    /// </remarks>
    /// <param name="key">The keycode.</param>
    /// <returns>The name of the keycode or <see cref="string.Empty"/> if the keycode doesn't have a name.</returns>
    public static string GetKeyName(Keycode key) => Native.SDL_GetKeyName(key);

    /// <summary>
    /// Gets all connected keyboards.
    /// </summary>
    /// <remarks>
    /// It will include any device or virtual driver that provides keyboard functionality,
    /// including some mice, KVM switches, motherboard power buttons, etc. You should wait
    /// for input from a device before you consider it actively in use.
    /// </remarks>
    /// <returns>All connected keyboards.</returns>
    public static Keyboard[] GetKeyboards()
    {
        ReadOnlySpan<uint> ids = Native.SDL_GetKeyboards(out _);

        if (ids.IsEmpty)
            return [];

        Keyboard[] keyboards = new Keyboard[ids.Length];

        for (int i = 0; i < ids.Length; i++)
            keyboards[i] = new Keyboard(ids[i]);

        return keyboards;
    }

    /// <summary>
    /// Gets the scancode from the given name.
    /// </summary>
    /// <param name="name">The name of the scancode.</param>
    /// <returns>The corresponding scancode or <see cref="Scancode.Unknown"/> if the name does not match any scancode.</returns>
    public static Scancode GetScancode(string name) => Native.SDL_GetScancodeFromName(name);

    /// <summary>
    /// Gets the scancode and modifier from the given keycode.
    /// </summary>
    /// <param name="key">The keycode.</param>
    /// <returns>>The corresponding scancode and modifier or <see cref="Scancode.Unknown"/> and <see langword="null"/> if the keycode does not have a corresponding scancode.</returns>
    public static unsafe (Scancode Code, Modifier? Modifier) GetScancode(Keycode key)
    {
        Modifier* modifier = null;

        Scancode code = Native.SDL_GetScancodeFromKey(key, modifier);

        return (code, *modifier);
    }

    /// <summary>
    /// Gets the name of a scancode.
    /// </summary>
    /// <remarks>
    /// The name is by design and not stable across platforms, e.g. the name for <see cref="Scancode.LeftGui"/>
    /// is "Left GUI" on Linux, but "Left Windows" on Windows. Some scancodes may not have a name, in which case
    /// an empty string is returned.
    /// </remarks>
    /// <param name="code">The scancode.</param>
    /// <returns>The name of the scancode or <see cref="string.Empty"/> if the scancode doesn't have a name.</returns>
    public static string GetScancodeName(Scancode code) => Native.SDL_GetScancodeName(code);

    /// <summary>
    /// Determines whether the specified scancode is currently pressed.
    /// </summary>
    /// <remarks>
    /// It gives you the current state of the keyboard after all events have been processed, so if a key is pressed and
    /// released before you process events, then the key will not appear as presed. Uses <see cref="Window.Poll(out Event)"/>,
    /// <see cref="EventManager.Poll(out Event)"/> or <see cref="EventManager.Pump"/> to update the keyboard state.
    /// </remarks>
    /// <param name="code">The scancode.</param>
    /// <returns><see langword="true"/> if the scancode is currently pressed; otherwise, <see langword="false"/>.</returns>
    public static bool IsDown(Scancode code)
    {
        ReadOnlySpan<byte> keys = Native.SDL_GetKeyboardState(out _);
        return keys[(int)code] == 1;
    }

    /// <summary>
    /// Clears the keyboard state.
    /// </summary>
    /// <remarks>
    /// It will generate <see cref="EventType.KeyUp"/> events for all pressed keys.
    /// </remarks>
    public static void Reset() => Native.SDL_ResetKeyboard();

    /// <summary>
    /// Sets the name of a scancode.
    /// </summary>
    /// <param name="code">The scancode.</param>
    /// <param name="name">The name to set.</param>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public static void SetScancodeName(Scancode code, string name)
    {
        bool success = Native.SDL_SetScancodeName(code, Encoding.UTF8.GetBytes(name));
        QuackNativeException.ThrowIfFailed(success);
    }
}

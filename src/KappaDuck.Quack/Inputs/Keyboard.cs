// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Windows;
using System.Text;

namespace KappaDuck.Quack.Inputs;

/// <summary>
/// Represents a keyboard device.
/// </summary>
public sealed class Keyboard
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
    /// Retrieves a keyboard by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the keyboard.</param>
    /// <returns>The keyboard with the specified identifier.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="id"/> is negative.</exception>
    public static Keyboard Get(uint id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        return new Keyboard(id);
    }

    /// <summary>
    /// Gets the key corresponding to the given code according to the current keyboard layout.
    /// </summary>
    /// <param name="code">The code to translate to a key.</param>
    /// <param name="modifier">The modifier to apply when translating the code to a key.</param>
    /// <returns>The corresponding key from the given code or <see cref="Keycode.Unknown"/> if the code does not have a corresponding key.</returns>
    public static Keycode GetKeyFromScancode(Scancode code, Modifier modifier = Modifier.None)
        => Native.SDL_GetKeyFromScancode(code, modifier, keyEvents: false);

    /// <summary>
    /// Gets the name of a key.
    /// </summary>
    /// <remarks>
    /// Letters will be presented in their uppercase form, if applicable.
    /// </remarks>
    /// <param name="key">The key.</param>
    /// <returns>The name of the key or <see cref="string.Empty"/> if the key doesn't have a name.</returns>
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
    /// Gets the code from the given name.
    /// </summary>
    /// <param name="name">The name of the code.</param>
    /// <returns>The corresponding code or <see cref="Scancode.Unknown"/> if the name does not match any code.</returns>
    public static Scancode GetScancode(string name) => Native.SDL_GetScancodeFromName(name);

    /// <summary>
    /// Gets the code and modifier from the given key.
    /// </summary>
    /// <param name="key">The key to translate to a code and optional modifier.</param>
    /// <returns>>The corresponding code and modifier or <see cref="Scancode.Unknown"/> and <see langword="null"/> if the key does not have a corresponding code.</returns>
    public static unsafe (Scancode Code, Modifier? Modifier) GetScancode(Keycode key)
    {
        Modifier* modifier = null;

        Scancode code = Native.SDL_GetScancodeFromKey(key, modifier);

        return (code, *modifier);
    }

    /// <summary>
    /// Gets the name of a code.
    /// </summary>
    /// <remarks>
    /// The name is by design and not stable across platforms, e.g. the name for <see cref="Scancode.LeftGui"/>
    /// is "Left GUI" on Linux, but "Left Windows" on Windows. Some codes may not have a name, in which case
    /// an empty string is returned.
    /// </remarks>
    /// <param name="code">The code.</param>
    /// <returns>The name of the code or <see cref="string.Empty"/> if the code doesn't have a name.</returns>
    public static string GetScancodeName(Scancode code) => Native.SDL_GetScancodeName(code);

    /// <summary>
    /// Determines whether the specified code is currently pressed.
    /// </summary>
    /// <remarks>
    /// It gives you the current state of the keyboard after all events have been processed, so if a key is pressed and
    /// released before you process events, then the key will not appear as pressed. Uses <see cref="WindowBase.Poll(out Event)"/>,
    /// <see cref="EventManager.Poll(out Event)"/> or <see cref="EventManager.Pump"/> to update the keyboard state.
    /// </remarks>
    /// <param name="code">The code to check.</param>
    /// <returns><see langword="true"/> if the code is currently pressed; otherwise, <see langword="false"/>.</returns>
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
    /// Sets the name of a code.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="name">The name to set.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to set the code name.</exception>
    public static void SetScancodeName(Scancode code, string name)
    {
        bool success = Native.SDL_SetScancodeName(code, Encoding.UTF8.GetBytes(name));
        QuackNativeException.ThrowIfFailed(success);
    }
}

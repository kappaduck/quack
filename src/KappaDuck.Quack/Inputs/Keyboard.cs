// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Windows;
using System.Text;

namespace KappaDuck.Quack.Inputs;

public sealed partial class Keyboard
{
    internal Keyboard(uint id)
    {
        Id = id;
        Name = SDL.Inputs.SDL_GetKeyboardNameForID(id);
    }

    /// <summary>
    /// Gets a value indicating whether a keyboard is currently connected.
    /// </summary>
    public static bool HasKeyboard => SDL.Inputs.SDL_HasKeyboard();

    /// <summary>
    /// Gets a value indicating whether the screen keyboard is supported.
    /// </summary>
    public static bool HasScreenKeyboardSupport => SDL.Inputs.SDL_HasScreenKeyboardSupport();

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
        get => SDL.Inputs.SDL_GetModState();
        set => SDL.Inputs.SDL_SetModState(value);
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
        => SDL.Inputs.SDL_GetKeyFromScancode(code, modifier ?? Modifier.None, keyEvents: false);

    /// <summary>
    /// Get the human-readable name of a key code.
    /// </summary>
    /// <remarks>
    /// Letters will be presented in their uppercase form, if applicable.
    /// </remarks>
    /// <param name="code">The key code.</param>
    /// <returns>The human-readable name of the key code or <see cref="string.Empty"/> if the key code doesn't have a name.</returns>
    public static string GetKeyName(Keycode code) => SDL.Inputs.SDL_GetKeyName(code);

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
        ReadOnlySpan<uint> ids = SDL.Inputs.SDL_GetKeyboards(out _);

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
    public static Scancode GetScancode(string name) => SDL.Inputs.SDL_GetScancodeFromName(name);

    /// <summary>
    /// Get the scancode corresponding to the given key code according to the current keyboard layout.
    /// </summary>
    /// <param name="keycode">The key code.</param>
    /// <param name="modifier">The modifier state that would be used when the scancode generates this key.</param>
    /// <returns>The scancode that corresponds to the given <see cref="Keycode"/>.</returns>
    public static Scancode GetScancode(Keycode keycode, out Modifier modifier)
        => SDL.Inputs.SDL_GetScancodeFromKey(keycode, out modifier);

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
    public static string GetScancodeName(Scancode scancode) => SDL.Inputs.SDL_GetScancodeName(scancode);

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
        ReadOnlySpan<byte> keys = SDL.Inputs.SDL_GetKeyboardState(out _);

        return keys[(int)code] == 1;
    }

    /// <summary>
    /// Clear the state of the keyboard.
    /// </summary>
    /// <remarks>
    /// It will generate <see cref="EventType.KeyUp"/> events for all pressed keys.
    /// </remarks>
    public static void Reset() => SDL.Inputs.SDL_ResetKeyboard();

    /// <summary>
    /// Sets the human-readable name of the scancode.
    /// </summary>
    /// <param name="scancode">The scancode.</param>
    /// <param name="name">The human-readable name.</param>
    /// <exception cref="QuackNativeException">An error occurred while setting the scancode name.</exception>
    public static void SetScancodeName(Scancode scancode, string name)
        => QuackNativeException.ThrowIfFailed(SDL.Inputs.SDL_SetScancodeName(scancode, Encoding.UTF8.GetBytes(name)));
}

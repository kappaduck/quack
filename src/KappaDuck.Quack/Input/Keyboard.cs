// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Events;

namespace KappaDuck.Quack.Input;

/// <summary>
/// Represents a keyboard input.
/// </summary>
/// <remarks>
/// State reflects the keyboard as of the last time the event queue was pumped
/// (see <see cref="EventQueue.Pump"/>, which <see cref="EventQueue.Poll"/>
/// does implicitly). Use state for continuous, per-frame input such as movement;
/// use key events for discrete actions such as menus and shortcuts.
/// </remarks>
public static class Keyboard
{
    /// <summary>
    /// Gets or sets the modifier state.
    /// </summary>
    /// <remarks>
    /// Modifying the modifier state allows you to impose modifier keys on your application.
    /// This does not affect the keyboard state, only the modifier state.
    /// </remarks>
    public static Keymod Modifiers
    {
        get => SDL3.GetModState();
        set => SDL3.SetModState(value);
    }

    /// <summary>
    /// Determines whether the given <see cref="Scancode"/> is currently held down.
    /// </summary>
    /// <param name="code">The <see cref="Scancode"/> to test.</param>
    /// <returns><see langword="true"/> if the key is down; otherwise <see langword="false"/>.</returns>
    public static bool IsDown(Scancode code)
    {
        Span<byte> state = SDL3.GetKeyboardState(out _);

        int index = (int)code;
        return index < state.Length && state[index] != 0;
    }

    /// <summary>
    /// Determines whether the given <see cref="Scancode"/> is currently released.
    /// </summary>
    /// <param name="code">The <see cref="Scancode"/> to test.</param>
    /// <returns><see langword="true"/> if the key is up; otherwise <see langword="false"/>.</returns>
    public static bool IsUp(Scancode code) => !IsDown(code);

    /// <summary>
    /// Clears the keyboard state.
    /// </summary>
    /// <remarks>
    /// It will generate <see cref="KeyReleasedEvent"/> for all pressed keys.
    /// </remarks>
    public static void Reset() => SDL3.ResetKeyboard();
}

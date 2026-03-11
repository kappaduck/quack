// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Input.Keyboard;

/// <summary>
/// Represents a keyboard input.
/// </summary>
public static class Keyboard
{
    /// <summary>
    /// Gets or sets the modifier state.
    /// </summary>
    /// <remarks>
    /// Modifying the modifier state allows you to impose modifier keys on your application.
    /// This does not affect the keyboard state, only the modifier state.
    /// </remarks>
    public static Modifier Modifiers
    {
        get => SDL3.Input.GetModState();
        set => SDL3.Input.SetModState(value);
    }

    /// <summary>
    /// Determines whether the specified code is currently pressed.
    /// </summary>
    /// <remarks>
    /// It gives you the current state of the keyboard after all events have been processed, so if a key is pressed and
    /// released before you process events, then the key will not appear as pressed. Uses <see cref="Window.Poll(out Event)"/>,
    /// <see cref="EventManager.Poll(out Event)"/> or <see cref="EventManager.Pump"/> to update the keyboard state.
    /// </remarks>
    /// <param name="code">The code to check.</param>
    /// <returns><see langword="true"/> if the code is currently pressed; otherwise, <see langword="false"/>.</returns>
    public static bool IsDown(Scancode code)
    {
        ReadOnlySpan<byte> state = SDL3.Input.GetKeyboardState(out _);
        return state[(int)code] == 1;
    }

    /// <summary>
    /// Clears the keyboard state.
    /// </summary>
    /// <remarks>
    /// It will generate <see cref="EventType.KeyUp"/> events for all pressed keys.
    /// </remarks>
    public static void Reset() => SDL3.Input.ResetKeyboard();
}

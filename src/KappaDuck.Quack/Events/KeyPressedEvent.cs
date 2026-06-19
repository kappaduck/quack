// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which a key has been pressed.
/// </summary>
public readonly struct KeyPressedEvent
{
    internal KeyPressedEvent(SDL_Event e)
    {
        Which = e.Keyboard.Which;
        Code = e.Keyboard.Scancode;
        Key = e.Keyboard.Key;
        Modifier = e.Keyboard.Mod;
        Repeat = e.Keyboard.Repeat != 0;
    }

    /// <summary>
    /// Gets the keyboard instance id.
    /// </summary>
    public uint Which { get; init; }

    /// <summary>
    /// Gets the pressed scancode.
    /// </summary>
    public Scancode Code { get; init; }

    /// <summary>
    /// Gets the pressed key.
    /// </summary>
    public Key Key { get; init; }

    /// <summary>
    /// Gets the current modifiers.
    /// </summary>
    public Keymod Modifier { get; init; }

    /// <summary>
    /// Gets a value indicating whether is a key repeat.
    /// </summary>
    public bool Repeat { get; init; }
}

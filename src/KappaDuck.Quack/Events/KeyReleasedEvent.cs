// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;
using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a key has been released.
/// </summary>
public readonly struct KeyReleasedEvent
{
    internal KeyReleasedEvent(SDL_KeyboardEvent e)
    {
        WindowId = e.WindowId;
        Which = e.Which;
        Code = e.Scancode;
        Key = e.Key;
        Modifier = e.Mod;
    }

    /// <summary>
    /// Gets the window id on which the key is released.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the keyboard device id which the key released.
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
    /// Gets the keyboard device which the key is released.
    /// </summary>
    public KeyboardDevice Device => KeyboardDevices.FromId(Which);
}

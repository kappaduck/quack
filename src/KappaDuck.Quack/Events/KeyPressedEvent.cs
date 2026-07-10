// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input;
using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a key has been pressed.
/// </summary>
[QuackEvent(SDL_EventType.KeyDown, NativeField = nameof(SDL_Event.Keyboard))]
public readonly struct KeyPressedEvent : IEvent
{
    internal KeyPressedEvent(SDL_KeyboardEvent e)
    {
        WindowId = e.WindowId;
        Which = e.Which;
        Code = e.Scancode;
        Key = e.Key;
        Modifier = e.Mod;
        Repeat = e.Repeat != 0;
    }

    /// <summary>
    /// Gets the window id on which the key is pressed.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the keyboard instance id which the key pressed.
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

    /// <summary>
    /// Gets the keyboard device which the key is pressed.
    /// </summary>
    public KeyboardDevice Device => KeyboardDevices.FromId(Which);

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => WindowManager.FromId(WindowId);
}

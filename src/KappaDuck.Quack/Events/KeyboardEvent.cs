// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Inputs;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a keyboard event.
/// </summary>
[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
public readonly struct KeyboardEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// The window with keyboard focus.
    /// </summary>
    public readonly uint WindowId;

    /// <summary>
    /// The keyboard instance id or 0 if unknown or virtual.
    /// </summary>
    public readonly uint Which;

    /// <summary>
    /// The physical key code.
    /// </summary>
    public readonly Keyboard.Scancode Code;

    /// <summary>
    /// The virtual key code.
    /// </summary>
    public readonly Keyboard.Keycode Key;

    /// <summary>
    /// Current key modifiers.
    /// </summary>
    public readonly Keyboard.Modifier Modifiers;

    private readonly ushort _raw;
    private readonly byte _down;
    private readonly byte _repeat;

    /// <summary>
    /// Gets a value indicating whether the key is pressed.
    /// </summary>
    public readonly bool Down => _down != 0;

    /// <summary>
    /// Gets a value indicating whether the key is repeated.
    /// </summary>
    public readonly bool Repeat => _repeat != 0;
}

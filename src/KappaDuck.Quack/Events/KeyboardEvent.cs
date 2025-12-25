// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Inputs;
using System.Diagnostics;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a keyboard event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct KeyboardEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the window id which has the keyboard focus.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the keyboard id or 0 if unknown or virtual.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the physical key code.
    /// </summary>
    public Scancode Code { get; }

    /// <summary>
    /// Gets the virtual key code.
    /// </summary>
    public Keycode Key { get; }

    /// <summary>
    /// Gets the key modifiers.
    /// </summary>
    public Modifier Modifiers { get; }

    private readonly ushort _raw;
    private readonly byte _down;
    private readonly byte _repeat;

    /// <summary>
    /// Gets the keyboard device associated with <see cref="EventType.KeyDown"/> or <see cref="EventType.KeyUp"/>.
    /// </summary>
    public Keyboard Keyboard
    {
        get
        {
            Debug.Assert(_type is EventType.KeyDown or EventType.KeyUp);
            return Keyboard.Get(Which);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the key is pressed.
    /// </summary>
    public bool Down => _down != 0;

    /// <summary>
    /// Gets a value indicating whether the key is repeated.
    /// </summary>
    public bool Repeat => _repeat != 0;
}

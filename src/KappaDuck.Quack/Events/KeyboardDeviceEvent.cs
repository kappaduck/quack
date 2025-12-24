// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Inputs;
using System.Diagnostics;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a keyboard device event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct KeyboardDeviceEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the keyboard id or 0 if unknown or virtual.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the keyboard device associated with <see cref="EventType.KeyboardAdded"/> or <see cref="EventType.KeyboardRemoved"/>.
    /// </summary>
    public Keyboard Device
    {
        get
        {
            Debug.Assert(_type is EventType.KeyboardAdded or EventType.KeyboardRemoved);
            return Keyboard.Get(Which);
        }
    }
}

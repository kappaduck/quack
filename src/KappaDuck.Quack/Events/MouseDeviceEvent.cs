// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Inputs;
using System.Diagnostics;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a mouse device event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseDeviceEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the mouse id which was added or removed.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the mouse device associated with <see cref="EventType.MouseAdded"/> or <see cref="EventType.MouseRemoved"/>.
    /// </summary>
    public Mouse Mouse
    {
        get
        {
            Debug.Assert(_type is EventType.MouseAdded or EventType.MouseRemoved);
            return Mouse.Get(Which);
        }
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Input.Devices;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a keyboard device event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct KeyboardDeviceEvent
{
    /// <summary>
    /// Gets the type of the keyboard device event.
    /// </summary>
    /// <remarks>
    /// The event is one of the following types:
    /// <list type="bullet">
    /// <item><see cref="EventType.KeyboardAdded"/></item>
    /// <item><see cref="EventType.KeyboardRemoved"/></item>
    /// </list>
    /// </remarks>
    public EventType Type { get; }

    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the keyboard id which was added or removed.
    /// </summary>
    /// <remarks>
    /// If the keyboard is unknown or virtual, this value is <c>0</c>
    /// </remarks>
    public uint Which { get; }

    /// <summary>
    /// Gets the keyboard device which generated this event.
    /// </summary>
    public KeyboardDevice Device => KeyboardDevices.FromId(Which);
}

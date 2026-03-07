// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Input.Devices;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a mouse device event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseDeviceEvent
{
    /// <summary>
    /// Gets the type of the mouse device event.
    /// </summary>
    /// <remarks>
    /// The event is one of the following types:
    /// <list type="bullet">
    /// <item><see cref="EventType.MouseAdded"/></item>
    /// <item><see cref="EventType.MouseRemoved"/></item>
    /// </list>
    /// </remarks>
    public EventType Type { get; }

    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the mouse id which was added or removed.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the mouse device which generated this event.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);
}

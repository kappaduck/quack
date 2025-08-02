// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.InteropServices;

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
    /// The keyboard instance id which was added or removed.
    /// </summary>
    public readonly uint Which;
}

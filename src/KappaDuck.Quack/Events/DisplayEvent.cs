// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a display event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct DisplayEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// The associated display.
    /// </summary>
    public readonly uint Id;

    /// <summary>
    /// The event data1.
    /// </summary>
    public readonly int Data1;

    /// <summary>
    /// The event data2.
    /// </summary>
    public readonly int Data2;
}

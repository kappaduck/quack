// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a window event.
/// </summary>
[PublicAPI]
[StructLayout(LayoutKind.Sequential)]
public readonly struct WindowEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// The associated window.
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

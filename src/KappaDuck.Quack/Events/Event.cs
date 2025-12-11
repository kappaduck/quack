// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which is processed by the event loop.
/// </summary>
[PublicAPI]
[StructLayout(LayoutKind.Explicit)]
public struct Event
{
    [FieldOffset(0)]
    private unsafe fixed byte _padding[128];
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which is pumped by the Quack Engine.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct Event
{
    [FieldOffset(0)]
    private unsafe fixed byte _padding[128];
}

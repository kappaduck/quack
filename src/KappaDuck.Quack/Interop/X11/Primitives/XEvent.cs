// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.X11.Primitives;

[StructLayout(LayoutKind.Explicit)]
internal struct XEvent
{
    [FieldOffset(0)]
    private readonly int _type;

    [FieldOffset(0)]
    private unsafe fixed long _padding[24];
}

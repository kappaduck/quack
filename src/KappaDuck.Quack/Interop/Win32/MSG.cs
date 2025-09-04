// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32;

/// <summary>
/// The MSG structure contains message information from a thread's message queue.
/// </summary>
[SupportedOSPlatform("windows")]
[StructLayout(LayoutKind.Sequential)]
internal readonly struct MSG
{
    private readonly nint _hwnd;

    public readonly uint Message;

    public readonly nuint WParam;

    public readonly nuint LParam;

    private readonly nuint _time;

    public readonly Vector2Int Point;
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32.Primitives;

/// <summary>
/// The MSG structure contains message information from a thread's message queue.
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
[StructLayout(LayoutKind.Sequential)]
internal readonly struct MSG
{
    private readonly nint _hwnd;

    internal uint Message { get; }

    internal nuint WParam { get; }

    internal nuint LParam { get; }

    private readonly nuint _time;

    internal POINT Point { get; }
}

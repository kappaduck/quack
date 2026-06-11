// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32.Primitives;

/// <summary>
/// Represents the message information from a thread's message queue.
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
[StructLayout(LayoutKind.Sequential)]
internal readonly struct MSG
{
    private readonly void* _hwnd;

    internal uint Message { get; }

    internal nuint WParam { get; }

    internal nint LParam { get; }

    private readonly uint _time;

    internal POINT Point { get; }

    private readonly uint _private;
}

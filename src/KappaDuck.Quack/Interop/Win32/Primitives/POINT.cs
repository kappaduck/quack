// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32.Primitives;

/// <summary>
/// The POINT structure defines the x and y coordinates of a point.
/// </summary>
[SupportedOSPlatform("windows")]
[StructLayout(LayoutKind.Sequential)]
internal readonly struct POINT
{
    public int X { get; }
    public int Y { get; }
}

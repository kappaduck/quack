// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32.Primitives;

/// <summary>
/// The POINT structure defines the x- and y-coordinates of a point.
/// </summary>
/// <param name="x">The x-coordinate of the point.</param>
/// <param name="y">The y-coordinate of the point.</param>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
[StructLayout(LayoutKind.Sequential)]
internal readonly struct POINT(int x, int y)
{
    public POINT() : this(0, 0)
    {
    }

    internal int X { get; } = x;

    internal int Y { get; } = y;
}

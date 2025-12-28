// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.X11.Primitives;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.X11;

[SupportedOSPlatform("linux")]
internal static class X11
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool MesageCallback(nint data, ref XEvent e);
}

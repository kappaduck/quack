// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.X11.Primitives;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.X11;

[SupportedOSPlatform(nameof(OSPlatform.Linux))]
internal static class X11
{
    internal delegate bool EventCallback(in XEvent xEvent);
}

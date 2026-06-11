// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.X11.Primitives;

/// <summary>
/// Represents an X11 event.
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Linux))]
[StructLayout(LayoutKind.Sequential, Size = 192)]
internal readonly struct XEvent;

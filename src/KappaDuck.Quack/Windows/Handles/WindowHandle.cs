// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Windows.Handles;

/// <summary>
/// Represents the specific platform window handle.
/// </summary>
public readonly union WindowHandle(Win32Handle, X11Handle, WaylandHandle);

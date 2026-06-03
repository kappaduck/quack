// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// Represents a Win32 handle
/// </summary>
public readonly struct Win32Handle
{
    internal Win32Handle(nint hwnd) => Hwnd = hwnd;

    /// <summary>
    /// Gets the HWND associated with the window.
    /// </summary>
    public nint Hwnd { get; }
}

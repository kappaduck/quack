// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows;

/// <summary>
/// Represents the state of a window.
/// </summary>
[Flags]
public enum WindowState : ulong
{
    /// <summary>
    /// No state to apply to the window.
    /// </summary>
    None = 0x0000000000000000,

    /// <summary>
    /// The window is in fullscreen mode.
    /// </summary>
    Fullscreen = 0x0000000000000001,

    /// <summary>
    /// The window is occluded.
    /// </summary>
    Occluded = 0x0000000000000004,

    /// <summary>
    /// The window is hidden.
    /// </summary>
    Hidden = 0x0000000000000008,

    /// <summary>
    /// The window has no decorations, such as title bar or borders.
    /// </summary>
    Borderless = 0x0000000000000010,

    /// <summary>
    /// The window can be resized by the user.
    /// </summary>
    Resizable = 0x0000000000000020,

    /// <summary>
    /// The window is minimized and not visible to the user.
    /// </summary>
    Minimized = 0x0000000000000040,

    /// <summary>
    /// The window is maximized and occupies the entire screen area.
    /// </summary>
    Maximized = 0x0000000000000080,

    /// <summary>
    /// The has grabbed the mouse input.
    /// </summary>
    MouseGrabbed = 0x0000000000000100,

    /// <summary>
    /// The window has input focus.
    /// </summary>
    InputFocus = 0x0000000000000200,

    /// <summary>
    /// The window has mouse focus.
    /// </summary>
    MouseFocus = 0x0000000000000400,

    /// <summary>
    /// The window uses high pixel density back buffering if available.
    /// </summary>
    HighPixelDensity = 0x0000000000002000,

    /// <summary>
    /// The window has captured the mouse input.
    /// </summary>
    /// <remark>
    /// Unrelated to <see cref="MouseGrabbed"/>.
    /// </remark>
    MouseCapture = 0x0000000000004000,

    /// <summary>
    /// The window is in relative mouse mode
    /// </summary>
    MouseRelativeMode = 0x0000000000008000,

    /// <summary>
    /// The window is always on top of other windows.
    /// </summary>
    AlwaysOnTop = 0x0000000000010000,

    /// <summary>
    /// The window has grabbed the keyboard input.
    /// </summary>
    KeyboardGrabbed = 0x0000000000100000,

    /// <summary>
    /// The window has transparent buffer.
    /// </summary>
    Transparent = 0x0000000040000000,

    /// <summary>
    /// The window is not focusable.
    /// </summary>
    NotFocusable = 0x0000000080000000
}

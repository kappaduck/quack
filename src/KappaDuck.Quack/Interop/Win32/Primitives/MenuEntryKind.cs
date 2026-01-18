// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Win32.Primitives;

internal enum MenuEntryKind
{
    /// <summary>
    /// Specifies that the menu item opens a drop-down menu or sub-menu.
    /// </summary>
    Popup = 0x00000010,

    /// <summary>
    /// Draws a horizontal dividing line.
    /// </summary>
    /// <remarks>
    /// It only applies to a drop-down menu, sub-menu or shortcut menu.
    /// </remarks>
    Separator = 0x00000800
}

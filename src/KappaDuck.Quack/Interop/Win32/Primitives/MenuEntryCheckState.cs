// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Win32.Primitives;

internal enum MenuEntryCheckState : uint
{
    /// <summary>
    /// Does not place a check mark next to the item.
    /// </summary>
    Unchecked = 0x00000000,

    /// <summary>
    /// Places a check mark next to the menu item.
    /// </summary>
    Checked = 0x00000008
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents the state of a menu item.
/// </summary>
public enum MenuItemState : uint
{
    /// <summary>
    /// Enables the menu item so that it can be selected, and restores it from its grayed state.
    /// </summary>
    Enabled = 0x00000000,

    /// <summary>
    /// Disables the menu item and grays it so that it cannot be selected.
    /// </summary>
    Grayed = 0x00000001,

    /// <summary>
    /// Disables the menu item so that it cannot be selected, but it does not apply the <see cref="Grayed"/> state.
    /// </summary>
    Disabled = 0x00000002
}

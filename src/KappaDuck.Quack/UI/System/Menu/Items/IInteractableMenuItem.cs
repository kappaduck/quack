// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu.Items;

/// <summary>
/// Represents a menu item that supports user interaction within a menu system.
/// </summary>
/// <remarks>
/// This is a marker interface used to identify interactive menu items within the menu system.
/// Do not implement this marker directly; instead, implement derived interfaces such as <see cref="IMenuCommand"/> or <see cref="ISubMenu"/>
/// or use existing implementations like <see cref="MenuCommand"/> and <see cref="SubMenu"/>.
/// </remarks>
public interface IInteractableMenuItem : IMenuItem
{
    /// <summary>
    /// Gets a value indicating whether the item is checked.
    /// </summary>
    bool Checked { get; }

    /// <summary>
    /// Gets the layout of the menu item.
    /// </summary>
    MenuItemLayout Layout { get; }

    /// <summary>
    /// Gets the state of the menu item.
    /// </summary>
    MenuItemState State { get; }

    /// <summary>
    /// Gets the label of the menu item.
    /// </summary>
    string Label { get; }
}

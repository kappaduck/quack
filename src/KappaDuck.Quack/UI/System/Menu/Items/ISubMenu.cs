// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu.Items;

/// <summary>
/// Represents a menu item that displays a dropdown submenu within a menu system.
/// </summary>
/// <remarks>
/// You can implement this interface to create custom submenus within the menu system.
/// Otherwise, you can use the existing <see cref="SubMenu"/> implementation.
/// </remarks>
public interface ISubMenu : IInteractableMenuItem
{
    /// <summary>
    /// Gets the collection of menu entries contained in this menu.
    /// </summary>
    IList<IMenuItem> Items { get; }
}

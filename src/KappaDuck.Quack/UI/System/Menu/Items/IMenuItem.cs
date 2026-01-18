// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu.Items;

/// <summary>
/// Represents a menu item that can be displayed within a menu system.
/// </summary>
/// <remarks>
/// This is a marker interface used to identify menu items within the menu system.
/// Do not implement this marker directly; instead, implement derived interfaces such as <see cref="IMenuCommand"/> or <see cref="ISubMenu"/>
/// or use existing implementations like <see cref="MenuCommand"/>, <see cref="MenuSeparator"/> and <see cref="SubMenu"/>.
/// </remarks>
public interface IMenuItem;

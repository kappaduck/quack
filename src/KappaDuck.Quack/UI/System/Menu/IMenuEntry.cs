// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a menu entry, which can be a menu item or a sub-menu.
/// </summary>
/// <remarks>
/// This interface is used as marker for menu entries in the menu system.
/// Do not implement this interface directly; instead, use the provided implementations such as <see cref="MenuItem"/> and <see cref="Menu"/>.
/// </remarks>
public interface IMenuEntry;

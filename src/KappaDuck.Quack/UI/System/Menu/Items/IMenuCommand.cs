// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu.Items;

/// <summary>
/// Represents a menu item that executes a command when selected.
/// </summary>
/// <remarks>
/// You can implement this interface to create custom menu commands within the menu system.
/// Otherwise, you can use the existing <see cref="MenuCommand"/> implementation.
/// </remarks>
public interface IMenuCommand : IInteractableMenuItem
{
    /// <summary>
    /// Gets the unique identifier of the menu command.
    /// </summary>
    int Id { get; }
}

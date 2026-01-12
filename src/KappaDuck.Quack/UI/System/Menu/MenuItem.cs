// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a menu item.
/// </summary>
public sealed record MenuItem : IMenuEntry
{
    /// <summary>
    /// Creates a menu item.
    /// </summary>
    /// <param name="id">The menu item identifier.</param>
    /// <param name="title">The menu item title.</param>
    /// <param name="command">The command to execute when the menu item is selected.</param>
    public MenuItem(uint id, string title, Action? command = null)
    {
        Id = id;
        Title = title;
        Command = command;
    }

    /// <summary>
    /// Gets or initializes a value indicating whether the menu item is checked.
    /// </summary>
    public bool Checked { get; init; }

    /// <summary>
    /// Gets the command to execute when the menu item is selected.
    /// </summary>
    public Action? Command { get; }

    /// <summary>
    /// Gets the menu item identifier.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets or initializes the menu item layout.
    /// </summary>
    public MenuItemLayout Layout { get; init; } = MenuItemLayout.Default;

    /// <summary>
    /// Gets or initializes the menu item state.
    /// </summary>
    public MenuItemState State { get; init; } = MenuItemState.Enabled;

    /// <summary>
    /// Gets the menu item title.
    /// </summary>
    public string Title { get; }
}

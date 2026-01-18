// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.UI.Window.Menu;

namespace KappaDuck.Quack.UI.System.Menu.Items;

/// <summary>
/// Represents a menu item that can execute a command when selected.
/// </summary>
/// <remarks>
/// To handle the selection of a menu item, pass a callback function to <see cref="MenuBar"/> or <see cref="ContextMenu"/> when creating them.
/// Uses the <see cref="Id"/> property to identify which menu item was selected.
/// </remarks>
/// <param name="Id">The unique identifier of the menu command.</param>
/// <param name="Label">The label of the menu command.</param>
public sealed record MenuCommand(int Id, string Label) : IMenuCommand
{
    /// <inheritdoc/>
    public bool Checked { get; init; }

    /// <inheritdoc/>
    public int Id { get; } = Id;

    /// <inheritdoc/>
    public MenuItemLayout Layout { get; init; } = MenuItemLayout.Default;

    /// <inheritdoc/>
    public MenuItemState State { get; init; } = MenuItemState.Enabled;

    /// <inheritdoc/>
    public string Label { get; } = Label;
}

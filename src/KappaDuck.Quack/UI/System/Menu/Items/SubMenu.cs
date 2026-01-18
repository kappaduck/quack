// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Collections;

namespace KappaDuck.Quack.UI.System.Menu.Items;

/// <summary>
/// Represents a menu entry that displays a nested submenu.
/// </summary>
public sealed record SubMenu : ISubMenu, IEnumerable<IMenuItem>
{
    private readonly List<IMenuItem> _entries = [];

    /// <summary>
    /// Creates a submenu.
    /// </summary>
    /// <param name="label">The text label of the submenu.</param>
    /// <param name="entries">The entries contained within the submenu.</param>
    public SubMenu(string label, params ReadOnlySpan<IMenuItem> entries)
    {
        Label = label;
        AddRange(entries);
    }

    /// <summary>
    /// Creates a submenu.
    /// </summary>
    /// <param name="label">The text label of the submenu.</param>
    public SubMenu(string label) => Label = label;

    /// <inheritdoc/>
    public MenuItemLayout Layout { get; init; } = MenuItemLayout.Default;

    /// <inheritdoc/>
    public MenuItemState State { get; init; } = MenuItemState.Enabled;

    /// <inheritdoc/>
    public string Label { get; }

    /// <inheritdoc/>
    public IEnumerator<IMenuItem> GetEnumerator() => _entries.GetEnumerator();

    bool IInteractableMenuItem.Checked => false;

    IList<IMenuItem> ISubMenu.Items => _entries;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Adds the menu item to the submenu.
    /// </summary>
    /// <param name="item">The menu item to add.</param>
    public void Add(IMenuItem item) => _entries.Add(item);

    /// <summary>
    /// Adds a range of menu items to the submenu.
    /// </summary>
    /// <param name="items">The menu items to add.</param>
    public void AddRange(params ReadOnlySpan<IMenuItem> items) => _entries.AddRange(items);

    /// <summary>
    /// Removes the menu item from the submenu.
    /// </summary>
    /// <param name="item">The menu item to remove.</param>
    public void Remove(IMenuItem item) => _entries.Remove(item);
}

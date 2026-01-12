// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Collections;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a menu/sub-menu that contains menu items or other sub-menus.
/// </summary>
public sealed record Menu : IMenuEntry, IList<IMenuEntry>
{
    private readonly List<IMenuEntry> _entries = [];

    /// <summary>
    /// Creates a menu.
    /// </summary>
    /// <param name="title">The menu title.</param>
    /// <param name="layout">The layout of the menu items.</param>
    public Menu(string title, MenuItemLayout layout = MenuItemLayout.Default)
    {
        Title = title;
        Layout = layout;
    }

    /// <summary>
    /// Gets the menu entry at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the menu entry to retrieve.</param>
    /// <returns>The menu entry located at the specified index.</returns>
    public IMenuEntry this[int index]
    {
        get => _entries[index];
        set => _entries[index] = value;
    }

    /// <summary>
    /// Gets the menu title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the number of menu entries in the menu.
    /// </summary>
    public int Count => _entries.Count;

    /// <summary>
    /// G
    /// </summary>
    public MenuItemLayout Layout { get; }

    bool ICollection<IMenuEntry>.IsReadOnly => false;

    /// <summary>
    /// Adds a menu entry to the menu.
    /// </summary>
    /// <param name="item">The menu entry to add.</param>
    public void Add(IMenuEntry item) => _entries.Add(item);

    /// <summary>
    /// Adds a range of menu entries to the menu.
    /// </summary>
    /// <param name="items">The menu entries to add.</param>
    public void AddRange(params ReadOnlySpan<IMenuEntry> items) => _entries.AddRange(items);

    /// <summary>
    /// Adds a range of menu entries to the menu.
    /// </summary>
    /// <param name="items">The collection of menu entries to add.</param>
    public void AddRange(IEnumerable<IMenuEntry> items) => _entries.AddRange(items);

    /// <summary>
    /// Clears all menu entries from the menu.
    /// </summary>
    public void Clear() => _entries.Clear();

    /// <summary>
    /// Determines whether the menu contains a specific menu entry.
    /// </summary>
    /// <param name="item">The menu entry to locate in the menu.</param>
    /// <returns><see langword="true"/> if the menu entry is found; otherwise, <see langword="false"/>.</returns>
    public bool Contains(IMenuEntry item) => _entries.Contains(item);

    /// <summary>
    /// Copies the elements of the menu to the array, starting at the specified array index.
    /// </summary>
    /// <param name="array">The destination array.</param>
    /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
    public void CopyTo(IMenuEntry[] array, int arrayIndex) => _entries.CopyTo(array, arrayIndex);

    /// <summary>
    /// Gets an enumerator that iterates through the menu.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<IMenuEntry> GetEnumerator() => _entries.GetEnumerator();

    /// <summary>
    /// Gets the zero-based index of the first occurrence of a specific menu entry.
    /// </summary>
    /// <param name="item">The menu entry to locate in the menu.</param>
    /// <returns>The zero-based index of the first occurrence of the menu entry; otherwise, -1 if not found.</returns>
    public int IndexOf(IMenuEntry item) => _entries.IndexOf(item);

    /// <summary>
    /// Inserts a menu entry at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which to insert the menu entry.</param>
    /// <param name="item">The menu entry to insert.</param>
    public void Insert(int index, IMenuEntry item) => _entries.Insert(index, item);

    /// <summary>
    /// Removes the first occurrence of a specific menu entry from the menu.
    /// </summary>
    /// <param name="item">The menu entry to remove.</param>
    /// <returns><see langword="true"/> if the menu entry was successfully removed; otherwise, <see langword="false"/>.</returns>
    public bool Remove(IMenuEntry item) => _entries.Remove(item);

    /// <summary>
    /// Removes the element at the specified index of the menu.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    public void RemoveAt(int index) => _entries.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.UI.System.Menu.Items;
using System.Collections;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a read-only menu definition to be used in menu systems.
/// </summary>
/// <param name="items">The menu items.</param>
[CollectionBuilder(typeof(MenuDefinition), nameof(Create))]
public sealed class MenuDefinition(ReadOnlySpan<IMenuItem> items) : IEnumerable<IMenuItem>
{
    private readonly IMenuItem[] _items = items.ToArray();

    /// <summary>
    /// Creates a menu definition.
    /// </summary>
    /// <param name="items">The menu items.</param>
    /// <returns>The created menu definition.</returns>
    public static MenuDefinition Create(params ReadOnlySpan<IMenuItem> items) => new(items);

    /// <inheritdoc/>
    public IEnumerator<IMenuItem> GetEnumerator() => _items.AsEnumerable().GetEnumerator();

    internal Span<IMenuItem> AsSpan() => _items;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

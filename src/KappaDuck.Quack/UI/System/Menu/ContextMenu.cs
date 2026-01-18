// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.Win32;
using KappaDuck.Quack.Interop.Win32.Handles;
using KappaDuck.Quack.Interop.Win32.Menu;
using KappaDuck.Quack.Interop.Win32.Primitives;
using KappaDuck.Quack.UI.System.Menu.Items;
using KappaDuck.Quack.Windows;
using System.ComponentModel;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a pop-up menu that displays a list of commands or options
/// when a user do a mouse right-click (generally) on a specific area of the application
/// or clicking on any other UI element.
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public sealed class ContextMenu : IDisposable
{
    private readonly HMenu _handle;
    private readonly MenuTree _tree;

    /// <summary>
    /// Creates a context menu based on the provided menu items.
    /// </summary>
    /// <param name="items">The menu items.</param>
    [OverloadResolutionPriority(1)]
    public ContextMenu(params ReadOnlySpan<IMenuItem> items)
    {
        _handle = User32.CreatePopupMenu();
        Win32Exception.ThrowIfHandleInvalid(_handle);

        _tree = MenuTree.Create(_handle, items);
    }

    /// <summary>
    /// Creates a context menu based on the menu definition.
    /// </summary>
    /// <param name="definition">The menu definition.</param>
    public ContextMenu(MenuDefinition definition) : this(definition.AsSpan())
    {
    }

    /// <summary>
    /// Creates a context menu based on the provided submenu.
    /// </summary>
    /// <param name="menu">The submenu.</param>
    public ContextMenu(ISubMenu menu) : this([.. menu.Items])
    {
    }

    /// <summary>
    /// Occurs when a menu item in the context menu is clicked by the user.
    /// </summary>
    public event Action<IMenuCommand>? ItemClicked;

    /// <inheritdoc/>
    public void Dispose()
    {
        _tree.Dispose();
        _handle.Dispose();
    }

    /// <summary>
    /// Shows the context menu.
    /// </summary>
    /// <remarks>
    /// The provided coordinates are converted from client-area coordinates to screen coordinates.
    /// </remarks>
    /// <param name="window">The window to which the context menu is attached.</param>
    /// <param name="x">The x-coordinate location of the context menu.</param>
    /// <param name="y">The y-coordinate location of the context menu.</param>
    /// <param name="options">The context menu display options.</param>
    public void Show(WindowBase window, int x, int y, ContextMenuOptions options)
    {
        POINT point = new(x, y);
        User32.ClientToScreen(window.Handle, ref point);

        uint id = User32.TrackPopupMenu(_handle, options.Flags, point, window.Handle);
        ItemClicked?.Invoke(MenuTree.Commands[(int)id]);
    }

    /// <summary>
    /// Shows the context menu.
    /// </summary>
    /// <remarks>
    /// The provided coordinates are converted from client-area coordinates to screen coordinates.
    /// </remarks>
    /// <param name="window">The window to which the context menu is attached.</param>
    /// <param name="x">The x-coordinate location of the context menu.</param>
    /// <param name="y">The y-coordinate location of the context menu.</param>
    public void Show(WindowBase window, int x, int y) => Show(window, x, y, ContextMenuOptions.Default);

    /// <summary>
    /// Shows the context menu.
    /// </summary>
    /// <remarks>
    /// The provided coordinates are converted from client-area coordinates to screen coordinates.
    /// </remarks>
    /// <param name="window">The window to which the context menu is attached.</param>
    /// <param name="position">The position of the context menu.</param>
    /// <param name="options">The context menu display options.</param>
    public void Show(WindowBase window, Vector2Int position, ContextMenuOptions options) => Show(window, position.X, position.Y, options);

    /// <summary>
    /// Shows the context menu.
    /// </summary>
    /// <remarks>
    /// The provided coordinates are converted from client-area coordinates to screen coordinates.
    /// </remarks>
    /// <param name="window">The window to which the context menu is attached.</param>
    /// <param name="position">The position of the context menu.</param>
    public void Show(WindowBase window, Vector2Int position) => Show(window, position, ContextMenuOptions.Default);

    /// <summary>
    /// Shows the context menu.
    /// </summary>
    /// <remarks>
    /// The provided coordinates are converted from client-area coordinates to screen coordinates.
    /// </remarks>
    /// <param name="window">The window to which the context menu is attached.</param>
    /// <param name="position">The position of the context menu.</param>
    /// <param name="options">The context menu display options.</param>
    public void Show(WindowBase window, Vector2 position, ContextMenuOptions options) => Show(window, (int)position.X, (int)position.Y, options);

    /// <summary>
    /// Shows the context menu.
    /// </summary>
    /// <remarks>
    /// The provided coordinates are converted from client-area coordinates to screen coordinates.
    /// </remarks>
    /// <param name="window">The window to which the context menu is attached.</param>
    /// <param name="position">The position of the context menu.</param>
    public void Show(WindowBase window, Vector2 position) => Show(window, position, ContextMenuOptions.Default);
}

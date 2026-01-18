// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Interop.Win32;
using KappaDuck.Quack.Interop.Win32.Handles;
using KappaDuck.Quack.Interop.Win32.Menu;
using KappaDuck.Quack.Interop.Win32.Primitives;
using KappaDuck.Quack.UI.System.Menu;
using KappaDuck.Quack.UI.System.Menu.Items;
using KappaDuck.Quack.Windows;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.UI.Window.Menu;

/// <summary>
/// Represents a container that is attached at the top of a window which holds menus and items.
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public sealed class MenuBar : IDisposable
{
    private readonly HMenu _handle;
    private readonly MenuTree _tree;

    private WindowBase? _window;

    /// <summary>
    /// Creates a menu bar based on the provided menu items.
    /// </summary>
    /// <param name="items">The menu items.</param>
    public MenuBar(params ReadOnlySpan<IMenuItem> items)
    {
        _handle = User32.CreateMenu();
        Win32Exception.ThrowIfHandleInvalid(_handle);

        _tree = MenuTree.Create(_handle, items);
    }

    /// <summary>
    /// Creates a menu bar based on the menu definition.
    /// </summary>
    /// <param name="definition">The menu definition.</param>
    public MenuBar(MenuDefinition definition) : this(definition.AsSpan())
    {
    }

    /// <summary>
    /// Occurs when a menu item in the menu bar is clicked by the user.
    /// </summary>
    public event Action<IMenuCommand>? ItemClicked;

    [MemberNotNullWhen(true, nameof(_window))]
    private bool IsWindowOpen => _window?.IsOpen == true;

    /// <inheritdoc/>
    public void Dispose()
    {
        Detach();

        _tree.Dispose();
        _handle.Dispose();
    }

    /// <summary>
    /// Hides the menu bar.
    /// </summary>
    public void Hide()
    {
        if (!IsWindowOpen)
            return;

        Win32Exception.ThrowIfFailed(User32.SetMenu(_window.Handle, HMenu.Zero));
        Win32Exception.ThrowIfFailed(User32.DrawMenuBar(_window.Handle));
    }

    /// <summary>
    /// Shows the menu bar.
    /// </summary>
    public void Show()
    {
        if (!IsWindowOpen)
            return;

        Win32Exception.ThrowIfFailed(User32.SetMenu(_window.Handle, _handle));
        Win32Exception.ThrowIfFailed(User32.DrawMenuBar(_window.Handle));
    }

    internal void AttachTo(WindowBase window)
    {
        _window = window;

        Show();
        HookWindowsMessage();
    }

    internal void Detach()
    {
        Hide();
        _window = null;
    }

    private void HookWindowsMessage()
    {
        QuackEngine.HookToWindowsMessage((_, ref msg) =>
        {
            if (IsItemClicked(msg) && MenuTree.Commands.TryGetValue(msg.WParam.Lower16Bits, out IMenuCommand? command))
            {
                ItemClicked?.Invoke(command);
                return false;
            }

            return true;
        });

        static bool IsItemClicked(MSG msg)
        {
            const int wmCommand = 0x0111;
            const int menu = 0x0000;

            return msg.Message == wmCommand && msg.WParam.Upper16Bits == menu;
        }
    }
}

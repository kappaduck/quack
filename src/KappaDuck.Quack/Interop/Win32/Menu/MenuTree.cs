// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Win32.Handles;
using KappaDuck.Quack.Interop.Win32.Primitives;
using KappaDuck.Quack.UI.System.Menu.Items;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32.Menu;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal sealed class MenuTree : IDisposable
{
    private readonly MenuRoot _root;

    internal MenuTree(MenuRoot root) => _root = root;

    internal static Dictionary<uint, IMenuCommand> Commands { get; } = [];

    public void Dispose()
    {
        foreach (IDisposable node in _root.Items.OfType<IDisposable>())
            node.Dispose();

        _root.Items.Clear();
    }

    internal static MenuTree Create(HMenu handle, params ReadOnlySpan<IMenuItem> entries)
    {
        MenuRoot root = new(handle);
        MenuTree tree = new(root);

        tree.AppendEntries(root, entries);
        return tree;
    }

    private void AppendEntries(ISubMenuNode parent, ReadOnlySpan<IMenuItem> entries)
    {
        foreach (IMenuItem entry in entries)
            AppendEntry(parent, entry);
    }

    private void AppendEntry(ISubMenuNode parent, IMenuItem entry)
    {
        if (entry is IMenuCommand item)
        {
            MenuCommandNode node = new(item.Id, parent);
            parent.Items.Add(node);

            Commands.TryAdd(item.Id, item);
            User32.AppendMenuW(parent.Handle, GetFlags(item), item.Id, item.Label);

            return;
        }

        if (entry is MenuSeparator)
        {
            parent.Items.Add(new MenuSeparatorNode(parent));
            User32.AppendMenuW(parent.Handle, (uint)MenuEntryKind.Separator, 0, string.Empty);

            return;
        }

        if (entry is ISubMenu submenu)
        {
            HMenu subHandle = User32.CreatePopupMenu();
            SubMenuNode node = new(subHandle, parent);

            parent.Items.Add(node);

            User32.AppendMenuW(parent.Handle, GetFlags(submenu), subHandle, submenu.Label);
            AppendEntries(node, [.. submenu.Items]);
        }
    }

    private static uint GetFlags(IMenuCommand command) => (uint)command.State | (uint)command.Layout | (uint)(command.Checked ? MenuEntryCheckState.Checked : MenuEntryCheckState.Unchecked);

    private static uint GetFlags(ISubMenu submenu) => (uint)MenuEntryKind.Popup | (uint)submenu.Layout | (uint)submenu.State;
}

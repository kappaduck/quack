// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Win32.Handles;
using KappaDuck.Quack.UI.System.Menu;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32.Builders;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal static class MenuBuilder
{
    internal static void Initialize(ReadOnlySpan<Menu> menus, HMenu parent)
    {
        foreach (Menu menu in menus)
            AppendEntry(menu, parent);
    }

    private static void AppendEntry(IMenuEntry entry, HMenu parent)
    {
        if (entry is MenuItem item)
        {
            uint flags = (uint)item.State
                       | (uint)item.Layout
                       | (uint)(item.Checked ? MenuItemCheckState.Checked : MenuItemCheckState.Unchecked);

            User32.AppendMenuW(parent, flags, item.Id, item.Title);
        }

        if (entry is MenuSeparator)
            User32.AppendMenuW(parent, (uint)MenuItemKind.Separator, 0, string.Empty);

        if (entry is Menu subMenu)
        {
            HMenu subHandle = User32.CreatePopupMenu();
            uint flags = (uint)MenuItemKind.Popup | (uint)subMenu.Layout;

            User32.AppendMenuW(parent, flags, subHandle, subMenu.Title);

            foreach (IMenuEntry subEntry in subMenu)
                AppendEntry(subEntry, subHandle);
        }
    }

    private enum MenuItemKind : uint
    {
        /// <summary>
        /// Specifies that the menu item is a text string; the lpNewItem parameter is a pointer to the string.
        /// </summary>
        String = 0x00000000,

        /// <summary>
        /// Specifies that the menu item opens a drop-down menu or sub-menu.
        /// </summary>
        Popup = 0x00000010,

        /// <summary>
        /// Draws a horizontal dividing line.
        /// </summary>
        /// <remarks>
        /// It only applies to a drop-down menu, sub-menu or shortcut menu.
        /// </remarks>
        Separator = 0x00000800
    }

    private enum MenuItemCheckState : uint
    {
        /// <summary>
        /// Does not place a check mark next to the item.
        /// </summary>
        Unchecked = 0x00000000,

        /// <summary>
        /// Places a check mark next to the menu item.
        /// </summary>
        Checked = 0x00000008
    }
}

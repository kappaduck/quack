// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Win32.Handles;

namespace KappaDuck.Quack.Interop.Win32.Menu;

internal sealed record SubMenuNode(HMenu Handle, ISubMenuNode SubMenu) : ISubMenuNode, IMenuNode
{
    public List<IMenuNode> Items { get; } = [];

    public uint Position => (uint)SubMenu.Items.IndexOf(this);
}

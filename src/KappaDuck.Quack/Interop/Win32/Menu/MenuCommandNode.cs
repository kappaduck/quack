// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Win32.Menu;

internal sealed record MenuCommandNode(uint Id, ISubMenuNode SubMenu) : IMenuNode
{
    public uint Position => (uint)SubMenu.Items.IndexOf(this);
}

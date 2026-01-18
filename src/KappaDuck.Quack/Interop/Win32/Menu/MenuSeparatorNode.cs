// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Win32.Menu;

internal sealed record MenuSeparatorNode(ISubMenuNode SubMenu) : IMenuNode
{
    public int Position => SubMenu.Items.IndexOf(this);
}

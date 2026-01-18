// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Win32.Menu;

internal interface IMenuNode
{
    ISubMenuNode SubMenu { get; }

    int Position { get; }
}

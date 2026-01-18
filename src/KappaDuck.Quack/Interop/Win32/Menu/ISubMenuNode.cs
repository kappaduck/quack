// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Win32.Handles;

namespace KappaDuck.Quack.Interop.Win32.Menu;

internal interface ISubMenuNode
{
    HMenu Handle { get; }

    List<IMenuNode> Items { get; }
}

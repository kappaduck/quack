// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using System.ComponentModel;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32.Handles;

[SupportedOSPlatform("windows")]
internal sealed class HMenu() : SafeHandleZeroInvalid(ownsHandle: true)
{
    protected override void Free()
    {
        if (!User32.DestroyMenu(handle))
        {
            throw new Win32Exception();
        }
    }
}

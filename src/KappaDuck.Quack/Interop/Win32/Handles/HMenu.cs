// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using System.ComponentModel;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32.Handles;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal sealed class HMenu() : SafeHandleZeroInvalid(ownsHandle: true)
{
    public static HMenu Zero { get; } = new HMenu();

    protected override void Free() => Win32Exception.ThrowIfFailed(User32.DestroyMenu(handle));
}

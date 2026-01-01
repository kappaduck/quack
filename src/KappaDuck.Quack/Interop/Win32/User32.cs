// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using KappaDuck.Quack.Interop.Win32.Handles;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32;

[SupportedOSPlatform("windows")]
internal static partial class User32
{
    private const string Core = "user32";

    [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial HMenu CreateMenu();

    [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial HMenu CreatePopupMenu();

    [LibraryImport(Core, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool DestroyMenu(nint menu);

    [LibraryImport(Core, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool DrawMenuBar(WindowHandle window);

    [LibraryImport(Core, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SetMenu(WindowHandle window, HMenu menu);
}
// https://learn.microsoft.com/en-us/windows/win32/menurc/menus

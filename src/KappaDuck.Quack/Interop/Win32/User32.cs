// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using KappaDuck.Quack.Interop.Win32.Handles;
using KappaDuck.Quack.Interop.Win32.Primitives;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal static partial class User32
{
    private const string DLL = "user32";
    private const uint MF_BYPOSITION = 0x00000400;
    private const uint TPM_RETURNCMD = 0x0100;

    [LibraryImport(DLL, SetLastError = true, StringMarshalling = StringMarshalling.Utf16), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool AppendMenuW(HMenu menu, uint flags, uint itemId, string label);

    [LibraryImport(DLL, SetLastError = true, StringMarshalling = StringMarshalling.Utf16), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool AppendMenuW(HMenu menu, uint flags, HMenu subMenu, string label);

    [LibraryImport(DLL), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial int CheckMenuItem(HMenu menu, uint id, uint check);

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool CheckMenuRadioItem(HMenu menu, uint first, uint last, uint check, uint flags);

    [LibraryImport(DLL), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool ClientToScreen(WindowHandle window, ref POINT point);

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial HMenu CreateMenu();

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial HMenu CreatePopupMenu();

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool DeleteMenu(HMenu menu, uint position, uint flags);

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool DestroyMenu(nint menu);

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool DrawMenuBar(WindowHandle window);

    [LibraryImport(DLL), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial int EnableMenuItem(HMenu menu, uint id, uint enable);

    [LibraryImport(DLL), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial void HiliteMenuItem(WindowHandle window, HMenu menu, uint id, uint hilite);

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool RemoveMenu(HMenu menu, uint id, uint flags);

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SetMenu(WindowHandle window, HMenu menu);

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SetMenuDefaultItem(HMenu menu, uint id, uint byPos);

    internal static uint TrackPopupMenu(HMenu menu, uint flags, POINT point, WindowHandle window)
        => TrackPopupMenuEx(menu, flags | TPM_RETURNCMD, point.X, point.Y, window);

    [LibraryImport(DLL, SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static partial uint TrackPopupMenuEx(HMenu menu, uint flags, int x, int y, WindowHandle window, nint parameters = default);
}

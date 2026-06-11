// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.Win32;
using KappaDuck.Quack.Interop.Win32.Primitives;
using KappaDuck.Quack.Interop.X11;
using KappaDuck.Quack.Interop.X11.Primitives;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Manage event hooks for differents platforms so features can observe them through a single SDL hook.
/// </summary>
/// <remarks>
/// <para>
/// SDL exposes only one hook slot per platform, so this installs the SDL hook once when the first
/// listener attaches and removes it when the last one detaches, fanning each event out to every
/// listener in between.
/// </para>
/// </remarks>
internal static class EventHook
{
    private static readonly Lock _lock = new();

    private static X11.EventCallback[] _x11Callbacks = [];
    private static Win32.MessageCallback[] _win32Callbacks = [];

    [SupportedOSPlatform(nameof(OSPlatform.Linux))]
    internal static void AddX11Callback(X11.EventCallback callback)
    {
        lock (_lock)
        {
            X11.EventCallback[] callbacks = [.. _x11Callbacks, callback];
            _x11Callbacks = callbacks;

            if (callbacks.Length == 1)
                SDL3.SetX11EventHook(&OnX11Event, null);
        }
    }

    [SupportedOSPlatform(nameof(OSPlatform.Linux))]
    internal static void RemoveX11Callback(X11.EventCallback callback)
    {
        lock (_lock)
        {
            X11.EventCallback[] callbacks = [.. _x11Callbacks.Where(c => !c.Equals(callback))];
            _x11Callbacks = callbacks;

            if (callbacks.Length == 0)
                SDL3.SetX11EventHook(null, null);
        }
    }

    [SupportedOSPlatform(nameof(OSPlatform.Windows))]
    internal static void AddWin32Callback(Win32.MessageCallback callback)
    {
        lock (_lock)
        {
            Win32.MessageCallback[] callbacks = [.. _win32Callbacks, callback];
            _win32Callbacks = callbacks;

            if (callbacks.Length == 1)
                SDL3.SetWindowsMessageHook(&OnWindowsMessage, null);
        }
    }

    [SupportedOSPlatform(nameof(OSPlatform.Windows))]
    internal static void RemoveWin32Callback(Win32.MessageCallback callback)
    {
        lock (_lock)
        {
            Win32.MessageCallback[] callbacks = [.. _win32Callbacks.Where(c => !c.Equals(callback))];
            _win32Callbacks = callbacks;

            if (callbacks.Length == 0)
                SDL3.SetWindowsMessageHook(null, null);
        }
    }

    [SupportedOSPlatform(nameof(OSPlatform.Windows))]
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte OnWindowsMessage(void* data, MSG* message)
    {
        byte keep = 1;
        Win32.MessageCallback[] callbacks = _win32Callbacks;

        unsafe
        {
            for (int i = 0; i < callbacks.Length; i++)
            {
                if (!callbacks[i](*message))
                    keep = 0;
            }
        }

        return keep;
    }

    [SupportedOSPlatform(nameof(OSPlatform.Linux))]
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte OnX11Event(void* data, XEvent* e)
    {
        byte keep = 1;
        X11.EventCallback[] callbacks = _x11Callbacks;

        unsafe
        {
            for (int i = 0; i < callbacks.Length; i++)
            {
                if (!callbacks[i](*e))
                    keep = 0;
            }
        }

        return keep;
    }
}

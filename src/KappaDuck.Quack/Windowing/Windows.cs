// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;
using System.Collections.Concurrent;

namespace KappaDuck.Quack.Windowing;

/// <summary>
/// Provides access to all open windows.
/// </summary>
public static class Windows
{
    private static readonly ConcurrentDictionary<nint, Window> _windows = [];

    /// <summary>
    /// Gets the number of currently open windows.
    /// </summary>
    public static int Count => _windows.Count;

    /// <summary>
    /// Gets the window that currently has input grabbed, or <see langword="null"/> if none.
    /// </summary>
    public static Window? Grabbed => unsafe (FromHandle(SDL3.GetGrabbedWindow()));

    /// <summary>
    /// Gets the window currently holding keyboard focus, or <see langword="null"/> if none.
    /// </summary>
    public static Window? KeyboardFocus => unsafe (FromHandle(SDL3.GetKeyboardFocus()));

    /// <summary>
    /// Gets the window currently holding mouse focus, or <see langword="null"/> if none.
    /// </summary>
    public static Window? MouseFocus => unsafe (FromHandle(SDL3.GetMouseFocus()));

    /// <summary>
    /// Gets a snapshot of all currently open windows.
    /// </summary>
    public static IReadOnlyList<Window> All => [.. _windows.Values];

    /// <summary>
    /// Gets the window with the given id, or <see langword="null"/> if no open window matches it.
    /// </summary>
    /// <param name="id">The window id.</param>
    /// <returns>The matching window, or <see langword="null"/>.</returns>
    public static Window? FromId(uint id) => unsafe (FromHandle(SDL3.GetWindowFromID(id)));

    internal static Window? FromHandle(SDL_Window* handle)
        => handle is not null && _windows.TryGetValue((nint)handle, out Window? window) ? window : null;

    internal static void Register(Window window) => _windows[unsafe ((nint)window.NativeHandle)] = window;

    internal static void Unregister(Window window) => _windows.TryRemove(unsafe ((nint)window.NativeHandle), out _);
}

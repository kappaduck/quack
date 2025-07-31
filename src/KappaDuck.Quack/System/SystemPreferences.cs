// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.System;

/// <summary>
/// Represents system preferences such as the current theme and screen saver.
/// </summary>
public static partial class SystemPreferences
{
    /// <summary>
    /// Gets or sets a value indicating whether the screen saver is enabled.
    /// </summary>
    /// <remarks>
    /// If you disable the screensaver, it is automatically re-enabled when the engine shuts down.
    /// The screensaver is disabled by default.
    /// </remarks>
    /// <exception cref="QuackNativeException">Failed to enable or disable the screensaver.</exception>
    public static bool ScreenSaver
    {
        get => SDL_ScreenSaverEnabled();
        set
        {
            if (value)
            {
                QuackNativeException.ThrowIfFailed(SDL_EnableScreenSaver());
                return;
            }

            QuackNativeException.ThrowIfFailed(SDL_DisableScreenSaver());
        }
    }

    /// <summary>
    /// Gets the current system theme.
    /// </summary>
    public static SystemTheme Theme => SDL_GetSystemTheme();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_DisableScreenSaver();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_EnableScreenSaver();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SystemTheme SDL_GetSystemTheme();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_ScreenSaverEnabled();
}

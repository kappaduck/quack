// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;

namespace KappaDuck.Quack.System;

/// <summary>
/// Represents system preferences such as the current theme and screen saver.
/// </summary>
public static class SystemPreferences
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
        get => SDL.System.SDL_ScreenSaverEnabled();
        set
        {
            if (value)
            {
                QuackNativeException.ThrowIfFailed(SDL.System.SDL_EnableScreenSaver());
                return;
            }

            QuackNativeException.ThrowIfFailed(SDL.System.SDL_DisableScreenSaver());
        }
    }

    /// <summary>
    /// Gets the current system theme.
    /// </summary>
    public static SystemTheme Theme => SDL.System.SDL_GetSystemTheme();
}

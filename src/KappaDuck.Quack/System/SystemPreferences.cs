// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;

namespace KappaDuck.Quack.System;

/// <summary>
/// Provides access to system preferences.
/// </summary>
[PublicAPI]
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
        get;
        set
        {
            if (value)
            {
                QuackNativeException.ThrowIfFailed(Native.SDL_EnableScreenSaver());

                field = true;
                return;
            }

            QuackNativeException.ThrowIfFailed(Native.SDL_DisableScreenSaver());
            field = false;
        }
    }

    /// <summary>
    /// Gets the current system theme.
    /// </summary>
    public static Theme Theme => Native.SDL_GetSystemTheme();
}

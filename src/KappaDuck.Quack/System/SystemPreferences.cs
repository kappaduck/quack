// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;

namespace KappaDuck.Quack.System;

/// <summary>
/// Provides access to system preferences.
/// </summary>
public static class SystemPreferences
{
    /// <summary>
    /// Gets or sets a value indicating whether the screen saver is enabled.
    /// </summary>
    /// <remarks>
    /// <para>If you disable the screen saver, it is automatically re-enabled when the application quits.</para>
    /// <para>The screen saver is disabled by default.</para>
    /// </remarks>
    /// <exception cref="QuackInteropException">Thrown if enabling or disabling the screen saver fails.</exception>
    public static bool ScreenSaverEnabled
    {
        get;
        set
        {
            if (value)
            {
                SDLThrowHelper.ThrowIfFailed(SDL3.EnableScreenSaver());

                field = true;
                return;
            }

            SDLThrowHelper.ThrowIfFailed(SDL3.DisableScreenSaver());
            field = false;
        }
    }

    /// <summary>
    /// Gets the current system theme.
    /// </summary>
    public static Theme Theme => SDL3.GetSystemTheme();
}

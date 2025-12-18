// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;

namespace KappaDuck.Quack.System;

/// <summary>
/// Provides functionality to launch URIs using the system's default applications.
/// </summary>
/// <remarks>
/// The behavior of URI launching may vary depending on the operating system and its configuration.
/// Ensure that the URI scheme is supported by the target system for successful launching.
/// </remarks>
public static class UriLauncher
{
    /// <summary>
    /// Open the specified URI using the system's default application.
    /// </summary>
    /// <param name="uri">The URL/URI to open.</param>
    /// <exception cref="QuackNativeException">Fails to open the URI.</exception>
    public static void Open(string uri)
        => QuackNativeException.ThrowIfFailed(Native.SDL_OpenURL(uri));

    /// <inheritdoc cref="Open(string)"/>
    public static void Open(Uri uri) => Open(uri.ToString());
}

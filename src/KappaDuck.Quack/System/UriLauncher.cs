// Copyright (c) KappaDuck.
// Licensed under the MIT license.

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
    /// Opens the URI using the system's default application.
    /// </summary>
    /// <remarks>
    /// The URI can be any valid URI, such as a web URL, an email address, file path, etc.
    /// </remarks>
    /// <param name="uri">The URI to be opened.</param>
    /// <exception cref="QuackInteropException">Thrown if the URI could not be opened successfully.</exception>
    public static void Open(string uri)
        => SDLThrowHelper.ThrowIfFailed(SDL3.OpenURL(uri));

    /// <inheritdoc cref="Open(string)"/>
    public static void Open(Uri uri) => Open(uri.ToString());
}

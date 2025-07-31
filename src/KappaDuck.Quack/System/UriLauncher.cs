// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.System;

/// <summary>
/// Provides methods to launch URLs.
/// </summary>
/// <remarks>
/// Open a URL in a separate, system-provided application. How this works will vary wildly depending on the platform.
/// This will likely launch what makes sense to handle a specific URL's protocol (a web browser for http://, etc),
/// but it might also be able to launch file managers for directories and other things.
/// What happens when you open a URL varies wildly as well: your game window may lose
/// focus and may or may not lose focus if your game was Fullscreen or grabbing input at the time.
/// </remarks>
public static partial class UriLauncher
{
    /// <summary>
    /// Open a URL/URI in the system's default web browser or other appropriate external application.
    /// </summary>
    /// <param name="uri">The URL/URI to open.</param>
    /// <exception cref="QuackNativeException">Failed to open the URL/URI.</exception>
    public static void Open(string uri) => QuackNativeException.ThrowIfFailed(SDL_OpenURL(uri));

    /// <inheritdoc cref="Open(string)"/>
    public static void Open(Uri uri) => Open(uri.ToString());

    [LibraryImport(SDLNative.Library, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_OpenURL(string url);
}

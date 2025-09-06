// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Native;

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Provides loading and saving functionality for images.
/// </summary>
public static unsafe class Image
{
    /// <summary>
    /// Loads an image from a file.
    /// </summary>
    /// <param name="filePath">The path to the image file.</param>
    /// <returns>The loaded image.</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="QuackNativeException">An error occurred while loading the image.</exception>
    public static Surface Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The file does not exist.", filePath);

        SDL_Surface* handle = SDL.Surface.IMG_Load(filePath);
        QuackNativeException.ThrowIfNull(handle);

        return new Surface(handle);
    }
}

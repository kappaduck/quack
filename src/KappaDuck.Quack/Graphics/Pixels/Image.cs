// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Provides loading and saving functionality for images.
/// </summary>
public static unsafe class Image
{
    /// <summary>
    /// Loads an image from a file.
    /// </summary>
    /// <param name="path">The path to the image file.</param>
    /// <returns>The loaded image.</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public static Surface Load(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("The file does not exist.", path);

        SDL_Surface* handle = Native.IMG_Load(path);
        QuackNativeException.ThrowIfNull(handle);

        return new Surface(handle);
    }
}

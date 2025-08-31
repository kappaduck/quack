// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Provides loading and saving functionality for images.
/// </summary>
public static unsafe partial class Image
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

        Surface.SurfaceHandle* handle = IMG_Load(filePath);
        QuackNativeException.ThrowIfNull(handle);

        return new Surface(handle);
    }

    [LibraryImport(SDLNative.ImageLibrary, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface.SurfaceHandle* IMG_Load(string filePath);
}

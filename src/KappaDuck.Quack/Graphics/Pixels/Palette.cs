// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Represents a color palette for indexed color graphics.
/// </summary>
public sealed unsafe class Palette : IDisposable
{
    /// <summary>
    /// Create a palette with the specified number of colors.
    /// </summary>
    /// <remarks>
    /// The palette entries are initialized to white.
    /// </remarks>
    /// <param name="length">The number of colors in the palette.</param>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public Palette(int length)
    {
        Handle = Native.SDL_CreatePalette(length);
        QuackNativeException.ThrowIfNull(Handle);
    }

    internal Palette(SDL_Surface* surface)
    {
        Handle = Native.SDL_CreateSurfacePalette(surface);
        QuackNativeException.ThrowIfNull(Handle);
    }

    internal Palette(SDL_Palette* handle)
    {
        QuackNativeException.ThrowIfNull(handle);
        Handle = handle;
    }

    /// <summary>
    /// Gets the native surface handle.
    /// </summary>
    internal SDL_Palette* Handle { get; private set; }

    /// <summary>
    /// Releases the unmanaged resources used by the palette.
    /// </summary>
    public void Dispose()
    {
        if (Handle is not null)
        {
            Native.SDL_DestroyPalette(Handle);
            Handle = null;
        }
    }

    /// <summary>
    /// Sets a range of colors in the palette.
    /// </summary>
    /// <param name="startIndex">The starting index in the palette to set colors.</param>
    /// <param name="colors">The colors to set in the palette.</param>
    /// <exception cref="QuackNativeException">Thrown when failed to set the palette colors.</exception>
    public void SetColors(int startIndex, ReadOnlySpan<Color> colors)
    {
        Span<SDL_Color> nativeColors = stackalloc SDL_Color[colors.Length];

        for (int i = 0; i < colors.Length; i++)
            nativeColors[i] = new SDL_Color(colors[i].R, colors[i].G, colors[i].B, colors[i].A);

        QuackNativeException.ThrowIfFailed(Native.SDL_SetPaletteColors(Handle, nativeColors, startIndex, colors.Length));
    }
}

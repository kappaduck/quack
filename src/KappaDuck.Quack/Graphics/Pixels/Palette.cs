// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Graphics.Drawing;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Pixels;

/// <summary>
/// Represents a color palette for indexed color graphics.
/// </summary>
public sealed unsafe partial class Palette : IDisposable
{
    /// <summary>
    /// Create a palette with the specified number of colors.
    /// </summary>
    /// <remarks>
    /// The palette entries are initialized to white.
    /// </remarks>
    /// <param name="length">The number of colors in the palette.</param>
    public Palette(int length)
    {
        Handle = SDL_CreatePalette(length);
        QuackNativeException.ThrowIfNull(Handle);
    }

    internal Palette(Surface.SurfaceHandle* surface)
    {
        Handle = SDL_CreateSurfacePalette(surface);
        QuackNativeException.ThrowIfNull(Handle);
    }

    internal Palette(PaletteHandle* handle)
    {
        QuackNativeException.ThrowIfNull(handle);
        Handle = handle;
    }

    /// <summary>
    /// Gets the native surface handle.
    /// </summary>
    internal PaletteHandle* Handle { get; private set; }

    /// <summary>
    /// Releases the unmanaged resources used by the palette.
    /// </summary>
    public void Dispose()
    {
        if (Handle is not null)
        {
            SDL_DestroyPalette(Handle);
            Handle = null;
        }
    }

    /// <summary>
    /// Sets a range of colors in the palette.
    /// </summary>
    /// <param name="startIndex">The starting index in the palette to set colors.</param>
    /// <param name="colors">The colors to set in the palette.</param>
    /// <exception cref="QuackNativeException">An error occurred while setting the palette colors.</exception>
    public void SetColors(int startIndex, ReadOnlySpan<Color> colors)
    {
        Span<NativeColor> nativeColors = stackalloc NativeColor[colors.Length];

        for (int i = 0; i < colors.Length; i++)
            nativeColors[i] = new NativeColor(colors[i].R, colors[i].G, colors[i].B, colors[i].A);

        QuackNativeException.ThrowIfFailed(SDL_SetPaletteColors(Handle, nativeColors, startIndex, colors.Length));
    }
}

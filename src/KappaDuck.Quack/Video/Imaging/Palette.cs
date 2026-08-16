// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Video.Imaging;

/// <summary>
/// A set of indexed colors used by indexed pixel formats such as <see cref="PixelFormat.Index8"/>.
/// </summary>
public sealed class Palette : IDisposable
{
    private readonly bool _owned;

    /// <summary>
    /// Creates a palette with the given number of entries, all initialized to opaque white.
    /// </summary>
    /// <param name="count">The number of color entries. Must be greater than zero.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than or equal to zero.</exception>
    /// <exception cref="QuackInteropException">Failed to create the palette.</exception>
    public Palette(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        unsafe
        {
            Handle = SDL3.CreatePalette(count);
            SDLThrowHelper.ThrowIfNull(Handle);
        }

        _owned = true;
    }

    /// <summary>
    /// Creates a palette filled with the given colors.
    /// </summary>
    /// <param name="colors">The colors to copy into the new palette. Must not be empty.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="colors"/> is empty.</exception>
    public Palette(ReadOnlySpan<Color> colors) : this(colors.Length)
        => SetColors(colors);

    internal Palette(SDL_Palette* palette)
    {
        unsafe
        {
            Handle = palette;
        }

        _owned = false;
    }

    /// <summary>
    /// Gets or sets the color at the given index.
    /// </summary>
    /// <param name="index">The zero-based entry index.</param>
    /// <returns>The color at <paramref name="index"/>.</returns>
    /// <exception cref="ObjectDisposedException">The palette is disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is negative or not less than <see cref="Count"/>.</exception>
    public Color this[int index]
    {
        get
        {
            ThrowIfDisposed();
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            unsafe
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Handle->Count);
                return Handle->Colors[index];
            }
        }

        set
        {
            ThrowIfDisposed();
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            unsafe
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Handle->Count);
                SDLThrowHelper.ThrowIfFailed(SDL3.SetPaletteColors(Handle, [value], index, 1));
            }
        }
    }

    /// <summary>
    /// Gets a read-only view over the palette entries.
    /// </summary>
    /// <remarks>
    /// The returned span aliases the palette's internal storage. It is only valid while the palette is
    /// alive and unmodified; do not store it across calls to <see cref="SetColors"/> or <see cref="Dispose"/>.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">The palette is disposed.</exception>
    public ReadOnlySpan<Color> Colors
    {
        get
        {
            ThrowIfDisposed();

            unsafe
            {
                return new ReadOnlySpan<Color>(Handle->Colors, Handle->Count);
            }
        }
    }

    /// <summary>
    /// Gets the number of color entries in the palette.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The palette is disposed.</exception>
    public int Count
    {
        get
        {
            ThrowIfDisposed();

            unsafe
            {
                return Handle->Count;
            }
        }
    }

    internal SDL_Palette* Handle { get; private set; }

    /// <inheritdoc/>
    public void Dispose()
    {
        unsafe
        {
            if (Handle is null)
                return;

            if (_owned)
                SDL3.DestroyPalette(Handle);

            Handle = null;
        }
    }

    /// <summary>
    /// Copies a range of colors into the palette.
    /// </summary>
    /// <param name="colors">The colors to copy.</param>
    /// <param name="startIndex">The index of the first entry to overwrite.</param>
    /// <exception cref="ObjectDisposedException">The palette is disposed.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="startIndex"/> is negative, or the range would extend past the end of the palette.
    /// </exception>
    public void SetColors(ReadOnlySpan<Color> colors, int startIndex = 0)
    {
        ThrowIfDisposed();
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);

        unsafe
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex + colors.Length, Handle->Count);
            SDLThrowHelper.ThrowIfFailed(SDL3.SetPaletteColors(Handle, colors, startIndex, colors.Length));
        }
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(unsafe (Handle is null), typeof(Palette));
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Input;

/// <summary>
/// Represents a mouse cursor.
/// </summary>
public sealed class Cursor : IDisposable
{
    private readonly bool _owned;

    /// <summary>
    /// Creates a cursor with a system cursor type.
    /// </summary>
    /// <param name="type">The system cursor type.</param>
    public Cursor(CursorType type) : this(SDL3.CreateSystemCursor(type), true)
    {
    }

    /// <summary>
    /// Creates a cursor with the image
    /// </summary>
    /// <remarks>
    /// If the surface contains alternate images added with <see cref="Surface.AddAlternateImage(Surface)"/>,
    /// the surface will be interpreted as the content to be used for 100% display scale.
    /// For example, if the original surface is 32x32, then on a 200% display scale on Windows,
    /// a 64x64 version of the image will be used, if available.
    /// If a matching version of the image isn't available, the closest larger size image will be downscaled
    /// to the appropriate size and be used instead, if available. Otherwise, the closest smaller image will be upscaled and be used instead.
    /// </remarks>
    /// <param name="surface">The image to use</param>
    /// <param name="hotspot">The cursor hotspot</param>
    public Cursor(Surface surface, Point hotspot) : this(SDL3.CreateColorCursor(surface.Handle, hotspot.X, hotspot.Y), true)
    {
    }

    private Cursor(SDL_Cursor* handle, bool owned)
    {
        QuackEngine.EnsureInitialized(Subsystem.Video);

        SDLThrowHelper.ThrowIfNull(handle);

        Handle = handle;
        _owned = owned;
    }

    /// <summary>
    /// Gets the active cursor.
    /// </summary>
    public static Cursor Current => new(SDL3.GetCursor(), false);

    /// <summary>
    /// Gets the default cursor.
    /// </summary>
    public static Cursor Default { get; } = new(SDL3.GetDefaultCursor(), false);

    /// <summary>
    /// Gets or sets a value indicating whether the active cursor is visible.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to change the cursor visibility.</exception>
    public static bool Visible
    {
        get;
        set
        {
            if (field == value)
                return;

            SDLThrowHelper.ThrowIfFailed(value ? SDL3.ShowCursor() : SDL3.HideCursor());
            field = value;
        }
    } = true;

    internal SDL_Cursor* Handle { get; private set; }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Handle is null || !_owned)
            return;

        SDL3.DestroyCursor(Handle);
        Handle = null;
    }
}

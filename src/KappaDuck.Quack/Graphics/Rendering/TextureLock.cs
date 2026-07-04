// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// A scope that keeps a <see cref="Texture"/> locked for direct write access to its pixel buffer as <see cref="Video.Pixels.Surface"/>.
/// </summary>
/// <remarks>
/// Obtained from <see cref="Texture.Lock"/>. Dispose it to upload the changes and unlock the texture.
/// The <see cref="Video.Pixels.Surface"/> is write-only and only valid for the lifetime of this scope; do not
/// let it escape the <see langword="using"/> block.
/// </remarks>
public readonly ref struct TextureLock : IDisposable
{
    private readonly SDL_Texture* _texture;

    internal TextureLock(SDL_Texture* texture, RectI? region)
    {
        _texture = texture;
        SDL_Surface* surface;

        if (region is { } area)
        {
            SDLThrowHelper.ThrowIfFailed(SDL3.LockTextureToSurface(_texture, &area, &surface));
        }
        else
        {
            SDLThrowHelper.ThrowIfFailed(SDL3.LockTextureToSurface(_texture, null, &surface));
        }

        Surface = new Surface(surface, false);
    }

    /// <summary>
    /// Gets the writable surface of the locked region.
    /// </summary>
    public Surface Surface { get; }

    /// <summary>
    /// Uploads the written pixels and unlocks the texture.
    /// </summary>
    public void Dispose()
    {
        if (_texture is null)
            return;

        Surface.Dispose();
        SDL3.UnlockTexture(_texture);
    }
}

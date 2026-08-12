// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// A scope that keeps a <see cref="Surface"/> locked for direct access to its pixel buffer.
/// </summary>
/// <remarks>
/// Obtained from <see cref="Surface.Lock"/>. Dispose it to unlock the surface. The <see cref="Pixels"/> buffer
/// is only valid for the lifetime of this scope; do not let it escape the <see langword="using"/> block.
/// </remarks>
public readonly ref struct SurfaceLock
{
    private readonly Surface _surface;

    internal SurfaceLock(Surface surface)
    {
        _surface = surface;

        unsafe
        {
            SDL3.LockSurface(_surface.Handle);
            Pixels = new Span<byte>(_surface.Handle->Pixels, _surface.Pitch * _surface.Height);
        }
    }

    /// <summary>
    /// Gets the raw pixel buffer of the surface, with each row occupying <see cref="Surface.Pitch"/> bytes.
    /// </summary>
    public Span<byte> Pixels { get; }

    /// <summary>
    /// Unlocks the surface.
    /// </summary>
    public void Dispose()
    {
        unsafe
        {
            if (_surface.Handle is null)
                return;

            SDL3.UnlockSurface(_surface.Handle);
        }
    }
}

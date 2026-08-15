// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// A scope that redirects a <see cref="Renderer"/> to draw into a texture and restores the previous target when disposed.
/// </summary>
/// <remarks>
/// Obtained from <see cref="Renderer.WithTarget(Texture)"/>. Drawing performed while the scope is alive renders into the texture.
/// </remarks>
public readonly ref struct RenderTargetScope : IDisposable
{
    private readonly SDL_Renderer* _renderer;

    internal unsafe RenderTargetScope(SDL_Renderer* renderer) => _renderer = renderer;

    /// <summary>
    /// Restores the render target that was active before this scope began.
    /// </summary>
    public void Dispose()
    {
        unsafe
        {
            SDL3.SetRenderTarget(_renderer, null);
        }
    }
}

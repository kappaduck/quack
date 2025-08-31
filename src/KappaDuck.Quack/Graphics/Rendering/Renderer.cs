// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Pixels;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Interop.SDL.Handles;
using KappaDuck.Quack.Video.Displays;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Rendering;

internal sealed partial class Renderer : IDisposable
{
    private readonly RendererHandle _handle = new();

    internal Renderer()
    {
    }

    internal Renderer(WindowHandle window, string? name)
    {
        _handle = SDL_CreateRenderer(window, name);
        QuackNativeException.ThrowIf(_handle.IsInvalid);

        QuackNativeException.ThrowIfFailed(SDL_SetRenderVSync(_handle, VSync));
        QuackNativeException.ThrowIfFailed(SDL_SetRenderLogicalPresentation(_handle, Presentation.Width, Presentation.Height, Presentation.Mode));
    }

    internal (int Width, int Height) CurrentOutputSize
    {
        get
        {
            if (_handle.IsInvalid)
                return (0, 0);

            QuackNativeException.ThrowIfFailed(SDL_GetCurrentRenderOutputSize(_handle, out int w, out int h));
            return (w, h);
        }
    }

    internal (int Width, int Height) OutputSize
    {
        get
        {
            if (_handle.IsInvalid)
                return (0, 0);

            QuackNativeException.ThrowIfFailed(SDL_GetRenderOutputSize(_handle, out int w, out int h));
            return (w, h);
        }
    }

    internal (int Width, int Height, LogicalPresentation Mode) Presentation
    {
        get
        {
            if (_handle.IsInvalid)
                return (0, 0, LogicalPresentation.Disabled);

            QuackNativeException.ThrowIfFailed(SDL_GetRenderLogicalPresentation(_handle, out int width, out int height, out LogicalPresentation mode));
            return (width, height, mode);
        }
        set
        {
            if (_handle.IsInvalid)
                return;

            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            QuackNativeException.ThrowIfFailed(SDL_SetRenderLogicalPresentation(_handle, value.Width, value.Height, value.Mode));
        }
    }

    internal Rect PresentationRectangle
    {
        get
        {
            if (_handle.IsInvalid)
                return default;

            QuackNativeException.ThrowIfFailed(SDL_GetRenderLogicalPresentationRect(_handle, out Rect rectangle));
            return rectangle;
        }
    }

    internal RectInt SafeArea
    {
        get
        {
            if (_handle.IsInvalid)
                return default;

            QuackNativeException.ThrowIfFailed(SDL_GetRenderSafeArea(_handle, out RectInt rectangle));
            return rectangle;
        }
    }

    internal int VSync
    {
        get;
        set
        {
            if (field == value)
                return;

            field = value;

            if (_handle.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(SDL_SetRenderVSync(_handle, field));
        }
    }

    public void Dispose() => _handle.Dispose();

    internal void Clear(Color color)
    {
        SDL_SetRenderDrawColor(_handle, color.R, color.G, color.B, color.A);
        SDL_RenderClear(_handle);
    }

    internal TextureHandle CreateTexture(PixelFormat format, TextureAccess access, int width, int height)
        => SDL_CreateTexture(_handle, format, access, width, height);

    internal unsafe TextureHandle CreateTextureFromSurface(Surface surface)
        => SDL_CreateTextureFromSurface(_handle, surface.Handle);

    internal TextureHandle LoadTexture(string file) => IMG_LoadTexture(_handle, file);

    internal void Draw(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices)
        => SDL_RenderGeometry(_handle, nint.Zero, vertices, vertices.Length, indices, indices.Length);

    internal void DrawDebugText(Vector2 position, string text, Color? color = null)
    {
        if (_handle.IsInvalid)
            return;

        Color textColor = color ?? Color.White;

        SDL_SetRenderDrawColor(_handle, textColor.R, textColor.G, textColor.B, textColor.A);
        QuackNativeException.ThrowIfFailed(SDL_RenderDebugText(_handle, position.X, position.Y, text));
    }

    internal Vector2 MapCoordinatesToPixels(Vector2 point)
    {
        SDL_RenderCoordinatesToWindow(_handle, point.X, point.Y, out float x, out float y);
        return new Vector2(x, y);
    }

    internal void MapEventToCoordinates(ref Event e)
        => SDL_ConvertEventToRenderCoordinates(_handle, ref e);

    internal Vector2 MapPixelsToCoordinates(Vector2 point)
    {
        SDL_RenderCoordinatesFromWindow(_handle, point.X, point.Y, out float x, out float y);
        return new Vector2(x, y);
    }

    internal void Render() => SDL_RenderPresent(_handle);
}

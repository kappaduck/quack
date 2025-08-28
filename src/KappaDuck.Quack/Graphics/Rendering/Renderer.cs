// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Interop.SDL.Handles;
using KappaDuck.Quack.Video.Displays;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Rendering;

internal sealed partial class Renderer : IDisposable
{
    private readonly RendererHandle _renderer = new();

    internal Renderer()
    {
    }

    internal Renderer(WindowHandle window, string? name)
    {
        _renderer = SDL_CreateRenderer(window, name);
        QuackNativeException.ThrowIf(_renderer.IsInvalid);

        QuackNativeException.ThrowIfFailed(SDL_SetRenderVSync(_renderer, VSync));
        QuackNativeException.ThrowIfFailed(SDL_SetRenderLogicalPresentation(_renderer, Presentation.Width, Presentation.Height, Presentation.Mode));
    }

    internal (int Width, int Height) CurrentOutputSize
    {
        get
        {
            if (_renderer.IsInvalid)
                return (0, 0);

            QuackNativeException.ThrowIfFailed(SDL_GetCurrentRenderOutputSize(_renderer, out int w, out int h));
            return (w, h);
        }
    }

    internal (int Width, int Height) OutputSize
    {
        get
        {
            if (_renderer.IsInvalid)
                return (0, 0);

            QuackNativeException.ThrowIfFailed(SDL_GetRenderOutputSize(_renderer, out int w, out int h));
            return (w, h);
        }
    }

    internal (int Width, int Height, LogicalPresentation Mode) Presentation
    {
        get
        {
            if (_renderer.IsInvalid)
                return (0, 0, LogicalPresentation.Disabled);

            QuackNativeException.ThrowIfFailed(SDL_GetRenderLogicalPresentation(_renderer, out int width, out int height, out LogicalPresentation mode));
            return (width, height, mode);
        }
        set
        {
            if (_renderer.IsInvalid)
                return;

            ArgumentOutOfRangeException.ThrowIfNegative(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegative(value.Height);

            QuackNativeException.ThrowIfFailed(SDL_SetRenderLogicalPresentation(_renderer, value.Width, value.Height, value.Mode));
        }
    }

    internal Rect PresentationRectangle
    {
        get
        {
            if (_renderer.IsInvalid)
                return default;

            QuackNativeException.ThrowIfFailed(SDL_GetRenderLogicalPresentationRect(_renderer, out Rect rectangle));
            return rectangle;
        }
    }

    internal RectInt SafeArea
    {
        get
        {
            if (_renderer.IsInvalid)
                return default;

            QuackNativeException.ThrowIfFailed(SDL_GetRenderSafeArea(_renderer, out RectInt rectangle));
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

            if (_renderer.IsInvalid)
                return;

            QuackNativeException.ThrowIfFailed(SDL_SetRenderVSync(_renderer, field));
        }
    }

    public void Dispose() => _renderer.Dispose();

    internal void Clear(Color color)
    {
        SDL_SetRenderDrawColor(_renderer, color.R, color.G, color.B, color.A);
        SDL_RenderClear(_renderer);
    }

    internal void Draw(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices)
        => SDL_RenderGeometry(_renderer, nint.Zero, vertices, vertices.Length, indices, indices.Length);

    internal Vector2 MapCoordinatesToPixels(Vector2 point)
    {
        SDL_RenderCoordinatesToWindow(_renderer, point.X, point.Y, out float x, out float y);
        return new Vector2(x, y);
    }

    internal void MapEventToCoordinates(ref Event e)
        => SDL_ConvertEventToRenderCoordinates(_renderer, ref e);

    internal Vector2 MapPixelsToCoordinates(Vector2 point)
    {
        SDL_RenderCoordinatesFromWindow(_renderer, point.X, point.Y, out float x, out float y);
        return new Vector2(x, y);
    }

    internal void Render() => SDL_RenderPresent(_renderer);
}

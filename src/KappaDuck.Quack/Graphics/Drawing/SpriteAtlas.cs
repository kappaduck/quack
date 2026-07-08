// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Video.Pixels;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A set of <see cref="Texture"/> pages divided into frames, addressed by index.
/// </summary>
/// <remarks>
/// The multi-page counterpart to <see cref="SpriteSheet"/>: where a sheet always packs frames into exactly one
/// texture and fails if they don't fit, an atlas spreads them across as many pages as needed, so animations with a
/// large frame count or large frame dimensions can still be loaded with <see cref="LoadAnimation(Renderer, string, int)"/>.
/// The trade-off is that consecutive frames can land on different pages, so drawing across a page boundary costs a
/// texture rebind. Animations that comfortably fit one texture, prefer <see cref="SpriteSheet"/>.
/// </remarks>
public sealed class SpriteAtlas : IDisposable
{
    private readonly Texture[] _pages;
    private readonly (int Page, RectI Region)[] _frames;
    private bool _disposed;

    private SpriteAtlas(List<Texture> pages, (int Page, RectI Region)[] frames)
    {
        _pages = [.. pages];
        _frames = frames;
    }

    /// <summary>
    /// Gets every texture page. Frames in <see cref="this[int]"/> reference these by index.
    /// </summary>
    public IReadOnlyList<Texture> Pages => _pages;

    /// <summary>
    /// Gets the number of frames in the atlas.
    /// </summary>
    public int Count => _frames.Length;

    /// <summary>
    /// Gets the page and source rectangle of a frame, in that page's texture pixels.
    /// </summary>
    /// <param name="index">The frame index, from 0 to <see cref="Count"/> minus one.</param>
    /// <returns>The page the frame is packed on, and its source rectangle within that page.</returns>
    public (Texture Texture, RectI Region) this[int index]
    {
        get
        {
            (int page, RectI region) = _frames[index];
            return (_pages[page], region);
        }
    }

    /// <summary>
    /// Loads an animated image (such as a GIF, APNG or animated WebP) into a sprite atlas and a matching animation.
    /// </summary>
    /// <remarks>
    /// Frames are packed into as few pages as possible: each page is filled as a grid bounded by
    /// <paramref name="maxAtlasWidth"/> before a new one is started, so a page is only ever as large as the frames
    /// it actually holds. Every returned <see cref="Pages"/> texture is created here and owned by the caller;
    /// disposing the atlas disposes every page. The returned animation uses each frame's own duration and loops.
    /// </remarks>
    /// <param name="renderer">The renderer used to create the packed textures.</param>
    /// <param name="path">The path to the animated image file.</param>
    /// <param name="maxAtlasWidth">
    /// The maximum width and height, in pixels, of each packed page. 4096 is a conservative default that fits within
    /// common GPU limits; raise it if the target hardware supports larger textures.
    /// </param>
    /// <returns>The packed atlas and an animation that plays its frames in order.</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxAtlasWidth"/> is negative or zero.</exception>
    /// <exception cref="InvalidOperationException">A single frame is larger than <paramref name="maxAtlasWidth"/> in either dimension.</exception>
    /// <exception cref="QuackInteropException">The animation could not be loaded.</exception>
    public static (SpriteAtlas Atlas, SpriteAnimation Animation) LoadAnimation(Renderer renderer, string path, int maxAtlasWidth = 4096)
    {
        using Animation animation = Animation.Load(path);
        return Pack(renderer, animation, maxAtlasWidth);
    }

    /// <summary>
    /// Loads an animated image from a stream into a sprite atlas and a matching animation.
    /// </summary>
    /// <remarks>
    /// Frames are packed into as few pages as possible: each page is filled as a grid bounded by
    /// <paramref name="maxAtlasWidth"/> before a new one is started, so a page is only ever as large as the frames
    /// it actually holds. Every returned <see cref="Pages"/> texture is created here and owned by the caller;
    /// disposing the atlas disposes every page. The returned animation uses each frame's own duration and loops.
    /// </remarks>
    /// <param name="renderer">The renderer used to create the packed textures.</param>
    /// <param name="stream">The stream to read the animated image from.</param>
    /// <param name="maxAtlasWidth">
    /// The maximum width and height, in pixels, of each packed page. 4096 is a conservative default that fits within
    /// common GPU limits; raise it if the target hardware supports larger textures.
    /// </param>
    /// <returns>The packed atlas and an animation that plays its frames in order.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxAtlasWidth"/> is negative or zero.</exception>
    /// <exception cref="InvalidOperationException">A single frame is larger than <paramref name="maxAtlasWidth"/> in either dimension.</exception>
    /// <exception cref="QuackInteropException">The animation could not be loaded.</exception>
    public static (SpriteAtlas Atlas, SpriteAnimation Animation) LoadAnimation(Renderer renderer, Stream stream, int maxAtlasWidth = 4096)
    {
        using Animation animation = Animation.Load(stream);
        return Pack(renderer, animation, maxAtlasWidth);
    }

    /// <summary>
    /// Disposes every page in the atlas.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;

        foreach (Texture page in _pages)
            page.Dispose();
    }

    private static (SpriteAtlas Atlas, SpriteAnimation Animation) Pack(Renderer renderer, Animation animation, int maxAtlasWidth)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxAtlasWidth);

        int frameWidth = animation.Width;
        int frameHeight = animation.Height;
        int count = animation.FrameCount;

        if (frameWidth > maxAtlasWidth || frameHeight > maxAtlasWidth)
        {
            ThrowHelper.ThrowInvalidOperation($"This animation's frames are {frameWidth}x{frameHeight}, which alone exceeds the {maxAtlasWidth} maximum atlas dimension. Raise maxAtlasWidth if the target hardware supports a larger texture.");
        }

        int columns = Math.Max(1, maxAtlasWidth / frameWidth);
        int rowsPerPage = Math.Max(1, maxAtlasWidth / frameHeight);
        int framesPerPage = columns * rowsPerPage;

        List<Texture> pages = [];
        (int Page, RectI Region)[] frames = new (int, RectI)[count];
        TimeSpan[] durations = new TimeSpan[count];

        int frameIndex = 0;

        try
        {
            while (frameIndex < count)
            {
                int framesOnPage = Math.Min(framesPerPage, count - frameIndex);
                int rows = (framesOnPage + columns - 1) / columns;

                using Surface page = new(columns * frameWidth, rows * frameHeight, animation.Frames[0].Format);

                for (int i = 0; i < framesOnPage; i++)
                {
                    RectI region = new(i % columns * frameWidth, i / columns * frameHeight, frameWidth, frameHeight);

                    page.Blit(animation.Frames[frameIndex], destination: region);

                    frames[frameIndex] = (pages.Count, region);
                    durations[frameIndex] = animation.Delays[frameIndex];

                    frameIndex++;
                }

                pages.Add(Texture.FromSurface(renderer, page));
            }
        }
        catch
        {
            foreach (Texture page in pages)
                page.Dispose();

            throw;
        }

        SpriteAtlas atlas = new(pages, frames);

        int[] indexes = new int[count];
        for (int i = 0; i < count; i++)
            indexes[i] = i;

        return (atlas, new SpriteAnimation(indexes, durations));
    }
}

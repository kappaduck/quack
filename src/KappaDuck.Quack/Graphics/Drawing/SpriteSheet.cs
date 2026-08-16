// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Video.Imaging;
using KappaDuck.Quack.Video.Imaging.Animations;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A <see cref="Rendering.Texture"/> divided into a set of frames, addressed by index.
/// </summary>
/// <remarks>
/// Build one from a uniform grid (the common case for character and tile sheets), from an explicit list of rectangles
/// for irregular sheets, or from an animated image with <see cref="LoadAnimation(Renderer, string, int)"/>. It is a
/// lightweight view onto the texture; it does not own or dispose it, except a sheet produced by
/// <see cref="LoadAnimation(Renderer, string, int)"/>, whose texture the caller owns.
/// </remarks>
public sealed class SpriteSheet
{
    private readonly RectI[] _frames;

    /// <summary>
    /// Creates a sprite sheet from a uniform grid of frames.
    /// </summary>
    /// <remarks>Frames are numbered left to right, then top to bottom. Any partial frame at the right or bottom edge is ignored.</remarks>
    /// <param name="texture">The texture to slice.</param>
    /// <param name="frameWidth">The width of each frame, in texture pixels.</param>
    /// <param name="frameHeight">The height of each frame, in texture pixels.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="frameWidth"/> or <paramref name="frameHeight"/> is negative or zero.</exception>
    public SpriteSheet(Texture texture, int frameWidth, int frameHeight)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(frameWidth);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(frameHeight);

        Texture = texture;

        int columns = texture.Width / frameWidth;
        int rows = texture.Height / frameHeight;

        _frames = new RectI[columns * rows];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
                _frames[(row * columns) + column] = new RectI(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
        }
    }

    /// <summary>
    /// Creates a sprite sheet from an explicit list of frame rectangles.
    /// </summary>
    /// <param name="texture">The texture to slice.</param>
    /// <param name="frames">The frame rectangles, in texture pixels.</param>
    public SpriteSheet(Texture texture, params ReadOnlySpan<RectI> frames)
    {
        Texture = texture;
        _frames = frames.ToArray();
    }

    /// <summary>
    /// Gets the texture the frames are taken from.
    /// </summary>
    public Texture Texture { get; }

    /// <summary>
    /// Gets the number of frames in the sheet.
    /// </summary>
    public int Count => _frames.Length;

    /// <summary>
    /// Gets the source rectangle of a frame, in texture pixels.
    /// </summary>
    /// <param name="index">The frame index, from 0 to <see cref="Count"/> minus one.</param>
    /// <returns>The frame's source rectangle.</returns>
    public RectI this[int index] => _frames[index];

    /// <summary>
    /// Loads an animated image (such as a GIF, APNG or animated WebP) into a sprite sheet and a matching animation.
    /// </summary>
    /// <remarks>
    /// Every frame is packed into a single texture — one texture bind for every sprite that plays this clip, instead
    /// of one per frame. Frames are arranged in a grid bounded by <paramref name="maxAtlasWidth"/> rather than a single
    /// row, so long or wide animations don't produce a texture wider than the GPU can allocate. The returned sheet's
    /// <see cref="Texture"/> is created here and is owned by the caller; dispose it when finished. The returned
    /// animation uses each frame's own duration and loops.
    /// </remarks>
    /// <param name="renderer">The renderer used to create the packed texture.</param>
    /// <param name="path">The path to the animated image file.</param>
    /// <param name="maxAtlasWidth">
    /// The maximum width, in pixels, of the packed texture. 4096 is a conservative default that fits within common
    /// GPU limits; raise it if the target hardware supports wider textures.
    /// </param>
    /// <returns>The packed sheet and an animation that plays its frames in order.</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxAtlasWidth"/> is negative or zero.</exception>
    /// <exception cref="InvalidOperationException">The animation's frames don't fit in a single texture within <paramref name="maxAtlasWidth"/>.</exception>
    /// <exception cref="QuackInteropException">The animation could not be loaded.</exception>
    public static (SpriteSheet Sheet, SpriteAnimation Animation) LoadAnimation(Renderer renderer, string path, int maxAtlasWidth = 4096)
    {
        using Animation animation = Animation.Load(path);
        return Pack(renderer, animation, maxAtlasWidth);
    }

    /// <summary>
    /// Loads an animated image from a stream into a sprite sheet and a matching animation.
    /// </summary>
    /// <remarks>
    /// Every frame is packed into a single texture — one texture bind for every sprite that plays this clip, instead
    /// of one per frame. Frames are arranged in a grid bounded by <paramref name="maxAtlasWidth"/> rather than a single
    /// row, so long or wide animations don't produce a texture wider than the GPU can allocate. The returned sheet's
    /// <see cref="Texture"/> is created here and is owned by the caller; dispose it when finished. The returned
    /// animation uses each frame's own duration and loops.
    /// </remarks>
    /// <param name="renderer">The renderer used to create the packed texture.</param>
    /// <param name="stream">The stream to read the animated image from.</param>
    /// <param name="maxAtlasWidth">
    /// The maximum width, in pixels, of the packed texture. 4096 is a conservative default that fits within common
    /// GPU limits; raise it if the target hardware supports wider textures.
    /// </param>
    /// <returns>The packed sheet and an animation that plays its frames in order.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxAtlasWidth"/> is negative or zero.</exception>
    /// <exception cref="InvalidOperationException">The animation's frames don't fit in a single texture within <paramref name="maxAtlasWidth"/>.</exception>
    /// <exception cref="QuackInteropException">The animation could not be loaded.</exception>
    public static (SpriteSheet Sheet, SpriteAnimation Animation) LoadAnimation(Renderer renderer, Stream stream, int maxAtlasWidth = 4096)
    {
        using Animation animation = Animation.Load(stream);
        return Pack(renderer, animation, maxAtlasWidth);
    }

    private static (SpriteSheet Sheet, SpriteAnimation Animation) Pack(Renderer renderer, Animation animation, int maxAtlasWidth)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxAtlasWidth);

        int frameWidth = animation.Width;
        int frameHeight = animation.Height;
        int count = animation.FrameCount;

        int columns = Math.Clamp(maxAtlasWidth / frameWidth, 1, count);
        int rows = (count + columns - 1) / columns;

        int atlasWidth = columns * frameWidth;
        int atlasHeight = rows * frameHeight;

        if (atlasWidth > maxAtlasWidth || atlasHeight > maxAtlasWidth)
            ThrowHelper.ThrowInvalidOperation($"This animation has {count} frames of {frameWidth}x{frameHeight}; packed as a grid it needs a {atlasWidth}x{atlasHeight} texture, which exceeds the {maxAtlasWidth} maximum atlas dimension. Raise maxAtlasWidth if the target hardware supports a larger texture, or decode this animation frame-by-frame with AnimationDecoder instead of using the atlas.");

        using Surface atlas = new(atlasWidth, atlasHeight, animation.Frames[0].Format);

        RectI[] regions = new RectI[count];
        TimeSpan[] durations = new TimeSpan[count];

        for (int i = 0; i < count; i++)
        {
            RectI region = new(i % columns * frameWidth, i / columns * frameHeight, frameWidth, frameHeight);

            atlas.Blit(animation.Frames[i], destination: region);

            regions[i] = region;
            durations[i] = animation.Delays[i];
        }

        Texture texture = Texture.FromSurface(renderer, atlas);
        SpriteSheet sheet = new(texture, regions);

        int[] frames = new int[count];
        for (int i = 0; i < count; i++)
            frames[i] = i;

        return (sheet, new SpriteAnimation(frames, durations));
    }
}

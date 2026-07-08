// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Rendering;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A <see cref="Sprite"/> that plays a <see cref="SpriteAnimation"/> from a <see cref="SpriteSheet"/>, cycling its
/// displayed frame over time.
/// </summary>
/// <remarks>
/// Choose a clip with <see cref="Play(SpriteAnimation)"/>, advance it with <see cref="Update(TimeSpan)"/>
/// once per frame, then draw it like any drawable with <see cref="IRenderTarget.Draw(IDrawable)"/>. Flip the sprite
/// with a negative <see cref="Transformable.Scale"/>. <see cref="Play(SpriteAnimation)"/> is idempotent, so calling it
/// every frame while a key is held keeps the animation running rather than restarting it.
/// </remarks>
/// <remarks>
/// Creates an animated sprite that draws frames from the given sheet.
/// </remarks>
/// <param name="sheet">The sheet the animation frames are taken from.</param>
public sealed class AnimatedSprite(SpriteSheet sheet)
    : Sprite(sheet.Texture, sheet.Count > 0 ? sheet[0] : new RectI(0, 0, sheet.Texture.Width, sheet.Texture.Height))
{
    private TimeSpan _elapsed;
    private int _frameIndex;

    /// <summary>
    /// Gets or sets the sheet the animation frames are taken from.
    /// </summary>
    public SpriteSheet Sheet
    {
        get;
        set
        {
            field = value;
            Texture = value.Texture;

            UpdateRegion();
        }
    } = sheet;

    /// <summary>
    /// Gets the animation currently playing, or <see langword="null"/> if none has been played yet.
    /// </summary>
    public SpriteAnimation? Animation { get; private set; }

    /// <summary>
    /// Gets a value indicating whether an animation is currently playing.
    /// </summary>
    public bool IsPlaying { get; private set; }

    /// <summary>
    /// Plays an animation from its first frame, or does nothing if it is already the animation playing.
    /// </summary>
    /// <param name="animation">The animation to play.</param>
    public void Play(SpriteAnimation animation)
    {
        if (ReferenceEquals(Animation, animation) && IsPlaying)
            return;

        Animation = animation;
        _elapsed = TimeSpan.Zero;
        _frameIndex = 0;
        IsPlaying = true;

        UpdateRegion();
    }

    /// <summary>
    /// Stops the animation and rewinds it to its first frame.
    /// </summary>
    public void Stop()
    {
        IsPlaying = false;
        _elapsed = TimeSpan.Zero;
        _frameIndex = 0;

        UpdateRegion();
    }

    /// <summary>
    /// Pauses the animation on its current frame.
    /// </summary>
    public void Pause() => IsPlaying = false;

    /// <summary>
    /// Resumes a paused animation from its current frame.
    /// </summary>
    public void Resume() => IsPlaying = Animation is not null;

    /// <summary>
    /// Advances the current animation by the elapsed time.
    /// </summary>
    /// <param name="deltaTime">The time since the last update, such as the frame delta.</param>
    public void Update(TimeSpan deltaTime)
    {
        if (!IsPlaying || Animation is null)
            return;

        _elapsed += deltaTime;

        while (_elapsed >= Animation.DurationAt(_frameIndex))
        {
            _elapsed -= Animation.DurationAt(_frameIndex);
            _frameIndex++;

            if (_frameIndex < Animation.FrameCount)
                continue;

            if (Animation.Loop)
            {
                _frameIndex = 0;
                continue;
            }

            _frameIndex = Animation.FrameCount - 1;
            IsPlaying = false;
            break;
        }

        UpdateRegion();
    }

    private void UpdateRegion()
    {
        if (Animation is not null)
            Region = Sheet[Animation.FrameAt(_frameIndex)];
    }
}

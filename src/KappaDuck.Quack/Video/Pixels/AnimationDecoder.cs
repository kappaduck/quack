// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// Decodes an animated image (such as a GIF, WebP, APNG, or AVIF) one frame at a time.
/// </summary>
/// <remarks>
/// Frames are produced on demand, so only one is decoded into memory at a time — unlike loading a whole animation up
/// front with <see cref="Animation"/>. Each frame is an independent <see cref="Surface"/> that the caller owns and
/// must dispose. Read frames with <see cref="TryReadFrame"/> (which reports how long each frame is shown) or
/// <see cref="TryReadNextFrame"/> (which reports the timestamp to show each frame at), and use <see cref="Reset"/>
/// to loop back to the first frame.
/// </remarks>
public sealed class AnimationDecoder : IDisposable
{
    private IMG_AnimationDecoder* _decoder;
    private IOStream? _stream;

    private unsafe AnimationDecoder(IMG_AnimationDecoder* decoder, IOStream? stream)
    {
        _decoder = decoder;
        _stream = stream;

        uint properties = SDL3_image.GetAnimationDecoderProperties(decoder);

        FrameCount = (int)SDL3.GetNumberProperty(properties, "SDL_image.metadata.frame_count", 0);
        LoopCount = (int)SDL3.GetNumberProperty(properties, "SDL_image.metadata.loop_count", 0);

        Title = SDL3.GetStringProperty(properties, "SDL_image.metadata.title", string.Empty);
        Author = SDL3.GetStringProperty(properties, "SDL_image.metadata.author", string.Empty);
        Description = SDL3.GetStringProperty(properties, "SDL_image.metadata.description", string.Empty);
        Copyright = SDL3.GetStringProperty(properties, "SDL_image.metadata.copyright", string.Empty);
        CreationTime = SDL3.GetStringProperty(properties, "SDL_image.metadata.creation_time", string.Empty);
    }

    /// <summary>
    /// Opens an animated image file for decoding. The format is detected from the file contents.
    /// </summary>
    /// <param name="path">The path to the animated image file.</param>
    /// <returns>A decoder positioned before the first frame.</returns>
    /// <exception cref="QuackInteropException">The file could not be opened for decoding.</exception>
    public static AnimationDecoder Open(string path)
    {
        unsafe
        {
            IMG_AnimationDecoder* decoder = SDL3_image.CreateAnimationDecoder(path);
            SDLThrowHelper.ThrowIfNull(decoder);

            return new AnimationDecoder(decoder, null);
        }
    }

    /// <summary>
    /// Opens an animated image from a stream for decoding.
    /// </summary>
    /// <remarks>The stream is read on demand as frames are decoded, so it must stay readable until the decoder is disposed.</remarks>
    /// <param name="stream">The stream to read the animated image from.</param>
    /// <param name="format">The animation format to decode as.</param>
    /// <returns>A decoder positioned before the first frame.</returns>
    /// <exception cref="QuackInteropException">The stream could not be opened for decoding.</exception>
    public static AnimationDecoder Open(Stream stream, AnimationFormat format)
    {
        IOStream source = IOStream.FromStream(stream);

        unsafe
        {
            IMG_AnimationDecoder* decoder;
            try
            {
                decoder = SDL3_image.CreateAnimationDecoder(source.Handle, false, format.Type);
                SDLThrowHelper.ThrowIfNull(decoder);
            }
            catch
            {
                source.Dispose();
                throw;
            }

            return new AnimationDecoder(decoder, source);
        }
    }

    /// <summary>
    /// Gets the current state of the decoder.
    /// </summary>
    public AnimationDecoderStatus Status => unsafe (SDL3_image.GetAnimationDecoderStatus(_decoder));

    /// <summary>
    /// Gets the number of frames in the animation, or 0 if the format does not report it.
    /// </summary>
    public int FrameCount { get; }

    /// <summary>
    /// Gets the number of times the animation is intended to play, or 0 to loop forever.
    /// </summary>
    public int LoopCount { get; }

    /// <summary>
    /// Gets the animation's title, or an empty string if the format does not report one.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the animation's author, or an empty string if the format does not report one.
    /// </summary>
    public string Author { get; }

    /// <summary>
    /// Gets the animation's description, or an empty string if the format does not report one.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the animation's copyright notice, or an empty string if the format does not report one.
    /// </summary>
    public string Copyright { get; }

    /// <summary>
    /// Gets the animation's creation time, or an empty string if the format does not report one.
    /// </summary>
    public string CreationTime { get; }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (unsafe (_decoder is null))
            return;

        unsafe
        {
            SDL3_image.CloseAnimationDecoder(_decoder);
            _decoder = null;
        }

        _stream?.Dispose();
        _stream = null;
    }

    /// <summary>
    /// Rewinds the decoder to the first frame.
    /// </summary>
    /// <exception cref="QuackInteropException">The decoder could not be reset.</exception>
    public void Reset() => SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.ResetAnimationDecoder(_decoder)));

    /// <summary>
    /// Reads the next frame of the animation.
    /// </summary>
    /// <param name="frame">
    /// When this method returns <see langword="true"/>, the decoded frame; the caller owns it and must dispose it.
    /// Otherwise <see langword="null"/>.
    /// </param>
    /// <param name="duration">
    /// When this method returns <see langword="true"/>, how long the frame is shown before the next one. Otherwise
    /// <see cref="TimeSpan.Zero"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if a frame was decoded; <see langword="false"/> once the animation is complete.
    /// </returns>
    /// <exception cref="QuackInteropException">Decoding failed.</exception>
    public bool TryReadFrame([NotNullWhen(true)] out Surface? frame, out TimeSpan duration)
    {
        SDL_Surface* surface;
        ulong milliseconds;

        unsafe
        {
            if (SDL3_image.GetAnimationDecoderFrame(_decoder, &surface, &milliseconds))
            {
                frame = new Surface(surface, true);
                duration = TimeSpan.FromMilliseconds(milliseconds);

                return true;
            }
        }

        frame = null;
        duration = TimeSpan.Zero;

        SDLThrowHelper.ThrowIf(Status == AnimationDecoderStatus.Failed);
        return false;
    }

    /// <summary>
    /// Reads the next frame of the animation along with the time it should be shown at.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="TryReadFrame"/>, which reports how long each frame is shown, this reports the timestamp at
    /// which the frame should be presented, measured from the start of the animation — convenient for synchronising
    /// playback to a clock.
    /// </remarks>
    /// <param name="frame">
    /// When this method returns <see langword="true"/>, the decoded frame; the caller owns it and must dispose it.
    /// Otherwise <see langword="null"/>.
    /// </param>
    /// <param name="presentationTimestamp">
    /// When this method returns <see langword="true"/>, the time from the start of the animation at which the frame
    /// should be shown. Otherwise <see cref="TimeSpan.Zero"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if a frame was decoded; <see langword="false"/> once the animation is complete.
    /// </returns>
    /// <exception cref="QuackInteropException">Decoding failed.</exception>
    public bool TryReadNextFrame([NotNullWhen(true)] out Surface? frame, out TimeSpan presentationTimestamp)
    {
        SDL_Surface* surface;
        long pts;

        unsafe
        {
            if (SDL3_image.GetNextAnimationDecoderFrame(_decoder, &surface, &pts))
            {
                frame = new Surface(surface, owned: true);

                int milliseconds = SDL3_image.GetAnimationDecoderPresentationTimestampMS(_decoder, pts);
                presentationTimestamp = TimeSpan.FromMilliseconds(milliseconds);

                return true;
            }
        }

        frame = null;
        presentationTimestamp = TimeSpan.Zero;

        SDLThrowHelper.ThrowIf(Status == AnimationDecoderStatus.Failed);
        return false;
    }
}

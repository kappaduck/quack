// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// Encodes frames into an animated image (such as a GIF, APNG, WebP, or AVIF sequence) one frame at a time.
/// </summary>
/// <remarks>
/// Frames are written as they are added, so memory use stays proportional to a single frame rather than the whole
/// animation — the inverse of <see cref="Animation"/>, which holds every frame in memory at once. Add every frame
/// with <see cref="AddFrame"/>, then dispose the encoder to finish writing.
/// </remarks>
public sealed class AnimationEncoder : IDisposable
{
    private IMG_AnimationEncoder* _encoder;
    private IOStream? _stream;

    private AnimationEncoder(IMG_AnimationEncoder* encoder, IOStream? stream)
    {
        unsafe
        {
            _encoder = encoder;
        }

        _stream = stream;
    }

    /// <summary>
    /// Adds the next frame to the animation.
    /// </summary>
    /// <param name="frame">The frame to encode.</param>
    /// <param name="duration">How long the frame is shown before the next one.</param>
    /// <exception cref="QuackInteropException">The frame could not be encoded.</exception>
    /// <exception cref="ObjectDisposedException">The encoder is disposed.</exception>
    public void AddFrame(Surface frame, TimeSpan duration)
    {
        unsafe
        {
            ObjectDisposedException.ThrowIf(_encoder is null, typeof(AnimationEncoder));
            SDLThrowHelper.ThrowIfFailed(SDL3_image.AddAnimationEncoderFrame(_encoder, frame.Handle, (ulong)duration.TotalMilliseconds));
        }
    }

    /// <summary>
    /// Opens an encoder that writes to a file. The format is chosen from the file extension.
    /// </summary>
    /// <param name="path">The path to write the animated image to.</param>
    /// <param name="options">Additional encoding options, or <see langword="null"/> for the format's defaults.</param>
    /// <returns>An encoder ready to accept frames.</returns>
    /// <exception cref="QuackInteropException">The file could not be opened for encoding.</exception>
    public static AnimationEncoder Open(string path, AnimationEncoderOptions? options = null)
    {
        using Properties properties = BuildProperties(options);
        properties.Set("SDL_image.animation_encoder.create.filename", path);

        unsafe
        {
            IMG_AnimationEncoder* encoder = SDL3_image.CreateAnimationEncoderWithProperties(properties);
            SDLThrowHelper.ThrowIfNull(encoder);

            return new AnimationEncoder(encoder, null);
        }
    }

    /// <summary>
    /// Opens an encoder that writes to a stream.
    /// </summary>
    /// <param name="stream">The stream to write the animated image to.</param>
    /// <param name="format">The animation format to encode as.</param>
    /// <param name="options">Additional encoding options, or <see langword="null"/> for the format's defaults.</param>
    /// <returns>An encoder ready to accept frames.</returns>
    /// <exception cref="QuackInteropException">The stream could not be opened for encoding.</exception>
    public static AnimationEncoder Open(Stream stream, AnimationFormat format, AnimationEncoderOptions? options = null)
    {
        IOStream destination = IOStream.FromStream(stream);

        using Properties properties = BuildProperties(options);

        unsafe
        {
            properties.Set("SDL_image.animation_encoder.create.iostream", destination.Handle);
            properties.Set("SDL_image.animation_encoder.create.type", format.Type);

            IMG_AnimationEncoder* encoder;
            try
            {
                encoder = SDL3_image.CreateAnimationEncoderWithProperties(properties);
                SDLThrowHelper.ThrowIfNull(encoder);
            }
            catch
            {
                destination.Dispose();
                throw;
            }

            return new(encoder, destination);
        }
    }

    /// <summary>
    /// Finishes encoding and releases the encoder's resources.
    /// </summary>
    /// <exception cref="QuackInteropException">The animation could not be finalized.</exception>
    public void Dispose()
    {
        unsafe
        {
            if (_encoder is null)
                return;

            SDLThrowHelper.ThrowIfFailed(SDL3_image.CloseAnimationEncoder(_encoder));
            _encoder = null;
        }

        _stream?.Dispose();
        _stream = null;
    }

    private static Properties BuildProperties(AnimationEncoderOptions? options)
    {
        Properties properties = new();

        if (options is null)
            return properties;

        if (options.Quality.HasValue)
            properties.Set("SDL_image.animation_encoder.create.quality", options.Quality.Value);

        if (options.TimebaseNumerator.HasValue)
            properties.Set("SDL_image.animation_encoder.create.timebase.numerator", options.TimebaseNumerator.Value);

        if (options.TimebaseDenominator.HasValue)
            properties.Set("SDL_image.animation_encoder.create.timebase.denominator", options.TimebaseDenominator.Value);

        if (options.GifUseLookupTable.HasValue)
            properties.Set("SDL_image.animation_encoder.create.gif.use_lut", options.GifUseLookupTable.Value);

        if (options.AvifKeyframeInterval.HasValue)
            properties.Set("SDL_image.animation_encoder.create.avif.keyframe_interval", options.AvifKeyframeInterval.HasValue);

        if (options.AvifMaxThreads.HasValue)
            properties.Set("SDL_image.animation_encoder.create.avif.max_threads", options.AvifMaxThreads.Value);

        return properties;
    }
}

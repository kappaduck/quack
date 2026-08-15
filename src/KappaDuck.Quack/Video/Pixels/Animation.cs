// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using CommunityToolkit.HighPerformance;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// A fully decoded animated image (such as a GIF, APNG, animated WebP, AVIF sequence, or Windows ANI cursor),
/// exposing every frame and its display duration.
/// </summary>
/// <remarks>
/// Unlike <see cref="AnimationDecoder"/>, which decodes one frame at a time, an <see cref="Animation"/> holds every
/// frame in memory at once. Prefer it for short clips and cursors; prefer <see cref="AnimationDecoder"/> for long or
/// unbounded animations. Frames are owned by the animation and become invalid once it is disposed; disposing an
/// individual frame from <see cref="Frames"/> does nothing.
/// </remarks>
public sealed class Animation : IDisposable
{
    private Animation(IMG_Animation* handle, Surface[] frames, TimeSpan[] delays, int loopCount)
    {
        Frames = frames;
        Delays = delays;
        LoopCount = loopCount;

        unsafe
        {
            Handle = handle;
            Width = Handle->Width;
            Height = Handle->Height;
        }
    }

    /// <summary>
    /// Gets the width of every frame, in pixels.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of every frame, in pixels.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the number of frames.
    /// </summary>
    public int FrameCount { get; }

    /// <summary>
    /// Gets every frame, in playback order.
    /// </summary>
    /// <remarks>Frames are owned by the animation; disposing one individually does nothing, and they become invalid once the animation is disposed.</remarks>
    public IReadOnlyList<Surface> Frames { get; }

    /// <summary>
    /// Gets how long each corresponding frame in <see cref="Frames"/> is shown.
    /// </summary>
    public IReadOnlyList<TimeSpan> Delays { get; }

    /// <summary>
    /// Gets the number of times the animation is intended to play, or 0 to loop forever.
    /// </summary>
    public int LoopCount { get; }

    internal IMG_Animation* Handle { get; private set; }

    /// <inheritdoc/>
    public void Dispose()
    {
        unsafe
        {
            if (Handle is null)
                return;

            SDL3_image.FreeAnimation(Handle);
            Handle = null;
        }
    }

    /// <summary>
    /// Loads every frame of an animated image file into memory.
    /// </summary>
    /// <param name="path">The path to the animated image file.</param>
    /// <returns>The loaded animation.</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="QuackInteropException">The animation could not be loaded.</exception>
    public static Animation Load(string path)
    {
        if (!File.Exists(path))
            ThrowHelper.ThrowFileNotFound("The file path does not exist.", path);

        unsafe
        {
            IMG_Animation* handle = SDL3_image.LoadAnimation(path);
            SDLThrowHelper.ThrowIfNull(handle);

            return Create(handle);
        }
    }

    /// <summary>
    /// Loads every frame of an animated image from a stream into memory.
    /// </summary>
    /// <param name="stream">The stream to read the animated image from.</param>
    /// <returns>The loaded animation.</returns>
    /// <exception cref="QuackInteropException">The animation could not be loaded.</exception>
    public static Animation Load(Stream stream)
    {
        using IOStream source = IOStream.FromStream(stream);

        unsafe
        {
            IMG_Animation* handle = SDL3_image.LoadAnimation(source.Handle, false);
            SDLThrowHelper.ThrowIfNull(handle);

            return Create(handle);
        }
    }

    /// <summary>
    /// Loads every frame of an animated image from raw file bytes into memory.
    /// </summary>
    /// <param name="bytes">The raw bytes of the animated image file.</param>
    /// <returns>The loaded animation.</returns>
    /// <exception cref="QuackInteropException">The animation could not be loaded.</exception>
    public static Animation Load(ReadOnlyMemory<byte> bytes) => Load(bytes.AsStream());

    /// <summary>
    /// Saves the animation to a file. The format is chosen from the file extension.
    /// </summary>
    /// <param name="path">The path to write the animated image to.</param>
    /// <exception cref="QuackInteropException">The animation could not be saved.</exception>
    /// <exception cref="ObjectDisposedException">The animation is disposed.</exception>
    public void Save(string path)
    {
        ThrowIfDisposed();
        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.SaveAnimation(Handle, path)));
    }

    /// <summary>
    /// Saves the animation to a stream, in the given format.
    /// </summary>
    /// <param name="stream">The stream to write the animated image to.</param>
    /// <param name="format">The animation format to save as.</param>
    /// <exception cref="QuackInteropException">The animation could not be saved.</exception>
    /// <exception cref="ObjectDisposedException">The animation is disposed.</exception>
    public void Save(Stream stream, AnimationFormat format)
    {
        ThrowIfDisposed();

        using IOStream destination = IOStream.FromStream(stream);
        SDLThrowHelper.ThrowIfFailed(unsafe (SDL3_image.SaveAnimation(Handle, destination.Handle, false, format.Type)));
    }

    private static unsafe Animation Create(IMG_Animation* handle)
    {
        int count = handle->Count;

        Surface[] frames = new Surface[count];
        TimeSpan[] delays = new TimeSpan[count];

        for (int i = 0; i < count; i++)
        {
            frames[i] = new Surface(handle->Frames[i], false);
            delays[i] = TimeSpan.FromMilliseconds(handle->Delays[i]);
        }

        int loopCount = (int)SDL3.GetNumberProperty(SDL3.GetSurfaceProperties(handle->Frames[0]), "SDL_image.metadata.loop_count", 0);
        return new Animation(handle, frames, delays, loopCount);
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(unsafe (Handle is null), typeof(Animation));
}

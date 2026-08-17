// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Imaging;
using KappaDuck.Quack.Video.Imaging.Animations;

namespace KappaDuck.Quack.Input;

/// <summary>
/// Represents a mouse cursor.
/// </summary>
public sealed class Cursor : IDisposable
{
    private readonly bool _owned;

    /// <summary>
    /// Creates a cursor with a system cursor type.
    /// </summary>
    /// <param name="type">The system cursor type.</param>
    public Cursor(CursorType type)
    {
        unsafe
        {
            InitializeCursor(SDL3.CreateSystemCursor(type));
            _owned = true;
        }
    }

    /// <summary>
    /// Creates a cursor with the image
    /// </summary>
    /// <remarks>
    /// If the surface contains alternate images added with <see cref="Surface.AddAlternateImage(Surface)"/>,
    /// the surface will be interpreted as the content to be used for 100% display scale.
    /// For example, if the original surface is 32x32, then on a 200% display scale on Windows,
    /// a 64x64 version of the image will be used, if available.
    /// If a matching version of the image isn't available, the closest larger size image will be downscaled
    /// to the appropriate size and be used instead, if available. Otherwise, the closest smaller image will be upscaled and be used instead.
    /// </remarks>
    /// <param name="surface">The image to use</param>
    /// <param name="hotspot">The cursor hotspot</param>
    public Cursor(Surface surface, PointI hotspot)
    {
        unsafe
        {
            InitializeCursor(SDL3.CreateColorCursor(surface.Handle, hotspot.X, hotspot.Y));
            _owned = true;
        }
    }

    /// <summary>
    /// Creates a monochrome (black and white) cursor from bitmap data and a mask.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <paramref name="data"/> and <paramref name="mask"/> are packed one bit per pixel, eight pixels per byte, most
    /// significant bit first. For each bit, in order: a data bit of 1 with a mask bit of 1 draws black, a data bit of
    /// 0 with a mask bit of 1 draws white, and a mask bit of 0 leaves the pixel transparent regardless of the data bit.
    /// </para>
    /// <para>For a full color cursor, use <see cref="Cursor(Surface, PointI)"/> instead.</para>
    /// </remarks>
    /// <param name="data">The bitmap data, packed one bit per pixel, most significant bit first.</param>
    /// <param name="mask">The bitmap mask, packed one bit per pixel, most significant bit first.</param>
    /// <param name="size">The cursor size in pixels. <see cref="Size.Width"/> must be a multiple of 8.</param>
    /// <param name="hotspot">The cursor hotspot.</param>
    /// <exception cref="ArgumentOutOfRangeException"><see cref="Size.Width"/> or <see cref="Size.Height"/> is negative or zero.</exception>
    /// <exception cref="ArgumentException">
    /// <see cref="Size.Width"/> is not a multiple of 8, or <paramref name="data"/> or <paramref name="mask"/> is not
    /// exactly <see cref="Size.Width"/> / 8 * <see cref="Size.Height"/> bytes long.
    /// </exception>
    /// <exception cref="QuackInteropException">Failed to create the cursor.</exception>
    public Cursor(ReadOnlySpan<byte> data, ReadOnlySpan<byte> mask, Size size, PointI hotspot) : this(data, mask, size.Width, size.Height, hotspot)
    {
    }

    /// <summary>
    /// Creates a monochrome (black and white) cursor from bitmap data and a mask.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <paramref name="data"/> and <paramref name="mask"/> are packed one bit per pixel, eight pixels per byte, most
    /// significant bit first. For each bit, in order: a data bit of 1 with a mask bit of 1 draws black, a data bit of
    /// 0 with a mask bit of 1 draws white, and a mask bit of 0 leaves the pixel transparent regardless of the data bit.
    /// </para>
    /// <para>For a full color cursor, use <see cref="Cursor(Surface, PointI)"/> instead.</para>
    /// </remarks>
    /// <param name="data">The bitmap data, packed one bit per pixel, most significant bit first.</param>
    /// <param name="mask">The bitmap mask, packed one bit per pixel, most significant bit first.</param>
    /// <param name="width">The cursor width in pixels. Must be a multiple of 8.</param>
    /// <param name="height">The cursor height in pixels.</param>
    /// <param name="hotspot">The cursor hotspot.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="width"/> or <paramref name="height"/> is negative or zero.</exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="width"/> is not a multiple of 8, or <paramref name="data"/> or <paramref name="mask"/> is not
    /// exactly <paramref name="width"/> / 8 * <paramref name="height"/> bytes long.
    /// </exception>
    /// <exception cref="QuackInteropException">Failed to create the cursor.</exception>
    public Cursor(ReadOnlySpan<byte> data, ReadOnlySpan<byte> mask, int width, int height, PointI hotspot)
    {
        unsafe
        {
            InitializeCursor(CreateMonochrome(data, mask, width, height, hotspot));
            _owned = true;
        }
    }

    /// <summary>
    /// Creates an animated cursor that cycles through a sequence of color frames.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The hotspot applies to every frame, and all frames must share the same dimensions. Alternatively, set
    /// <see cref="Surface.Hotspot"/> on the first frame's image to override <paramref name="hotspot"/>.
    /// </para>
    /// <para>
    /// If a frame's image contains alternate images added with <see cref="Surface.AddAlternateImage(Surface)"/>,
    /// they are interpreted the same way as with <see cref="Cursor(Surface, PointI)"/>.
    /// </para>
    /// <para>To create a one-shot animation that stops on the last frame, set that frame's duration to <see cref="TimeSpan.Zero"/>.</para>
    /// </remarks>
    /// <param name="frames">The sequence of frames to animate, in order.</param>
    /// <param name="hotspot">The cursor hotspot, shared by every frame.</param>
    /// <exception cref="ArgumentException"><paramref name="frames"/> is empty.</exception>
    /// <exception cref="QuackInteropException">Failed to create the cursor.</exception>
    public Cursor(ReadOnlySpan<CursorFrame> frames, PointI hotspot)
    {
        unsafe
        {
            InitializeCursor(CreateAnimated(frames, hotspot));
            _owned = true;
        }
    }

    /// <summary>
    /// Creates an animated cursor from an already loaded or built animation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The hotspot applies to every frame, and every frame in <paramref name="animation"/> shares the same dimensions.
    /// </para>
    /// <para>
    /// Useful when the frames come from a file like a GIF, APNG, or Windows .ani cursor loaded with <see cref="Animation.Load(string)"/>.
    /// For building one from scratch out of individual frames, <see cref="Cursor(ReadOnlySpan{CursorFrame}, PointI)"/> is more direct.
    /// </para>
    /// </remarks>
    /// <param name="animation">The animation to cycle through.</param>
    /// <param name="hotspot">The cursor hotspot, shared by every frame.</param>
    /// <exception cref="QuackInteropException">Failed to create the cursor.</exception>
    public Cursor(Animation animation, PointI hotspot)
    {
        unsafe
        {
            InitializeCursor(SDL3_image.CreateAnimatedCursor(animation.Handle, hotspot.X, hotspot.Y));
            _owned = true;
        }
    }

    private unsafe Cursor(SDL_Cursor* handle, bool owned)
    {
        InitializeCursor(handle);
        _owned = owned;
    }

    /// <summary>
    /// Gets the active cursor.
    /// </summary>
    public static Cursor Current => unsafe (new(SDL3.GetCursor(), false));

    /// <summary>
    /// Gets the default cursor.
    /// </summary>
    public static Cursor Default { get; } = unsafe (new(SDL3.GetDefaultCursor(), false));

    /// <summary>
    /// Gets or sets a value indicating whether the active cursor is visible.
    /// </summary>
    /// <exception cref="QuackInteropException">Thrown when failed to change the cursor visibility.</exception>
    public static bool Visible
    {
        get;
        set
        {
            if (field == value)
                return;

            SDLThrowHelper.ThrowIfFailed(value ? SDL3.ShowCursor() : SDL3.HideCursor());
            field = value;
        }
    } = true;

    internal SDL_Cursor* Handle { get; private set; }

    /// <inheritdoc/>
    public void Dispose()
    {
        unsafe
        {
            if (Handle is null || !_owned)
                return;

            SDL3.DestroyCursor(Handle);
            Handle = null;
        }
    }

    /// <summary>
    /// Creates a color cursor from an image file.
    /// </summary>
    /// <remarks>The image is loaded, used to build the cursor, and disposed; the cursor does not depend on the file afterwards.</remarks>
    /// <param name="path">The path to the image file.</param>
    /// <param name="hotspot">The cursor hotspot.</param>
    /// <returns>The newly created cursor.</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="QuackInteropException">Failed to load the image or create the cursor.</exception>
    public static Cursor FromFile(string path, PointI hotspot)
    {
        using Surface surface = Surface.FromFile(path);
        return new Cursor(surface, hotspot);
    }

    /// <summary>
    /// Creates a color cursor from an image stream.
    /// </summary>
    /// <remarks>The image is loaded, used to build the cursor, and disposed; the cursor does not depend on the stream afterwards.</remarks>
    /// <param name="stream">The stream to read the image from.</param>
    /// <param name="hotspot">The cursor hotspot.</param>
    /// <returns>The newly created cursor.</returns>
    /// <exception cref="QuackInteropException">Failed to load the image or create the cursor.</exception>
    public static Cursor FromStream(Stream stream, PointI hotspot)
    {
        using Surface surface = Surface.FromStream(stream);
        return new Cursor(surface, hotspot);
    }

    private static SDL_Cursor* CreateMonochrome(ReadOnlySpan<byte> data, ReadOnlySpan<byte> mask, int width, int height, PointI hotspot)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

        if (width % 8 != 0)
            throw new ArgumentException("Width must be a multiple of 8.", nameof(width));

        int expected = width / 8 * height;

        ArgumentOutOfRangeException.ThrowIfNotEqual(data.Length, expected);
        ArgumentOutOfRangeException.ThrowIfNotEqual(mask.Length, expected);

        return unsafe (SDL3.CreateCursor(data, mask, width, height, hotspot.X, hotspot.Y));
    }

    private static SDL_Cursor* CreateAnimated(ReadOnlySpan<CursorFrame> frames, PointI hotspot)
    {
        ArgumentOutOfRangeException.ThrowIfZero(frames.Length);

        Span<SDL_CursorFrameInfo> info = new SDL_CursorFrameInfo[frames.Length];

        for (int i = 0; i < frames.Length; i++)
        {
            CursorFrame frame = frames[i];
            uint duration = frame.Duration > TimeSpan.Zero ? (uint)frame.Duration.TotalMilliseconds : 0;

            info[i] = unsafe (new SDL_CursorFrameInfo(frame.Surface.Handle, duration));
        }

        return unsafe (SDL3.CreateAnimatedCursor(info, info.Length, hotspot.X, hotspot.Y));
    }

    private unsafe void InitializeCursor(SDL_Cursor* handle)
    {
        QuackEngine.EnsureInitialized(Subsystem.Video);

        SDLThrowHelper.ThrowIfNull(handle);
        Handle = handle;
    }
}

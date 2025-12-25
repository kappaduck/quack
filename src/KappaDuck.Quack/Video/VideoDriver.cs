// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video;

/// <summary>
/// A collection of video drivers used by the engine.
/// </summary>
public static class VideoDriver
{
    /// <summary>
    /// Gets the number of video drivers available.
    /// </summary>
    public static int Count { get; } = Native.SDL_GetNumVideoDrivers();

    /// <summary>
    /// Gets the name of the currently initialized video driver.
    /// </summary>
    public static string Current { get; } = Native.SDL_GetCurrentVideoDriver();

    /// <summary>
    /// Gets the name of the video driver at the specified index.
    /// </summary>
    /// <param name="index">The index of the video driver to get the name of.</param>
    /// <returns>The name of the video driver at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is negative or greater than or equal to <see cref="Count"/>.</exception>
    public static string Get(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);

        return Native.SDL_GetVideoDriver(index);
    }

    /// <summary>
    /// Get all available video drivers.
    /// </summary>
    /// <returns>The names of all available video drivers.</returns>
    public static string[] GetAll()
    {
        string[] drivers = new string[Count];

        for (int i = 0; i < Count; i++)
            drivers[i] = Get(i);

        return drivers;
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL;

namespace KappaDuck.Quack.Video;

/// <summary>
/// A collection of rendering drivers available for the current display.
/// </summary>
/// <remarks>
/// A render driver is a set of code that handles rendering and texture management on a particular display.
/// Normally there is only one, but some drivers may have several available with different capabilities.
/// </remarks>
[PublicAPI]
public static class RenderDriver
{
    /// <summary>
    /// Gets the number of rendering drivers available for the current display.
    /// </summary>
    public static int Count { get; } = Native.SDL_GetNumRenderDrivers();

    /// <summary>
    /// Gets the name of the rendering driver at the specified index.
    /// </summary>
    /// <param name="index">The index of the rendering driver to get the name of.</param>
    /// <returns>The name of the rendering driver at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="index"/> is negative or greater than or equal to <see cref="Count"/>.</exception>
    public static string Get(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);

        return Native.SDL_GetRenderDriver(index);
    }

    /// <summary>
    /// Get all available rendering drivers.
    /// </summary>
    /// <returns>The names of all available rendering drivers.</returns>
    public static string[] GetAll()
    {
        string[] drivers = new string[Count];

        for (int i = 0; i < Count; i++)
            drivers[i] = Get(i);

        return drivers;
    }
}

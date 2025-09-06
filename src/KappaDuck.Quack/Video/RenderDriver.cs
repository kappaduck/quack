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
public static class RenderDriver
{
    /// <summary>
    /// Gets the number of 2D rendering drivers available for the current display.
    /// </summary>
    public static int Count { get; } = SDL.Video.SDL_GetNumRenderDrivers();

    /// <summary>
    /// Gets the name of a built-in 2D rendering driver.
    /// </summary>
    /// <param name="index">The index of a 2D rendering driver.</param>
    /// <returns>The name of the 2D rendering driver with the given index.</returns>
    public static string Get(int index) => SDL.Video.SDL_GetRenderDriver(index);

    /// <summary>
    /// Get all available 2D rendering drivers.
    /// </summary>
    /// <returns>The names of all available 2D rendering drivers.</returns>
    public static string[] GetAll()
    {
        string[] drivers = new string[Count];

        for (int i = 0; i < Count; i++)
            drivers[i] = Get(i);

        return drivers;
    }
}

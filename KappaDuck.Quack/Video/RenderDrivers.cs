// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video;

/// <summary>
/// A collection of available rendering drivers.
/// </summary>
/// <remarks>
/// A render driver is a set of code that handles rendering and texture management on a particular display.
/// Normally there is only one, but some drivers may have several available with different capabilities.
/// </remarks>
public static class RenderDrivers
{
    private static readonly Lazy<string[]> _drivers = new(GetAll, isThreadSafe: true);

    /// <summary>
    /// Gets all built-in rendering driver names.
    /// </summary>
    public static IReadOnlyList<string> All => _drivers.Value;

    /// <summary>
    /// Gets the number of built-in rendering drivers.
    /// </summary>
    public static int Count { get; } = SDL3.Video.GetNumRenderDrivers();

    /// <summary>
    /// Determines whether a rendering driver with the specified name exists.
    /// </summary>
    /// <param name="name">The name of the rendering driver to check for.</param>
    /// <returns><see langword="true"/> if a rendering driver with the name exists; otherwise, <see langword="false"/>.</returns>
    public static bool Contains(string name) => _drivers.Value.Contains(name);

    /// <summary>
    /// Gets the rendering driver name.
    /// </summary>
    /// <param name="index">The index of the rendering driver to query.</param>
    /// <returns>The name of the rendering driver.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is negative or greater than or equal to <see cref="Count"/>.</exception>
    public static string Get(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _drivers.Value.Length);

        return _drivers.Value[index];
    }

    private static string[] GetAll()
    {
        string[] drivers = new string[Count];

        for (int i = 0; i < drivers.Length; i++)
            drivers[i] = SDL3.Video.GetRenderDriver(i);

        return drivers;
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video;

/// <summary>
/// A collection of available video drivers.
/// </summary>
public static class VideoDrivers
{
    private static readonly Lazy<string[]> _drivers = new(GetAll, isThreadSafe: true);

    /// <summary>
    /// Gets all built-in video drivers
    /// </summary>
    public static IReadOnlyList<string> All => _drivers.Value;

    /// <summary>
    /// Gets the number of built-in video drivers.
    /// </summary>
    public static int Count { get; } = SDL3.Video.GetNumVideoDrivers();

    /// <summary>
    /// Gets the name of the currently initialized video driver
    /// or <see langword="null"/> if no video driver has been initialized.
    /// </summary>
    public static string? Current { get; } = SDL3.Video.GetCurrentVideoDriver();

    /// <summary>
    /// Determines whether a video driver with the specified name exists.
    /// </summary>
    /// <param name="name">The name of the video driver to check for.</param>
    /// <returns><see langword="true"/> if a video driver with the name exists; otherwise, <see langword="false"/>.</returns>
    public static bool Contains(string name) => _drivers.Value.Contains(name);

    /// <summary>
    /// Gets the video driver name.
    /// </summary>
    /// <param name="index">The index of the video driver to query.</param>
    /// <returns>The name of the video driver.</returns>
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
            drivers[i] = SDL3.Video.GetVideoDriver(i);

        return drivers;
    }
}

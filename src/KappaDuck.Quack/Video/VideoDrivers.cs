// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Collections.Immutable;

namespace KappaDuck.Quack.Video;

/// <summary>
/// A collection of available video drivers.
/// </summary>
public static class VideoDrivers
{
    private static readonly Lazy<ImmutableArray<string>> _drivers = new(GetAll);

    /// <summary>
    /// Gets all built-in video drivers
    /// </summary>
    public static IReadOnlyList<string> All => _drivers.Value;

    /// <summary>
    /// Gets the name of the currently initialized video driver
    /// or <see langword="null"/> if no video driver has been initialized.
    /// </summary>
    public static string? Current => SDL3.GetCurrentVideoDriver();

    private static ImmutableArray<string> GetAll()
    {
        ImmutableArray<string>.Builder builder = ImmutableArray.CreateBuilder<string>(SDL3.GetNumVideoDrivers());

        for (int i = 0; i < builder.Capacity; i++)
            builder.Add(SDL3.GetVideoDriver(i));

        return builder.MoveToImmutable();
    }
}

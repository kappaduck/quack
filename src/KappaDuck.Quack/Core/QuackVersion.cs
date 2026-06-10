// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Reflection;

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents the versions of Quack! and the native libraries it is built on.
/// </summary>
public static class QuackVersion
{
    /// <summary>
    /// Gets the version of the linked SDL_image runtime.
    /// </summary>
    public static Version Image { get; } = Decode(SDL3_image.Version());

    /// <summary>
    /// Gets the version of the linked SDL_mixer runtime.
    /// </summary>
    public static Version Mixer { get; } = Decode(SDL3_mixer.Version());

    /// <summary>
    /// Gets the version of Quack!.
    /// </summary>
    public static Version Quack { get; } = Resolve();

    /// <summary>
    /// Gets the version of the linked SDL runtime.
    /// </summary>
    public static Version SDL { get; } = Decode(SDL3.Version());

    /// <summary>
    /// Gets the version of the linked SDL_ttf runtime.
    /// </summary>
    public static Version TTF { get; } = Decode(SDL3_ttf.Version());

    private static Version Decode(int version)
    {
        int major = version / 1000000;
        int minor = version / 1000 % 1000;
        int patch = version % 1000;

        return new Version(major, minor, patch);
    }

    private static Version Resolve() => new(typeof(QuackVersion).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version);
}

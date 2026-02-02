// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Reflection;

namespace KappaDuck.Quack.Core;

/// <summary>
/// Provides version information about Quack! and its components.
/// </summary>
[ExcludeFromCodeCoverage]
public static class QuackVersion
{
    /// <summary>
    /// Gets the complete version information including <see cref="Quack"/>, <see cref="SDL"/>, <see cref="Image"/>, and <see cref="TTF"/> versions.
    /// </summary>
    public static string FullVersion => $"{Quack} (SDL: {SDL}, SDL_image: {Image}, SDL_ttf: {TTF})";

    /// <summary>
    /// Gets the version of the SDL3_image library currently in use.
    /// </summary>
    public static Version Image { get; } = GetVersion(SDL3_image.Version());

    /// <summary>
    /// Gets the current version of Quack!.
    /// </summary>
    public static Version Quack { get; } = GetQuackVersion();

    /// <summary>
    /// Gets the version of the SDL3 library currently in use.
    /// </summary>
    public static Version SDL { get; } = GetVersion(SDL3.Version());

    /// <summary>
    /// Gets the version of the SDL3_ttf library currently in use.
    /// </summary>
    public static Version TTF { get; } = GetVersion(SDL3_ttf.Version());

    private static Version GetQuackVersion() => new(typeof(QuackVersion).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version);

    private static Version GetVersion(int version)
    {
        int major = version / 1000000;
        int minor = version / 1000 % 1000;
        int patch = version % 1000;

        return new Version(major, minor, patch);
    }
}

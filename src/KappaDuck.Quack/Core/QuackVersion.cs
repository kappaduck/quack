// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Provides version information about Quack! and its components.
/// </summary>
public static class QuackVersion
{
    /// <summary>
    /// Gets the version of the SDL_image library used by Quack!.
    /// </summary>
    public static Version Image { get; } = GetVersion(Native.IMG_Version());

    /// <summary>
    /// Gets the current version of Quack!.
    /// </summary>
    public static Version Quack { get; } = new(0, 3, 0);

    /// <summary>
    /// Gets the version of the SDL library used by Quack!.
    /// </summary>
    public static Version SDL { get; } = GetVersion(Native.SDL_GetVersion());

    /// <summary>
    /// Gets the version of the SDL_ttf library used by Quack!.
    /// </summary>
    public static Version TTF { get; } = GetVersion(Native.TTF_Version());

    private static Version GetVersion(int version)
    {
        int major = version / 1000000;
        int minor = version / 1000 % 1000;
        int patch = version % 1000;

        return new Version(major, minor, patch);
    }
}

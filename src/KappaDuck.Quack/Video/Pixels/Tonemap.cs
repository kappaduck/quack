// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video.Pixels;

/// <summary>
/// Provides operators to set the tone mapping for <see cref="Surface.ToneMapOperator"/>
/// </summary>
public static class ToneMap
{
    /// <summary>
    /// Disables the tone mapping.
    /// </summary>
    public const string None = "none";

    /// <summary>
    /// Uses that Chrome uses for HDR content.
    /// </summary>
    public const string Chrome = "chrome";

    /// <summary>
    /// Apply a linear space.
    /// </summary>
    /// <param name="n">The scale factor.</param>
    /// <returns>The formatted operator.</returns>
    public static string LinearSpaceScaleFactor(float n) => $"*={n}";
}

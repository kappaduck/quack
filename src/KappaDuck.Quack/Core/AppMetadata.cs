// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

/// <summary>
/// The metadata of the application.
/// </summary>
public sealed record AppMetadata
{
    /// <summary>
    /// Gets the reverse domain identifier of the application, e.g. com.kappaduck.quack.demo.
    /// </summary>
    /// <remarks>
    /// This is used by desktop compositors to identify and group windows together, as
    /// well as match applications with associated desktop settings and icons.
    /// </remarks>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the human-readable name of the application, e.g. Quack! Demo.
    /// </summary>
    /// <remarks>
    /// This will show up anywhere the OS shows the name of the application
    /// separately from window titles, such as volume control applets, etc.
    /// This defaults to "Quack! Application" if not set.
    /// </remarks>
    public string Name { get; init; } = "Quack! Application";

    /// <summary>
    /// Gets the version of the application, e.g. 1.0.0.
    /// </summary>
    /// <remarks>
    /// There are no restrictions on the version number format.
    /// </remarks>
    public string? Version { get; init; }

    /// <summary>
    /// Gets the human-readable name of the author/creator/developer of the application, e.g. KappaDuck.
    /// </summary>
    public string? Author { get; init; }

    /// <summary>
    /// Gets the human-readable copyright notice, e.g. Copyright 2025 (c) KappaDuck.
    /// </summary>
    /// <remarks>
    /// Keep this to one line, don't paste a copy of a whole software license here.
    /// </remarks>
    public string? Copyright { get; init; }

    /// <summary>
    /// Gets the url of the application, e.g. https://kappaduck.com.
    /// </summary>
    public Uri? Url { get; init; }

    /// <summary>
    /// Gets the type of the application. Default is <see cref="AppType.Application"/>.
    /// </summary>
    public string Type { get; init; } = AppType.Application;

    /// <summary>
    /// The type of the application.
    /// </summary>
    public static class AppType
    {
        /// <summary>
        /// The application is a video game.
        /// </summary>
        public const string Game = "game";

        /// <summary>
        /// The application is a media player.
        /// </summary>
        public const string MediaPlayer = "mediaplayer";

        /// <summary>
        /// Other types of applications.
        /// </summary>
        public const string Application = "application";
    }
}

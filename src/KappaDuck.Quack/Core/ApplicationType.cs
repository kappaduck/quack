// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

/// <summary>
/// The type of the application.
/// </summary>
public enum ApplicationType
{
    /// <summary>
    /// The application is a video game.
    /// </summary>
    Game = 0,

    /// <summary>
    /// The application is a media player.
    /// </summary>
    MediaPlayer = 1,

    /// <summary>
    /// Other type of application.
    /// </summary>
    Application = 2
}

/// <summary>
/// Provides extension on <see cref="ApplicationType"/>.
/// </summary>
public static class ApplicationTypeExtensions
{
    extension(ApplicationType type)
    {
        /// <summary>
        /// Gets the human-readable name of the application type.
        /// </summary>
        /// <remarks>
        /// It returns <see cref="ApplicationType.Application"/> for unrecognized application types.
        /// </remarks>
        public string Name => type switch
        {
            ApplicationType.Game => nameof(ApplicationType.Game),
            ApplicationType.MediaPlayer => nameof(ApplicationType.MediaPlayer),
            _ => nameof(ApplicationType.Application)
        };
    }
}

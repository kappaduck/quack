// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

/// <summary>
/// The type of the application.
/// </summary>
[PublicAPI]
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

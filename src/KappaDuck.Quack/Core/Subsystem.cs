// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Identifies the engine subsystems and extensions that can be brought up through
/// <see cref="QuackEngine.Init(Subsystem, ApplicationMetadata?)"/>.
/// </summary>
/// <remarks>
/// Combine values with a bitwise OR to initialize several at once. Some subsystems implicitly
/// enable others they depend on, as noted on each value.
/// </remarks>
[Flags]
public enum Subsystem
{
    /// <summary>
    /// No subsystem.
    /// </summary>
    None = 0x00000000,

    /// <summary>
    /// Audio capture and playback devices. Implicitly enables <see cref="Events"/>.
    /// </summary>
    Audio = 0x00000010,

    /// <summary>
    /// Windows, displays and rendering. Implicitly enables <see cref="Events"/>.
    /// </summary>
    /// <remarks>
    /// Must be initialized on the application's main thread.
    /// </remarks>
    Video = 0x00000020,

    /// <summary>
    /// Joystick input. Implicitly enables <see cref="Events"/>.
    /// </summary>
    /// <remarks>
    /// Should be initialized on the same thread as <see cref="Video"/>.
    /// </remarks>
    Joystick = 0x00000200,

    /// <summary>
    /// Force-feedback and rumble effects.
    /// </summary>
    Haptic = 0x00001000,

    /// <summary>
    /// Gamepad input and button mappings. Implicitly enables <see cref="Joystick"/>.
    /// </summary>
    Gamepad = 0x00002000,

    /// <summary>
    /// Input and window event delivery, required to poll events.
    /// </summary>
    Events = 0x00004000,

    /// <summary>
    /// Device sensors such as accelerometers and gyroscopes. Implicitly enables <see cref="Events"/>.
    /// </summary>
    Sensor = 0x00008000,

    /// <summary>
    /// Video capture devices. Implicitly enables <see cref="Events"/>.
    /// </summary>
    Camera = 0x00010000,

    /// <summary>
    /// Font loading and text rendering.
    /// </summary>
    TTF = 0x40000000,

    /// <summary>
    /// Loading, mixing and playback of sound effects and music.
    /// </summary>
    Mixer = 0x20000000
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents a subsystem that can be initialized and shutdown by <see cref="QuackEngine"/>.
/// </summary>
[Flags]
public enum SubSystem : uint
{
    /// <summary>
    /// No subsystem.
    /// </summary>
    /// <remarks>
    /// It is only used to check if the subsystem is already initialized.
    /// </remarks>
    None = 0x00000000u,

    /// <summary>
    /// Initialize the audio subsystem. Implicitly initializes the <see cref="Events" /> subsystem.
    /// </summary>
    Audio = 0x00000010u,

    /// <summary>
    /// Initialize the video subsystem. Implicitly initializes the <see cref="Events" /> subsystem.
    /// </summary>
    /// <remarks>
    /// It should be initialized on the main thread.
    /// </remarks>
    Video = 0x00000020u,

    /// <summary>
    /// Initialize the joystick subsystem. Implicitly initializes the <see cref="Events" /> subsystem.
    /// </summary>
    /// <remarks>
    /// It should be initialized on the same thread as <see cref="Video" /> subsystem on Window.
    /// </remarks>
    Joystick = 0x00000200u,

    /// <summary>
    /// Initialize the Force Feedback subsystem.
    /// </summary>
    Haptic = 0x00001000u,

    /// <summary>
    /// Initialize the gamepad subsystem. Implicitly initializes the <see cref="Joystick" /> subsystem.
    /// </summary>
    Gamepad = 0x00002000u,

    /// <summary>
    /// Initialize the events subsystem.
    /// </summary>
    Events = 0x00004000u,

    /// <summary>
    /// Initialize the sensor subsystem. Implicitly initializes the <see cref="Events" /> subsystem.
    /// </summary>
    Sensor = 0x00008000u,

    /// <summary>
    /// Initialize the camera subsystem. Implicitly initializes the <see cref="Events" /> subsystem.
    /// </summary>
    Camera = 0x00010000u,
}

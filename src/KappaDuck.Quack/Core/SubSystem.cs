// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents a subsystem that can be enabled or disabled in the <see cref="QuackEngine"/>.
/// </summary>
[Flags]
internal enum Subsystem : uint
{
    /// <summary>
    /// Enable the audio subsystem. Implicitly enables the <see cref="Events"/> subsystem.
    /// </summary>
    Audio = 0x00000010u,

    /// <summary>
    /// Enable the video subsystem. Implicitly enables the <see cref="Events"/> subsystem.
    /// </summary>
    /// <remarks>
    /// It should be enabled on the main thread.
    /// </remarks>
    Video = 0x00000020u,

    /// <summary>
    /// Enable the joystick subsystem. Implicitly enables the <see cref="Events"/> subsystem.
    /// </summary>
    /// <remarks>
    /// It should be enabled on the same thread as <see cref="Video"/> subsystem.
    /// </remarks>
    Joystick = 0x00000200u,

    /// <summary>
    /// Enable the Force Feedback subsystem.
    /// </summary>
    Haptic = 0x00001000u,

    /// <summary>
    /// Enable the gamepad subsystem. Implicitly enables the <see cref="Joystick"/> subsystem.
    /// </summary>
    Gamepad = 0x00002000u,

    /// <summary>
    /// Enable the Events subsystem.
    /// </summary>
    Events = 0x00004000u,

    /// <summary>
    /// Enable the sensor subsystem. Implicitly enables the <see cref="Events"/> subsystem.
    /// </summary>
    Sensor = 0x00008000u,

    /// <summary>
    /// Enable the camera subsystem. Implicitly enables the <see cref="Events"/> subsystem.
    /// </summary>
    Camera = 0x00010000u,

    /// <summary>
    /// Enable the TrueType Font extension.
    /// </summary>
    TTF = 0x00020000u
}

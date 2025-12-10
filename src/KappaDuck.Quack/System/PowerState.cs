// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.System;

/// <summary>
/// Represents the power state of the system.
/// </summary>
[PublicAPI]
public enum PowerState
{
    /// <summary>
    /// An error occurred while determining the power state.
    /// </summary>
    Error = -1,

    /// <summary>
    /// The power state could not be determined (e.g., no battery).
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The system is running on battery power.
    /// </summary>
    OnBattery = 1,

    /// <summary>
    /// The system is plugged in but does not have a battery available.
    /// </summary>
    NoBattery = 2,

    /// <summary>
    /// The system is plugged in and charging the battery.
    /// </summary>
    Charging = 3,

    /// <summary>
    /// The system is plugged in and the battery is fully charged.
    /// </summary>
    Charged = 4
}

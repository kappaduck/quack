// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.System;

/// <summary>
/// Provides information about the system's power status, e.g. laptop battery status.
/// </summary>
public sealed partial class PowerStatus
{
    private PowerStatus(int? remaining, int? percentage, PowerState state)
    {
        Remaining = remaining == null ? null : TimeSpan.FromSeconds(remaining.Value);
        Percentage = percentage;
        State = state;
    }

    /// <summary>
    /// Gets the current power status of the system.
    /// </summary>
    /// <remarks>
    /// You should never take a battery status as absolute truth.
    /// Batteries (especially failing batteries) are delicate hardware,
    /// and the values reported here are best estimates based on what that hardware reports.
    /// It's not uncommon for older batteries to lose stored power much faster than it reports,
    /// or completely drain when reporting it has 20 percent left, etc.
    /// Battery status can change at any time; if you are concerned with power state,
    /// you should call this function frequently, and perhaps ignore changes until they seem to be stable for a few seconds.
    /// It's possible a platform can only report battery percentage or time left but not both.
    /// </remarks>
    public static PowerStatus Current { get; } = GetCurrentPowerStatus();

    /// <summary>
    /// Gets the remaining percentage of power left or <see langword="null"/> if the system cannot determine it.
    /// </summary>
    public int? Percentage { get; }

    /// <summary>
    /// Gets the current power state.
    /// </summary>
    public PowerState State { get; }

    /// <summary>
    /// Gets the remaining seconds of power left or <see langword="null"/> if the system cannot determine it.
    /// </summary>
    public TimeSpan? Remaining { get; }

    private static PowerStatus GetCurrentPowerStatus()
    {
        PowerState state = SDL_GetPowerInfo(out int seconds, out int percent);

        return new PowerStatus(seconds == -1 ? null : seconds, percent == -1 ? null : percent, state);
    }

    /// <summary>
    /// Represents the power state of the system.
    /// </summary>
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

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState SDL_GetPowerInfo(out int seconds, out int percent);
}

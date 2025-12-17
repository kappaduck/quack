// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL;

namespace KappaDuck.Quack.System;

/// <summary>
/// Provides information about the system's power status, e.g. laptop battery status.
/// </summary>
public sealed class PowerStatus
{
    private PowerStatus(int? remaining, int? percentage, PowerState state)
    {
        Remaining = remaining is null ? null : TimeSpan.FromSeconds(remaining.Value);
        Percentage = percentage;
        State = state;
    }

    /// <summary>
    /// Gets the current power status of the system.
    /// </summary>
    /// <remarks>
    /// <para>
    /// You should never take the power status for granted.
    /// Batteries (especially failing ones) can report incorrect values,
    /// and the values reported here are best estimates based on what that hardware reports.
    /// It's not uncommon for older batteries to lose stored power much faster than reported,
    /// or completely drain when reporting it has 20% left, etc.
    /// </para>
    /// <para>
    /// Battery status can change at any time, so if your application depends on accurate power status,
    /// you should periodically refresh the values by accessing this property again, and perhaps ignore changes
    /// until they seem to be stable for a few seconds. It's possible a platform can only report battery percentage
    /// or time left but not both.
    /// </para>
    /// </remarks>
    public static PowerStatus Current => GetCurrentPowerStatus();

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
        PowerState state = Native.SDL_GetPowerInfo(out int seconds, out int percent);

        return new PowerStatus(seconds == -1 ? null : seconds, percent == -1 ? null : percent, state);
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.System;

/// <summary>
/// Provides a snapshot of the system's power supply, e.g. laptop battery status.
/// </summary>
public readonly record struct PowerInfo
{
    private PowerInfo(PowerState state, int? percentage, TimeSpan? remaining)
    {
        State = state;
        Percentage = percentage;
        Remaining = remaining;
    }

    /// <summary>
    /// Gets a snapshot of the current power supply.
    /// </summary>
    /// <remarks>
    /// <para>
    /// You should never take the power status for granted. Batteries (especially failing ones) can
    /// report incorrect values, and the values reported here are best estimates based on what the
    /// hardware reports. It's not uncommon for older batteries to lose stored power much faster than
    /// reported, or completely drain when reporting it has 20% left, etc.
    /// </para>
    /// <para>
    /// Battery status can change at any time, so if your application depends on accurate power status,
    /// refresh the values by reading this property again, and perhaps ignore changes until they seem
    /// stable for a few seconds. A platform may only report battery percentage or time left, not both.
    /// </para>
    /// <para>
    /// On some platforms retrieving power details can be expensive; for continuous display, read it
    /// about once a minute rather than every frame.
    /// </para>
    /// </remarks>
    public static PowerInfo Current
    {
        get
        {
            PowerState state = SDL3.GetPowerInfo(out int seconds, out int percent);

            int? percentage = percent < 0 ? null : percent;
            TimeSpan? remaining = seconds < 0 ? null : TimeSpan.FromSeconds(seconds);

            return new PowerInfo(state, percentage, remaining);
        }
    }

    /// <summary>
    /// Gets the power state of the system.
    /// </summary>
    public PowerState State { get; }

    /// <summary>
    /// Gets the battery charge between 0 and 100, or <see langword="null"/> if it can't be determined.
    /// </summary>
    public int? Percentage { get; }

    /// <summary>
    /// Gets the estimated battery life remaining, or <see langword="null"/> if it can't be determined.
    /// </summary>
    public TimeSpan? Remaining { get; }

    /// <summary>
    /// Gets a value indicating whether the system is running on battery (not plugged in).
    /// </summary>
    public bool IsOnBattery => State == PowerState.OnBattery;

    /// <summary>
    /// Gets a value indicating whether the battery is charging.
    /// </summary>
    public bool IsCharging => State == PowerState.Charging;

    /// <summary>
    /// Gets a value indicating whether the battery is fully charged.
    /// </summary>
    public bool IsCharged => State == PowerState.Charged;

    /// <summary>
    /// Gets a value indicating whether a battery is present.
    /// </summary>
    public bool HasBattery => State is PowerState.OnBattery or PowerState.Charging or PowerState.Charged;

}

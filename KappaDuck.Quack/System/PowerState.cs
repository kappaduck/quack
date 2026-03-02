// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.System;

/// <summary>
/// Provides information about the current power state of the system, e.g. laptop battery status.
/// </summary>
public sealed class PowerState
{
    private PowerState(PowerStatus status, int? percentage, int? remainingInSeconds)
    {
        Status = status;
        Percentage = percentage;
        Remaining = remainingInSeconds.HasValue ? TimeSpan.FromSeconds(remainingInSeconds.Value) : null;
    }

    /// <summary>
    /// Gets the current power state of the system.
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
    public static PowerState Current => GetCurrent();

    /// <summary>
    /// Gets the current power status of the system.
    /// </summary>
    public PowerStatus Status { get; }

    /// <summary>
    /// Gets the current battery percentage, if available.
    /// </summary>
    public int? Percentage { get; }

    /// <summary>
    /// Gets the estimated remaining battery life, if available.
    /// </summary>
    public TimeSpan? Remaining { get; }

    private static PowerState GetCurrent()
    {
        PowerStatus status = SDL3.System.GetPowerInfo(out int remainingInSeconds, out int percent);
        return new PowerState(status, percent == -1 ? null : percent, remainingInSeconds == -1 ? null : remainingInSeconds);
    }
}

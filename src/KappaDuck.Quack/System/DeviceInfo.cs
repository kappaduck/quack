// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Interop.SDL.Primitives;
using System.Globalization;

namespace KappaDuck.Quack.System;

/// <summary>
/// Represents the device on which is currently running.
/// </summary>
public static class DeviceInfo
{
    /// <summary>
    /// Gets the operating system's process architecture, e.g. <see cref="Architecture.X64"/> or <see cref="Architecture.X86"/>.
    /// </summary>
    public static Architecture Architecture { get; } = RuntimeInformation.OSArchitecture;

    /// <summary>
    /// Gets the user's preferred cultures.
    /// </summary>
    /// <remarks>
    /// This might be a "slow" call that has to query the operating system. It's best to ask for this once and save
    /// the results. However, this list can change, usually because the user has changed a system preference outside
    /// of your application; Quack! will send a <see cref="CultureChangedEvent"/> so you can update by calling this method again.
    /// </remarks>
    public static IReadOnlyList<CultureInfo> Cultures
    {
        get
        {
            unsafe
            {
                SDL_Locale** locales = SDL3.GetPreferredLocales(out int count);

                if (locales is null || count == 0)
                    return [];

                try
                {
                    List<CultureInfo> cultures = [with(count)];

                    for (int i = 0; i < count; i++)
                    {
                        SDL_Locale* locale = locales[i];

                        string language = SDLStringMarshaller.ConvertToManaged(locale->Language)!;
                        string? country = SDLStringMarshaller.ConvertToManaged(locale->Country);

                        if (TryGetCulture(language, country, out CultureInfo? culture))
                            cultures.Add(culture);
                    }

                    return [.. cultures];
                }
                finally
                {
                    SDL3.Free(locales);
                }
            }
        }
    }

    /// <summary>
    /// Gets the platform name
    /// </summary>
    public static string Platform { get; } = RuntimeInformation.OSDescription;

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
    public static PowerInfo Power => PowerInfo.Capture();

    /// <summary>
    /// Gets the number of processors available to the current process.
    /// </summary>
    public static int ProcessorCount => Environment.ProcessorCount;

    /// <summary>
    /// Gets the amount of RAM configured in the system in MiB.
    /// </summary>
    public static long RAM { get; } = SDL3.GetSystemRAM();

    /// <summary>
    /// Gets the current system theme.
    /// </summary>
    public static Theme Theme => SDL3.GetSystemTheme();

    private static bool TryGetCulture(string language, string? country, [NotNullWhen(true)] out CultureInfo? culture)
    {
        string name = string.IsNullOrEmpty(country) ? language : $"{language}-{country}";
        try
        {
            culture = CultureInfo.GetCultureInfo(name);
            return true;
        }
        catch (CultureNotFoundException)
        {
            culture = null;
            return false;
        }
    }
}

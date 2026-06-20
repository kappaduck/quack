// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Input.Devices;

/// <summary>
/// Provides functionalities for keyboard input devices.
/// </summary>
public static class KeyboardDevices
{
    /// <summary>
    /// Gets all connected keyboard devices.
    /// </summary>
    /// <remarks>
    /// It will include any device or virtual driver that provides keyboard functionality,
    /// including some mice, KVM switches, motherboard power buttons, etc. You should wait
    /// for input from a device before you consider it actively in use.
    /// </remarks>
    public static IReadOnlyList<KeyboardDevice> All
    {
        get
        {
            ReadOnlySpan<uint> ids = SDL3.GetKeyboards(out _);

            if (ids.IsEmpty)
                return [];

            KeyboardDevice[] devices = new KeyboardDevice[ids.Length];

            for (int i = 0; i < ids.Length; i++)
                devices[i] = new KeyboardDevice(ids[i]);

            return devices;
        }
    }

    /// <summary>
    /// Gets a value indicating whether a keyboard is connected.
    /// </summary>
    public static bool HasKeyboard => SDL3.HasKeyboard();

    /// <summary>
    /// Gets a value indicating whether a virtual keyboard is supported.
    /// </summary>
    public static bool HasVirtualKeyboard => SDL3.HasScreenKeyboardSupport();

    /// <summary>
    /// Gets the keyboard device with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the keyboard device.</param>
    /// <returns>The keyboard device with the specified identifier.</returns>
    public static KeyboardDevice FromId(uint id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        return new(id);
    }
}

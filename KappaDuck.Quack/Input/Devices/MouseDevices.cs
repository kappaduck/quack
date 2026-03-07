// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Input.Devices;

/// <summary>
/// Provides functionality for mouse input devices.
/// </summary>
public static class MouseDevices
{
    /// <summary>
    /// Gets all connected mouse devices.
    /// </summary>
    /// <remarks>
    /// It will include any device or virtual driver that provides mouse functionality,
    /// including some game controllers, KVM switches, etc. You should wait for input from
    /// a device before you consider it actively in use.
    /// </remarks>
    public static IEnumerable<MouseDevice> All
    {
        get
        {
            ReadOnlySpan<uint> ids = SDL3.Input.GetMice(out _);

            if (ids.IsEmpty)
                return [];

            MouseDevice[] devices = new MouseDevice[ids.Length];

            for (int i = 0; i < ids.Length; i++)
                devices[i] = new MouseDevice(ids[i]);

            return devices;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the system has a mouse.
    /// </summary>
    public static bool HasMouse => SDL3.Input.HasMouse();

    /// <summary>
    /// Gets the mouse device with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the mouse device.</param>
    /// <returns>The mouse device with the specified identifier.</returns>
    public static MouseDevice FromId(uint id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        return new(id);
    }
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Input.Devices;

/// <summary>
/// Represents a mouse input device.
/// </summary>
public sealed record MouseDevice
{
    internal MouseDevice(uint id)
    {
        Id = id;
        Name = SDL3.Input.GetMouseNameById(id);
    }

    /// <summary>
    /// Gets the mouse device's id.
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Gets the mouse device's name.
    /// </summary>
    public string Name { get; }
}

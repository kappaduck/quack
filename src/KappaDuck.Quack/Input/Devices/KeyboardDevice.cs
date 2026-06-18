// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;

namespace KappaDuck.Quack.Input.Devices;

/// <summary>
/// Represents a keyboard input device.
/// </summary>
public sealed record KeyboardDevice
{
    internal KeyboardDevice(uint id)
    {
        Id = id;
        string? name = SDL3.GetKeyboardNameById(id);

        SDLThrowHelper.ThrowIfNull(name);
        Name = name;
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

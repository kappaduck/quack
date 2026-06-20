// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a new keyboard device was connected.
/// </summary>
public readonly struct KeyboardAddedEvent
{
    internal KeyboardAddedEvent(SDL_KeyboardDeviceEvent e) => Which = e.Which;

    /// <summary>
    /// Gets the keyboard device id which was added.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the keyboard device which was added.
    /// </summary>
    public KeyboardDevice Device => KeyboardDevices.FromId(Which);
}

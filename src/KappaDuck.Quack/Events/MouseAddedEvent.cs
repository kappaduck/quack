// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a new mouse device was connected.
/// </summary>
public readonly struct MouseAddedEvent : IEvent
{
    internal MouseAddedEvent(SDL_MouseDeviceEvent e) => Which = e.Which;

    /// <summary>
    /// Gets the mouse device id which was added.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the mouse device which was added.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);
}

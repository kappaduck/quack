// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a mouse device was disconnected.
/// </summary>
public readonly struct MouseRemovedEvent
{
    internal MouseRemovedEvent(SDL_MouseDeviceEvent e) => Which = e.Which;

    /// <summary>
    /// Gets the mouse device id which was removed.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the mouse device which was removed.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(Which);
}

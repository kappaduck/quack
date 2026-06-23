// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a keyboard device was disconnected.
/// </summary>
public readonly struct KeyboardRemovedEvent : IEvent
{
    internal KeyboardRemovedEvent(SDL_KeyboardDeviceEvent e) => Which = e.Which;

    /// <summary>
    /// Gets the keyboard device id which was removed.
    /// </summary>
    public uint Which { get; }

    /// <summary>
    /// Gets the keyboard device which was removed.
    /// </summary>
    public KeyboardDevice Device => KeyboardDevices.FromId(Which);
}

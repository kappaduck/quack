// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which a new keyboard device was connected.
/// </summary>
/// <param name="keyboardId">The keyboard id which was added.</param>
public readonly struct KeyboardAddedEvent(uint keyboardId)
{
    /// <summary>
    /// Gets the keyboard device which was added.
    /// </summary>
    public KeyboardDevice Device => KeyboardDevices.FromId(keyboardId);
}

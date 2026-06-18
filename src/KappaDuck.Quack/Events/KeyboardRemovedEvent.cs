// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which a keyboard device was disconnected.
/// </summary>
/// <param name="keyboardId">The keyboard id which was removed.</param>
public readonly struct KeyboardRemovedEvent(uint keyboardId)
{
    /// <summary>
    /// Gets the keyboard device which was removed.
    /// </summary>
    public KeyboardDevice Device => KeyboardDevices.FromId(keyboardId);
}

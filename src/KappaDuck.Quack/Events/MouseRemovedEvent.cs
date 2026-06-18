// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which a mouse device was disconnected.
/// </summary>
/// <param name="mouseId">The mouse id which was removed.</param>
public readonly struct MouseRemovedEvent(uint mouseId)
{
    /// <summary>
    /// Gets the mouse device which was removed.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(mouseId);
}

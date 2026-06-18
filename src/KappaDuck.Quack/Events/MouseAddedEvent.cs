// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Input.Devices;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which a new mouse device was connected.
/// </summary>
/// <param name="mouseId">The mouse id which was added.</param>
public readonly struct MouseAddedEvent(uint mouseId)
{
    /// <summary>
    /// Gets the mouse device which was added.
    /// </summary>
    public MouseDevice Device => MouseDevices.FromId(mouseId);
}

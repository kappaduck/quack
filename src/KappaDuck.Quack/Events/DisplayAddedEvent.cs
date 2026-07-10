// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a display is connected to the system.
/// </summary>
[QuackEvent(SDL_EventType.DisplayAdded, NativeField = nameof(SDL_Event.Display))]
public readonly struct DisplayAddedEvent : IEvent
{
    internal DisplayAddedEvent(SDL_DisplayEvent e) => DisplayId = e.DisplayId;

    /// <summary>
    /// Gets the id of the display that was added.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the display that was added.
    /// </summary>
    public Display Display => new(DisplayId);
}

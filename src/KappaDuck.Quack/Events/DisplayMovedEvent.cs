// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a display changes position within the desktop layout.
/// </summary>
[QuackEvent(SDL_EventType.DisplayMoved, NativeField = nameof(SDL_Event.Display))]
public readonly struct DisplayMovedEvent : IEvent
{
    internal DisplayMovedEvent(SDL_DisplayEvent e) => DisplayId = e.DisplayId;

    /// <summary>
    /// Gets the id of the display that moved.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the display that moved.
    /// </summary>
    public Display Display => new(DisplayId);
}

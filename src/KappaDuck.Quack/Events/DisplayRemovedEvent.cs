// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a display is disconnected from the system.
/// </summary>
public readonly struct DisplayRemovedEvent : IEvent
{
    internal DisplayRemovedEvent(SDL_DisplayEvent e) => DisplayId = e.DisplayId;

    /// <summary>
    /// Gets the id of the display that was removed.
    /// </summary>
    /// <remarks>
    /// The display is no longer connected, so querying it through <see cref="Display"/> may fail.
    /// </remarks>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the display that was removed.
    /// </summary>
    public Display Display => new(DisplayId);
}

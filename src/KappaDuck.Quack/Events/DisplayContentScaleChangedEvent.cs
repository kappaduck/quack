// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the content scale of a display changes.
/// </summary>
public readonly struct DisplayContentScaleChangedEvent : IEvent
{
    internal DisplayContentScaleChangedEvent(SDL_DisplayEvent e) => DisplayId = e.DisplayId;

    /// <summary>
    /// Gets the id of the display whose content scale changed.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the display whose content scale changed.
    /// </summary>
    public Display Display => new(DisplayId);
}

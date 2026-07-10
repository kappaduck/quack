// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the usable bounds of a display change, for example when the taskbar is resized.
/// </summary>
[QuackEvent(SDL_EventType.DisplayUsableBoundsChanged, NativeField = nameof(SDL_Event.Display))]
public readonly struct DisplayUsableBoundsChangedEvent : IEvent
{
    internal DisplayUsableBoundsChangedEvent(SDL_DisplayEvent e) => DisplayId = e.DisplayId;

    /// <summary>
    /// Gets the id of the display whose usable bounds changed.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the display whose usable bounds changed.
    /// </summary>
    public Display Display => new(DisplayId);
}

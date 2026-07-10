// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the orientation of a display changes.
/// </summary>
[QuackEvent(SDL_EventType.DisplayOrientation, NativeField = nameof(SDL_Event.Display))]
public readonly struct DisplayOrientationChangedEvent : IEvent
{
    internal DisplayOrientationChangedEvent(SDL_DisplayEvent e)
    {
        DisplayId = e.DisplayId;
        Orientation = (DisplayOrientation)e.Data1;
    }

    /// <summary>
    /// Gets the id of the display whose orientation changed.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the new orientation of the display.
    /// </summary>
    public DisplayOrientation Orientation { get; }

    /// <summary>
    /// Gets the display whose orientation changed.
    /// </summary>
    public Display Display => new(DisplayId);
}

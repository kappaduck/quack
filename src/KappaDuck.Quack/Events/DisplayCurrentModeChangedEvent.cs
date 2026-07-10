// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the current mode of a display changes.
/// </summary>
[QuackEvent(SDL_EventType.DisplayCurrentModeChanged, NativeField = nameof(SDL_Event.Display))]
public readonly struct DisplayCurrentModeChangedEvent : IEvent
{
    internal DisplayCurrentModeChangedEvent(SDL_DisplayEvent e)
    {
        DisplayId = e.DisplayId;
        Size = new Size(e.Data1, e.Data2);
        Width = Size.Width;
        Height = Size.Height;
    }

    /// <summary>
    /// Gets the id of the display whose current mode changed.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the new current mode size.
    /// </summary>
    public Size Size { get; }

    /// <summary>
    /// Gets the updated width.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the updated height.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the display whose current mode changed.
    /// </summary>
    public Display Display => new(DisplayId);
}

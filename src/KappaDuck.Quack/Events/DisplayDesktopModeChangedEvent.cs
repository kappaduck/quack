// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when the desktop mode of a display changes.
/// </summary>
[QuackEvent(SDL_EventType.DisplayDesktopModeChanged, NativeField = nameof(SDL_Event.Display))]
public readonly struct DisplayDesktopModeChangedEvent : IEvent
{
    internal DisplayDesktopModeChangedEvent(SDL_DisplayEvent e)
    {
        DisplayId = e.DisplayId;
        Size = new SizeI(e.Data1, e.Data2);
        Width = Size.Width;
        Height = Size.Height;
    }

    /// <summary>
    /// Gets the id of the display whose desktop mode changed.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the new desktop mode size.
    /// </summary>
    public SizeI Size { get; }

    /// <summary>
    /// Gets the updated width.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the updated height.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the display whose desktop mode changed.
    /// </summary>
    public Display Display => new(DisplayId);
}

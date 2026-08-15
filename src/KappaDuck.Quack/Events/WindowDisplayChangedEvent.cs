// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Video;
using KappaDuck.Quack.Windowing;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a window is moved to a different display.
/// </summary>
[QuackEvent(SDL_EventType.WindowDisplayChanged, NativeField = nameof(SDL_Event.Window))]
public readonly struct WindowDisplayChangedEvent : IEvent
{
    internal WindowDisplayChangedEvent(SDL_WindowEvent e)
    {
        WindowId = e.WindowId;
        DisplayId = (uint)e.Data1;
    }

    /// <summary>
    /// Gets the id of the window the event is for.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the id of the display the window moved to.
    /// </summary>
    public uint DisplayId { get; }

    /// <summary>
    /// Gets the display the window moved to.
    /// </summary>
    public Display Display => new(DisplayId);

    /// <summary>
    /// Gets the window the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => Windows.FromId(WindowId);
}

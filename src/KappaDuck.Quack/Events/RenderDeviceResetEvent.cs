// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Raised when a renderer's device has been reset and all textures need to be recreated.
/// </summary>
public readonly struct RenderDeviceResetEvent : IEvent
{
    internal RenderDeviceResetEvent(SDL_RenderEvent e) => WindowId = e.WindowId;

    /// <summary>
    /// Gets the id of the window containing the renderer the event is for.
    /// </summary>
    public uint WindowId { get; }

    /// <summary>
    /// Gets the window containing the renderer the event is for, or <see langword="null"/> if it cannot be resolved.
    /// </summary>
    public Window? Window => WindowManager.FromId(WindowId);
}

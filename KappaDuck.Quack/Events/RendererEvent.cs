// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a renderer event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct RendererEvent
{
    /// <summary>
    /// Gets the renderer event type.
    /// </summary>
    /// <remarks>
    /// The event is one of the following types:
    /// <list type="bullet">
    /// <item><see cref="EventType.RenderDeviceLost"/></item>
    /// <item><see cref="EventType.RenderDeviceReset"/></item>
    /// <item><see cref="EventType.RenderTargetsReset"/></item>
    /// </list>
    /// </remarks>
    public EventType Type { get; }

    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the window id associated with the renderer event.
    /// </summary>
    public uint WindowId { get; }
}

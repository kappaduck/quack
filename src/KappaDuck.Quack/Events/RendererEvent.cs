// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a renderer event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct RendererEvent
{
    private readonly EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    /// <summary>
    /// Gets the window Id containing the renderer.
    /// </summary>
    public readonly uint WindowId { get; }
}

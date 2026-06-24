// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_WindowEvent
{
    private readonly SDL_EventType _type;
    private readonly uint _reserved;
    private readonly ulong _timestamp;

    internal uint WindowId { get; }

    internal int Data1 { get; }

    internal int Data2 { get; }
}

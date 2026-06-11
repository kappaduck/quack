// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Explicit, Size = 128)]
internal readonly struct SDL_Event
{
    [field: FieldOffset(0)]
    internal SDL_EventType Type { get; }
}

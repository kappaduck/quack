// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

[StructLayout(LayoutKind.Explicit, Size = 128)]
internal struct SDL_Event
{
    [field: FieldOffset(0)]
    internal SDL_EventType Type { get; set; }

    [field: FieldOffset(0)]
    internal SDL_QuitEvent Quit { get; set; }
}

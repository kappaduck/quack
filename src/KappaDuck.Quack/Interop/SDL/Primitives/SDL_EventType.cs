// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal enum SDL_EventType : uint
{
    /// <summary>
    /// Unused (do not remove).
    /// </summary>
    SDL_EVENT_FIRST = 0,

    /// <summary>
    /// User-requested quit.
    /// </summary>
    SDL_EVENT_QUIT = 0x100,

    /// <summary>
    /// This last event is only for bounding internal arrays.
    /// </summary>
    SDL_EVENT_LAST = 0xFFFF,
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

internal static partial class EventMapper
{
    internal const SDL_EventType None = 0;

    internal const SDL_EventType End = (SDL_EventType)ushort.MaxValue;
}

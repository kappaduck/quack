// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Events;

internal static class EventRange
{
    internal static (SDL_EventType min, SDL_EventType max) Of<T>() where T : IEvent
    {
        if (typeof(T) == typeof(QuitEvent))
            return (SDL_EventType.SDL_EVENT_QUIT, SDL_EventType.SDL_EVENT_QUIT);

        throw new NotSupportedException($"'{typeof(T).Name}' is not mapped to an SDL event range.");
    }
}

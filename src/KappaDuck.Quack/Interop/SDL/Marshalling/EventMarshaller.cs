// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

internal static class EventMarshaller
{
    internal static Event Convert(SDL_Event sdlEvent) => sdlEvent.Type switch
    {
        SDL_EventType.SDL_EVENT_QUIT => new QuitEvent(),
        SDL_EventType.SDL_EVENT_FIRST or SDL_EventType.SDL_EVENT_LAST => default,
        _ => default
    };

    internal static bool TryConvert(Event e, out SDL_Event sdlEvent)
    {
        sdlEvent = default;

        if (e.TryGetValue(out QuitEvent _))
        {
            sdlEvent = new SDL_Event
            {
                Quit = new SDL_QuitEvent(SDL_EventType.SDL_EVENT_QUIT)
            };

            return true;
        }

        return false;
    }
}

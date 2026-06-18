// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

internal static class EventType
{
    internal const SDL_EventType None = 0;

    internal const SDL_EventType End = (SDL_EventType)65535;

    internal static SDL_EventType Of<TEvent>() where TEvent : IEvent
    {
        Type type = typeof(TEvent);

        if (type == typeof(QuitRequestedEvent))
            return SDL_EventType.Quit;

        if (type == typeof(CultureChangedEvent))
            return SDL_EventType.LocaleChanged;

        if (type == typeof(ThemeChangedEvent))
            return SDL_EventType.SystemThemeChanged;

        return None;
    }

    [SuppressMessage("Style", "IDE0072:Add missing cases", Justification = "The remaining types will be implemented in the future")]
    internal static Event Convert(SDL_Event e) => e.Type switch
    {
        SDL_EventType.Quit => new QuitRequestedEvent(),
        SDL_EventType.LocaleChanged => new CultureChangedEvent(),
        SDL_EventType.SystemThemeChanged => new ThemeChangedEvent(),
        _ => default
    };

    internal static SDL_Event Convert(Event e)
    {
        SDL_Event sdlEvent = new() { Type = e.Type };

        if (e.Type == SDL_EventType.Quit)
            sdlEvent.Quit = new SDL_QuitEvent();

        return sdlEvent;
    }
}

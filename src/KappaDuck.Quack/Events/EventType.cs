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

        if (type == typeof(KeyboardAddedEvent))
            return SDL_EventType.KeyboardAdded;

        if (type == typeof(KeyboardRemovedEvent))
            return SDL_EventType.KeyboardRemoved;

        return None;
    }

    [SuppressMessage("Style", "IDE0072:Add missing cases", Justification = "The remaining types will be implemented in the future")]
    internal static Event Convert(SDL_Event e) => e.Type switch
    {
        SDL_EventType.Quit => new QuitRequestedEvent(),
        SDL_EventType.LocaleChanged => new CultureChangedEvent(),
        SDL_EventType.SystemThemeChanged => new ThemeChangedEvent(),
        SDL_EventType.KeyboardAdded => new KeyboardAddedEvent(e.KeyboardDevice.Which),
        SDL_EventType.KeyboardRemoved => new KeyboardAddedEvent(e.KeyboardDevice.Which),
        _ => default
    };

    internal static SDL_Event Convert(Event e)
    {
        SDL_Event native = new() { Type = e.Type };

        if (e.Type == SDL_EventType.Quit)
            native.Quit = new SDL_QuitEvent();

        if (e.Type == SDL_EventType.KeyboardAdded)
        {
            e.TryGetValue(out KeyboardAddedEvent keyboardAddedEvent);
            native.KeyboardDevice = new SDL_KeyboardDeviceEvent(keyboardAddedEvent.Device.Id, SDL_EventType.KeyboardAdded);
        }

        if (e.Type == SDL_EventType.KeyboardAdded)
        {
            e.TryGetValue(out KeyboardRemovedEvent keyboardRemovedEvent);
            native.KeyboardDevice = new SDL_KeyboardDeviceEvent(keyboardRemovedEvent.Device.Id, SDL_EventType.KeyboardRemoved);
        }

        return native;
    }
}

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

        if (type == typeof(MouseAddedEvent))
            return SDL_EventType.MouseAdded;

        if (type == typeof(MouseRemovedEvent))
            return SDL_EventType.MouseRemoved;

        if (type == typeof(KeyPressedEvent))
            return SDL_EventType.KeyDown;

        if (type == typeof(KeyReleasedEvent))
            return SDL_EventType.KeyUp;

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
        SDL_EventType.MouseAdded => new MouseAddedEvent(e.MouseDevice.Which),
        SDL_EventType.MouseRemoved => new MouseRemovedEvent(e.MouseDevice.Which),
        SDL_EventType.KeyDown => new KeyPressedEvent(e),
        SDL_EventType.KeyUp => new KeyReleasedEvent(e),
        _ => default
    };

    internal static SDL_Event Convert(Event e)
    {
        SDL_Event native = new() { Type = e.Type };

        if (e.Type == SDL_EventType.Quit)
        {
            native.Quit = new SDL_QuitEvent();
            return native;
        }

        if (e.Type == SDL_EventType.KeyboardAdded)
        {
            e.TryGetValue(out KeyboardAddedEvent keyboardAddedEvent);
            native.KeyboardDevice = new SDL_KeyboardDeviceEvent(keyboardAddedEvent.Device.Id, SDL_EventType.KeyboardAdded);

            return native;
        }

        if (e.Type == SDL_EventType.KeyboardAdded)
        {
            e.TryGetValue(out KeyboardRemovedEvent keyboardRemovedEvent);
            native.KeyboardDevice = new SDL_KeyboardDeviceEvent(keyboardRemovedEvent.Device.Id, SDL_EventType.KeyboardRemoved);

            return native;
        }

        if (e.Type == SDL_EventType.MouseAdded)
        {
            e.TryGetValue(out MouseAddedEvent mouseAddedEvent);
            native.MouseDevice = new SDL_MouseDeviceEvent(mouseAddedEvent.Device.Id, SDL_EventType.MouseAdded);

            return native;
        }

        if (e.Type == SDL_EventType.KeyboardAdded)
        {
            e.TryGetValue(out MouseRemovedEvent mouseRemovedEvent);
            native.MouseDevice = new SDL_MouseDeviceEvent(mouseRemovedEvent.Device.Id, SDL_EventType.MouseRemoved);

            return native;
        }

        if (e.Type == SDL_EventType.KeyDown)
        {
            e.TryGetValue(out KeyPressedEvent keyPressedEvent);
            native.Keyboard = new SDL_KeyboardEvent()
            {
                Type = SDL_EventType.KeyDown,
                Which = keyPressedEvent.Which,
                Scancode = keyPressedEvent.Code,
                Key = keyPressedEvent.Key,
                Keymod = keyPressedEvent.Keymod,
                Repeat = keyPressedEvent.Repeat ? (byte)1 : (byte)0
            };

            return native;
        }

        if (e.Type == SDL_EventType.KeyUp)
        {
            e.TryGetValue(out KeyReleasedEvent keyReleasedEvent);
            native.Keyboard = new SDL_KeyboardEvent()
            {
                Type = SDL_EventType.KeyUp,
                Which = keyReleasedEvent.Which,
                Scancode = keyReleasedEvent.Code,
                Key = keyReleasedEvent.Key,
                Keymod = keyReleasedEvent.Keymod
            };

            return native;
        }

        return native;
    }
}

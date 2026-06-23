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

        if (type == typeof(MouseButtonPressedEvent))
            return SDL_EventType.MouseButtonDown;

        if (type == typeof(MouseButtonReleasedEvent))
            return SDL_EventType.MouseButtonUp;

        if (type == typeof(MouseMovedEvent))
            return SDL_EventType.MouseMotion;

        if (type == typeof(DisplayAddedEvent))
            return SDL_EventType.DisplayAdded;

        if (type == typeof(DisplayRemovedEvent))
            return SDL_EventType.DisplayRemoved;

        if (type == typeof(DisplayMovedEvent))
            return SDL_EventType.DisplayMoved;

        if (type == typeof(DisplayContentScaleChangedEvent))
            return SDL_EventType.DisplayContentScaleChanged;

        if (type == typeof(DisplayCurrentModeChangedEvent))
            return SDL_EventType.DisplayCurrentModeChanged;

        if (type == typeof(DisplayDesktopModeChangedEvent))
            return SDL_EventType.DisplayDesktopModeChanged;

        if (type == typeof(DisplayOrientationChangedEvent))
            return SDL_EventType.DisplayOrientation;

        if (type == typeof(DisplayUsableBoundsChangedEvent))
            return SDL_EventType.DisplayUsableBoundsChanged;

        return None;
    }

    [SuppressMessage("Style", "IDE0072:Add missing cases", Justification = "The remaining types will be implemented in the future")]
    internal static Event Convert(in SDL_Event e) => e.Type switch
    {
        SDL_EventType.Quit => new QuitRequestedEvent(),
        SDL_EventType.LocaleChanged => new CultureChangedEvent(),
        SDL_EventType.SystemThemeChanged => new ThemeChangedEvent(),
        SDL_EventType.KeyboardAdded => new KeyboardAddedEvent(e.KeyboardDevice),
        SDL_EventType.KeyboardRemoved => new KeyboardRemovedEvent(e.KeyboardDevice),
        SDL_EventType.MouseAdded => new MouseAddedEvent(e.MouseDevice),
        SDL_EventType.MouseRemoved => new MouseRemovedEvent(e.MouseDevice),
        SDL_EventType.KeyDown => new KeyPressedEvent(e.Keyboard),
        SDL_EventType.KeyUp => new KeyReleasedEvent(e.Keyboard),
        SDL_EventType.MouseButtonDown => new MouseButtonPressedEvent(e.Button),
        SDL_EventType.MouseButtonUp => new MouseButtonReleasedEvent(e.Button),
        SDL_EventType.MouseMotion => new MouseMovedEvent(e.Motion),
        SDL_EventType.MouseWheel => new MouseWheelEvent(e.Wheel),
        SDL_EventType.DisplayAdded => new DisplayAddedEvent(e.Display),
        SDL_EventType.DisplayRemoved => new DisplayRemovedEvent(e.Display),
        SDL_EventType.DisplayMoved => new DisplayMovedEvent(e.Display),
        SDL_EventType.DisplayContentScaleChanged => new DisplayContentScaleChangedEvent(e.Display),
        SDL_EventType.DisplayCurrentModeChanged => new DisplayCurrentModeChangedEvent(e.Display),
        SDL_EventType.DisplayDesktopModeChanged => new DisplayDesktopModeChangedEvent(e.Display),
        SDL_EventType.DisplayOrientation => new DisplayOrientationChangedEvent(e.Display),
        SDL_EventType.DisplayUsableBoundsChanged => new DisplayUsableBoundsChangedEvent(e.Display),
        _ => default
    };
}

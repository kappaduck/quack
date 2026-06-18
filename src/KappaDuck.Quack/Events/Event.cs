// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a event polled from the event queue.
/// </summary>
[Union]
public readonly struct Event : IUnion
{
    private readonly QuitRequestedEvent _quitEvent;
    private readonly CultureChangedEvent _localeChangedEvent;
    private readonly ThemeChangedEvent _themeChangedEvent;
    private readonly KeyboardAddedEvent _keyboardAddedEvent;
    private readonly KeyboardRemovedEvent _keyboardRemovedEvent;
    private readonly MouseAddedEvent _mouseAddedEvent;
    private readonly MouseRemovedEvent _mouseRemovedEvent;

    /// <summary>
    /// Initializes a quit requested event.
    /// </summary>
    /// <param name="e">The quit event.</param>
    public Event(QuitRequestedEvent e)
    {
        _quitEvent = e;
        Type = SDL_EventType.Quit;
    }

    /// <summary>
    /// Initializes a Locale changed event.
    /// </summary>
    /// <param name="e">The locale changed event.</param>
    public Event(CultureChangedEvent e)
    {
        _localeChangedEvent = e;
        Type = SDL_EventType.LocaleChanged;
    }

    /// <summary>
    /// Initializes a theme changed event.
    /// </summary>
    /// <param name="e">The theme changed event.</param>
    public Event(ThemeChangedEvent e)
    {
        _themeChangedEvent = e;
        Type = SDL_EventType.SystemThemeChanged;
    }

    /// <summary>
    /// Initializes a keyboard added event.
    /// </summary>
    /// <param name="e">The keyboard added event.</param>
    public Event(KeyboardAddedEvent e)
    {
        _keyboardAddedEvent = e;
        Type = SDL_EventType.KeyboardAdded;
    }

    /// <summary>
    /// Initializes a keyboard removed event.
    /// </summary>
    /// <param name="e">The keyboard removed event.</param>
    public Event(KeyboardRemovedEvent e)
    {
        _keyboardRemovedEvent = e;
        Type = SDL_EventType.KeyboardRemoved;
    }

    /// <summary>
    /// Initializes a mouse added event.
    /// </summary>
    /// <param name="e">The mouse added event.</param>
    public Event(MouseAddedEvent e)
    {
        _mouseAddedEvent = e;
        Type = SDL_EventType.MouseAdded;
    }

    /// <summary>
    /// Initializes a mouse removed event.
    /// </summary>
    /// <param name="e">The mouse removed event.</param>
    public Event(MouseRemovedEvent e)
    {
        _mouseRemovedEvent = e;
        Type = SDL_EventType.MouseRemoved;
    }

    internal SDL_EventType Type { get; }

    /// <summary>
    /// Gets a value indicating whether this event holds a value or not.
    /// </summary>
    public bool HasValue => Type != EventType.None;

    /// <summary>
    /// Gets the underlying value by boxing or <see langword="null"/> if this event holds none.
    /// </summary>
    [SuppressMessage("Style", "IDE0072:Add missing cases", Justification = "The remaining types will be implemented in the future")]
    public object? Value => Type switch
    {
        SDL_EventType.Quit => _quitEvent,
        SDL_EventType.LocaleChanged => _localeChangedEvent,
        SDL_EventType.SystemThemeChanged => _themeChangedEvent,
        SDL_EventType.KeyboardAdded => _keyboardAddedEvent,
        SDL_EventType.KeyboardRemoved => _keyboardRemovedEvent,
        SDL_EventType.MouseAdded => _mouseAddedEvent,
        SDL_EventType.MouseRemoved => _mouseRemovedEvent,
        _ => null
    };

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="QuitRequestedEvent"/>.
    /// </summary>
    /// <param name="e">The quit requested event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="QuitRequestedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out QuitRequestedEvent e)
    {
        if (Type != SDL_EventType.Quit)
            return false;

        e = _quitEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="CultureChangedEvent"/>.
    /// </summary>
    /// <param name="e">The culture changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="CultureChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out CultureChangedEvent e)
    {
        if (Type != SDL_EventType.LocaleChanged)
            return false;

        e = _localeChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="ThemeChangedEvent"/>.
    /// </summary>
    /// <param name="e">The theme changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="ThemeChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out ThemeChangedEvent e)
    {
        if (Type != SDL_EventType.SystemThemeChanged)
            return false;

        e = _themeChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="KeyboardAddedEvent"/>.
    /// </summary>
    /// <param name="e">The keyboard added event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="KeyboardAddedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out KeyboardAddedEvent e)
    {
        if (Type != SDL_EventType.KeyboardAdded)
        {
            e = default;
            return false;
        }

        e = _keyboardAddedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="KeyboardRemovedEvent"/>.
    /// </summary>
    /// <param name="e">The keyboard removed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="KeyboardRemovedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out KeyboardRemovedEvent e)
    {
        if (Type != SDL_EventType.KeyboardRemoved)
        {
            e = default;
            return false;
        }

        e = _keyboardRemovedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="MouseAddedEvent"/>.
    /// </summary>
    /// <param name="e">The mouse added event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="MouseAddedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out MouseAddedEvent e)
    {
        if (Type != SDL_EventType.MouseAdded)
        {
            e = default;
            return false;
        }

        e = _mouseAddedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="MouseRemovedEvent"/>.
    /// </summary>
    /// <param name="e">The mouse removed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="MouseRemovedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out MouseRemovedEvent e)
    {
        if (Type != SDL_EventType.MouseRemoved)
        {
            e = default;
            return false;
        }

        e = _mouseRemovedEvent;
        return true;
    }
}

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
}

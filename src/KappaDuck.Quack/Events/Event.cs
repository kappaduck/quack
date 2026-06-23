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
    private readonly KeyPressedEvent _keyPressedEvent;
    private readonly KeyReleasedEvent _keyReleasedEvent;
    private readonly MouseButtonPressedEvent _buttonPressedEvent;
    private readonly MouseButtonReleasedEvent _buttonReleasedEvent;
    private readonly MouseMovedEvent _mouseMovedEvent;
    private readonly MouseWheelEvent _wheelEvent;
    private readonly DisplayAddedEvent _displayAddedEvent;
    private readonly DisplayRemovedEvent _displayRemovedEvent;
    private readonly DisplayMovedEvent _displayMovedEvent;
    private readonly DisplayContentScaleChangedEvent _displayContentScaleChangedEvent;
    private readonly DisplayCurrentModeChangedEvent _displayCurrentModeChangedEvent;
    private readonly DisplayDesktopModeChangedEvent _displayDesktopModeChangedEvent;
    private readonly DisplayOrientationChangedEvent _displayOrientationChangedEvent;
    private readonly DisplayUsableBoundsChangedEvent _displayUsableBoundsChangedEvent;

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

    /// <summary>
    /// Initializes a key pressed event.
    /// </summary>
    /// <param name="e">The key pressed event.</param>
    public Event(KeyPressedEvent e)
    {
        _keyPressedEvent = e;
        Type = SDL_EventType.KeyDown;
    }

    /// <summary>
    /// Initializes a key released event.
    /// </summary>
    /// <param name="e">The key released event.</param>
    public Event(KeyReleasedEvent e)
    {
        _keyReleasedEvent = e;
        Type = SDL_EventType.KeyUp;
    }

    /// <summary>
    /// Initializes a mouse button pressed event.
    /// </summary>
    /// <param name="e">The mouse button pressed event.</param>
    public Event(MouseButtonPressedEvent e)
    {
        _buttonPressedEvent = e;
        Type = SDL_EventType.MouseButtonDown;
    }

    /// <summary>
    /// Initializes a mouse button released event.
    /// </summary>
    /// <param name="e">The mouse button released event.</param>
    public Event(MouseButtonReleasedEvent e)
    {
        _buttonReleasedEvent = e;
        Type = SDL_EventType.MouseButtonUp;
    }

    /// <summary>
    /// Initializes a mouse moved event.
    /// </summary>
    /// <param name="e">The mouse moved event.</param>
    public Event(MouseMovedEvent e)
    {
        _mouseMovedEvent = e;
        Type = SDL_EventType.MouseMotion;
    }

    /// <summary>
    /// Initializes a mouse wheel event.
    /// </summary>
    /// <param name="e">The mouse wheel event.</param>
    public Event(MouseWheelEvent e)
    {
        _wheelEvent = e;
        Type = SDL_EventType.MouseWheel;
    }

    /// <summary>
    /// Initializes a display added event.
    /// </summary>
    /// <param name="e">The display added event.</param>
    public Event(DisplayAddedEvent e)
    {
        _displayAddedEvent = e;
        Type = SDL_EventType.DisplayAdded;
    }

    /// <summary>
    /// Initializes a display removed event.
    /// </summary>
    /// <param name="e">The display removed event.</param>
    public Event(DisplayRemovedEvent e)
    {
        _displayRemovedEvent = e;
        Type = SDL_EventType.DisplayRemoved;
    }

    /// <summary>
    /// Initializes a display moved event.
    /// </summary>
    /// <param name="e">The display moved event.</param>
    public Event(DisplayMovedEvent e)
    {
        _displayMovedEvent = e;
        Type = SDL_EventType.DisplayMoved;
    }

    /// <summary>
    /// Initializes a display content scale changed event.
    /// </summary>
    /// <param name="e">The display content scale changed event.</param>
    public Event(DisplayContentScaleChangedEvent e)
    {
        _displayContentScaleChangedEvent = e;
        Type = SDL_EventType.DisplayContentScaleChanged;
    }

    /// <summary>
    /// Initializes a display current mode changed event.
    /// </summary>
    /// <param name="e">The display current mode changed event.</param>
    public Event(DisplayCurrentModeChangedEvent e)
    {
        _displayCurrentModeChangedEvent = e;
        Type = SDL_EventType.DisplayCurrentModeChanged;
    }

    /// <summary>
    /// Initializes a display desktop mode changed event.
    /// </summary>
    /// <param name="e">The display desktop mode changed event.</param>
    public Event(DisplayDesktopModeChangedEvent e)
    {
        _displayDesktopModeChangedEvent = e;
        Type = SDL_EventType.DisplayDesktopModeChanged;
    }

    /// <summary>
    /// Initializes a display orientation changed event.
    /// </summary>
    /// <param name="e">The display orientation changed event.</param>
    public Event(DisplayOrientationChangedEvent e)
    {
        _displayOrientationChangedEvent = e;
        Type = SDL_EventType.DisplayOrientation;
    }

    /// <summary>
    /// Initializes a display usable bounds changed event.
    /// </summary>
    /// <param name="e">The display usable bounds changed event.</param>
    public Event(DisplayUsableBoundsChangedEvent e)
    {
        _displayUsableBoundsChangedEvent = e;
        Type = SDL_EventType.DisplayUsableBoundsChanged;
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
        SDL_EventType.KeyDown => _keyPressedEvent,
        SDL_EventType.KeyUp => _keyReleasedEvent,
        SDL_EventType.MouseButtonDown => _buttonPressedEvent,
        SDL_EventType.MouseButtonUp => _buttonReleasedEvent,
        SDL_EventType.MouseMotion => _mouseMovedEvent,
        SDL_EventType.MouseWheel => _wheelEvent,
        SDL_EventType.DisplayAdded => _displayAddedEvent,
        SDL_EventType.DisplayRemoved => _displayRemovedEvent,
        SDL_EventType.DisplayMoved => _displayMovedEvent,
        SDL_EventType.DisplayContentScaleChanged => _displayContentScaleChangedEvent,
        SDL_EventType.DisplayCurrentModeChanged => _displayCurrentModeChangedEvent,
        SDL_EventType.DisplayDesktopModeChanged => _displayDesktopModeChangedEvent,
        SDL_EventType.DisplayOrientation => _displayOrientationChangedEvent,
        SDL_EventType.DisplayUsableBoundsChanged => _displayUsableBoundsChangedEvent,
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

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="KeyPressedEvent"/>.
    /// </summary>
    /// <param name="e">The key pressed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="KeyPressedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out KeyPressedEvent e)
    {
        if (Type != SDL_EventType.KeyDown)
        {
            e = default;
            return false;
        }

        e = _keyPressedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="KeyReleasedEvent"/>.
    /// </summary>
    /// <param name="e">The key released event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="KeyReleasedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out KeyReleasedEvent e)
    {
        if (Type != SDL_EventType.KeyUp)
        {
            e = default;
            return false;
        }

        e = _keyReleasedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="MouseButtonPressedEvent"/>.
    /// </summary>
    /// <param name="e">The mouse button pressed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="MouseButtonPressedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out MouseButtonPressedEvent e)
    {
        if (Type != SDL_EventType.MouseButtonDown)
        {
            e = default;
            return false;
        }

        e = _buttonPressedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="MouseButtonReleasedEvent"/>.
    /// </summary>
    /// <param name="e">The mouse button released event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="MouseButtonReleasedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out MouseButtonReleasedEvent e)
    {
        if (Type != SDL_EventType.MouseButtonUp)
        {
            e = default;
            return false;
        }

        e = _buttonReleasedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="MouseMovedEvent"/>.
    /// </summary>
    /// <param name="e">The mouse moved event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="MouseMovedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out MouseMovedEvent e)
    {
        if (Type != SDL_EventType.MouseMotion)
        {
            e = default;
            return false;
        }

        e = _mouseMovedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="MouseWheelEvent"/>.
    /// </summary>
    /// <param name="e">The mouse wheel event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="MouseWheelEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out MouseWheelEvent e)
    {
        if (Type != SDL_EventType.KeyUp)
        {
            e = default;
            return false;
        }

        e = _wheelEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="DisplayAddedEvent"/>.
    /// </summary>
    /// <param name="e">The display added event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="DisplayAddedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out DisplayAddedEvent e)
    {
        if (Type != SDL_EventType.DisplayAdded)
        {
            e = default;
            return false;
        }

        e = _displayAddedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="DisplayRemovedEvent"/>.
    /// </summary>
    /// <param name="e">The display removed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="DisplayRemovedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out DisplayRemovedEvent e)
    {
        if (Type != SDL_EventType.DisplayRemoved)
        {
            e = default;
            return false;
        }

        e = _displayRemovedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="DisplayMovedEvent"/>.
    /// </summary>
    /// <param name="e">The display moved event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="DisplayMovedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out DisplayMovedEvent e)
    {
        if (Type != SDL_EventType.DisplayMoved)
        {
            e = default;
            return false;
        }

        e = _displayMovedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="DisplayContentScaleChangedEvent"/>.
    /// </summary>
    /// <param name="e">The display content scale changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="DisplayContentScaleChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out DisplayContentScaleChangedEvent e)
    {
        if (Type != SDL_EventType.DisplayContentScaleChanged)
        {
            e = default;
            return false;
        }

        e = _displayContentScaleChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="DisplayCurrentModeChangedEvent"/>.
    /// </summary>
    /// <param name="e">The display current mode changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="DisplayCurrentModeChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out DisplayCurrentModeChangedEvent e)
    {
        if (Type != SDL_EventType.DisplayCurrentModeChanged)
        {
            e = default;
            return false;
        }

        e = _displayCurrentModeChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="DisplayDesktopModeChangedEvent"/>.
    /// </summary>
    /// <param name="e">The display desktop mode changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="DisplayDesktopModeChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out DisplayDesktopModeChangedEvent e)
    {
        if (Type != SDL_EventType.DisplayDesktopModeChanged)
        {
            e = default;
            return false;
        }

        e = _displayDesktopModeChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="DisplayOrientationChangedEvent"/>.
    /// </summary>
    /// <param name="e">The display orientation changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="DisplayOrientationChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out DisplayOrientationChangedEvent e)
    {
        if (Type != SDL_EventType.DisplayOrientation)
        {
            e = default;
            return false;
        }

        e = _displayOrientationChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="DisplayUsableBoundsChangedEvent"/>.
    /// </summary>
    /// <param name="e">The display usable bounds changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="DisplayUsableBoundsChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out DisplayUsableBoundsChangedEvent e)
    {
        if (Type != SDL_EventType.DisplayUsableBoundsChanged)
        {
            e = default;
            return false;
        }

        e = _displayUsableBoundsChangedEvent;
        return true;
    }
}

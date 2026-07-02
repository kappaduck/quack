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
    private readonly WindowShownEvent _windowShownEvent;
    private readonly WindowHiddenEvent _windowHiddenEvent;
    private readonly WindowExposedEvent _windowExposedEvent;
    private readonly WindowMovedEvent _windowMovedEvent;
    private readonly WindowResizedEvent _windowResizedEvent;
    private readonly WindowPixelSizeChangedEvent _windowPixelSizeChangedEvent;
    private readonly WindowMinimizedEvent _windowMinimizedEvent;
    private readonly WindowMaximizedEvent _windowMaximizedEvent;
    private readonly WindowRestoredEvent _windowRestoredEvent;
    private readonly WindowMouseEnteredEvent _windowMouseEnteredEvent;
    private readonly WindowMouseLeftEvent _windowMouseLeftEvent;
    private readonly WindowFocusGainedEvent _windowFocusGainedEvent;
    private readonly WindowFocusLostEvent _windowFocusLostEvent;
    private readonly WindowCloseRequestedEvent _windowCloseRequestedEvent;
    private readonly WindowHitTestEvent _windowHitTestEvent;
    private readonly WindowIccProfileChangedEvent _windowIccProfileChangedEvent;
    private readonly WindowDisplayChangedEvent _windowDisplayChangedEvent;
    private readonly WindowDisplayScaleChangedEvent _windowDisplayScaleChangedEvent;
    private readonly WindowSafeAreaChangedEvent _windowSafeAreaChangedEvent;
    private readonly WindowOccludedEvent _windowOccludedEvent;
    private readonly WindowEnteredFullscreenEvent _windowEnteredFullscreenEvent;
    private readonly WindowLeftFullscreenEvent _windowLeftFullscreenEvent;
    private readonly WindowDestroyedEvent _windowDestroyedEvent;
    private readonly WindowHdrStateChangedEvent _windowHdrStateChangedEvent;
    private readonly RenderTargetsResetEvent _renderTargetsResetEvent;
    private readonly RenderDeviceResetEvent _renderDeviceResetEvent;
    private readonly RenderDeviceLostEvent _renderDeviceLostEvent;

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

    /// <summary>
    /// Initializes a window shown event.
    /// </summary>
    /// <param name="e">The window shown event.</param>
    public Event(WindowShownEvent e)
    {
        _windowShownEvent = e;
        Type = SDL_EventType.WindowShown;
    }

    /// <summary>
    /// Initializes a window hidden event.
    /// </summary>
    /// <param name="e">The window hidden event.</param>
    public Event(WindowHiddenEvent e)
    {
        _windowHiddenEvent = e;
        Type = SDL_EventType.WindowHidden;
    }

    /// <summary>
    /// Initializes a window exposed event.
    /// </summary>
    /// <param name="e">The window exposed event.</param>
    public Event(WindowExposedEvent e)
    {
        _windowExposedEvent = e;
        Type = SDL_EventType.WindowExposed;
    }

    /// <summary>
    /// Initializes a window moved event.
    /// </summary>
    /// <param name="e">The window moved event.</param>
    public Event(WindowMovedEvent e)
    {
        _windowMovedEvent = e;
        Type = SDL_EventType.WindowMoved;
    }

    /// <summary>
    /// Initializes a window resized event.
    /// </summary>
    /// <param name="e">The window resized event.</param>
    public Event(WindowResizedEvent e)
    {
        _windowResizedEvent = e;
        Type = SDL_EventType.WindowResized;
    }

    /// <summary>
    /// Initializes a window pixel size changed event.
    /// </summary>
    /// <param name="e">The window pixel size changed event.</param>
    public Event(WindowPixelSizeChangedEvent e)
    {
        _windowPixelSizeChangedEvent = e;
        Type = SDL_EventType.WindowPixelSizeChanged;
    }

    /// <summary>
    /// Initializes a window minimized event.
    /// </summary>
    /// <param name="e">The window minimized event.</param>
    public Event(WindowMinimizedEvent e)
    {
        _windowMinimizedEvent = e;
        Type = SDL_EventType.WindowMinimized;
    }

    /// <summary>
    /// Initializes a window maximized event.
    /// </summary>
    /// <param name="e">The window maximized event.</param>
    public Event(WindowMaximizedEvent e)
    {
        _windowMaximizedEvent = e;
        Type = SDL_EventType.WindowMaximized;
    }

    /// <summary>
    /// Initializes a window restored event.
    /// </summary>
    /// <param name="e">The window restored event.</param>
    public Event(WindowRestoredEvent e)
    {
        _windowRestoredEvent = e;
        Type = SDL_EventType.WindowRestored;
    }

    /// <summary>
    /// Initializes a window mouse entered event.
    /// </summary>
    /// <param name="e">The window mouse entered event.</param>
    public Event(WindowMouseEnteredEvent e)
    {
        _windowMouseEnteredEvent = e;
        Type = SDL_EventType.WindowMouseEnter;
    }

    /// <summary>
    /// Initializes a window mouse left event.
    /// </summary>
    /// <param name="e">The window mouse left event.</param>
    public Event(WindowMouseLeftEvent e)
    {
        _windowMouseLeftEvent = e;
        Type = SDL_EventType.WindowMouseLeave;
    }

    /// <summary>
    /// Initializes a window focus gained event.
    /// </summary>
    /// <param name="e">The window focus gained event.</param>
    public Event(WindowFocusGainedEvent e)
    {
        _windowFocusGainedEvent = e;
        Type = SDL_EventType.WindowFocusGained;
    }

    /// <summary>
    /// Initializes a window focus lost event.
    /// </summary>
    /// <param name="e">The window focus lost event.</param>
    public Event(WindowFocusLostEvent e)
    {
        _windowFocusLostEvent = e;
        Type = SDL_EventType.WindowFocusLost;
    }

    /// <summary>
    /// Initializes a window close requested event.
    /// </summary>
    /// <param name="e">The window close requested event.</param>
    public Event(WindowCloseRequestedEvent e)
    {
        _windowCloseRequestedEvent = e;
        Type = SDL_EventType.WindowCloseRequested;
    }

    /// <summary>
    /// Initializes a window hit test event.
    /// </summary>
    /// <param name="e">The window hit test event.</param>
    public Event(WindowHitTestEvent e)
    {
        _windowHitTestEvent = e;
        Type = SDL_EventType.WindowHitTest;
    }

    /// <summary>
    /// Initializes a window icc profile changed event.
    /// </summary>
    /// <param name="e">The window icc profile changed event.</param>
    public Event(WindowIccProfileChangedEvent e)
    {
        _windowIccProfileChangedEvent = e;
        Type = SDL_EventType.WindowIccProfileChanged;
    }

    /// <summary>
    /// Initializes a window display changed event.
    /// </summary>
    /// <param name="e">The window display changed event.</param>
    public Event(WindowDisplayChangedEvent e)
    {
        _windowDisplayChangedEvent = e;
        Type = SDL_EventType.WindowDisplayChanged;
    }

    /// <summary>
    /// Initializes a window display scale changed event.
    /// </summary>
    /// <param name="e">The window display scale changed event.</param>
    public Event(WindowDisplayScaleChangedEvent e)
    {
        _windowDisplayScaleChangedEvent = e;
        Type = SDL_EventType.WindowDisplayScaleChanged;
    }

    /// <summary>
    /// Initializes a window safe area changed event.
    /// </summary>
    /// <param name="e">The window safe area changed event.</param>
    public Event(WindowSafeAreaChangedEvent e)
    {
        _windowSafeAreaChangedEvent = e;
        Type = SDL_EventType.WindowSafeAreaChanged;
    }

    /// <summary>
    /// Initializes a window occluded event.
    /// </summary>
    /// <param name="e">The window occluded event.</param>
    public Event(WindowOccludedEvent e)
    {
        _windowOccludedEvent = e;
        Type = SDL_EventType.WindowOccluded;
    }

    /// <summary>
    /// Initializes a window entered fullscreen event.
    /// </summary>
    /// <param name="e">The window entered fullscreen event.</param>
    public Event(WindowEnteredFullscreenEvent e)
    {
        _windowEnteredFullscreenEvent = e;
        Type = SDL_EventType.WindowEnterFullscreen;
    }

    /// <summary>
    /// Initializes a window left fullscreen event.
    /// </summary>
    /// <param name="e">The window left fullscreen event.</param>
    public Event(WindowLeftFullscreenEvent e)
    {
        _windowLeftFullscreenEvent = e;
        Type = SDL_EventType.WindowLeaveFullscreen;
    }

    /// <summary>
    /// Initializes a window destroyed event.
    /// </summary>
    /// <param name="e">The window destroyed event.</param>
    public Event(WindowDestroyedEvent e)
    {
        _windowDestroyedEvent = e;
        Type = SDL_EventType.WindowDestroyed;
    }

    /// <summary>
    /// Initializes a window hdr state changed event.
    /// </summary>
    /// <param name="e">The window hdr state changed event.</param>
    public Event(WindowHdrStateChangedEvent e)
    {
        _windowHdrStateChangedEvent = e;
        Type = SDL_EventType.WindowHdrStateChanged;
    }

    /// <summary>
    /// Initializes a render targets reset event.
    /// </summary>
    /// <param name="e">The render targets reset event.</param>
    public Event(RenderTargetsResetEvent e)
    {
        _renderTargetsResetEvent = e;
        Type = SDL_EventType.RenderTargetsReset;
    }

    /// <summary>
    /// Initializes a render device reset event.
    /// </summary>
    /// <param name="e">The render device reset event.</param>
    public Event(RenderDeviceResetEvent e)
    {
        _renderDeviceResetEvent = e;
        Type = SDL_EventType.RenderDeviceReset;
    }

    /// <summary>
    /// Initializes a render device lost event.
    /// </summary>
    /// <param name="e">The render device lost event.</param>
    public Event(RenderDeviceLostEvent e)
    {
        _renderDeviceLostEvent = e;
        Type = SDL_EventType.RenderDeviceLost;
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
        SDL_EventType.WindowShown => _windowShownEvent,
        SDL_EventType.WindowHidden => _windowHiddenEvent,
        SDL_EventType.WindowExposed => _windowExposedEvent,
        SDL_EventType.WindowMoved => _windowMovedEvent,
        SDL_EventType.WindowResized => _windowResizedEvent,
        SDL_EventType.WindowPixelSizeChanged => _windowPixelSizeChangedEvent,
        SDL_EventType.WindowMinimized => _windowMinimizedEvent,
        SDL_EventType.WindowMaximized => _windowMaximizedEvent,
        SDL_EventType.WindowRestored => _windowRestoredEvent,
        SDL_EventType.WindowMouseEnter => _windowMouseEnteredEvent,
        SDL_EventType.WindowMouseLeave => _windowMouseLeftEvent,
        SDL_EventType.WindowFocusGained => _windowFocusGainedEvent,
        SDL_EventType.WindowFocusLost => _windowFocusLostEvent,
        SDL_EventType.WindowCloseRequested => _windowCloseRequestedEvent,
        SDL_EventType.WindowHitTest => _windowHitTestEvent,
        SDL_EventType.WindowIccProfileChanged => _windowIccProfileChangedEvent,
        SDL_EventType.WindowDisplayChanged => _windowDisplayChangedEvent,
        SDL_EventType.WindowDisplayScaleChanged => _windowDisplayScaleChangedEvent,
        SDL_EventType.WindowSafeAreaChanged => _windowSafeAreaChangedEvent,
        SDL_EventType.WindowOccluded => _windowOccludedEvent,
        SDL_EventType.WindowEnterFullscreen => _windowEnteredFullscreenEvent,
        SDL_EventType.WindowLeaveFullscreen => _windowLeftFullscreenEvent,
        SDL_EventType.WindowDestroyed => _windowDestroyedEvent,
        SDL_EventType.WindowHdrStateChanged => _windowHdrStateChangedEvent,
        SDL_EventType.RenderTargetsReset => _renderTargetsResetEvent,
        SDL_EventType.RenderDeviceReset => _renderDeviceResetEvent,
        SDL_EventType.RenderDeviceLost => _renderDeviceLostEvent,
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

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowShownEvent"/>.
    /// </summary>
    /// <param name="e">The window shown event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowShownEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowShownEvent e)
    {
        if (Type != SDL_EventType.WindowShown)
        {
            e = default;
            return false;
        }

        e = _windowShownEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowHiddenEvent"/>.
    /// </summary>
    /// <param name="e">The window hidden event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowHiddenEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowHiddenEvent e)
    {
        if (Type != SDL_EventType.WindowHidden)
        {
            e = default;
            return false;
        }

        e = _windowHiddenEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowExposedEvent"/>.
    /// </summary>
    /// <param name="e">The window exposed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowExposedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowExposedEvent e)
    {
        if (Type != SDL_EventType.WindowExposed)
        {
            e = default;
            return false;
        }

        e = _windowExposedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowMovedEvent"/>.
    /// </summary>
    /// <param name="e">The window moved event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowMovedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowMovedEvent e)
    {
        if (Type != SDL_EventType.WindowMoved)
        {
            e = default;
            return false;
        }

        e = _windowMovedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowResizedEvent"/>.
    /// </summary>
    /// <param name="e">The window resized event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowResizedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowResizedEvent e)
    {
        if (Type != SDL_EventType.WindowResized)
        {
            e = default;
            return false;
        }

        e = _windowResizedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowPixelSizeChangedEvent"/>.
    /// </summary>
    /// <param name="e">The window pixel size changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowPixelSizeChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowPixelSizeChangedEvent e)
    {
        if (Type != SDL_EventType.WindowPixelSizeChanged)
        {
            e = default;
            return false;
        }

        e = _windowPixelSizeChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowMinimizedEvent"/>.
    /// </summary>
    /// <param name="e">The window minimized event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowMinimizedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowMinimizedEvent e)
    {
        if (Type != SDL_EventType.WindowMinimized)
        {
            e = default;
            return false;
        }

        e = _windowMinimizedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowMaximizedEvent"/>.
    /// </summary>
    /// <param name="e">The window maximized event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowMaximizedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowMaximizedEvent e)
    {
        if (Type != SDL_EventType.WindowMaximized)
        {
            e = default;
            return false;
        }

        e = _windowMaximizedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowRestoredEvent"/>.
    /// </summary>
    /// <param name="e">The window restored event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowRestoredEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowRestoredEvent e)
    {
        if (Type != SDL_EventType.WindowRestored)
        {
            e = default;
            return false;
        }

        e = _windowRestoredEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowMouseEnteredEvent"/>.
    /// </summary>
    /// <param name="e">The window mouse entered event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowMouseEnteredEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowMouseEnteredEvent e)
    {
        if (Type != SDL_EventType.WindowMouseEnter)
        {
            e = default;
            return false;
        }

        e = _windowMouseEnteredEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowMouseLeftEvent"/>.
    /// </summary>
    /// <param name="e">The window mouse left event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowMouseLeftEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowMouseLeftEvent e)
    {
        if (Type != SDL_EventType.WindowMouseLeave)
        {
            e = default;
            return false;
        }

        e = _windowMouseLeftEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowFocusGainedEvent"/>.
    /// </summary>
    /// <param name="e">The window focus gained event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowFocusGainedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowFocusGainedEvent e)
    {
        if (Type != SDL_EventType.WindowFocusGained)
        {
            e = default;
            return false;
        }

        e = _windowFocusGainedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowFocusLostEvent"/>.
    /// </summary>
    /// <param name="e">The window focus lost event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowFocusLostEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowFocusLostEvent e)
    {
        if (Type != SDL_EventType.WindowFocusLost)
        {
            e = default;
            return false;
        }

        e = _windowFocusLostEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowCloseRequestedEvent"/>.
    /// </summary>
    /// <param name="e">The window close requested event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowCloseRequestedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowCloseRequestedEvent e)
    {
        if (Type != SDL_EventType.WindowCloseRequested)
        {
            e = default;
            return false;
        }

        e = _windowCloseRequestedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowHitTestEvent"/>.
    /// </summary>
    /// <param name="e">The window hit test event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowHitTestEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowHitTestEvent e)
    {
        if (Type != SDL_EventType.WindowHitTest)
        {
            e = default;
            return false;
        }

        e = _windowHitTestEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowIccProfileChangedEvent"/>.
    /// </summary>
    /// <param name="e">The window icc profile changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowIccProfileChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowIccProfileChangedEvent e)
    {
        if (Type != SDL_EventType.WindowIccProfileChanged)
        {
            e = default;
            return false;
        }

        e = _windowIccProfileChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowDisplayChangedEvent"/>.
    /// </summary>
    /// <param name="e">The window display changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowDisplayChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowDisplayChangedEvent e)
    {
        if (Type != SDL_EventType.WindowDisplayChanged)
        {
            e = default;
            return false;
        }

        e = _windowDisplayChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowDisplayScaleChangedEvent"/>.
    /// </summary>
    /// <param name="e">The window display scale changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowDisplayScaleChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowDisplayScaleChangedEvent e)
    {
        if (Type != SDL_EventType.WindowDisplayScaleChanged)
        {
            e = default;
            return false;
        }

        e = _windowDisplayScaleChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowSafeAreaChangedEvent"/>.
    /// </summary>
    /// <param name="e">The window safe area changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowSafeAreaChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowSafeAreaChangedEvent e)
    {
        if (Type != SDL_EventType.WindowSafeAreaChanged)
        {
            e = default;
            return false;
        }

        e = _windowSafeAreaChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowOccludedEvent"/>.
    /// </summary>
    /// <param name="e">The window occluded event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowOccludedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowOccludedEvent e)
    {
        if (Type != SDL_EventType.WindowOccluded)
        {
            e = default;
            return false;
        }

        e = _windowOccludedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowEnteredFullscreenEvent"/>.
    /// </summary>
    /// <param name="e">The window entered fullscreen event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowEnteredFullscreenEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowEnteredFullscreenEvent e)
    {
        if (Type != SDL_EventType.WindowEnterFullscreen)
        {
            e = default;
            return false;
        }

        e = _windowEnteredFullscreenEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowLeftFullscreenEvent"/>.
    /// </summary>
    /// <param name="e">The window left fullscreen event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowLeftFullscreenEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowLeftFullscreenEvent e)
    {
        if (Type != SDL_EventType.WindowLeaveFullscreen)
        {
            e = default;
            return false;
        }

        e = _windowLeftFullscreenEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowDestroyedEvent"/>.
    /// </summary>
    /// <param name="e">The window destroyed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowDestroyedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowDestroyedEvent e)
    {
        if (Type != SDL_EventType.WindowDestroyed)
        {
            e = default;
            return false;
        }

        e = _windowDestroyedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="WindowHdrStateChangedEvent"/>.
    /// </summary>
    /// <param name="e">The window hdr state changed event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="WindowHdrStateChangedEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out WindowHdrStateChangedEvent e)
    {
        if (Type != SDL_EventType.WindowHdrStateChanged)
        {
            e = default;
            return false;
        }

        e = _windowHdrStateChangedEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="RenderTargetsResetEvent"/>.
    /// </summary>
    /// <param name="e">The render targets reset event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="RenderTargetsResetEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out RenderTargetsResetEvent e)
    {
        if (Type != SDL_EventType.RenderTargetsReset)
        {
            e = default;
            return false;
        }

        e = _renderTargetsResetEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="RenderDeviceResetEvent"/>.
    /// </summary>
    /// <param name="e">The render device reset event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="RenderDeviceResetEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out RenderDeviceResetEvent e)
    {
        if (Type != SDL_EventType.RenderDeviceReset)
        {
            e = default;
            return false;
        }

        e = _renderDeviceResetEvent;
        return true;
    }

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="RenderDeviceLostEvent"/>.
    /// </summary>
    /// <param name="e">The render device lost event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="RenderDeviceLostEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out RenderDeviceLostEvent e)
    {
        if (Type != SDL_EventType.RenderDeviceLost)
        {
            e = default;
            return false;
        }

        e = _renderDeviceLostEvent;
        return true;
    }
}

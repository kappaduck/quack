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

        if (type == typeof(WindowShownEvent))
            return SDL_EventType.WindowShown;

        if (type == typeof(WindowHiddenEvent))
            return SDL_EventType.WindowHidden;

        if (type == typeof(WindowExposedEvent))
            return SDL_EventType.WindowExposed;

        if (type == typeof(WindowMovedEvent))
            return SDL_EventType.WindowMoved;

        if (type == typeof(WindowResizedEvent))
            return SDL_EventType.WindowResized;

        if (type == typeof(WindowPixelSizeChangedEvent))
            return SDL_EventType.WindowPixelSizeChanged;

        if (type == typeof(WindowMinimizedEvent))
            return SDL_EventType.WindowMinimized;

        if (type == typeof(WindowMaximizedEvent))
            return SDL_EventType.WindowMaximized;

        if (type == typeof(WindowRestoredEvent))
            return SDL_EventType.WindowRestored;

        if (type == typeof(WindowMouseEnteredEvent))
            return SDL_EventType.WindowMouseEnter;

        if (type == typeof(WindowMouseLeftEvent))
            return SDL_EventType.WindowMouseLeave;

        if (type == typeof(WindowFocusGainedEvent))
            return SDL_EventType.WindowFocusGained;

        if (type == typeof(WindowFocusLostEvent))
            return SDL_EventType.WindowFocusLost;

        if (type == typeof(WindowCloseRequestedEvent))
            return SDL_EventType.WindowCloseRequested;

        if (type == typeof(WindowHitTestEvent))
            return SDL_EventType.WindowHitTest;

        if (type == typeof(WindowIccProfileChangedEvent))
            return SDL_EventType.WindowIccProfileChanged;

        if (type == typeof(WindowDisplayChangedEvent))
            return SDL_EventType.WindowDisplayChanged;

        if (type == typeof(WindowDisplayScaleChangedEvent))
            return SDL_EventType.WindowDisplayScaleChanged;

        if (type == typeof(WindowSafeAreaChangedEvent))
            return SDL_EventType.WindowSafeAreaChanged;

        if (type == typeof(WindowOccludedEvent))
            return SDL_EventType.WindowOccluded;

        if (type == typeof(WindowEnteredFullscreenEvent))
            return SDL_EventType.WindowEnterFullscreen;

        if (type == typeof(WindowLeftFullscreenEvent))
            return SDL_EventType.WindowLeaveFullscreen;

        if (type == typeof(WindowDestroyedEvent))
            return SDL_EventType.WindowDestroyed;

        if (type == typeof(WindowHdrStateChangedEvent))
            return SDL_EventType.WindowHdrStateChanged;

        if (type == typeof(RenderTargetsResetEvent))
            return SDL_EventType.RenderTargetsReset;

        if (type == typeof(RenderDeviceResetEvent))
            return SDL_EventType.RenderDeviceReset;

        if (type == typeof(RenderDeviceLostEvent))
            return SDL_EventType.RenderDeviceLost;

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
        SDL_EventType.WindowShown => new WindowShownEvent(e.Window),
        SDL_EventType.WindowHidden => new WindowHiddenEvent(e.Window),
        SDL_EventType.WindowExposed => new WindowExposedEvent(e.Window),
        SDL_EventType.WindowMoved => new WindowMovedEvent(e.Window),
        SDL_EventType.WindowResized => new WindowResizedEvent(e.Window),
        SDL_EventType.WindowPixelSizeChanged => new WindowPixelSizeChangedEvent(e.Window),
        SDL_EventType.WindowMinimized => new WindowMinimizedEvent(e.Window),
        SDL_EventType.WindowMaximized => new WindowMaximizedEvent(e.Window),
        SDL_EventType.WindowRestored => new WindowRestoredEvent(e.Window),
        SDL_EventType.WindowMouseEnter => new WindowMouseEnteredEvent(e.Window),
        SDL_EventType.WindowMouseLeave => new WindowMouseLeftEvent(e.Window),
        SDL_EventType.WindowFocusGained => new WindowFocusGainedEvent(e.Window),
        SDL_EventType.WindowFocusLost => new WindowFocusLostEvent(e.Window),
        SDL_EventType.WindowCloseRequested => new WindowCloseRequestedEvent(e.Window),
        SDL_EventType.WindowHitTest => new WindowHitTestEvent(e.Window),
        SDL_EventType.WindowIccProfileChanged => new WindowIccProfileChangedEvent(e.Window),
        SDL_EventType.WindowDisplayChanged => new WindowDisplayChangedEvent(e.Window),
        SDL_EventType.WindowDisplayScaleChanged => new WindowDisplayScaleChangedEvent(e.Window),
        SDL_EventType.WindowSafeAreaChanged => new WindowSafeAreaChangedEvent(e.Window),
        SDL_EventType.WindowOccluded => new WindowOccludedEvent(e.Window),
        SDL_EventType.WindowEnterFullscreen => new WindowEnteredFullscreenEvent(e.Window),
        SDL_EventType.WindowLeaveFullscreen => new WindowLeftFullscreenEvent(e.Window),
        SDL_EventType.WindowDestroyed => new WindowDestroyedEvent(e.Window),
        SDL_EventType.WindowHdrStateChanged => new WindowHdrStateChangedEvent(e.Window),
        SDL_EventType.RenderTargetsReset => new RenderTargetsResetEvent(e.Render),
        SDL_EventType.RenderDeviceReset => new RenderDeviceResetEvent(e.Render),
        SDL_EventType.RenderDeviceLost => new RenderDeviceLostEvent(e.Render),
        _ => default
    };
}

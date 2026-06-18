// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives.Events;

/// <summary>
/// Identifies the kind of an <see cref="SDL_Event"/>. Values mirror SDL's event numbering
/// so conversion to and from the native layer is a direct cast.
/// </summary>
[SuppressMessage("Roslynator", "RCS1161:Enum should declare explicit values", Justification = "It is better to auto-increment that doing manually because SDL can add a new event type in the futur.")]
internal enum SDL_EventType : uint
{
    /// <summary>
    /// The user requested that the application quit.
    /// </summary>
    Quit = 0x100,

    /// <summary>
    /// The application is being terminated by the OS.
    /// </summary>
    Terminating,

    /// <summary>
    /// The application is low on memory; free memory if possible.
    /// </summary>
    LowMemory,

    /// <summary>
    /// The application is about to enter the background.
    /// </summary>
    WillEnterBackground,

    /// <summary>
    /// The application entered the background and may not get CPU time for a while.
    /// </summary>
    DidEnterBackground,

    /// <summary>
    /// The application is about to enter the foreground.
    /// </summary>
    WillEnterForeground,

    /// <summary>
    /// The application is now interactive.
    /// </summary>
    DidEnterForeground,

    /// <summary>
    /// The user's locale preferences changed.
    /// </summary>
    LocaleChanged,

    /// <summary>
    /// The system theme changed.
    /// </summary>
    SystemThemeChanged,

    /// <summary>
    /// A display's orientation changed.
    /// </summary>
    DisplayOrientation = 0x151,

    /// <summary>
    /// A display was added to the system.
    /// </summary>
    DisplayAdded,

    /// <summary>
    /// A display was removed from the system.
    /// </summary>
    DisplayRemoved,

    /// <summary>
    /// A display changed position.
    /// </summary>
    DisplayMoved,

    /// <summary>
    /// A display's desktop mode changed.
    /// </summary>
    DisplayDesktopModeChanged,

    /// <summary>
    /// A display's current mode changed.
    /// </summary>
    DisplayCurrentModeChanged,

    /// <summary>
    /// A display's content scale changed.
    /// </summary>
    DisplayContentScaleChanged,

    /// <summary>
    /// A display's usable bounds changed.
    /// </summary>
    DisplayUsableBoundsChanged,

    /// <summary>
    /// A window was shown.
    /// </summary>
    WindowShown = 0x202,

    /// <summary>
    /// A window was hidden.
    /// </summary>
    WindowHidden,

    /// <summary>
    /// A window was exposed and should be redrawn.
    /// </summary>
    WindowExposed,

    /// <summary>
    /// A window was moved.
    /// </summary>
    WindowMoved,

    /// <summary>
    /// A window was resized.
    /// </summary>
    WindowResized,

    /// <summary>
    /// A window's pixel size changed.
    /// </summary>
    WindowPixelSizeChanged,

    /// <summary>
    /// The pixel size of a window's Metal view changed.
    /// </summary>
    WindowMetalViewResized,

    /// <summary>
    /// A window was minimized.
    /// </summary>
    WindowMinimized,

    /// <summary>
    /// A window was maximized.
    /// </summary>
    WindowMaximized,

    /// <summary>
    /// A window was restored to normal size and position.
    /// </summary>
    WindowRestored,

    /// <summary>
    /// A window gained mouse focus.
    /// </summary>
    WindowMouseEnter,

    /// <summary>
    /// A window lost mouse focus.
    /// </summary>
    WindowMouseLeave,

    /// <summary>
    /// A window gained keyboard focus.
    /// </summary>
    WindowFocusGained,

    /// <summary>
    /// A window lost keyboard focus.
    /// </summary>
    WindowFocusLost,

    /// <summary>
    /// The window manager requested that a window be closed.
    /// </summary>
    WindowCloseRequested,

    /// <summary>
    /// A window had a hit test that wasn't normal.
    /// </summary>
    WindowHitTest,

    /// <summary>
    /// A window's ICC profile changed.
    /// </summary>
    WindowIccProfileChanged,

    /// <summary>
    /// A window was moved to a different display.
    /// </summary>
    WindowDisplayChanged,

    /// <summary>
    /// A window's display scale changed.
    /// </summary>
    WindowDisplayScaleChanged,

    /// <summary>
    /// A window's safe area changed.
    /// </summary>
    WindowSafeAreaChanged,

    /// <summary>
    /// A window was occluded.
    /// </summary>
    WindowOccluded,

    /// <summary>
    /// A window entered fullscreen mode.
    /// </summary>
    WindowEnterFullscreen,

    /// <summary>
    /// A window left fullscreen mode.
    /// </summary>
    WindowLeaveFullscreen,

    /// <summary>
    /// A window is being or has been destroyed.
    /// </summary>
    WindowDestroyed,

    /// <summary>
    /// A window's HDR properties changed.
    /// </summary>
    WindowHdrStateChanged,

    /// <summary>
    /// A window's settings changed.
    /// </summary>
    WindowSettingsChanged,

    /// <summary>
    /// A key was pressed.
    /// </summary>
    KeyDown = 0x300,

    /// <summary>
    /// A key was released.
    /// </summary>
    KeyUp,

    /// <summary>
    /// Keyboard text editing (composition).
    /// </summary>
    TextEditing,

    /// <summary>
    /// Keyboard text input.
    /// </summary>
    TextInput,

    /// <summary>
    /// The keymap changed due to a system event such as a layout change.
    /// </summary>
    KeymapChanged,

    /// <summary>
    /// A keyboard was connected.
    /// </summary>
    KeyboardAdded,

    /// <summary>
    /// A keyboard was removed.
    /// </summary>
    KeyboardRemoved,

    /// <summary>
    /// Keyboard text editing candidates.
    /// </summary>
    TextEditingCandidates,

    /// <summary>
    /// The on-screen keyboard was shown.
    /// </summary>
    ScreenKeyboardShown,

    /// <summary>
    /// The on-screen keyboard was hidden.
    /// </summary>
    ScreenKeyboardHidden,

    /// <summary>
    /// The mouse moved.
    /// </summary>
    MouseMotion = 0x400,

    /// <summary>
    /// A mouse button was pressed.
    /// </summary>
    MouseButtonDown,

    /// <summary>
    /// A mouse button was released.
    /// </summary>
    MouseButtonUp,

    /// <summary>
    /// The mouse wheel moved.
    /// </summary>
    MouseWheel,

    /// <summary>
    /// A mouse was connected.
    /// </summary>
    MouseAdded,

    /// <summary>
    /// A mouse was removed.
    /// </summary>
    MouseRemoved,

    /// <summary>
    /// A joystick axis moved.
    /// </summary>
    JoystickAxisMotion = 0x600,

    /// <summary>
    /// A joystick trackball moved.
    /// </summary>
    JoystickBallMotion,

    /// <summary>
    /// A joystick hat position changed.
    /// </summary>
    JoystickHatMotion,

    /// <summary>
    /// A joystick button was pressed.
    /// </summary>
    JoystickButtonDown,

    /// <summary>
    /// A joystick button was released.
    /// </summary>
    JoystickButtonUp,

    /// <summary>
    /// A joystick was connected.
    /// </summary>
    JoystickAdded,

    /// <summary>
    /// A joystick was removed.
    /// </summary>
    JoystickRemoved,

    /// <summary>
    /// A joystick battery level changed.
    /// </summary>
    JoystickBatteryUpdated,

    /// <summary>
    /// A joystick update completed.
    /// </summary>
    JoystickUpdateComplete,

    /// <summary>
    /// A gamepad axis moved.
    /// </summary>
    GamepadAxisMotion = 0x650,

    /// <summary>
    /// A gamepad button was pressed.
    /// </summary>
    GamepadButtonDown,

    /// <summary>
    /// A gamepad button was released.
    /// </summary>
    GamepadButtonUp,

    /// <summary>
    /// A gamepad was connected.
    /// </summary>
    GamepadAdded,

    /// <summary>
    /// A gamepad was removed.
    /// </summary>
    GamepadRemoved,

    /// <summary>
    /// A gamepad mapping was updated.
    /// </summary>
    GamepadRemapped,

    /// <summary>
    /// A gamepad touchpad was touched.
    /// </summary>
    GamepadTouchpadDown,

    /// <summary>
    /// A gamepad touchpad finger moved.
    /// </summary>
    GamepadTouchpadMotion,

    /// <summary>
    /// A gamepad touchpad finger was lifted.
    /// </summary>
    GamepadTouchpadUp,

    /// <summary>
    /// A gamepad sensor was updated.
    /// </summary>
    GamepadSensorUpdate,

    /// <summary>
    /// A gamepad update completed.
    /// </summary>
    GamepadUpdateComplete,

    /// <summary>
    /// A gamepad's Steam handle changed.
    /// </summary>
    GamepadSteamHandleUpdated,

    /// <summary>
    /// A gamepad capsense was touched.
    /// </summary>
    GamepadCapsenseTouch,

    /// <summary>
    /// A gamepad capsense was released.
    /// </summary>
    GamepadCapsenseRelease,

    /// <summary>
    /// A finger touched the touch device.
    /// </summary>
    FingerDown = 0x700,

    /// <summary>
    /// A finger was lifted from the touch device.
    /// </summary>
    FingerUp,

    /// <summary>
    /// A finger moved on the touch device.
    /// </summary>
    FingerMotion,

    /// <summary>
    /// A finger touch was canceled.
    /// </summary>
    FingerCanceled,

    /// <summary>
    /// A pinch gesture started.
    /// </summary>
    PinchBegin = 0x710,

    /// <summary>
    /// A pinch gesture updated.
    /// </summary>
    PinchUpdate,

    /// <summary>
    /// A pinch gesture ended.
    /// </summary>
    PinchEnd,

    /// <summary>
    /// The clipboard contents changed.
    /// </summary>
    ClipboardUpdate = 0x900,

    /// <summary>
    /// The system requests a file open (drag-and-drop).
    /// </summary>
    DropFile = 0x1000,

    /// <summary>
    /// A text/plain drag-and-drop occurred.
    /// </summary>
    DropText,

    /// <summary>
    /// A new set of drops is beginning.
    /// </summary>
    DropBegin,

    /// <summary>
    /// The current set of drops is complete.
    /// </summary>
    DropComplete,

    /// <summary>
    /// The drop position moved over the window.
    /// </summary>
    DropPosition,

    /// <summary>
    /// A new audio device is available.
    /// </summary>
    AudioDeviceAdded = 0x1100,

    /// <summary>
    /// An audio device was removed.
    /// </summary>
    AudioDeviceRemoved,

    /// <summary>
    /// An audio device's format was changed by the system.
    /// </summary>
    AudioDeviceFormatChanged,

    /// <summary>
    /// A sensor was updated.
    /// </summary>
    SensorUpdate = 0x1200,

    /// <summary>
    /// A pen became available.
    /// </summary>
    PenProximityIn = 0x1300,

    /// <summary>
    /// A pen became unavailable.
    /// </summary>
    PenProximityOut,

    /// <summary>
    /// A pen touched the drawing surface.
    /// </summary>
    PenDown,

    /// <summary>
    /// A pen stopped touching the drawing surface.
    /// </summary>
    PenUp,

    /// <summary>
    /// A pen button was pressed.
    /// </summary>
    PenButtonDown,

    /// <summary>
    /// A pen button was released.
    /// </summary>
    PenButtonUp,

    /// <summary>
    /// A pen is moving on the tablet.
    /// </summary>
    PenMotion,

    /// <summary>
    /// A pen's angle, pressure, or other axis changed.
    /// </summary>
    PenAxis,

    /// <summary>
    /// A new camera device is available.
    /// </summary>
    CameraDeviceAdded = 0x1400,

    /// <summary>
    /// A camera device was removed.
    /// </summary>
    CameraDeviceRemoved,

    /// <summary>
    /// A camera device was approved for use by the user.
    /// </summary>
    CameraDeviceApproved,

    /// <summary>
    /// A camera device was denied use by the user.
    /// </summary>
    CameraDeviceDenied,

    /// <summary>
    /// A response to a system notification was received.
    /// </summary>
    NotificationActionInvoked = 0x1500,

    /// <summary>
    /// The render targets were reset and their contents need updating.
    /// </summary>
    RenderTargetsReset = 0x2000,

    /// <summary>
    /// The render device was reset and all textures need recreating.
    /// </summary>
    RenderDeviceReset,

    /// <summary>
    /// The render device was lost and can't be recovered.
    /// </summary>
    RenderDeviceLost
}

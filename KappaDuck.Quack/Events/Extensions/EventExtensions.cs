// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Input.Keyboard;
using KappaDuck.Quack.Input.Mouse;

namespace KappaDuck.Quack.Events.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Event"/>.
/// </summary>
public static class EventExtensions
{
    extension(Event e)
    {
        /// <summary>
        /// Determines whether the specified key is currently pressed and the event is a <see cref="EventType.KeyDown"/>.
        /// </summary>
        /// <param name="code">The code to compare.</param>
        /// <returns><see langword="true"/> if the specified key is pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyDown(Scancode code) => e.Type is EventType.KeyDown && e.Keyboard.Code == code;

        /// <summary>
        /// Determines whether the specified key is currently pressed and the event is a <see cref="EventType.KeyDown"/>.
        /// </summary>
        /// <param name="mod">The modifier to compare.</param>
        /// <param name="code">The code to compare.</param>
        /// <returns><see langword="true"/> if the specified key is pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyDown(Modifier mod, Scancode code) => e.Type is EventType.KeyDown && e.Keyboard.Code == code && (e.Keyboard.Modifiers & mod) == mod;

        /// <summary>
        /// Determines whether the specified key is currently pressed and the event is a <see cref="EventType.KeyDown"/>.
        /// </summary>
        /// <param name="key">The key to compare.</param>
        /// <returns><see langword="true"/> if the specified key is pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyDown(Keycode key) => e.Type is EventType.KeyDown && e.Keyboard.Key == key;

        /// <summary>
        /// Determines whether the specified key is currently pressed and the event is a <see cref="EventType.KeyDown"/>.
        /// </summary>
        /// <param name="mod">The modifier to compare.</param>
        /// <param name="key">The key to compare.</param>
        /// <returns><see langword="true"/> if the specified key is pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyDown(Modifier mod, Keycode key) => e.Type is EventType.KeyDown && e.Keyboard.Key == key && (e.Keyboard.Modifiers & mod) == mod;

        /// <summary>
        /// Determines whether the specified key was released and the event is a <see cref="EventType.KeyUp"/>.
        /// </summary>
        /// <param name="code">The code to compare.</param>
        /// <returns><see langword="true"/> if the specified key is released; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyUp(Scancode code) => e.Type is EventType.KeyUp && e.Keyboard.Code == code;

        /// <summary>
        /// Determines whether the specified key was released and the event is a <see cref="EventType.KeyUp"/>.
        /// </summary>
        /// <param name="mod">The modifier to compare.</param>
        /// <param name="code">The code to compare.</param>
        /// <returns><see langword="true"/> if the specified key is released; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyUp(Modifier mod, Scancode code) => e.Type is EventType.KeyUp && e.Keyboard.Code == code && (e.Keyboard.Modifiers & mod) == mod;

        /// <summary>
        /// Determines whether the specified key was released and the event is a <see cref="EventType.KeyUp"/>.
        /// </summary>
        /// <param name="key">The key to compare.</param>
        /// <returns><see langword="true"/> if the specified key is released; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyUp(Keycode key) => e.Type is EventType.KeyUp && e.Keyboard.Key == key;

        /// <summary>
        /// Determines whether the specified key was released and the event is a <see cref="EventType.KeyUp"/>.
        /// </summary>
        /// <param name="mod">The modifier to compare.</param>
        /// <param name="key">The key to compare.</param>
        /// <returns><see langword="true"/> if the specified key is released; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyUp(Modifier mod, Keycode key) => e.Type is EventType.KeyUp && e.Keyboard.Key == key && (e.Keyboard.Modifiers & mod) == mod;

        /// <summary>
        /// Determines whether the specified mouse button is currently pressed and the event is a <see cref="EventType.MouseButtonDown"/>.
        /// </summary>
        /// <param name="button">The button to compare.</param>
        /// <returns><see langword="true"/> if the specified mouse button is currently pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsButtonDown(MouseButton button) => e.Type is EventType.MouseButtonDown && e.Mouse.Button == button;

        /// <summary>
        /// Determines whether the specified mouse button was released and the event is a <see cref="EventType.MouseButtonUp"/>.
        /// </summary>
        /// <param name="button">The button to compare.</param>
        /// <returns><see langword="true"/> if the specified mouse button was released; otherwise, <see langword="false"/>.</returns>
        public bool IsButtonUp(MouseButton button) => e.Type is EventType.MouseButtonUp && e.Mouse.Button == button;

        /// <summary>
        /// Determines whether the mouse has moved.
        /// </summary>
        /// <param name="position">The position of the mouse cursor if it has moved; otherwise, <see cref="Vector2.Zero"/>.</param>
        /// <returns><see langword="true"/> if the mouse has moved; otherwise, <see langword="false"/>.</returns>
        public bool IsMouseMoved(out Vector2 position)
        {
            if (e.Type is EventType.MouseMotion)
            {
                position = e.Motion.Position;
                return true;
            }

            position = Vector2.Zero;
            return false;
        }

        /// <summary>
        /// Determines whether a quit request has been made, either by a specific key press or by a <see cref="EventType.Quit"/> or <see cref="EventType.WindowCloseRequested"/> event.
        /// </summary>
        /// <remarks>
        /// <para>By default, it checks if the <see cref="Scancode.Escape"/> key is pressed.</para>
        /// <para>
        /// The window close request is only considered if a <paramref name="windowId"/> is provided.
        /// It helps to know which window is requesting to close in multi-window applications.
        /// </para>
        /// </remarks>
        /// <param name="code">The code to quit.</param>
        /// <param name="windowId">The identifier of the window to monitor the close request.</param>
        /// <returns>true if a quit request is detected by the specified key or window close event; otherwise, false.</returns>
        [OverloadResolutionPriority(1)]
        public bool QuitRequested(Scancode code = Scancode.Escape, uint? windowId = null)
        {
            bool quit = e.Type is EventType.Quit || (e.Type is EventType.WindowCloseRequested && e.Window.Id == windowId);
            return quit || IsKeyDown(e, code);
        }

        /// <summary>
        /// Determines whether a quit request has been made, either by a specific key press or by a <see cref="EventType.Quit"/> or <see cref="EventType.WindowCloseRequested"/> event.
        /// </summary>
        /// <remarks>
        /// <para>By default, it checks if the <see cref="Keycode.Escape"/> key is pressed.</para>
        /// <para>
        /// The window close request is only considered if a <paramref name="windowId"/> is provided.
        /// It helps to know which window is requesting to close in multi-window applications.
        /// </para>
        /// </remarks>
        /// <param name="key">The key to quit.</param>
        /// <param name="windowId">The identifier of the window to monitor the close request.</param>
        /// <returns>true if a quit request is detected by the specified key or window close event; otherwise, false.</returns>
        public bool QuitRequested(Keycode key = Keycode.Escape, uint? windowId = null)
        {
            bool quit = e.Type is EventType.Quit || (e.Type is EventType.WindowCloseRequested && e.Window.Id == windowId);
            return quit || IsKeyDown(e, key);
        }

        /// <summary>
        /// Tries to get the display event data if the event is a display-related event.
        /// </summary>
        /// <param name="display">The event data for display-related events.</param>
        /// <returns><see langword="true"/> if the event is a display-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out DisplayEvent display)
        {
            if (IsDisplayEvent(e.Type))
            {
                display = e.Display;
                return true;
            }

            display = default;
            return false;

            static bool IsDisplayEvent(EventType type)
            {
                return type is EventType.ContentScaleChanged
                    or EventType.CurrentModeChanged
                    or EventType.DesktopModeChanged
                    or EventType.DisplayAdded
                    or EventType.DisplayMoved
                    or EventType.DisplayOrientationChanged
                    or EventType.DisplayRemoved
                    or EventType.UsableBoundsChanged;
            }
        }

        /// <summary>
        /// Tries to get the keyboard device event data if the event is a keyboard device-related event.
        /// </summary>
        /// <param name="device">The event data for keyboard device-related events.</param>
        /// <returns><see langword="true"/> if the event is a keyboard device-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out KeyboardDeviceEvent device)
        {
            if (e.Type is EventType.KeyboardAdded or EventType.KeyboardRemoved)
            {
                device = e.KeyboardDevice;
                return true;
            }

            device = default;
            return false;
        }

        /// <summary>
        /// Tries to get the keyboard event data if the event is a keyboard-related event.
        /// </summary>
        /// <param name="keyboard">The event data for keyboard-related events.</param>
        /// <returns><see langword="true"/> if the event is a keyboard-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out KeyboardEvent keyboard)
        {
            if (e.Type is EventType.KeyDown or EventType.KeyUp)
            {
                keyboard = e.Keyboard;
                return true;
            }

            keyboard = default;
            return false;
        }

        /// <summary>
        /// Tries to get the mouse button event data if the event is a mouse button-related event.
        /// </summary>
        /// <param name="button">The event data for mouse button-related events.</param>
        /// <returns><see langword="true"/> if the event is a mouse button-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out MouseButtonEvent button)
        {
            if (e.Type is EventType.MouseButtonDown or EventType.MouseButtonUp)
            {
                button = e.Mouse;
                return true;
            }

            button = default;
            return false;
        }

        /// <summary>
        /// Tries to get the mouse device event data if the event is a mouse device-related event.
        /// </summary>
        /// <param name="device">The event data for mouse device-related events.</param>
        /// <returns><see langword="true"/> if the event is a mouse device-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out MouseDeviceEvent device)
        {
            if (e.Type is EventType.MouseAdded or EventType.MouseRemoved)
            {
                device = e.MouseDevice;
                return true;
            }

            device = default;
            return false;
        }

        /// <summary>
        /// Tries to get the mouse motion event data if the event is a mouse motion-related event.
        /// </summary>
        /// <param name="motion">The event data for mouse motion-related events.</param>
        /// <returns><see langword="true"/> if the event is a mouse motion-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out MouseMotionEvent motion)
        {
            if (e.Type is EventType.MouseMotion)
            {
                motion = e.Motion;
                return true;
            }

            motion = default;
            return false;
        }

        /// <summary>
        /// Tries to get the mouse wheel event data if the event is a mouse wheel-related event.
        /// </summary>
        /// <param name="wheel">The event data for mouse wheel-related events.</param>
        /// <returns><see langword="true"/> if the event is a mouse wheel-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out MouseWheelEvent wheel)
        {
            if (e.Type is EventType.MouseWheel)
            {
                wheel = e.Wheel;
                return true;
            }

            wheel = default;
            return false;
        }

        /// <summary>
        /// Tries to get the renderer event data if the event is a renderer-related event.
        /// </summary>
        /// <param name="renderer">The event data for renderer-related events.</param>
        /// <returns><see langword="true"/> if the event is a renderer-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out RendererEvent renderer)
        {
            if (e.Type is EventType.RenderDeviceLost or EventType.RenderDeviceReset or EventType.RenderTargetsReset)
            {
                renderer = e.Renderer;
                return true;
            }

            renderer = default;
            return false;
        }

        /// <summary>
        /// Tries to get the window event data if the event is a window-related event.
        /// </summary>
        /// <param name="window">The event data for window-related events.</param>
        /// <returns><see langword="true"/> if the event is a window-related event; otherwise, <see langword="false"/>.</returns>
        public bool TryGet(out WindowEvent window)
        {
            if (IsWindowEvent(e.Type))
            {
                window = e.Window;
                return true;
            }

            window = default;
            return false;

            static bool IsWindowEvent(EventType type)
            {
                return type is EventType.EnterFullScreen
                    or EventType.FocusGained
                    or EventType.FocusLost
                    or EventType.HdrStateChanged
                    or EventType.IccProfileChanged
                    or EventType.LeaveFullScreen
                    or EventType.MouseEnter
                    or EventType.MouseLeave
                    or EventType.WindowCloseRequested
                    or EventType.WindowDestroyed
                    or EventType.WindowDisplayChanged
                    or EventType.WindowDisplayScaleChanged
                    or EventType.WindowExposed
                    or EventType.WindowHidden
                    or EventType.WindowHitTest
                    or EventType.WindowMaximized
                    or EventType.WindowMinimized
                    or EventType.WindowMoved
                    or EventType.WindowOccluded
                    or EventType.WindowPixelSizeChanged
                    or EventType.WindowResized
                    or EventType.WindowRestored
                    or EventType.WindowSafeAreaChanged
                    or EventType.WindowShown;
            }
        }
    }
}

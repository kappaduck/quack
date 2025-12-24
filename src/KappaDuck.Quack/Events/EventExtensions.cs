// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Inputs;

namespace KappaDuck.Quack.Events;

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
        public bool IsKeyDown(Keyboard.Scancode code) => e.Type is EventType.KeyDown && e.Keyboard.Code == code;

        /// <summary>
        /// Determines whether the specified key is currently pressed and the event is a <see cref="EventType.KeyDown"/>.
        /// </summary>
        /// <param name="key">The key to compare.</param>
        /// <returns><see langword="true"/> if the specified key is pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyDown(Keyboard.Keycode key) => e.Type is EventType.KeyDown && e.Keyboard.Key == key;

        /// <summary>
        /// Determines whether the specified key was released and the event is a <see cref="EventType.KeyUp"/>.
        /// </summary>
        /// <param name="code">The code to compare.</param>
        /// <returns><see langword="true"/> if the specified key is released; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyUp(Keyboard.Scancode code) => e.Type is EventType.KeyUp && e.Keyboard.Code == code;

        /// <summary>
        /// Determines whether the specified key was released and the event is a <see cref="EventType.KeyUp"/>.
        /// </summary>
        /// <param name="key">The key to compare.</param>
        /// <returns><see langword="true"/> if the specified key is released; otherwise, <see langword="false"/>.</returns>
        public bool IsKeyUp(Keyboard.Keycode key) => e.Type is EventType.KeyUp && e.Keyboard.Key == key;

        /// <summary>
        /// Determines whether the specified mouse button is currently pressed and the event is a <see cref="EventType.MouseButtonDown"/>.
        /// </summary>
        /// <param name="button">The button to compare.</param>
        /// <returns><see langword="true"/> if the specified mouse button is currently pressed; otherwise, <see langword="false"/>.</returns>
        public bool IsButtonDown(Mouse.Button button) => e.Type is EventType.MouseButtonDown && e.Mouse.Button == button;

        /// <summary>
        /// Determines whether the specified mouse button was released and the event is a <see cref="EventType.MouseButtonUp"/>.
        /// </summary>
        /// <param name="button">The button to compare.</param>
        /// <returns><see langword="true"/> if the specified mouse button was released; otherwise, <see langword="false"/>.</returns>
        public bool IsButtonUp(Mouse.Button button) => e.Type is EventType.MouseButtonUp && e.Mouse.Button == button;

        /// <summary>
        /// Determines whether a quit request has been made, either by a specific key press or by a <see cref="EventType.Quit"/> or <see cref="EventType.WindowCloseRequested"/> event.
        /// </summary>
        /// <remarks>
        /// <para>By default, it checks if the <see cref="Keyboard.Scancode.Escape"/> key is pressed.</para>
        /// <para>
        /// The window close request is only considered if a <paramref name="windowId"/> is provided.
        /// It helps to know which window is requesting to close in multi-window applications.
        /// </para>
        /// </remarks>
        /// <param name="code">The code to quit.</param>
        /// <param name="windowId">The identifier of the window to monitor the close request.</param>
        /// <returns>true if a quit request is detected by the specified key or window close event; otherwise, false.</returns>
        [OverloadResolutionPriority(1)]
        public bool RequestQuit(Keyboard.Scancode code = Keyboard.Scancode.Escape, uint? windowId = null)
        {
            bool quit = e.Type is EventType.Quit || (e.Type is EventType.WindowCloseRequested && e.Window.Id == windowId);
            return quit || IsKeyDown(e, code);
        }

        /// <summary>
        /// Determines whether a quit request has been made, either by a specific key press or by a <see cref="EventType.Quit"/> or <see cref="EventType.WindowCloseRequested"/> event.
        /// </summary>
        /// <remarks>
        /// <para>By default, it checks if the <see cref="Keyboard.Keycode.Escape"/> key is pressed.</para>
        /// <para>
        /// The window close request is only considered if a <paramref name="windowId"/> is provided.
        /// It helps to know which window is requesting to close in multi-window applications.
        /// </para>
        /// </remarks>
        /// <param name="key">The key to quit.</param>
        /// <param name="windowId">The identifier of the window to monitor the close request.</param>
        /// <returns>true if a quit request is detected by the specified key or window close event; otherwise, false.</returns>
        public bool RequestQuit(Keyboard.Keycode key = Keyboard.Keycode.Escape, uint? windowId = null)
        {
            bool quit = e.Type is EventType.Quit || (e.Type is EventType.WindowCloseRequested && e.Window.Id == windowId);
            return quit || IsKeyDown(e, key);
        }
    }
}

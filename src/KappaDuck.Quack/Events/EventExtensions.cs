// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Input;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Provides extension for <see cref="Event"/>
/// </summary>
public static class EventExtensions
{
    extension(Event e)
    {
        /// <summary>
        /// Gets a value indicating whether the user requested to quit the application, for example by
        /// closing the last window or when the operating system asks the application to terminate.
        /// </summary>
        public bool QuitRequested => e is QuitRequestedEvent;

        /// <summary>
        /// Determines whether the given mouse button was double-clicked.
        /// </summary>
        /// <param name="button">The mouse button to check. Defaults to <see cref="MouseButton.Left"/>.</param>
        /// <returns><see langword="true"/> if the button was clicked twice in a row; otherwise <see langword="false"/>.</returns>
        public bool DoubleClick(MouseButton button = MouseButton.Left) => e is MouseButtonPressedEvent { Clicks: 2, Button: MouseButton pressed } && pressed == button;

        /// <summary>
        /// Determines whether the application should exit, either because a quit was requested
        /// or because the given code was pressed.
        /// </summary>
        /// <remarks>
        /// This is a shortcut for the common game-loop check <c>e.QuitRequested || e.KeyPressed(<see cref="Scancode.Escape"/>)</c>.
        /// </remarks>
        /// <param name="code">The code that also signals an exit. Defaults to <see cref="Scancode.Escape"/>.</param>
        /// <returns><see langword="true"/> if a quit was requested or the code was pressed; otherwise <see langword="false"/>.</returns>
        [OverloadResolutionPriority(1)]
        public bool ExitRequested(Scancode code = Scancode.Escape) => e.QuitRequested || e.KeyPressed(code);

        /// <summary>
        /// Determines whether the application should exit, either because a quit was requested
        /// or because the given key was pressed.
        /// </summary>
        /// <remarks>
        /// This is a shortcut for the common game-loop check <c>e.QuitRequested || e.KeyPressed(<see cref="Key.Escape"/>)</c>.
        /// </remarks>
        /// <param name="key">The key that also signals an exit. Defaults to <see cref="Key.Escape"/>.</param>
        /// <returns><see langword="true"/> if a quit was requested or the key was pressed; otherwise <see langword="false"/>.</returns>
        public bool ExitRequested(Key key = Key.Escape) => e.QuitRequested || e.KeyPressed(key);

        /// <summary>
        /// Determines whether the given key was pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><see langword="true"/> if the key was pressed; otherwise <see langword="false"/>.</returns>
        public bool KeyPressed(Key key) => e is KeyPressedEvent { Key: Key pressed } && pressed == key;

        /// <summary>
        /// Determines whether the given key was pressed with exactly the given modifiers held.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <param name="modifier">The modifiers that must be held. A combined value such as
        /// <see cref="Keymod.Control"/> matches either the left or right key.</param>
        /// <returns><see langword="true"/> if the key was pressed with exactly those modifiers; otherwise <see langword="false"/>.</returns>
        public bool KeyPressed(Key key, Keymod modifier) => e is KeyPressedEvent pressed && pressed.Key == key && HasModifiers(pressed.Modifier, modifier);

        /// <summary>
        /// Determines whether the given physical key was pressed.
        /// </summary>
        /// <remarks>
        /// Use a <see cref="Scancode"/> when the key's position matters more than the symbol it
        /// produces, such as WASD movement, so the control stays in place across keyboard layouts.
        /// </remarks>
        /// <param name="code">The physical key to check.</param>
        /// <returns><see langword="true"/> if the key was pressed; otherwise <see langword="false"/>.</returns>
        public bool KeyPressed(Scancode code) => e is KeyPressedEvent { Code: Scancode pressed } && pressed == code;

        /// <summary>
        /// Determines whether the given physical key was pressed with exactly the given modifiers held.
        /// </summary>
        /// <param name="code">The physical key to check.</param>
        /// <param name="modifier">The modifiers that must be held. A combined value such as
        /// <see cref="Keymod.Control"/> matches either the left or right key.</param>
        /// <returns><see langword="true"/> if the key was pressed with exactly those modifiers; otherwise <see langword="false"/>.</returns>
        public bool KeyPressed(Scancode code, Keymod modifier) => e is KeyPressedEvent pressed && pressed.Code == code && HasModifiers(pressed.Modifier, modifier);

        /// <summary>
        /// Determines whether the given key was released.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><see langword="true"/> if the key was released; otherwise <see langword="false"/>.</returns>
        public bool KeyReleased(Key key) => e is KeyReleasedEvent { Key: Key pressed } && pressed == key;

        /// <summary>
        /// Determines whether the given physical key was released.
        /// </summary>
        /// <param name="code">The physical key to check.</param>
        /// <returns><see langword="true"/> if the key was released; otherwise <see langword="false"/>.</returns>
        public bool KeyReleased(Scancode code) => e is KeyReleasedEvent { Code: Scancode pressed } && pressed == code;

        /// <summary>
        /// Determines whether the mouse moved while the given button was held, for drag-and-drop style input.
        /// </summary>
        /// <param name="button">The button that must be held during the move.</param>
        /// <param name="position">The cursor position, relative to the top-left of the window,
        /// when this method returns <see langword="true"/>.</param>
        /// <param name="delta">The distance moved since the previous position when this method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the mouse moved with the button held; otherwise <see langword="false"/>.</returns>
        public bool MouseDragged(MouseButton button, out PointF position, out Vector2 delta)
        {
            if (e is MouseMovedEvent { Buttons: MouseButtonState buttons } moved && buttons.IsDown(button))
            {
                position = moved.Position;
                delta = moved.Delta;

                return true;
            }

            position = default;
            delta = default;

            return false;
        }

        /// <summary>
        /// Determines whether the mouse moved and, if so, retrieves its position.
        /// </summary>
        /// <param name="position">The cursor position, relative to the top-left of the window,
        /// when this method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the mouse moved; otherwise <see langword="false"/>.</returns>
        public bool MouseMoved(out PointF position)
        {
            if (e is MouseMovedEvent { Position: PointF moved })
            {
                position = moved;
                return true;
            }

            position = default;
            return false;
        }

        /// <summary>
        /// Determines whether the mouse moved and, if so, retrieves its position and the buttons held during the move.
        /// </summary>
        /// <param name="position">The cursor position, relative to the top-left of the window,
        /// when this method returns <see langword="true"/>.</param>
        /// <param name="buttons">The buttons held during the move when this method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the mouse moved; otherwise <see langword="false"/>.</returns>
        public bool MouseMoved(out PointF position, out MouseButtonState buttons)
        {
            if (e.TryGetValue(out MouseMovedEvent moved))
            {
                position = moved.Position;
                buttons = moved.Buttons;

                return true;
            }

            position = default;
            buttons = default;

            return false;
        }

        /// <summary>
        /// Determines whether the given mouse button was pressed.
        /// </summary>
        /// <param name="button">The mouse button to check.</param>
        /// <returns><see langword="true"/> if the button was pressed; otherwise <see langword="false"/>.</returns>
        public bool MousePressed(MouseButton button) => e is MouseButtonPressedEvent { Button: MouseButton pressed } && pressed == button;

        /// <summary>
        /// Determines whether the given mouse button was released.
        /// </summary>
        /// <param name="button">The mouse button to check.</param>
        /// <returns><see langword="true"/> if the button was released; otherwise <see langword="false"/>.</returns>
        public bool MouseReleased(MouseButton button) => e is MouseButtonReleasedEvent { Button: MouseButton pressed } && pressed == button;

        /// <summary>
        /// Determines whether the mouse wheel scrolled and, if so, retrieves the scroll amount.
        /// </summary>
        /// <param name="delta">The horizontal and vertical scroll amount when this method returns <see langword="true"/>.</param>
        /// <returns><see langword="true"/> if the wheel scrolled; otherwise <see langword="false"/>.</returns>
        public bool MouseWheel(out Vector2 delta)
        {
            if (e is MouseWheelEvent { Delta: Vector2 wheel })
            {
                delta = wheel;
                return true;
            }

            delta = default;
            return false;
        }
    }

    private static bool HasModifiers(Keymod current, Keymod requested)
    {
        const Keymod locks = Keymod.NumLock | Keymod.CapsLock | Keymod.ScrollLock;
        current &= ~(locks & ~requested);

        if (!GroupMatches(current, requested, Keymod.Shift)
            || !GroupMatches(current, requested, Keymod.Control)
            || !GroupMatches(current, requested, Keymod.Alt)
            || !GroupMatches(current, requested, Keymod.Gui))
        {
            return false;
        }

        const Keymod groups = Keymod.Shift | Keymod.Control | Keymod.Alt | Keymod.Gui;
        return (current & ~groups) == (requested & ~groups);

        static bool GroupMatches(Keymod current, Keymod requested, Keymod group)
        {
            Keymod wanted = requested & group;
            Keymod held = current & group;

            if (wanted == Keymod.None)
                return held == Keymod.None;

            return wanted == group ? held != Keymod.None : held == wanted;
        }
    }
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Input;

/// <summary>
/// Provides extensions for <see cref="MouseButtonState"/>.
/// </summary>
public static class MouseButtonStateExtensions
{
    extension(MouseButtonState buttons)
    {
        /// <summary>
        /// Determines whether the given button is held in this state.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns><see langword="true"/> if the button is held; otherwise <see langword="false"/>.</returns>
        public bool IsDown(MouseButton button) => (buttons & ToState(button)) != MouseButtonState.None;

        /// <summary>
        /// Determines whether the given button is released in this state.
        /// </summary>
        /// <param name="button">The button to check.</param>
        /// <returns><see langword="true"/> if the button is released; otherwise <see langword="false"/>.</returns>
        public bool IsUp(MouseButton button) => (buttons & ToState(button)) == MouseButtonState.None;
    }

    private static MouseButtonState ToState(MouseButton button) => (MouseButtonState)(1 << ((byte)button - 1));
}

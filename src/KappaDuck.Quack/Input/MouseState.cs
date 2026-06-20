// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Input;

/// <summary>
/// A snapshot of the mouse buttons and cursor position.
/// </summary>
public readonly struct MouseState
{
    internal MouseState(MouseButtonState buttons, PointF position)
    {
        Buttons = buttons;
        Position = position;
    }

    /// <summary>
    /// Gets the buttons held in this snapshot.
    /// </summary>
    public MouseButtonState Buttons { get; }

    /// <summary>
    /// Gets the cursor position captured with this snapshot.
    /// </summary>
    public PointF Position { get; }

    /// <summary>
    /// Determines whether the given button is held in this snapshot.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns><see langword="true"/> if the button is held; otherwise, <see langword="false"/>.</returns>
    public bool IsDown(MouseButton button) => (Buttons & ToState(button)) != 0;

    /// <summary>
    /// Determines whether the given button is released in this snapshot.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns><see langword="true"/> if the button is released; otherwise, <see langword="false"/>.</returns>
    public bool IsUp(MouseButton button) => !IsDown(button);

    private static MouseButtonState ToState(MouseButton button) => (MouseButtonState)(1 << ((byte)button - 1));
}

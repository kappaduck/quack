// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Input.Mouse;

/// <summary>
/// Represents the state of the mouse.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly struct MouseState
{
    internal MouseState(MouseButtonState buttons, Vector2 position)
    {
        Buttons = buttons;
        Position = position;
    }

    /// <summary>
    /// Gets the state of the mouse buttons.
    /// </summary>
    public MouseButtonState Buttons { get; }

    /// <summary>
    /// Gets the position of the mouse.
    /// </summary>
    public Vector2 Position { get; }

    /// <summary>
    /// Determines whether the specified mouse button is currently pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns><see langword="true"/> if the specified mouse button is currently pressed; otherwise, <see langword="false"/>.</returns>
    public bool IsDown(MouseButton button) => (Buttons & (MouseButtonState)(1 << ((byte)button - 1))) != MouseButtonState.None;
}

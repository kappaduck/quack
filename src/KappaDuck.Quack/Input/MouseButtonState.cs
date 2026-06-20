// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Input;

/// <summary>
/// A bitmask of the mouse buttons currently held.
/// </summary>
[Flags]
public enum MouseButtonState : uint
{
    /// <summary>
    /// No button is held.
    /// </summary>
    None = 0,

    /// <summary>
    /// The left mouse button.
    /// </summary>
    Left = 1 << 0,

    /// <summary>
    /// The middle mouse button, usually the wheel.
    /// </summary>
    Middle = 1 << 1,

    /// <summary>
    /// The right mouse button.
    /// </summary>
    Right = 1 << 2,

    /// <summary>
    /// The first extra mouse button, usually the side button.
    /// </summary>
    X1 = 1 << 3,

    /// <summary>
    /// The second extra mouse button, usually the side button.
    /// </summary>
    X2 = 1 << 4
}

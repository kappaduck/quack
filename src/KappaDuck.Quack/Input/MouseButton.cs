// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Input;

/// <summary>
/// Represents a mouse button.
/// </summary>
public enum MouseButton : byte
{
    /// <summary>
    /// The left mouse button.
    /// </summary>
    Left = 1,

    /// <summary>
    /// The middle mouse button, usually the wheel.
    /// </summary>
    Middle = 2,

    /// <summary>
    /// The right mouse button.
    /// </summary>
    Right = 3,

    /// <summary>
    /// The first extra mouse button, usually the side button.
    /// </summary>
    X1 = 4,

    /// <summary>
    /// The second extra mouse button, usually the side button.
    /// </summary>
    X2 = 5
}

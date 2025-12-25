// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Inputs;

/// <summary>
/// Represents the state of the mouse buttons.
/// </summary>
/// <remarks>
/// It is a mask of the current button state.
/// </remarks>
[Flags]
public enum MouseButtonState : uint
{
    /// <summary>
    /// No mouse button.
    /// </summary>
    None = 0,

    /// <summary>
    /// The left mouse button.
    /// </summary>
    Left = 0x1,

    /// <summary>
    /// The middle mouse button.
    /// </summary>
    Middle = 0x2,

    /// <summary>
    /// The right mouse button.
    /// </summary>
    Right = 0x4,

    /// <summary>
    /// The first extra mouse button.
    /// </summary>
    /// <remarks>
    /// Generally is the side mouse button.
    /// </remarks>
    X1 = 0x08,

    /// <summary>
    /// The second extra mouse button.
    /// </summary>
    /// <remarks>
    /// Generally is the side mouse button.
    /// </remarks>
    X2 = 0x10
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Inputs;

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
    /// The middle mouse button.
    /// </summary>
    Middle = 2,

    /// <summary>
    /// The right mouse button.
    /// </summary>
    Right = 3,

    /// <summary>
    /// The first extra mouse button.
    /// </summary>
    /// <remarks>
    /// Generally is the side mouse button.
    /// </remarks>
    X1 = 4,

    /// <summary>
    /// The second extra mouse button.
    /// </summary>
    /// <remarks>
    /// Generally is the side mouse button.
    /// </remarks>
    X2 = 5
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Windowing;

/// <summary>
/// Describes how a window should request the user's attention.
/// </summary>
public enum FlashOperation
{
    /// <summary>
    /// Stop flashing the window.
    /// </summary>
    Cancel = 0,

    /// <summary>
    /// Flash the window briefly to get attention.
    /// </summary>
    Briefly = 1,

    /// <summary>
    /// Keep flashing the window until it gains focus.
    /// </summary>
    UntilFocused = 2
}

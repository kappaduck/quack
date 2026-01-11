// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.Window;

/// <summary>
/// The operation to perform when flashing the window.
/// </summary>
public enum FlashOperation
{
    /// <summary>
    /// Stop flashing the window.
    /// </summary>
    Stop = 0,

    /// <summary>
    /// Flash the window briefly, then stop flashing it.
    /// </summary>
    Briefly = 1,

    /// <summary>
    /// Flash the window until it is focused.
    /// </summary>
    UntilFocused = 2
}

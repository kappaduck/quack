// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Dialog;

/// <summary>
/// Specifies the default key that is considered as the primary action in a message box dialog.
/// </summary>
public enum MessageBoxDefaultKey : uint
{
    /// <summary>
    /// Specifies the key Return is the default to trigger the default button
    /// </summary>
    Return = 0x00000001u,

    /// <summary>
    /// Specifies the key Escape is the default to trigger the default button
    /// </summary>
    Escape = 0x00000002u
}

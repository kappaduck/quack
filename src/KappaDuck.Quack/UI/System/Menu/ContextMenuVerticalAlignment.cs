// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Specifies the vertical alignment options for a context menu.
/// </summary>
public enum ContextMenuVerticalAlignment : uint
{
    /// <summary>
    /// Position the context menu so that its top side is aligned with the coordinate specified by the y parameter.
    /// </summary>
    Top = 0,

    /// <summary>
    /// Centers the context menu vertically relative to the coordinate specified by the y parameter.
    /// </summary>
    Middle = 10,

    /// <summary>
    /// Position the context menu so that its bottom side is aligned with the coordinate specified by the y parameter.
    /// </summary>
    Bottom = 20
}

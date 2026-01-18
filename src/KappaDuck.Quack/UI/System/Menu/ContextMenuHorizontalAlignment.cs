// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Specifies the horizontal alignment options for a context menu.
/// </summary>
public enum ContextMenuHorizontalAlignment : uint
{
    /// <summary>
    /// Position the context menu so that its left side is aligned with the coordinate specified by the x parameter.
    /// </summary>
    Left = 0,

    /// <summary>
    /// Centers the context menu horizontally relative to the coordinate specified by the x parameter.
    /// </summary>
    Center = 4,

    /// <summary>
    /// Position the context menu so that its right side is aligned with the coordinate specified by the x parameter.
    /// </summary>
    Right = 8
}

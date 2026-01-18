// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Provides options for configuring the behavior of a context menu.
/// </summary>
public sealed class ContextMenuOptions
{
    /// <summary>
    /// Gets or sets the horizontal alignment of the context menu.
    /// </summary>
    public ContextMenuHorizontalAlignment HorizontalAlignment { get; set; } = ContextMenuHorizontalAlignment.Left;

    /// <summary>
    /// Gets or sets the vertical alignment of the context menu.
    /// </summary>
    public ContextMenuVerticalAlignment VerticalAlignment { get; set; } = ContextMenuVerticalAlignment.Top;

    /// <summary>
    /// Gets or sets a value indicating whether the user can select menu items using both left and right mouse buttons.
    /// </summary>
    public bool UseBothButtons { get; set; }

    internal uint Flags => (uint)HorizontalAlignment | (uint)VerticalAlignment | (UseBothButtons ? 0x0002u : 0x0000u);

    /// <summary>
    /// Gets the default context menu options.
    /// </summary>
    public static ContextMenuOptions Default { get; } = new ContextMenuOptions();
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents the layout options for a menu entry.
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public enum MenuEntryLayout : uint
{
    /// <summary>
    /// Specifies the default layout behavior.
    /// </summary>
    Default = 0x00000000,

    /// <summary>
    /// Functions the same as the <see cref="Break"/> for a menu bar.
    /// For a drop-down menu, sub-menu, or shortcut menu, the new column is separated from the old column by a vertical line.
    /// </summary>
    BarBreak = 0x00000020,

    /// <summary>
    /// Places the item on a new line (for a menu bar) or in a new column (for a drop-down menu, sub-menu, or shortcut menu) without separating columns.
    /// </summary>
    Break = 0x00000040
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a menu entry that supports user interaction within a menu system.
/// </summary>
/// <remarks>
/// This is a marker interface used to identify interactive menu entries within the menu system.
/// Do not implement this marker directly; instead, use derived interfaces such as <see cref="MenuCommand"/> or <see cref="SubMenu"/>.
/// </remarks>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public interface IInteractableMenuEntry : IMenuEntry
{
    /// <summary>
    /// Gets a value indicating whether the item is checked.
    /// </summary>
    bool Checked { get; }

    /// <summary>
    /// Gets the layout of the menu entry.
    /// </summary>
    MenuEntryLayout Layout { get; }

    /// <summary>
    /// Gets the state of the menu entry.
    /// </summary>
    MenuEntryState State { get; }

    /// <summary>
    /// Gets the label of the menu entry.
    /// </summary>
    string Label { get; }
}

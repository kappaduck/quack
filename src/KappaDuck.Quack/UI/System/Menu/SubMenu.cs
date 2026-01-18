// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Collections;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a menu entry that displays a nested submenu.
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public sealed record SubMenu : IInteractableMenuEntry, IEnumerable<IMenuEntry>
{
    private readonly List<IMenuEntry> _entries = [];

    /// <summary>
    /// Creates a submenu.
    /// </summary>
    /// <param name="label">The text label of the submenu.</param>
    /// <param name="entries">The entries contained within the submenu.</param>
    public SubMenu(string label, params ReadOnlySpan<IMenuEntry> entries)
    {
        Label = label;
        _entries.AddRange(entries);
    }

    /// <inheritdoc/>
    bool IInteractableMenuEntry.Checked => false;

    /// <inheritdoc/>
    public MenuEntryLayout Layout { get; init; } = MenuEntryLayout.Default;

    /// <inheritdoc/>
    public MenuEntryState State { get; init; } = MenuEntryState.Enabled;

    /// <inheritdoc/>
    public string Label { get; }

    /// <inheritdoc/>
    public IEnumerator<IMenuEntry> GetEnumerator() => _entries.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a menu item that can execute a command when selected.
/// </summary>
/// <param name="Id">The unique identifier of the menu command.</param>
/// <param name="Label">The label of the menu command.</param>
/// <param name="Command">The command to execute when the menu item is selected.</param>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public sealed record MenuCommand(uint Id, string Label, Action Command) : IInteractableMenuEntry
{
    /// <inheritdoc/>
    public bool Checked { get; init; }

    /// <summary>
    /// Gets the command to execute when the menu item is selected.
    /// </summary>
    public Action Command { get; } = Command;

    /// <summary>
    /// Gets the unique identifier of the menu command.
    /// </summary>
    public uint Id { get; } = Id;

    /// <inheritdoc/>
    public MenuEntryLayout Layout { get; init; } = MenuEntryLayout.Default;

    /// <inheritdoc/>
    public MenuEntryState State { get; init; } = MenuEntryState.Enabled;

    /// <inheritdoc/>
    public string Label { get; } = Label;
}

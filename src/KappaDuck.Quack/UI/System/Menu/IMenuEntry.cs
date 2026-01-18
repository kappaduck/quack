// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a menu entry that can be displayed within a menu system.
/// </summary>
/// <remarks>
/// This is a marker interface used to identify menu entries within the menu system.
/// Do not implement this marker directly; instead, use derived interfaces such as <see cref="MenuCommand"/>, <see cref="SubMenu"/> or <see cref="MenuSeparator"/>.
/// </remarks>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public interface IMenuEntry;

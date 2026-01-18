// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.UI.System.Menu;

/// <summary>
/// Represents a visual separator within a menu, used to group related menu items and improve menu organization.
/// </summary>
[SupportedOSPlatform(nameof(OSPlatform.Windows))]
public sealed record MenuSeparator : IMenuEntry;

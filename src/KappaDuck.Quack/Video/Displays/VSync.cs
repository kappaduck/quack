// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Video.Displays;

/// <summary>
/// Constants values for vertical synchronization (VSync) settings.
/// </summary>
[PublicAPI]
public static class VSync
{
    /// <summary>
    /// Disable the vertical synchronization.
    /// </summary>
    public const int Disabled = 0;

    /// <summary>
    /// Enable the adaptive vertical synchronization.
    /// </summary>
    public const int Adaptive = -1;
}

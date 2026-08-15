// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// A vertical synchronization mode used when presenting frames.
/// </summary>
/// <remarks>
/// Use <see cref="Disabled"/> to present immediately, <see cref="Enabled"/> to synchronize with every vertical
/// refresh, <see cref="Adaptive"/> for adaptive synchronization where supported, or <see cref="Every(int)"/> to
/// synchronize with every Nth refresh. Not every mode is supported on every platform; read the value back after
/// setting it to see what was actually applied. The default value is <see cref="Disabled"/>.
/// </remarks>
public readonly record struct VSync
{
    internal VSync(int refresh) => Refresh = refresh;

    /// <summary>
    /// Gets the adaptive synchronization mode, which synchronizes with the vertical refresh but presents late frames
    /// immediately, where supported.
    /// </summary>
    public static VSync Adaptive { get; } = new(-1);

    /// <summary>
    /// Gets the mode that disables synchronization, presenting frames immediately.
    /// </summary>
    public static VSync Disabled { get; } = new(0);

    /// <summary>
    /// Gets the mode that synchronizes with every vertical refresh.
    /// </summary>
    public static VSync Enabled { get; } = new(1);

    internal int Refresh { get; }

    /// <summary>
    /// Creates a mode that synchronizes with every Nth vertical refresh.
    /// </summary>
    /// <param name="refreshes">The number of vertical refreshes between presentations. Must be at least 1.</param>
    /// <returns>The synchronization mode.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="refreshes"/> is less than 1.</exception>
    public static VSync Every(int refreshes)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(refreshes, 1);
        return new VSync(refreshes);
    }

    /// <inheritdoc/>
    public override string ToString() => Refresh switch
    {
        -1 => "Adaptive",
        0 => "Disabled",
        1 => "Enabled",
        _ => $"Every {Refresh} refreshes"
    };
}

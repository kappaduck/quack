// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Numerics;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// Controls how the pixels from a drawing operation (the source) are combined with the pixels already in the
/// render target (the destination).
/// </summary>
/// <remarks>
/// The predefined modes are supported on every backend. Additional modes can be built with
/// <see cref="Compose"/>, though a backend may not support every combination.
/// </remarks>
public readonly struct BlendMode : IEquatable<BlendMode>, IEqualityOperators<BlendMode, BlendMode, bool>
{
    internal BlendMode(uint value) => Value = value;

    internal uint Value { get; }

    /// <summary>
    /// Gets the mode that performs no blending; the source replaces the destination.
    /// </summary>
    public static BlendMode None { get; } = new(0x00000000);

    /// <summary>
    /// Gets the standard alpha blending mode, weighted by the source alpha.
    /// </summary>
    public static BlendMode Blend { get; } = new(0x00000001);

    /// <summary>
    /// Gets the alpha blending mode for sources whose color is already multiplied by their alpha.
    /// </summary>
    public static BlendMode BlendPremultiplied { get; } = new(0x00000010);

    /// <summary>
    /// Gets the additive blending mode; the source is weighted by its alpha and added to the destination.
    /// </summary>
    public static BlendMode Add { get; } = new(0x00000002);

    /// <summary>
    /// Gets the additive blending mode for sources whose color is already multiplied by their alpha.
    /// </summary>
    public static BlendMode AddPremultiplied { get; } = new(0x00000020);

    /// <summary>
    /// Gets the color modulate mode; the source and destination colors are multiplied together.
    /// </summary>
    public static BlendMode Mod { get; } = new(0x00000004);

    /// <summary>
    /// Gets the color multiply mode; the source and destination colors are multiplied and weighted by the source alpha.
    /// </summary>
    public static BlendMode Mul { get; } = new(0x00000008);

    /// <summary>
    /// Builds a custom blend mode from individual factors and operations for the color and alpha channels.
    /// </summary>
    /// <remarks>
    /// The result is <c>destinationColor = colorOperation(sourceColor * sourceColorFactor, destinationColor * destinationColorFactor)</c>
    /// and likewise for alpha. Applying the returned mode to a target may still fail if the backend does not support the combination.
    /// </remarks>
    /// <param name="sourceColorFactor">The factor applied to the source red, green and blue channels.</param>
    /// <param name="destinationColorFactor">The factor applied to the destination red, green and blue channels.</param>
    /// <param name="colorOperation">The operation combining the weighted color channels.</param>
    /// <param name="sourceAlphaFactor">The factor applied to the source alpha channel.</param>
    /// <param name="destinationAlphaFactor">The factor applied to the destination alpha channel.</param>
    /// <param name="alphaOperation">The operation combining the weighted alpha channels.</param>
    /// <returns>The composed blend mode.</returns>
    public static BlendMode Compose(BlendFactor sourceColorFactor, BlendFactor destinationColorFactor, BlendOperation colorOperation, BlendFactor sourceAlphaFactor, BlendFactor destinationAlphaFactor, BlendOperation alphaOperation)
    {
        uint value = SDL3.ComposeCustomBlendMode(sourceColorFactor, destinationColorFactor, colorOperation, sourceAlphaFactor, destinationAlphaFactor, alphaOperation);
        return new BlendMode(value);
    }

    /// <summary>
    /// Determines whether this blend mode is equal to another blend mode.
    /// </summary>
    /// <param name="other">The blend mode to compare with the current blend mode.</param>
    /// <returns><see langword="true"/> if the blend modes are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(BlendMode other) => Value == other.Value;

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is BlendMode other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => Value switch
    {
        0x00000000u => nameof(None),
        0x00000001u => nameof(Blend),
        0x00000010u => nameof(BlendPremultiplied),
        0x00000002u => nameof(Add),
        0x00000020u => nameof(AddPremultiplied),
        0x00000004u => nameof(Mod),
        0x00000008u => nameof(Mul),
        _ => $"Custom(0x{Value:X8})"
    };

    /// <summary>
    /// Determines whether two blend modes are equal.
    /// </summary>
    /// <param name="left">The left blend mode.</param>
    /// <param name="right">The right blend mode.</param>
    /// <returns><see langword="true"/> if the blend modes are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(BlendMode left, BlendMode right) => left.Equals(right);

    /// <summary>
    /// Determines whether two blend modes are not equal.
    /// </summary>
    /// <param name="left">The left blend mode.</param>
    /// <param name="right">The right blend mode.</param>
    /// <returns><see langword="true"/> if the blend modes are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(BlendMode left, BlendMode right) => !(left == right);
}

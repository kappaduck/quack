// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Windows;

/// <summary>
/// Represents the permitted range of aspect ratios for a resizable window, expressed as width divided by height.
/// </summary>
/// <remarks>A value of <c>0</c> on either bound leaves that side unconstrained. The default value places no constraint on either side.</remarks>
public readonly record struct AspectRatio
{
    /// <summary>
    /// Create the <see cref="AspectRatio"/> with a minimum and maximum bound.
    /// </summary>
    /// <param name="minimum">The narrowest permitted ratio, or <c>0</c> to leave it unconstrained.</param>
    /// <param name="maximum">The widest permitted ratio, or <c>0</c> to leave it unconstrained.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="minimum"/> or <paramref name="maximum"/> is negative or not a finite number.</exception>
    /// <exception cref="ArgumentException">Both bounds are constrained and <paramref name="minimum"/> is greater than <paramref name="maximum"/>.</exception>
    public AspectRatio(float minimum, float maximum)
    {
        ThrowIfInvalid(minimum);
        ThrowIfInvalid(maximum);

        if (minimum > 0f && maximum > 0f)
            ArgumentOutOfRangeException.ThrowIfGreaterThan(minimum, maximum);

        Minimum = minimum;
        Maximum = maximum;

        static void ThrowIfInvalid(float value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (!float.IsFinite(value) || value < 0f)
                throw new ArgumentOutOfRangeException(paramName, value, "The aspect ratio must be a finite, non-negative number.");
        }
    }

    /// <summary>
    /// Create the <see cref="AspectRatio"/> locked to a single, fixed ratio.
    /// </summary>
    /// <param name="ratio">The exact width-to-height ratio.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="ratio"/> is negative.</exception>
    public AspectRatio(float ratio) : this(ratio, ratio)
    {
    }

    /// <summary>
    /// Gets the narrowest permitted ratio, or <c>0</c> if unconstrained.
    /// </summary>
    public float Minimum { get; }

    /// <summary>
    /// Gets the widest permitted ratio, or <c>0</c> if unconstrained.
    /// </summary>
    public float Maximum { get; }

    /// <summary>
    /// Deconstructs the aspect ratio into its minimum and maximum bounds.
    /// </summary>
    /// <param name="minimum">Receives <see cref="Minimum"/>.</param>
    /// <param name="maximum">Receives <see cref="Maximum"/>.</param>
    public void Deconstruct(out float minimum, out float maximum)
    {
        minimum = Minimum;
        maximum = Maximum;
    }
}

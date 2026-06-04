// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Extension methods for <see cref="Vector2"/> providing named rounding conversions to <see cref="Vector2i"/>.
/// </summary>
public static class Vector2Extensions
{
    extension(Vector2 vector)
    {
        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector2i"/> by flooring each component.
        /// </summary>
        /// <remarks>
        /// Useful for tile snapping, where you want the tile that contains the position.
        /// </remarks>
        /// <returns>The converted vector with each component floored.</returns>
        public Vector2i Floor() => new((int)MathF.Floor(vector.X), (int)MathF.Floor(vector.Y));

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector2i"/> by rounding each component to the nearest integer.
        /// </summary>
        /// <returns>The converted vector with each component rounded.</returns>
        public Vector2i Round() => new((int)MathF.Round(vector.X), (int)MathF.Round(vector.Y));

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector2i"/> by truncating each component toward zero.
        /// </summary>
        /// <returns>The converted vector with each component truncated.</returns>
        public Vector2i Truncate() => new((int)vector.X, (int)vector.Y);
    }
}

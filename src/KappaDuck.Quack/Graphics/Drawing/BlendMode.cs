// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL;

namespace KappaDuck.Quack.Graphics.Drawing;

/// <summary>
/// A set of blend modes used in drawing operations.
/// </summary>
public enum BlendMode : uint
{
    /// <summary>
    /// No blending: destination RGBA = source RGBA.
    /// </summary>
    None = 0x00000000u,

    /// <summary>
    /// Alpha blending: destination RGB = (source RGB * source Alpha) + (destination RGB * (1 - source Alpha)),
    /// destination Alpha = source Alpha + (destination Alpha * (1 - source Alpha)).
    /// </summary>
    Blend = 0x00000001u,

    /// <summary>
    /// Additive blending: destination RGB = (source RGB * source Alpha) + destination RGB, destination Alpha = destination Alpha.
    /// </summary>
    Add = 0x00000002u,

    /// <summary>
    /// Color modulate: destination RGB = source RGB * destination RGB, destination Alpha = destination Alpha.
    /// </summary>
    ColorModulate = 0x00000004u,

    /// <summary>
    /// Color multiply: destination RGB = (source RGB * destination RGB) + (destination RGB * (1 - source Alpha)), destination Alpha = destination Alpha.
    /// </summary>
    ColorMultiply = 0x00000008u,

    /// <summary>
    /// Pre-multiplied alpha blending: destination RGBA = source RGBA + (destination RGBA * (1 - source Alpha)).
    /// </summary>
    BlendPreMultiplied = 0x00000010u,

    /// <summary>
    /// Pre-multiplied additive blending: destination RGB = source RGB + destination RGB, destination Alpha = destination Alpha.
    /// </summary>
    AddPreMultiplied = 0x00000020u,

    /// <summary>
    /// Invalid blend mode.
    /// </summary>
    Invalid = 0x7FFFFFFFu
}

/// <summary>
/// Provides extension methods for the <see cref="BlendMode"/>.
/// </summary>
public static class BlendModeExtensions
{
    extension(BlendMode)
    {
        /// <summary>
        /// Composes a custom blend mode.
        /// </summary>
        /// <param name="source">The factor applied to the red, green, and blue components of the source pixel.</param>
        /// <param name="destination">The factor applied to the red, green, and blue components of the destination pixel.</param>
        /// <param name="operation">The operation used to combine the source and destination pixel components.</param>
        /// <param name="sourceAlpha">The factor applied to the alpha component of the source pixel.</param>
        /// <param name="destinationAlpha">The factor applied to the alpha component of the destination pixel.</param>
        /// <param name="alphaOperation">The operation used to combine the source and destination alpha components.</param>
        /// <returns>The composed blend mode.</returns>
        public static BlendMode Compose(BlendFactor source, BlendFactor destination, BlendOperation operation, BlendFactor sourceAlpha, BlendFactor destinationAlpha, BlendOperation alphaOperation)
            => SDL.Surface.SDL_ComposeCustomBlendMode(source, destination, operation, sourceAlpha, destinationAlpha, alphaOperation);
    }
}

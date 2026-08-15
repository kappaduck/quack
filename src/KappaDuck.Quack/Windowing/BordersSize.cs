// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Windowing;

/// <summary>
/// Represents the thickness, in screen coordinates, of a window's decorations on each side.
/// </summary>
/// <param name="Top">The height of the title bar plus the top border.</param>
/// <param name="Left">The width of the left border.</param>
/// <param name="Bottom">The height of the bottom border.</param>
/// <param name="Right">The width of the right border.</param>
public readonly record struct BordersSize(int Top, int Left, int Bottom, int Right);

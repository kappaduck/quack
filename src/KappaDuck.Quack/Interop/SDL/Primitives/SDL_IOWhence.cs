// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal enum SDL_IOWhence
{
    /// <summary>
    /// Seek from the beginning of data
    /// </summary>
    Set = 0,

    /// <summary>
    /// Seek relative to current read point
    /// </summary>
    Current = 1,

    /// <summary>
    /// Seek relative to the end of data
    /// </summary>
    End = 2
}

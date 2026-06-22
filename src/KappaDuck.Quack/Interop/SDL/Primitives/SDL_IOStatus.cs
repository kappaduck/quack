// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal enum SDL_IOStatus
{
    /// <summary>
    /// No errors and not <see cref="EndOfFile"/>
    /// </summary>
    Ready = 0,

    Error = 1,

    EndOfFile = 2,

    /// <summary>
    /// Non blocking I/O, not ready.
    /// </summary>
    NotReady = 3,

    ReadOnly = 4,

    WriteOnly = 5
}

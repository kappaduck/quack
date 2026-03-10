// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal enum ProgressState
{
    /// <summary>
    /// An invalid progress state indicating an error.
    /// </summary>
    Invalid = -1,

    /// <summary>
    /// Show no progress bar.
    /// </summary>
    None = 0,

    /// <summary>
    /// Show a progress bar in indeterminate state.
    /// </summary>
    Indeterminate = 1,

    /// <summary>
    /// Show a normal progress bar.
    /// </summary>
    Normal = 2,

    /// <summary>
    /// Show a paused progress bar.
    /// </summary>
    Paused = 3,

    /// <summary>
    /// Show an error progress bar to indicate the application has encountered an error.
    /// </summary>
    Error = 4
}

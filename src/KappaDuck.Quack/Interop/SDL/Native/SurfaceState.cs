// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Native;

[Flags]
internal enum SurfaceState : uint
{
    None = 0x00000000u,

    /// <summary>
    /// Surface uses preallocated pixel memory.
    /// </summary>
    PreAllocated = 0x00000001u,

    /// <summary>
    /// Surface needs to be locked to access pixels.
    /// </summary>
    LockNeeded = 0x00000002u,

    /// <summary>
    /// Surface is currently locked.
    /// </summary>
    Locked = 0x00000004u,

    /// <summary>
    /// Surface uses pixels that are allocated with SIMD alignment.
    /// </summary>
    SimdAligned = 0x00000008u,
}

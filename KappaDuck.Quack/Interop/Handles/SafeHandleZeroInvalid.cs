// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// Provides a specialized SafeHandle that is considered invalid when the handle is <see cref="nint.Zero"/>.
/// </summary>
/// <param name="handle">The low-level handle to be wrapped.</param>
/// <param name="ownsHandle"><see langword="true"/> means the handle is owned by this instance and will be released when the instance is disposed.</param>
internal abstract class SafeHandleZeroInvalid(nint handle, bool ownsHandle) : SafeHandle(handle, ownsHandle)
{
    protected SafeHandleZeroInvalid(bool ownsHandle) : this(nint.Zero, ownsHandle)
    {
    }

    public override bool IsInvalid => handle == nint.Zero;
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// Provides a specialized SafeHandle that is considered invalid when the handle is <see cref="nint.Zero"/>.
/// </summary>
/// <param name="ownsHandle"><see langword="true"/> means the handle is owned by this instance and will be released when the instance is disposed.</param>
public abstract class SafeHandleZeroInvalid(bool ownsHandle) : SafeHandle(nint.Zero, ownsHandle)
{

    /// <summary>
    /// Creates a SafeHandleZeroInvalid instance with the specified handle.
    /// </summary>
    /// <param name="handle">The handle to wrap.</param>
    /// <param name="ownsHandle"><see langword="true"/> means the handle is owned by this instance and will be released when the instance is disposed.</param>
    protected SafeHandleZeroInvalid(nint handle, bool ownsHandle) : this(ownsHandle)
        => SetHandle(handle);

    /// <inheritdoc/>
    public override bool IsInvalid => handle == nint.Zero;
}

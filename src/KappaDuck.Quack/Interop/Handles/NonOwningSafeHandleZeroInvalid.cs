// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// Provides a specialized SafeHandle that is considered invalid when the handle is <see cref="nint.Zero"/> and does not own the handle.
/// </summary>
/// <param name="handle">The handle to wrap.</param>
public abstract class NonOwningSafeHandleZeroInvalid(nint handle) : SafeHandleZeroInvalid(handle, ownsHandle: false)
{
}

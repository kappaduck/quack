// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// Provides a specialized SafeHandle that is considered invalid when the handle is <see cref="nint.Zero"/> and does not own the handle.
/// </summary>
/// <remarks>
/// This class is not intended to be used directly. Instead, uses the specialized safe handle such as <see cref="WindowHandle"/>.
/// </remarks>
/// <param name="handle">The handle to wrap.</param>
public abstract class NonOwningSafeHandleZeroInvalid(nint handle) : SafeHandle(handle, ownsHandle: false)
{
    /// <inheritdoc/>
    public override bool IsInvalid => handle == nint.Zero;
}

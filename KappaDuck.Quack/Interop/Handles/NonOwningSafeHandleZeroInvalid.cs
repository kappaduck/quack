// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.ComponentModel;

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// Provides a specialized SafeHandle that is considered invalid when the handle is <see cref="nint.Zero"/> and does not own the handle.
/// </summary>
/// <remarks>
/// This class is not intended to be used directly. Instead, uses the specialized safe handle such as <see cref="WindowHandle"/>.
/// </remarks>
/// <param name="handle">The low-level handle to be wrapped.</param>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class NonOwningSafeHandleZeroInvalid(nint handle) : SafeHandle(handle, ownsHandle: false)
{
    /// <summary>
    /// Gets a value indicating whether the handle is invalid.
    /// </summary>
    public override bool IsInvalid => handle == nint.Zero;

    /// <inheritdoc/>
    /// <remarks>This implementation does nothing since the handle is non-owning.</remarks>
    protected override bool ReleaseHandle() => true;
}

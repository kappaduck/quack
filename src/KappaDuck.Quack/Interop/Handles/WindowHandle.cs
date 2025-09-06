// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// Represents a handle to a window.
/// </summary>
/// <remarks>
/// <para>
/// This safe handle does not own the underlying handle and will not release it when disposed.
/// </para>
/// <para>
/// Depending on the platform, the type of the handle may vary (e.g., HWND on Windows, X11 Window on Linux).
/// </para>
/// </remarks>
public sealed class WindowHandle : NonOwningSafeHandleZeroInvalid
{
    internal WindowHandle(nint handle) : base(handle)
    {
    }

    /// <inheritdoc/>
    protected override bool ReleaseHandle() => true;
}

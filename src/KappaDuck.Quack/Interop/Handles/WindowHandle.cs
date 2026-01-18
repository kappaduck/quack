// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// A specialized SafeHandle for window handle depending on the platform.
/// </summary>
/// <remarks>
/// This safe handle does not own the handle and will not release it when disposed.
/// Depending on the platform, this handle represents different types of window handles:
/// <list type="bullet">
/// <item><description>On Windows, it represents an <c>HWND</c>.</description></item>
/// <item><description>On Linux with X11, it represents a <c>Window</c> (X11 window ID).</description></item>
/// </list>
/// </remarks>
public sealed class WindowHandle : NonOwningSafeHandleZeroInvalid
{
    internal WindowHandle(nint handle) : base(handle)
    {
    }

    internal static WindowHandle Zero { get; } = new WindowHandle(nint.Zero);

    /// <inheritdoc/>
    protected override bool ReleaseHandle() => true;
}

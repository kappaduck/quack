// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.Handles;

/// <summary>
/// A specialized SafeHandle for window handle depending on the platform.
/// </summary>
/// <remarks>
/// <para>This safe handle does not own the underlying handle, so it will not release it when disposed.</para>
/// <para>
/// The handle represent a platform-specific window handle:
/// <list type="bullet">
/// <item><c>HWND</c> on Windows.</item>
/// <item><c>Window</c> on X11 Linux.</item>
/// </list>
/// </para>
/// </remarks>
public sealed class WindowHandle : NonOwningSafeHandleZeroInvalid
{
    internal WindowHandle(nint handle) : base(handle)
    {
    }

    internal static WindowHandle Zero { get; } = new WindowHandle(nint.Zero);
}

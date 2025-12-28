// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

internal static class OperatingSystemExtensions
{
    extension(OperatingSystem)
    {
        /// <summary>
        /// Determines whether the operating system is running Wayland as the display server protocol.
        /// </summary>
        /// <returns><see langword="true"/> if the operating system is Linux with Wayland; otherwise, <see langword="false"/>.</returns>
        internal static bool IsWayland() => Environment.GetEnvironmentVariable("XDG_SESSION_TYPE") == "wayland";

        /// <summary>
        /// Determines whether the operating system is running X11 as the display server protocol.
        /// </summary>
        /// <returns><see langword="true"/> if the operating system is Linux with X11; otherwise, <see langword="false"/>.</returns>
        internal static bool IsX11() => Environment.GetEnvironmentVariable("XDG_SESSION_TYPE") == "x11";
    }
}

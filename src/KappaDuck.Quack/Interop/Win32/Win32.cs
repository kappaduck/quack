// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.Win32.Primitives;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal static class Win32
{
    extension(nuint value)
    {
        /// <summary>
        /// Gets the low-order word (LOWORD) of the value.
        /// </summary>
        internal ushort LowerBits => (ushort)(value & 0xFFFF);

        /// <summary>
        /// Gets the high-order word (HIWORD) of the value.
        /// </summary>
        internal ushort UpperBits => (ushort)((value >> 16) & 0xFFFF);
    }

    internal delegate bool MessageCallback(MSG message);
}

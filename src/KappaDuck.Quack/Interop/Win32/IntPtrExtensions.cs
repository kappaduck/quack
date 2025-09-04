// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32;

[SupportedOSPlatform("windows")]
internal static class IntPtrExtensions
{
    extension(nuint pointer)
    {
        internal ushort Lower16Bits => (ushort)(pointer & 0xFFFF);

        internal ushort Upper16Bits => (ushort)((pointer >> 16) & 0xFFFF);
    }
}

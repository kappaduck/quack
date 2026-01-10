// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.Win32.Primitives;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;

namespace KappaDuck.Quack.Interop.Win32;

[SupportedOSPlatform(nameof(OSPlatform.Windows))]
internal static class Win32
{
    extension(nuint pointer)
    {
        internal ushort Lower16Bits => (ushort)(pointer & 0xFFFF);

        internal ushort Upper16Bits => (ushort)((pointer >> 16) & 0xFFFF);
    }

    extension(Win32Exception)
    {
        internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, string message, [CallerMemberName] string memberName = "")
        {
            if (condition)
                throw new QuackException($"{memberName} failed: {message}");
        }

        internal static void ThrowIfFailed(bool condition, [CallerMemberName] string memberName = "")
            => ThrowIf(!condition, memberName);
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal delegate bool MessageCallback(nint data, MSG message);
}

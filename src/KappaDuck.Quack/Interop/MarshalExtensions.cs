// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop;

internal static class MarshalExtensions
{
    extension(Marshal)
    {
        internal static string? PtrToStringUTF8(byte* bytes) => Marshal.PtrToStringUTF8((nint)bytes);
    }
}

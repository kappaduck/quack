// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop;

internal static class MarshalExtensions
{
    extension(Marshal)
    {
        internal static unsafe string PtrToStringUTF8(byte* bytes)
            => Marshal.PtrToStringUTF8((nint)bytes) ?? string.Empty;
    }
}

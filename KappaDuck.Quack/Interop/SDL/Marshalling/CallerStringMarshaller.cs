// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

/// <summary>
/// Custom marshaller for a caller-owned UTF-8 encoded string.
/// </summary>
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(CallerStringMarshaller))]
internal static unsafe class CallerStringMarshaller
{
    internal static string ConvertToManaged(byte* unmanaged) => Marshal.PtrToStringUTF8(unmanaged);

    internal static void Free(byte* ptr) => SDL3.Memory.Free(ptr);
}

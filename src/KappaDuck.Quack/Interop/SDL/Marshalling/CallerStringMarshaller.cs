// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

/// <summary>
/// Custom marshaller for a caller-owned UTF-8 encoded string.
/// </summary>
/// <remarks>
/// It will free the allocated UTF-8 encoded string.
/// </remarks>
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(CallerStringMarshaller))]
internal static class CallerStringMarshaller
{
    internal static string? ConvertToManaged(byte* unmanaged) => Marshal.PtrToStringUTF8(unmanaged);

    internal static void Free(byte* unmanaged) => SDL3.Free(unmanaged);
}

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

/// <summary>
/// Custom marshaller for a UTF-8 encoded string owned by SDL.
/// </summary>
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(SDLStringMarshaller))]
internal static class SDLStringMarshaller
{
    internal static string? ConvertToManaged(byte* unmanaged) => Marshal.PtrToStringUTF8(unmanaged);
}

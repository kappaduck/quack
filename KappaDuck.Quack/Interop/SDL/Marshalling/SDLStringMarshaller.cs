// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

/// <summary>
/// Custom marshaller for a UTF-8 encoded string owned by SDL.
/// </summary>
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(SDLStringMarshaller))]
internal static unsafe class SDLStringMarshaller
{
    public static string ConvertToManaged(byte* unmanaged) => Marshal.PtrToStringUTF8(unmanaged);
}

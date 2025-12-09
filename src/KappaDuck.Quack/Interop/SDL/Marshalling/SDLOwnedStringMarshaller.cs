// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

/// <summary>
/// Custom marshaller for string that is owned by SDL and is freed automatically by SDL.
/// </summary>
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(SDLOwnedStringMarshaller))]
internal static class SDLOwnedStringMarshaller
{
    internal static string ConvertToManaged(nint unmanaged)
        => Marshal.PtrToStringUTF8(unmanaged) ?? string.Empty;
}

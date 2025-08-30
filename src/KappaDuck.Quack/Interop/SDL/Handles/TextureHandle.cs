// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Handles;

/// <summary>
/// Marshaller needs a public parameterless constructor.
/// </summary>
internal sealed partial class TextureHandle() : SafeHandleZeroInvalid(ownsHandle: true)
{
    protected override bool ReleaseHandle()
    {
        SDL_DestroyTexture(handle);

        SetHandle(nint.Zero);
        SetHandleAsInvalid();

        return true;
    }

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyTexture(nint texture);
}

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Handles;

internal sealed partial class SDL_RendererHandle() : SafeHandleZeroInvalid(ownsHandle: true)
{
    protected override bool ReleaseHandle()
    {
        SDL_DestroyRenderer(handle);

        SetHandle(nint.Zero);
        SetHandleAsInvalid();

        return true;
    }

    [LibraryImport(SDL.Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyRenderer(nint renderer);
}

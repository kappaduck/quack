// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Handles;

internal sealed partial class SDL_WindowHandle : SafeHandleZeroInvalid
{
    public SDL_WindowHandle() : base(nint.Zero, ownsHandle: true)
    {
    }

    private SDL_WindowHandle(nint handle, bool ownsHandle) : base(handle, ownsHandle)
    {
    }

    internal SDL_WindowHandle ToNonOwningHandle() => new(handle, ownsHandle: false);

    protected override bool ReleaseHandle()
    {
        SDL_DestroyWindow(handle);

        SetHandle(nint.Zero);
        SetHandleAsInvalid();

        return true;
    }

    [LibraryImport(SDL.Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(nint window);
}

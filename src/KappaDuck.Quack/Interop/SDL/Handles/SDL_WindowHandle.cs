// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace KappaDuck.Quack.Interop.SDL.Handles;

internal sealed class SDL_WindowHandle : SafeHandleZeroInvalid
{
    public SDL_WindowHandle() : base(ownsHandle: true)
    {
    }

    private SDL_WindowHandle(nint handle, bool ownsHandle) : base(handle, ownsHandle)
    {
    }

    internal SDL_WindowHandle ToNonOwningHandle() => new(handle, ownsHandle: false);

    protected override void Free() => Native.SDL_DestroyWindow(handle);
}

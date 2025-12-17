// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace KappaDuck.Quack.Interop.SDL.Handles;

internal sealed class SDLWindowHandle : SafeHandleZeroInvalid
{
    public SDLWindowHandle() : base(ownsHandle: true)
    {
    }

    private SDLWindowHandle(nint handle, bool ownsHandle) : base(handle, ownsHandle)
    {
    }

    internal SDLWindowHandle ToNonOwningHandle() => new(handle, ownsHandle: false);

    protected override bool ReleaseHandle()
    {
        Native.SDL_DestroyWindow(handle);

        SetHandle(nint.Zero);
        SetHandleAsInvalid();

        return true;
    }
}

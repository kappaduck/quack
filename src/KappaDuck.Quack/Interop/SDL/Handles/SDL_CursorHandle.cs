// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace KappaDuck.Quack.Interop.SDL.Handles;

internal sealed class SDL_CursorHandle() : SafeHandleZeroInvalid(ownsHandle: true)
{
    protected override bool ReleaseHandle()
    {
        Native.SDL_FreeCursor(handle);

        SetHandle(nint.Zero);
        SetHandleAsInvalid();

        return true;
    }
}

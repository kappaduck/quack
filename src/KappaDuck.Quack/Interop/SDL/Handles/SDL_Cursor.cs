// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace KappaDuck.Quack.Interop.SDL.Handles;

internal sealed class SDL_Cursor() : SafeHandleZeroInvalid(ownsHandle: true)
{
    protected override void Free() => Native.SDL_DestroyCursor(handle);
}

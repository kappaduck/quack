// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace KappaDuck.Quack.Interop.SDL.Handles;

internal sealed class SDL_Window : SafeHandleZeroInvalid
{
    public SDL_Window() : base(ownsHandle: true)
    {
    }

    private SDL_Window(nint handle, bool ownsHandle) : base(handle, ownsHandle)
    {
    }

    internal static SDL_Window Null { get; } = new SDL_Window();

    internal SDL_Window ToNonOwningHandle() => new(handle, ownsHandle: false);

    protected override void Free() => Native.SDL_DestroyWindow(handle);
}

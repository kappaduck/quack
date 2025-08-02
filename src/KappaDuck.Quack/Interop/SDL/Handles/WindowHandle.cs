// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Handles;

internal sealed partial class WindowHandle : SafeHandleZeroInvalid
{
    /// <summary>
    /// Marshaller needs a public parameterless constructor.
    /// </summary>
    public WindowHandle() : base(ownsHandle: true)
    {
    }

    internal WindowHandle(WindowHandle window) : base(ownsHandle: false)
         => SetHandle(window.handle);

    protected override bool ReleaseHandle()
    {
        SDL_DestroyWindow(handle);

        SetHandle(nint.Zero);
        SetHandleAsInvalid();

        return true;
    }

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(nint window);
}
